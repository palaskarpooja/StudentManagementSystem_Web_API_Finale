using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public StudentManagementSystemContex db;
        public StudentController(StudentManagementSystemContex db1)
        {
            db = db1;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        
        private IActionResult View()
        {
            throw new NotImplementedException();
        }
    }
}
