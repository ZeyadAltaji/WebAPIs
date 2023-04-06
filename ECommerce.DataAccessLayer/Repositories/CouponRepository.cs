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
    public class CouponRepository : IRepository<Coupon>
    {
        private readonly DBContext Dc;

        public CouponRepository(DBContext dc)
        {
            Dc = dc;
        }


        public void Create(Coupon entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            Dc.Coupons.Add(entity);
            Dc.SaveChanges();
        }

        public void Delete(int Id, Coupon entity)
        {
            entity.IsDelete = true;
            Dc.Coupons.Update(entity);
            Dc.SaveChanges();
        }
        public async Task<IEnumerable<Coupon>> GetAll()
        {
            return await Dc.Coupons.Include(Getbyid => Getbyid.User).Where(x => x.IsDelete == false).ToListAsync();

        }

        public async Task<Coupon> GetByID(int id)
        {
            return await Dc.Coupons.Include(Getbyid => Getbyid.User).SingleOrDefaultAsync(Getbyid => Getbyid.Id == id);

        }
        
        public void Update(int Id, Coupon entity)
        {
            Dc.Coupons.Update(entity);
            Dc.SaveChanges();
        }

        public void Active(int Id, Coupon entity)
        {
            if (entity.IsActive == false)
            {
                entity.IsActive = true;
            }
            else if (entity.IsActive == true)
            {
                entity.IsActive = false;
            }
            Dc.Coupons.Update(entity);
            Dc.SaveChanges();
        }

        public IList<Coupon> GetAllViewFrontClinet()
        {
            return Dc.Coupons.Include(Getbyid => Getbyid.User).Where(x => x.IsActive == true && x.IsDelete == false).ToList();
        }

      
    }
}
