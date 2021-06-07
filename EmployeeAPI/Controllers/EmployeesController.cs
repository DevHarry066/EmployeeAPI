using EmployeeAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private EmployeeDBContext _dbContext;

        public EmployeesController(EmployeeDBContext context)
        {
            _dbContext = context;
        }
        
        // GET: api/<EmployeesController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_dbContext.Employeess);
            //return StatusCode(StatusCodes.Status200OK);
        }

        // GET api/<EmployeesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var c = _dbContext.Employeess.Find(id);
            if (c == null) return NotFound("Employee Not Found");
            return Ok(c);
        }

        // POST api/<EmployeesController>
        [HttpPost]
        public IActionResult Post([FromBody]Employee employee)
        {
            _dbContext.Employeess.Add(employee);
            _dbContext.SaveChanges();
            return Ok("Employee Details Added");
            //return StatusCode(StatusCodes.Status201Created);
        }

        // PUT api/<EmployeesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Employee employee)
        {
           var c= _dbContext.Employeess.Find(id);
            if (c == null) return NotFound("Id not found");

            c.Name = employee.Name;
            c.LastName = employee.LastName;
            c.DateOfJoining = employee.DateOfJoining;
            c.Salary = employee.Salary;
            c.DepartmentName = employee.DepartmentName;
            c.PhoneNumber = employee.PhoneNumber;
            _dbContext.SaveChanges();
            return Ok("Employee Details updated successfully");
        }

        // DELETE api/<EmployeesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var c = _dbContext.Employeess.Find(id);
            if (c == null) return NotFound("Id not found in database");
            _dbContext.Employeess.Remove(c);
            _dbContext.SaveChanges();
            return Ok("Employee Record deleted");
        }
    }
}
