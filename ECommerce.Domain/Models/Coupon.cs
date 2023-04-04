using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Models
{
    public class Coupon: BaseEntity
    {

        
        public int Admin_Id { get; set; }
        public int Seller_Id{ get; set; }
        [Required]
        public string no_Coupon { get; set; } = string.Empty;
        [Required]
        public DateTime? Start_Date { get; set; }
        [Required]
        public DateTime? End_Date { get; set; }
        public User? User { get; set; }
        public Product? product { get; set; }

    }
}
