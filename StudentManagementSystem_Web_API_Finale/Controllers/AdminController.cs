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
    public class AdminController : ControllerBase
    {
        public StudentManagementSystemContext db;

        private IConfiguration _config;

        public AdminController(IConfiguration config, StudentManagementSystemContext db1)
        {
            _config = config;
            db = db1;
        }



        [AllowAnonymous]
        [HttpPost("validate")]
        public IActionResult Login([FromBody] Admin admin)
        {
            var User = Authenticate(admin);



            if (User != null)
            {
                var token = Generate(User);
                return Ok(token);
            }
            return NotFound("User not found");
        }

        private object Generate(object user)
        {
            throw new NotImplementedException();
        }

        

        private string Generate(Admin admin)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);



            var claims = new[]
            {
                new Claim(ClaimTypes.GivenName, admin.Password),

                };



            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(15),
            signingCredentials: credentials);



            return new JwtSecurityTokenHandler().WriteToken(token);
        }



        private Admin Authenticate(Admin admin)
        {
            var currentUser = db.Admins.FirstOrDefault(o => o.Username == admin.Username && o.Password == admin.Password);

            if (currentUser != null)
            {
                return currentUser;
            }
            return null;
        }



    }
}

        

        
    