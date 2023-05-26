using ECommerce.Domain.Models;
using Microsoft.AspNetCore.Http;
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
        IEnumerable<Order> GetOrdersBydelivery();
        Task<Order> EditAsyncTest(int id,object entity);

        void CreateOrder(Order order);
         void DeleteOrder(Order order);
        Task<IEnumerable<Order>> GetAll();

    }
}
