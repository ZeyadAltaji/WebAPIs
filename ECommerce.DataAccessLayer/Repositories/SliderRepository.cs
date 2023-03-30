using ECommerce.Application.Abstractions;
using ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAccessLayer.Repositories
{
    public class SliderRepository : IRepository<Slider>
    {
        public DBContext Dc { get; }

        public SliderRepository(DBContext dc)
        {
            Dc = dc;
        }


        public void Create(Slider entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int Id, Slider entity)
        {
            throw new NotImplementedException();
        }

        public List<Slider> GetAll()
        {
            throw new NotImplementedException();
        }

        public Slider GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(int Id, Slider entity)
        {
            throw new NotImplementedException();
        }
    }
}
