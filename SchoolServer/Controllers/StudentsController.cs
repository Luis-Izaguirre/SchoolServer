using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolServer.Models;

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

        // GET: api/Students/5 -> May not be needed,
        //[HttpGet("GetPopulation")]
        //public async Task<ActionResult<Student>> GetStudent(int id)
        //{
        //    var student = await context.Students.FindAsync(id);

        //    if (student == null)
        //    {
        //        return NotFound();
        //    }

        //    return student;
        //}

        
    }
}
