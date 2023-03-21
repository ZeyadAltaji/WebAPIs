using ECommerce.Application.Abstractions;
using ECommerce.Application.UnitOfWork;
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
        public IUserRepository UserRepository => throw new NotImplementedException();
    }
}
