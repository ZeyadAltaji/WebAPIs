using ECommerce.Application.Abstractions;
using ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAccessLayer.Repositories
{
    public class CouponRepository : IRepository<Coupon>
    {
        public DBContext Dc { get; }

        public CouponRepository(DBContext dc)
        {
            Dc = dc;
        }


        public void Create(Coupon entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int Id, Coupon entity)
        {
            throw new NotImplementedException();
        }

        public List<Coupon> GetAll()
        {
            throw new NotImplementedException();
        }

        public Coupon GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(int Id, Coupon entity)
        {
            throw new NotImplementedException();
        }
    }
}
