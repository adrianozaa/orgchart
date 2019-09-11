using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace orgchart.Models
{
    [Table("Role")]
    public class Role
    {
        public int ID { get; set; }
        public string Description { get; set; }
    }
}