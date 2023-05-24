using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.Models
{
    public class ItemCart
    {
         public int Id { get; set; }

        [ForeignKey("SubProducts")] // add this attribute
        public int SubProductsId { get; set; }
        public SubProducts? subProducts { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public int CartId { get; set; }
        public Cart? Cart { get; set; }
        //public int SubProductsId { get; set; }
        //public SubProducts? subProducts{ get; set; }
        //public int Quantity { get; set; }
        //public double TotalPrice { get; set; }
        //public int CustomerId { get; set; }
        ////public List<ItemCart> Item { get; set; }

    }
}
