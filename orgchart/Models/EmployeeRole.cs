using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace orgchart.Models
{
    [Table("EmployeeRole")]

    public class EmployeeRole
    {
        public int ID { get; set; }
        public int EmployeeID { get; set; }
        public int RoleID { get; set; }
    }
}