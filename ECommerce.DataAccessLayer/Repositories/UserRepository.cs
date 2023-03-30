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
        private readonly DBContext DC;
        public UserRepository(DBContext dc)
        {
            DC = dc;     
        }
        public async Task<User> Authenticate(string UserName, string password)
        {
            throw new NotImplementedException();

        }

        public void Register(string UserName, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UserAlreadyExists(string UserName)
        {
            throw new NotImplementedException();
        }
    }
}
