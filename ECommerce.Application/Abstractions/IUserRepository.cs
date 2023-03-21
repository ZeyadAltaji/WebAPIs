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
        Task<User> Authenticate(string Email, string password);
        void Register(string Email, string password);

        Task<bool> UserAlreadyExists(string Email);
    }
}
