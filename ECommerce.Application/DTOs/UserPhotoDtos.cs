using ECommerce.Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.DTOs
{
    public class UserPhotoDtos:BaseClass
    {
         public int user_Id { get; set; }
        public string Image_userUrl { get; set; }

        public string Public_id { get; set; }
        public IFormFile file { get; set; }

    }
}
