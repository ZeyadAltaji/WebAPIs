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
    public class Product:BaseEntity
    {
        //[Required]
        //public string Serial_Id { get; set; }

        [Required]
        public string Title { get; set; }=string.Empty;
        //[Required]
         public int? BrandsId { get; set; }
        public int? CarId { get; set; }
        public int? CategoryId { get; set; }
        public double? offers { get; set; }
        [Required]
        public Brands? Brands { get; set; }
        public Car? Car { get; set; }
        public Category? Category { get; set; }
        public int Admin_Id { get; set; }
        public User? User { get; set; }
        public string IsPrimaryImage { get; set; } = string.Empty;
        public string IsForeignImage1 { get; set; } = string.Empty;
        public string IsForeignImage2 { get; set; } = string.Empty;
        [NotMapped]
        public IFormFile Primary_Image { get; set; }
        [NotMapped]
        public IFormFile ForeignImage1 { get; set; }
        [NotMapped]
        public IFormFile ForeignImage2 { get; set; }

    }
}
