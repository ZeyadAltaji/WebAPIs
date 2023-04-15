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
        public string publicID { get; set; }
        public string main_ImageUrl { get; set; }
        public string sub_Image1Url { get; set; }
        public string sub_Image2Url { get; set; }


        public bool main_Image { get; set; }
        public bool sub_Image1 { get; set; }
        public bool sub_Image2 { get; set; }
        public Product? Product { get; set; }


        public User? user { get; set; }
        public int user_Id { get; set; }
        public string Image_userUrl { get; set; }

        public string Public_id { get; set; }
    }
}
