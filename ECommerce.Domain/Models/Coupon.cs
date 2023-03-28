using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Models
{
    internal class Coupon
    {

        public int Id { get; set; }
        public int Admin_Id { get; set; }
        public int Seller_Id{ get; set; }
        public string no_Coupon { get; set; } = string.Empty;
        public DateTime Start_Date { get; set; }
        public DateTime End_Date { get; set; }
        public User? User { get; set; }

    }
}
