﻿using ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.DTOs
{
    public class SpecialDTOs : BaseClass
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public DateTime remaining_time { get; set; }
        public int Price { get; set; }
        public int seller_Id { get; set; }
        public User? User { get; set; }
    }
}
