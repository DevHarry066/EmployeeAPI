﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPI.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Name cant be null or empty")]
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfJoining { get; set; }
        [Required]
        public double Salary { get; set; }
        public string DepartmentName { get; set; }
        public double PhoneNumber { get; set; }
    }
}
