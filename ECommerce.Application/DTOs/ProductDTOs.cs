using ECommerce.Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.DTOs
{
    public class ProductDTOs:BaseClass
    {
         

        public string Title { get; set; } = string.Empty;
 
        public double? offers { get; set; }
    
        public int? Brands_Id { get; set; }
        public int? Car_Id { get; set; }
        public int? Category_Id { get; set; }
         public int Admin_Id { get; set; }
        public User? User { get; set; }


        public string IsPrimaryImage { get; set; } = string.Empty;
        public string IsForeignImage1 { get; set; } = string.Empty;
        public string IsForeignImage2 { get; set; } = string.Empty;
        public IFormFile Primary_Image { get; set; }
        public IFormFile ForeignImage1 { get; set; }
        public IFormFile ForeignImage2 { get; set; }
    }
}
