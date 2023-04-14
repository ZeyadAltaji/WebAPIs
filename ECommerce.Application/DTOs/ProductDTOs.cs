using ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.DTOs
{
    public class ProductDTOs:BaseClass
    {
         public string Serial_Id { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string main_Image { get; set; } = string.Empty;
        public string sub_Image1 { get; set; } = string.Empty;
        public string sub_Image2 { get; set; } = string.Empty;
        public double Price { get; set; }
        public double offers { get; set; }
        public double New_price { get; set; }
        public int Quantity { get; set; }
        public string Link { get; set; } = string.Empty;
        public string Button { get; set; } = string.Empty;
        public int? Brands_Id { get; set; }
        public int? Car_Id { get; set; }
        public int? Category_Id { get; set; }
        public int Customer_Id { get; set; }
        public int Admin_Id { get; set; }
        public User? User { get; set; }
    }
}
