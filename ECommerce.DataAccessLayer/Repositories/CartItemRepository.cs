using ECommerce.Application.Abstractions.Command;
using ECommerce.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAccessLayer.Repositories
{
    public class CartItemRepository: ICartItemRepository
    {
        private readonly DBContext Dc;

        public CartItemRepository(DBContext dc)
        {
            Dc = dc;
        }



        public void Create(ItemCart itemCart)
        {
            Dc.ItemCart.Add(itemCart);
            Dc.SaveChanges();
         }

        public async Task<IEnumerable<ItemCart>> GetAll()
        {
            return await Dc.ItemCart.ToListAsync();

        }
    }
}
