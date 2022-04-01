using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem_Web_API_Finale.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementSystem_Web_API_Finale.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : Controller
    {
        public StudentManagementSystemContext db;


        public CourseController(StudentManagementSystemContext db1)
        {
            db = db1;
        }

        [HttpGet]

        public IActionResult GetAll()
        {
            return Ok(db.Courses);
        }

        [HttpPost]
        public IActionResult AddCourse(Course course)
        {
            db.Courses.Add(course);
            db.SaveChanges();
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(byte id)
        {
            var course = await db.Courses.FindAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            return course;
        }

        [HttpPut("PutCourse/{id}")]
        public async Task<IActionResult> PutCourses(byte id, Course course)
        {
            try
            {
                if (id != course.Id)
                {
                    return BadRequest();
                }
                else
                {
                    db.Entry(course).State = EntityState.Modified;
                    var res = await db.SaveChangesAsync();
                    return Ok(res);
                }



            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }



            }

        }

        /*[HttpPut("putcourse/{id}")]
        public async Task<IActionResult> EditCourse(int id, Course course)
        {

            if (id != course.Id)
            {
                return BadRequest();
            }

            db.Entry(course).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CourseExists(course.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCars", new { id = course.Id }, course);

        }*/

        private bool CourseExists(object courseId)
        {
            throw new NotImplementedException();
        }

        [HttpGet("courselist")]
        public IActionResult GetCourseList()
        {
            return Ok(db.Courses);
        }
    }
}

