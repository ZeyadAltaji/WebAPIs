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
    public class PhotoLogo :BaseEntity
    {
        public string IsLogoUrl{ get; set; } = string.Empty;
        [NotMapped]
        public IFormFile Logoimage { get; set; }

    }
}
