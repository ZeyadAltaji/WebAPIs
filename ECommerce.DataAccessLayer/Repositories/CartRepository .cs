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
    public class CartRepository : ICartRepository
    {
        private readonly DBContext Dc;

        public CartRepository(DBContext dc)
        {
            Dc = dc;
        }


    
        public void Create(Cart cart)
        {
            Dc.Carts.Add(cart);
            Dc.SaveChanges();
        }
        public async Task<Cart> GetById(int id)
        {
            return await Dc.Carts.FindAsync(id);
        }

    }
}
