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
    public class ContactUsRepository : IRepository<ContactUs>
    {
        private readonly DBContext Dc;


        public ContactUsRepository(DBContext dc)
        {
            Dc = dc;
        }
        public void Active(int Id, ContactUs entity)
        {
            throw new NotImplementedException();
        }

        public void Create(ContactUs entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            Dc.ContactUs.Add(entity);
            Dc.SaveChanges();
        }

        public void Delete(int Id, ContactUs entity)
        {
            entity.Show = true;
            Dc.ContactUs.Update(entity);
            Dc.SaveChanges();
        }

        public async Task<IEnumerable<ContactUs>> GetAll()
        {
            return await Dc.ContactUs.Where(x => x.Show == true).ToListAsync();

         }

        public IList<ContactUs> GetAllViewFrontClinet()
        {
            throw new NotImplementedException();
        }

        public async Task<ContactUs> GetByID(int id)
        {
            return await Dc.ContactUs.SingleOrDefaultAsync(Getbyid => Getbyid.Id == id);
        }

        public void Update(int Id, ContactUs entity)
        {
            Dc.ContactUs.Update(entity);
            Dc.SaveChanges();
        }
    }
}
