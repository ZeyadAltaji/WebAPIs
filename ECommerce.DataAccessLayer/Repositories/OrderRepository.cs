using ECommerce.Application.Abstractions;
using ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAccessLayer.Repositories
{
    public class OrderRepository : IRepository<Order>
    {
        public DBContext Dc { get; }

        public OrderRepository(DBContext dc)
        {
            Dc = dc;
        }


        public void Create(Order entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int Id, Order entity)
        {
            throw new NotImplementedException();
        }

        public List<Order> GetAll()
        {
            throw new NotImplementedException();
        }

        public Order GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(int Id, Order entity)
        {
            throw new NotImplementedException();
        }
    }
}
