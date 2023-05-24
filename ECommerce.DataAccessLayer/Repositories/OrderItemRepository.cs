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
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly DBContext Dc;

        public OrderItemRepository(DBContext dc)
        {
            Dc = dc;
        }
        public void AddOrderItem(OrderItem orderItem)
        {
            Dc.OrderItem.Add(orderItem);
            Dc.SaveChanges();
        }

        public OrderItem GetOrderItemById(int orderItemId)
        {
            return Dc.OrderItem.FirstOrDefault(oi => oi.OrderItemId == orderItemId);
        }

        public void RemoveOrderItem(OrderItem orderItem)
        {
            Dc.OrderItem.Remove(orderItem);
            Dc.SaveChanges();
        }

        public void UpdateOrderItem(OrderItem orderItem)
        {
            Dc.OrderItem.Add(orderItem);
            Dc.SaveChanges();
        }
    }
}
