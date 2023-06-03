using ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Abstractions.Command
{
    public interface ICartItemRepository
    {
        void Create(ItemCart itemCart);
        Task<IEnumerable<ItemCart>> GetAll();
        void Deleteitem(int itemCartId);
        Task<IEnumerable<ItemCart>> GetByCustomerId(int customerId);
        void UpdateItems(IEnumerable<ItemCart> itemCarts);
        List<ItemCart> GetByCustomerCartId(int customerId, int cartId);


    }
}
