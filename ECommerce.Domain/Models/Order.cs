using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int Customer_Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Mobile{ get; set; }
        public DateTime? Order_Date { get; set; }
        public double TotalPrice { get; set; }
        public string ShippingAddress { get; set; }
        public string OrderStatus { get; set; }
        public int CartId { get; set; }
        public Cart? Cart { get; set; }
    }
}
