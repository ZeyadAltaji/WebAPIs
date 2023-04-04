﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Models
{
    public class Product:BaseEntity
    {
        [Required]
        public string Title { get; set; }=string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public string main_Image { get; set; } = string.Empty;
        [Required]
        public string sub_Image1 { get; set; } = string.Empty;
        [Required]
        public string sub_Image2 { get; set; } = string.Empty;
        [Required]
        public double Price { get; set; }
        [Required]
        public double offers { get; set; }
        [Required]
        public double New_price { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public string Link { get; set; } = string.Empty;
        public string Button { get; set; } = string.Empty;
        public Brands? Brands { get; set; }
        public Car? Car { get; set; }
        public Category? Category { get; set; }
        public int Customer_Id { get; set; }
        public int Admin_Id { get; set; }
        public User? User { get; set; }

    }
}
