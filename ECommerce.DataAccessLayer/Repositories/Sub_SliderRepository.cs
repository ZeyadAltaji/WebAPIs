using ECommerce.Application.Abstractions;
using ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAccessLayer.Repositories
{
    public class Sub_SliderRepository : IRepository<Sub_Slider>
    {
        public DBContext Dc { get; }

        public Sub_SliderRepository(DBContext dc)
        {
            Dc = dc;
        }


        public void Create(Sub_Slider entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int Id, Sub_Slider entity)
        {
            throw new NotImplementedException();
        }

        public List<Sub_Slider> GetAll()
        {
            throw new NotImplementedException();
        }

        public Sub_Slider GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(int Id, Sub_Slider entity)
        {
            throw new NotImplementedException();
        }
    }
}
