using ECommerce.Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.DTOs
{
    public class BrandsDTOs : BaseClass
    {
        public IFormFile Image_BrandUrl { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Admin_Id { get; set; }
        public User? User { get; set; }
        public string Public_id { get; set; } = string.Empty;

    }
}
