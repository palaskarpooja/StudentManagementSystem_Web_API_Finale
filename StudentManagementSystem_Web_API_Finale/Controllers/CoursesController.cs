using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem_Web_API_Finale.Models;

namespace StudentManagementSystem_Web_API_Finale.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly StudentManagementSystemContext _context;


        public CoursesController(StudentManagementSystemContext context)
        {
            _context = context;
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
        {
            return await _context.Courses.ToListAsync();
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(byte id)
        {
            var course = await _context.Courses.FindAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            return course;
        }

        [HttpGet("mycourse/{name}")]

        public ActionResult<Course> GetCourses(string name)
        {
            var result = from c in _context.Courses
                         join e in _context.Enrollments on
                            c.Id equals e.CourseId
                         join s in _context.StudentRegistrations
                        on e.StudentId equals s.Id
                         where s.Username == name
                               select c;          

            if (result == null)
            {
                return NotFound();
            }


            return Ok(result);
        }


        //------------------------------------------------------


       [HttpGet("studentcourseenrollment/{id}")]//Admin- Enroll Courses
        public ActionResult<Course> StudentCourseEnrollment(int id)
        {
           
            var result = (from c in _context.Courses
                         join e in _context.Enrollments on
                            c.Id equals e.CourseId
                         join s in _context.StudentRegistrations
                        on e.StudentId equals s.Id
                         where c.Id == id
                         select new 
                         {
                             s.Id,
                             s.FirstName,
                             s.LastName,
                             c.Name,
                             c.Duration,
                             s.ContactNumber,
                             s.CollegeId
                         }).Distinct();

            if (result == null)
            {
                return NotFound();
              }

            return Ok(result);
}


       

        // PUT: api/Courses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(byte id, Course course)
        {
            if (id != course.Id)
            {
                return BadRequest();
            }

            _context.Entry(course).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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

            return NoContent();
        }

        // POST: api/Courses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Course>> PostCourse(Course course)
        {
            _context.Courses.Add(course);
            try
            {
                await _context.SaveChangesAsync();
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

            return CreatedAtAction("GetCourse", new { id = course.Id }, course);
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(byte id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CourseExists(byte id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }
    }

}
