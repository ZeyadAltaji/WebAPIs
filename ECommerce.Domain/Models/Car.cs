using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Models
{
    public class Car: BaseEntity
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Class { get; set; } = string.Empty;
        [Required]
        public int? Production_Date { get; set; }
        public int Admin_Id { get; set; }
        public User? User { get; set; }
        public string Image_CarUrl { get; set; } = string.Empty;
        public string Public_id { get; set; } = string.Empty;
    }
}
