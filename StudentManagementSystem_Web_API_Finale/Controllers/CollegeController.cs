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
    public class CollegeController : ControllerBase
    {
        private readonly StudentManagementSystemContext _context;

        public CollegeController(StudentManagementSystemContext context)
        {
            _context = context;
        }

        // GET: api/College
        [HttpGet]
        public async Task<ActionResult<IEnumerable<College>>> GetColleges()
        {
            return await _context.Colleges.ToListAsync();
        }

        // GET: api/College/5
        [HttpGet("{id}")]
        public async Task<ActionResult<College>> GetCollege(byte id)
        {
            var college = await _context.Colleges.FindAsync(id);

            if (college == null)
            {
                return NotFound();
            }

            return college;
        }

        // PUT: api/College/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCollege(byte id, College college)
        {
            if (id != college.Id)
            {
                return BadRequest();
            }

            _context.Entry(college).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CollegeExists(id))
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

        // POST: api/College
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<College>> PostCollege(College college)
        {
            _context.Colleges.Add(college);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CollegeExists(college.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCollege", new { id = college.Id }, college);
        }

        // DELETE: api/College/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCollege(byte id)
        {
            var college = await _context.Colleges.FindAsync(id);
            if (college == null)
            {
                return NotFound();
            }

            _context.Colleges.Remove(college);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CollegeExists(byte id)
        {
            return _context.Colleges.Any(e => e.Id == id);
        }
    }
}
