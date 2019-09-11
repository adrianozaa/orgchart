using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace orgchart.Models
{
    [Table("Employee")]
    public class Employee
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int ReportsToID { get; set; }
        public List<int> Roles { get; set; }
        public int DepartmentID { get; set; }

        public string FullName
        {
            get
            {
                return string.Format("{0}, {1} {2}", LastName, FirstName, MiddleName);
            }
        }
    }
}