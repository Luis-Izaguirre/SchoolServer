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

        // GET: api/Population returns the same thing not just 
        [HttpGet("GetPopulation")]
        public async Task<ActionResult<IEnumerable<CoursePopulation>>> GetPopulation()
        {
            //Old way to query old synax
            //Await is like a query, because it
            //var x = await (from c in context.Countries
            //               select new CountryPopulation
            //        {
            //            Name = c.Name,
            //            CountryId = c.CountryId,
            //            //Population = c.Cities.Sum(t => t.Population)
            //        }).ToListAsync();
            //return x;
            // New way old syntax
            //    var x = from c in context.Countries
            //            select new CountryPopulation
            //            {
            //                Name = c.Name,
            //                CountryId = c.CountryId,
            //                //Population = c.Cities.Sum(t => t.Population)
            //            };
            //    return await x.ToListAsync();
            //}

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
