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
    public class LoginController : ControllerBase
    {

        public StudentManagementSystemContext db;
        public LoginController(StudentManagementSystemContext db1)
        {
            db = db1;
        }
        public object ViewBag { get; private set; }

        

        

        

        private IActionResult View()
        {
            throw new NotImplementedException();
        }
    }
}
