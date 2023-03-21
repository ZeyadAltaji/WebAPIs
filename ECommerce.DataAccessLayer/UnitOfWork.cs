using ECommerce.Application.Abstractions;
using ECommerce.Application.UnitOfWork;
using ECommerce.DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAccessLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DBContext dc;
        public UnitOfWork(DBContext dc)
        {
            this.dc = dc;     
        }
        public IUserRepository UserRepository => new UserRepository(dc);
    }
}
