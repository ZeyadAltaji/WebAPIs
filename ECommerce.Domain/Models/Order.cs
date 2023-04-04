using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Models
{
    public class Order: BaseEntity
    {
        
        public int Customer_Id { get; set; }
        public DateTime Order_Date { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string Payment_Info { get; set; } = string.Empty;
        public Product? Product { get; set; }
        public User? User { get; set; }
    }
}
