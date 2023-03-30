using ECommerce.Application.Abstractions;
using ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAccessLayer.Repositories
{
    public class CategoryRepository : IRepository<Category>
    {
        public DBContext Dc { get; }

        public CategoryRepository(DBContext dc)
        {
            Dc = dc;
        }


        public void Create(Category entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int Id, Category entity)
        {
            throw new NotImplementedException();
        }

        public List<Category> GetAll()
        {
            throw new NotImplementedException();
        }

        public Category GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(int Id, Category entity)
        {
            throw new NotImplementedException();
        }
    }
}
