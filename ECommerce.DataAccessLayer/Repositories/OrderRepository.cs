using ECommerce.Application.Abstractions;
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
    public class OrderRepository : IOrderRepository
    {
        private readonly DBContext Dc;

        public OrderRepository(DBContext dc)
        {
            Dc = dc;
        }

        public void CreateOrder(Order order)
        {
            Dc.Orders.Add(order);
            Dc.SaveChanges();
        }

        public void DeleteOrder(Order order)
        {
            Dc.Orders.Add(order);
            Dc.SaveChanges();
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            return await Dc.Orders.ToListAsync();
        }

        public async Task<Order> GetOrderById(int id)
        {
 
            return Dc.Orders.Include(o => o.Id).FirstOrDefault(o => o.Id == id);
        }
         
        public async Task<Order> GetOrdersByCustomerId(int customerId)
        {
            return await Dc.Orders.FirstOrDefaultAsync(o => o.Customer_Id == customerId);
        }
        public void UpdateOrder(Order order)
        {
            Dc.Orders.Add(order);
            Dc.SaveChanges();
        }

        
    }
}
