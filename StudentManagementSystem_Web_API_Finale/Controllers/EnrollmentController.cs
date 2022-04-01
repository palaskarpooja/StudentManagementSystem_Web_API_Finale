using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using StudentManagementSystem_Web_API_Finale.Models;

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

        [HttpGet("todaysenrollment")]

        public IActionResult GetEnrollmentByTodaysDate(int id)
        {


            var result = from c in db.Courses
                         join e in db.Enrollments on
                            c.Id equals e.CourseId
                         join s in db.StudentRegistrations
                        on e.StudentId equals s.Id
                         where e.CreatedDate.Date == DateTime.Now.Date
                         select new
                         {
                             
                             s.FirstName,
                             s.LastName,
                             c.Name,
                             s.ContactNumber

                         };



            return Ok(result);
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
