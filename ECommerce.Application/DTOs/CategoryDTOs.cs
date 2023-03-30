using ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.DTOs
{
    public class CategoryDTOs :BaseClass
    {
        public string Name { get; set; } = string.Empty;
        public int Admin_Id { get; set; }
        public User? User { get; set; }
    }
}
