using ECommerce.Application.Abstractions;
using ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAccessLayer.Repositories
{
    public class ProductRepository : IRepository<Product>
    {
        public DBContext Dc { get; }

        public ProductRepository(DBContext dc)
        {
            Dc = dc;
        }


        public void Create(Product entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int Id, Product entity)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAll()
        {
            throw new NotImplementedException();
        }

        public Product GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(int Id, Product entity)
        {
            throw new NotImplementedException();
        }
    }
}
