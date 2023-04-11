using ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.DTOs
{
    public class PorductsListDto:BaseClass
    {

        public string Title { get; set; } = string.Empty;
        public double Price { get; set; }
        public double Offers { get; set; }
        public double NewPrice { get; set; }
        public int Quantity { get; set; }
 
        public int Customer_Id { get; set; }
        public int Admin_Id { get; set; }
        public User? User { get; set; }
        public string mainImage { get; set; } = string.Empty;
        public string subImage1 { get; set; } = string.Empty;
        public string subImage2 { get; set; } = string.Empty;

    }
}
