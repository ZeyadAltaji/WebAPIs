using ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.DTOs
{
    public class ItemCartDTOs
    {

        public int SubProductsId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public int CartId { get; set; }

        //public List<ItemCart> Item { get; set; }
    }
}
