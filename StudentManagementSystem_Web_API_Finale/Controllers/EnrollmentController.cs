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
    public class EnrollmentController : ControllerBase
    {
        public StudentManagementSystemContext db;
        public EnrollmentController(StudentManagementSystemContext db1)
        {
            db = db1;
        }

        [HttpGet]

        public IActionResult GetEnrollment()
        {
            return Ok(db.Enrollments);
        }

        [HttpPost]
        public IActionResult AddEnrollment(Enrollment enrollment)

        {
            db.Enrollments.Add(enrollment);
            db.SaveChanges();
            return Ok();

        }

    }
}
