using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Models
{
    public class Special: BaseEntity
    {
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public string Image { get; set; } = string.Empty;
        [Required]
        public DateTime? remaining_time { get; set; }
        [Required]
        public int Price { get; set; }
        public int seller_Id { get; set; }
        public User? User { get; set; }
        public Product? product { get; set; }


    }
}
