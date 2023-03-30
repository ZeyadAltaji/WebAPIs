using ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.DTOs
{
    public class Sub_SliderDTOs : BaseClass
    {
         public string Image { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Button { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;
        public int Admin_Id { get; set; }
        public User? User { get; set; }
    }
}
