using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int Customer_Id { get; set; }
        public User?User { get; set; }
         public DateTime CreatedAt { get; set; }
        public int GetCustomerId()
        {
            return Customer_Id;
        }
        //public int Id { get; set; }
        //public int CustomerId { get; set; }
        //public int ItemCart_Id { get; set; }
        //public ItemCart? ItemCart { get; set; }
        //public DateTime CreatedAt { get; set; }
    }
}
