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
    public class StudentController : ControllerBase
    {
        public StudentManagementSystemContext db;
        public StudentController(StudentManagementSystemContext db1)
        {
            db = db1;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentRegistration>>> GetStudentDetail()
        {
            return await db.StudentRegistrations.ToListAsync();
        }



        [HttpGet("{name}")]
        public ActionResult<StudentRegistration> GetStudentRegistration(string name)
        {
            /*var studentRegistration = await db.StudentRegistrations.FindAsync(name);*/

            var student = from s in db.StudentRegistrations where s.Username == name select s;

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        // PUT: api/StudentRegistration/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("putstudent/{id}")]
        public async Task<IActionResult> PutStudentRegistration(byte id, StudentRegistration studentRegistration)
        {
            try
            {
                if (id != studentRegistration.Id)
                {
                    return BadRequest();
                }
                else
                {
                    db.Entry(studentRegistration).State = EntityState.Modified;
                    var res = await db.SaveChangesAsync();
                    return Ok(res);
                }

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

            
        }

        private bool StudentRegistrationExists(byte id)
        {
            throw new NotImplementedException();
        }

        private IActionResult View()
        {
            throw new NotImplementedException();
        }
    }
}
