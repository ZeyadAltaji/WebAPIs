using ECommerce.Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.DTOs
{
    public class CarDTOs : BaseClass
    {
        public string Name { get; set; } = string.Empty;
        public string Class { get; set; } = string.Empty;
        public int? Production_Date { get; set; }
        public int Admin_Id { get; set; }
        public User? User { get; set; }
        public string Public_id { get; set; } = string.Empty;
        public IFormFile Image_CarUrl { get; set; }

    }
}
