using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Models
{
    public class SubProducts:BaseEntity
    {
        [Required]
        public string Serial_Id { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        public int? BrandsId { get; set; }
        public int? CarId { get; set; }
        public int? CategoryId { get; set; }

        [Required]
        public double Price { get; set; }

        public double? offers { get; set; } = 0.0;

        public double? New_price { get; set; } = 0.0;
        [Required]
        public int Quantity { get; set; }
        [Required]
        public string Link { get; set; } = string.Empty;
        public bool? IsSpecialProduct { get; set; }
        public Brands? Brands { get; set; }
        public Car? Car { get; set; }
        public Category? Category { get; set; }
        public int Customer_Id { get; set; }
        public int Admin_Id { get; set; }
        public User? User { get; set; }

         public int? productId { get; set; }
        public Product?product { get; set; }

        public string IsPrimaryImage { get; set; } = string.Empty;


        [NotMapped]
        public IFormFile Primary_Image { get; set; }
        // This method returns the final price after applying the offer if any
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
