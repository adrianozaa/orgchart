using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace orgchart.Models
{
    [Table("EmployeeDepartment")]

    public class EmployeeDepartment
    {
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public int DepartmentID { get; set; }
    }
}