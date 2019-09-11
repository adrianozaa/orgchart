using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace orgchart.Models
{
    public class EmployeeContext: DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<EmployeeRole> EmployeeRoles { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<EmployeeDepartment> EmployeeDepartments { get; set; }
    }
}