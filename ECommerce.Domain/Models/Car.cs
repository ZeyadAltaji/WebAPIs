using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Models
{
    internal class Car
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Class { get; set; } = string.Empty;
        public DateOnly Production_Date { get; set; }
        public int Admin_Id { get; set; }
        public User? User { get; set; }
    }
}
