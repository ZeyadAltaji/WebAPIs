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
    public class Slider: BaseEntity
    {

        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; }=string.Empty;
        public string Button { get; set; } = string.Empty;
         public int Admin_Id { get; set; }
        public User? User { get; set; }

        public string ImageURl { get; set; } = string.Empty;

        [NotMapped]
        public IFormFile Image { get; set; }
     

    }
}
