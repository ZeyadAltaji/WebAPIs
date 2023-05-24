using ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Abstractions.Command
{
    public interface IOrderRepository
    {
         Task<Order> GetOrderById(int id);
        Task<Order> GetOrdersByCustomerId(int customerId);

        void CreateOrder(Order order);
        void UpdateOrder(Order order);
        void DeleteOrder(Order order);
        Task<IEnumerable<Order>> GetAll();

    }
}
