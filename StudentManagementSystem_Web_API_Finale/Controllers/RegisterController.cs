using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StudentManagementSystem_Web_API_Finale.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem_Web_API_Finale.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {

        public StudentManagementSystemContext db;
        private IConfiguration _config;

        public RegisterController(IConfiguration config,StudentManagementSystemContext db1)
        {
            _config = config;
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


        [AllowAnonymous]
        [HttpPost("validate")]
        public IActionResult Login([FromBody] StudentLogin studentlogin)
        {
            var user = Authenticate(studentlogin);

            if (user != null)
            {
                var token = Generate(user);
                return Ok(token);
            }

            return NotFound("User not found");
        }

        private object Authenticate(StudentRegistration studentRegistration)
        {
            throw new NotImplementedException();
        }

        private object Generate(object user)
        {
            throw new NotImplementedException();
        }

        private string Generate(StudentRegistration studentRegistration)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, studentRegistration.Id.ToString()),
                new Claim(ClaimTypes.Name, studentRegistration.FirstName),
                new Claim(ClaimTypes.Name, studentRegistration.LastName),
                new Claim(ClaimTypes.Name, studentRegistration.Username)

            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"],
              claims,
              expires: DateTime.Now.AddMinutes(15),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private StudentRegistration Authenticate(StudentLogin studentlogin)
        {
            var currentUser = db.StudentRegistrations.FirstOrDefault(o => o.Username.ToLower() == studentlogin.Username.ToLower() && o.Password == studentlogin.Password);

            if (currentUser != null)
            {
                return currentUser;
            }

            return null;
        }
    }
}
