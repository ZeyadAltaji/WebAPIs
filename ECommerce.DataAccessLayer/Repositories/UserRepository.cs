using ECommerce.Application.Abstractions;
using ECommerce.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAccessLayer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DBContext dc;
        public UserRepository(DBContext dc)
        {
            this.dc = dc;     
        }
        public async Task<User> Authenticate(string Email, string password)
        {
            throw new NotImplementedException();
        }

        public void Register(string Email, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UserAlreadyExists(string Email)
        {
            throw new NotImplementedException();
        }
    }
}
