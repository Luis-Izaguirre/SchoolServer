using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using SchoolServer.Models;
using SchoolServer.DTO;

namespace SchoolServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController(SchooldbContext context) : ControllerBase
    {

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            return await context.Students.ToListAsync();
        }

        [Authorize]
        [HttpGet("GetPopulation")]
        public async Task<ActionResult<IEnumerable<CoursePopulation>>> GetPopulation()
        {

            IQueryable<CoursePopulation> x = context.Courses.
                    Select(c => new CoursePopulation
                    {
                        Name = c.CourseName,
                        CourseId = c.CourseId,
                        Population = c.Students.Sum(t => t.Population)
                    });

            return await x.ToListAsync();
        }



        [HttpGet("GetPopulation2")]
        public async Task<ActionResult<IEnumerable<CoursePopulation>>> GetPopulation2()
        {

            IQueryable<CoursePopulation> x = context.Courses.
                    Select(c => new CoursePopulation
                    {
                        Name = c.CourseName,
                        CourseId = c.CourseId,
                        Population = c.Students.Sum(t => t.Population)
                    });

            return await x.ToListAsync();
        }


    }
}
