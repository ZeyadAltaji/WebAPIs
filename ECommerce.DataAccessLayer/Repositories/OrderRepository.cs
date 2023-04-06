using ECommerce.Application.Abstractions;
using ECommerce.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAccessLayer.Repositories
{
    public class OrderRepository : IRepository<Order>
    {
        private readonly DBContext Dc;

        public OrderRepository(DBContext dc)
        {
            Dc = dc;
        }


        public void Create(Order entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            Dc.Orders.Add(entity);
            Dc.SaveChanges();
        }

        public void Delete(int Id, Order entity)
        {
            entity.IsDelete = true;
            Dc.Orders.Update(entity);
            Dc.SaveChanges();
        }
        public async Task<IEnumerable<Order>> GetAll()
        {
            return await Dc.Orders.Include(Getbyid => Getbyid.User).Where(x => x.IsDelete == false).ToListAsync();

        }

        public async Task<Order> GetByID(int id)
        {
            return await Dc.Orders.Include(Getbyid => Getbyid.User).SingleOrDefaultAsync(Getbyid => Getbyid.Id == id);

        }
        
        public void Update(int Id, Order entity)
        {
            Dc.Orders.Update(entity);
            Dc.SaveChanges();
        }

        public void Active(int Id, Order entity)
        {
            if (entity.IsActive == false)
            {
                entity.IsActive = true;
            }
            else if (entity.IsActive == true)
            {
                entity.IsActive = false;
            }
            Dc.Orders.Update(entity);
            Dc.SaveChanges();
        }

        public IList<Order> GetAllViewFrontClinet()
        {
            return Dc.Orders.Include(Getbyid => Getbyid.User).Include(x => x.Product).Where(x => x.IsActive == true && x.IsDelete == false).ToList();
        }

    
    }
}
