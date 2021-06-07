using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPI.Models
{
    public class EmployeeDBContext:DbContext
    {
        public EmployeeDBContext(DbContextOptions options)
             : base(options)
        {
        }
        public DbSet<Employee> Employeess { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
