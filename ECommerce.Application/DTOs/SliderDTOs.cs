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
    public class SliderDTOs : BaseClass
    {
         public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Button { get; set; } = string.Empty;
        public int Admin_Id { get; set; }
        public User? User { get; set; }
        public string ImageURl { get; set; } = string.Empty;
 

         public IFormFile Image { get; set; }
 
    }
}
