using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Models
{
    public class Photo :BaseEntity
    {
        [Required]
        public string publicID { get; set; }
        [Required]
        public string main_ImageUrl { get; set; }
        public string sub_Image1Url { get; set; }
        public string sub_Image2Url { get; set; }


        public bool main_Image { get; set; }
        public bool sub_Image1 { get; set; }
        public bool sub_Image2 { get; set; }
        public Product? Product { get; set; }
    }
}
