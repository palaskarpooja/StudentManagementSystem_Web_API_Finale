using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem_Web_API_Finale.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementSystem_Web_API_Finale.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {

        public StudentManagementSystemContext db;
        public RegisterController(StudentManagementSystemContext db1)
        {
            db = db1;
        }

        [HttpGet]

        public IActionResult GetAll()
        {
            return Ok(db.StudentRegistrations);
        }

        [HttpPost]
        public IActionResult AddStudent(StudentRegistration student)
        {
            db.StudentRegistrations.Add(student);
            db.SaveChanges();
            return Ok();

        }
    }
}
