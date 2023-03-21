using ECommerce.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.UnitOfWork
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
    }
}
