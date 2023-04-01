using ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Abstractions
{
    public interface IUserRepository
    {
        Task<User> Authenticate(string UserName, string password);
        void Register(string UserName, string password,string ComfirmPassword);

        Task<bool> UserAlreadyExists(string UserName);
    }
}
