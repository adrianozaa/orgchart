using orgchart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace orgchart.ViewModels.Employee
{
    public class EmployeesVM
    {
        public IEnumerable<Role> Roles { get; set; }
        public IEnumerable<orgchart.Models.Employee> Employees { get; set; }
        public IEnumerable<EmployeeRole> EmployeeRoles { get; set; }
        public IEnumerable<orgchart.Models.Employee> Managers { get; set; }
        public IEnumerable<orgchart.Models.Department> Departments { get; set; }
        public int SelectedDepartment { get; set; }
    }
}