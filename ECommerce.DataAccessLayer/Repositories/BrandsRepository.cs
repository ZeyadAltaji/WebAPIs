using ECommerce.Application.Abstractions;
using ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAccessLayer.Repositories
{
    public class BrandsRepository : IRepository<Brands>
    {
        public DBContext Dc { get; }

        public BrandsRepository(DBContext dc)
        {
            Dc = dc;
        }


        public void Create(Brands entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int Id, Brands entity)
        {
            throw new NotImplementedException();
        }

        public List<Brands> GetAll()
        {
            throw new NotImplementedException();
        }

        public Brands GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(int Id, Brands entity)
        {
            throw new NotImplementedException();
        }
    }
}
