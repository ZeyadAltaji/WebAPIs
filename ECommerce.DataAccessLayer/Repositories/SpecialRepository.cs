using ECommerce.Application.Abstractions;
using ECommerce.Domain.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAccessLayer.Repositories
{
    public class SpecialRepository : IRepository<Special>
    {
        public DBContext Dc { get; }

        public SpecialRepository(DBContext dc)
        {
            Dc = dc;
        }


        public void Create(Special entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int Id, Special entity)
        {
            throw new NotImplementedException();
        }

        public List<Special> GetAll()
        {
            throw new NotImplementedException();
        }

        public Special GetByID(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(int Id, Special entity)
        {
            throw new NotImplementedException();
        }
    }
}
