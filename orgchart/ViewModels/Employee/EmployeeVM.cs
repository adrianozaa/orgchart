using orgchart.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace orgchart.ViewModels.Employee
{ 
    public class EmployeeVM
    {
        public List<Role> Roles { get; set; }
        public orgchart.Models.Employee Employee { get; set; }
        public List<int> EmployeeRoles { get; set; }

        public List<orgchart.Models.Employee> Employees { get; set; }
        public List<orgchart.Models.Department> Departments { get; set; }
    }
}