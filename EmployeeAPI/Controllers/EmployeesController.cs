using EmployeeAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private EmployeeDBContext _dbContext;

        public EmployeesController(EmployeeDBContext context)
        {
            _dbContext = context;
        }
        
        [HttpGet]
        public IActionResult GetEmployees(string sort, int pageNumber, int pageSize)
        {
            if (pageNumber == 0) pageNumber =pageNumber + 1;
            if (pageSize == 0) pageSize = 2;
            var employees = from employee in _dbContext.Employeess
                        select new
                        {
                            Id= employee.Id,
                            Name=employee.Name,
                            LastName=employee.LastName,
                            DateOfJoining=employee.DateOfJoining,
                            Salary=employee.Salary,
                            DepartmentName=employee.DepartmentName,
                            PhoneNumber=employee.PhoneNumber

                        };
            switch (sort)
            {
                case "desc":
                    return Ok(employees.Skip((pageNumber-1)*pageSize).Take(pageSize).OrderByDescending(m => m.Id)); //Paging and Sorting Concept
                case "asc":
                    return Ok(employees.Skip((pageNumber - 1) * pageSize).Take(pageSize).OrderBy(m => m.Id));
                default:
                    return Ok(employees.Skip((pageNumber - 1) * pageSize).Take(pageSize));

            }
            //return StatusCode(StatusCodes.Status200OK);
        }

        // GET api/<EmployeesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var c = _dbContext.Employeess.Find(id);  //Get Employee with Id
            if (c == null) return NotFound("Employee Not Found");
            return Ok(c);
        }

        [Authorize(Roles ="Admin")]
        // POST api/<EmployeesController>
        [HttpPost]
        public IActionResult Post([FromBody]Employee employee)
        {
            _dbContext.Employeess.Add(employee); //Add new Employee details
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
            c.PhoneNumber = employee.PhoneNumber;  //Update Employee details
            _dbContext.SaveChanges();
            return Ok("Employee Details updated successfully");
        }

        [Authorize(Roles = "Admin")]
        // DELETE api/<EmployeesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var c = _dbContext.Employeess.Find(id);
            if (c == null) return NotFound("Id not found in database");
            _dbContext.Employeess.Remove(c); //Delete Employee
            _dbContext.SaveChanges();
            return Ok("Employee Record deleted");
        }

        [Authorize(Roles = "User")]
        //Find api/<EmployeesController>/5
        [HttpGet("[action]")]
        public IActionResult FindEmployee(string employeeName)
        {
            var employees = from employee in _dbContext.Employeess
                            where employee.Name.StartsWith(employeeName) //Searching Concept
                            select new
                            {
                                Id = employee.Id,
                                Name = employee.Name,
                                LastName = employee.LastName,
                                DateOfJoining = employee.DateOfJoining,
                                Salary = employee.Salary,
                                //DepartmentName = employee.DepartmentName,
                                PhoneNumber = employee.PhoneNumber

                            };

            return Ok(employees);
        }
    }
}
