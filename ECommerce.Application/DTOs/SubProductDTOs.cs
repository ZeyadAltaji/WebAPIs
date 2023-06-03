using ECommerce.Domain.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.DTOs
{
    public class SubProductDTOs:BaseClass
    {
        public string Serial_Id { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? offers { get; set; }= default(double); 
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? New_price { get; set; }=  default(double); 
        public int Quantity { get; set; }
        public string Link { get; set; } = string.Empty;
        public bool? IsSpecialProduct { get; set; }
        public int? Brands_Id { get; set; }
        public int? Car_Id { get; set; }
        public int? Category_Id { get; set; }
        public int Customer_Id { get; set; }
        public int? productId { get; set; }

        public int Admin_Id { get; set; }
        public User? User { get; set; }

        public Product? product { get; set; }
        public string IsPrimaryImage { get; set; } = string.Empty;
 
        public IFormFile Primary_Image { get; set; }
        public double GetFinalPrice()
        {
            // Use the ?? operator to assign a default value of 0 if offers is null
            double offer = offers ?? 0;
            // Use the ?? operator to assign the original price if New_price is null
            double newPrice = New_price ?? Price;
            // Return the final price after subtracting the offer
            return newPrice - offer;
        }

    }
}
