﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Models
{
    public class Brands: BaseEntity
    {
        [Required]
        public string Image { get; set; } = string.Empty;
        [Required]
        public string Name { get; set; }= string.Empty;

        public string Link { get; set; } = string.Empty;
        public int Admin_Id { get; set; }
        public User? User { get; set; }
    }
}
