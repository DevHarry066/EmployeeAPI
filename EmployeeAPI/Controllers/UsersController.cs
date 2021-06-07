using AuthenticationPlugin;
using EmployeeAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPI.Controllers
{
    public class UsersController : Controller
    {
        private EmployeeDBContext _dbContext;
        public UsersController(EmployeeDBContext context)
        {
            _dbContext = context;
        }

        [HttpPost("[action]")]
        public IActionResult Register([FromBody]User user)
        {
            var e = _dbContext.Users.Where(u => u.Email == user.Email).SingleOrDefault();
            if (e != null) return BadRequest("Email already Registred");

            var user1 = new User
            {
                Name = user.Name,
                Email = user.Email,
                Password = SecurePasswordHasherHelper.Hash(user.Password), //Change stored procedure of password from string to Hashing
                Role = user.Role
            };

            _dbContext.Users.Add(user1);
            _dbContext.SaveChanges();

            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
