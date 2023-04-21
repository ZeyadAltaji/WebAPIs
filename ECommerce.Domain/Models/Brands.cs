using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Models
{
    public class Brands: BaseEntity
    {
        [Required]
        public string Name { get; set; }= string.Empty;
        public int Admin_Id { get; set; }
        public User? User { get; set; }
        [NotMapped]
        public IFormFile Image_BrandUrl { get; set; }
        public string Public_id { get; set; } = string.Empty;
    }
}
