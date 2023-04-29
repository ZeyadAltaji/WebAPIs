using ECommerce.Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.DTOs
{
    public class PhotoDTOs :BaseClass
    {

        public string IsLogoUrl { get; set; } = string.Empty;
         public IFormFile Logoimage { get; set; }
    }
}
