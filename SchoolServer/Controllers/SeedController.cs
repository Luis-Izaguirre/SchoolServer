using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolServer.Data;
using SchoolServer.Models;


namespace SchoolServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedController(SchooldbContext db, IHostEnvironment environment,
        UserManager<CourseUser> userManager) : ControllerBase
    {
        private readonly string _pathName = Path.Combine(environment.ContentRootPath, "Data/students_courses_final.csv");


        [HttpPost("User")]
        public async Task<ActionResult> SeedUsers()
        {
            (string name, string email) = ("user1", "comp584@csun.edu");
            CourseUser user = new()
            {
                UserName = name,
                Email = email,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            if (await userManager.FindByNameAsync(name) is not null)
            {
                user.UserName = "user2";
            }
            _ = await userManager.CreateAsync(user, "P@ssw0rd!")
                    ?? throw new InvalidOperationException();
            user.EmailConfirmed = true;
            user.LockoutEnabled = false;
            await db.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("Student")]
        public async Task<IActionResult> SeedStudent()
        {
            Dictionary<string, Course> courses = await db.Courses//.AsNoTracking()
             .ToDictionaryAsync(c => c.CourseName);

            CsvConfiguration config = new(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                HeaderValidated = null
            };
            int studentCount = 0;
            using (StreamReader reader = new(_pathName))
            using (CsvReader csv = new(reader, config))
            {
                IEnumerable<MyCSV>? records = csv.GetRecords<MyCSV>();
                foreach (MyCSV record in records)
                {
                    if (!courses.TryGetValue(record.courseName, out Course? value))
                    {
                        Console.WriteLine($"Not found country for {record.studentName}");
                        return NotFound(record);
                    }
                    //Deleted => string.IsNullOrEmpty(record.studentEmail)
                    if (!record.population.HasValue )
                    {
                        Console.WriteLine($"Skipping {record.studentName}");
                        continue;
                    }
                    Student student = new()
                    {
                        StudentName = record.studentName,
                        StudentEmail = record.studentEmail,
                        Major = record.major,
                        StudentYear = record.studentYear,
                        Population = (int)record.population.Value,
                        CourseId = value.CourseId
                    };
                    db.Students.Add(student);
                    studentCount++;
                }
                await db.SaveChangesAsync();
            }
            return new JsonResult(studentCount);
        }

        [HttpPost("Course")]
        public async Task<IActionResult> SeedCourse()
        {
            Dictionary<string, Course> coursesByName = db.Courses
             .AsNoTracking().ToDictionary(x => x.CourseName, StringComparer.OrdinalIgnoreCase);

            CsvConfiguration config = new(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                HeaderValidated = null
            };
            using StreamReader reader = new(_pathName);
            using CsvReader csv = new(reader, config);

            List<MyCSV> records = csv.GetRecords<MyCSV>().ToList();
            foreach (MyCSV record in records)
            {
                if (coursesByName.ContainsKey(record.courseName))
                {
                    continue;
                }

                Course course = new()
                {
                    CourseName = record.courseName,
                    CourseDescription = record.description,
                    InstructorName = record.instructorName
                };
                await db.Courses.AddAsync(course);
                coursesByName.Add(record.courseName, course);
            }

            await db.SaveChangesAsync();

            return new JsonResult(coursesByName.Count);
        }
    }
}
