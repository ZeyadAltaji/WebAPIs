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



        public void Deleteitem(int itemCartId)
        {
            var itemCart = Dc.ItemCart.Find(itemCartId);
            if (itemCart != null)
            {
                Dc.ItemCart.Remove(itemCart);
                Dc.SaveChanges();
            }
        }

        public async Task<IEnumerable<ItemCart>> GetByCustomerId(int customerId)
        {
            return await Dc.ItemCart
                    .Where(ic => ic.Cart.GetCustomerId() == customerId)
                    .ToListAsync();
        }

        public void UpdateItems(IEnumerable<ItemCart> itemCarts)
        {
            foreach (var itemCart in itemCarts)
            {
                var existingItem = Dc.ItemCart.Find(itemCart.Id);
                if (existingItem != null)
                {
                    // Update the properties of the existing item
                    existingItem.Quantity = itemCart.Quantity;
                    existingItem.Price = itemCart.Price;
                    // Update other properties as needed

                    Dc.ItemCart.Update(existingItem);
                }
            }
            Dc.SaveChanges();
        }

        public List<ItemCart> GetByCustomerCartId(int customerId, int cartId)
        {
            var cartItems =  Dc.ItemCart
                .Join(
                    Dc.Carts,
                    i => i.CartId,
                    c => c.Id,
                    (itemCart, cart) => new { ItemCart = itemCart, Cart = cart }
                )
                .AsEnumerable()
                .Where(ic => ic.Cart.Customer_Id == customerId && ic.ItemCart.CartId == cartId)
                .Select(ic => ic.ItemCart)
                .ToList();

            return cartItems;
        }
    }
    
}
