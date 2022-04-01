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
    public class Student1Controller : ControllerBase
    {
        private readonly StudentManagementSystemContext _context;

        public Student1Controller(StudentManagementSystemContext context)
        {
            _context = context;
        }

        // GET: api/Student1
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentRegistration>>> GetStudentRegistrations()
        {
            return await _context.StudentRegistrations.ToListAsync();
        }

        // GET: api/Student1/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentRegistration>> GetStudentRegistration(byte id)
        {
            var studentRegistration = await _context.StudentRegistrations.FindAsync(id);

            if (studentRegistration == null)
            {
                return NotFound();
            }

            return studentRegistration;
        }

        // PUT: api/Student1/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudentRegistration(byte id, StudentRegistration studentRegistration)
        {
            if (id != studentRegistration.Id)
            {
                return BadRequest();
            }

            _context.Entry(studentRegistration).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentRegistrationExists(id))
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

        // POST: api/Student1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StudentRegistration>> PostStudentRegistration(StudentRegistration studentRegistration)
        {
            _context.StudentRegistrations.Add(studentRegistration);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (StudentRegistrationExists(studentRegistration.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetStudentRegistration", new { id = studentRegistration.Id }, studentRegistration);
        }

        // DELETE: api/Student1/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudentRegistration(byte id)
        {
            var studentRegistration = await _context.StudentRegistrations.FindAsync(id);
            if (studentRegistration == null)
            {
                return NotFound();
            }

            _context.StudentRegistrations.Remove(studentRegistration);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentRegistrationExists(byte id)
        {
            return _context.StudentRegistrations.Any(e => e.Id == id);
        }
    }
}
