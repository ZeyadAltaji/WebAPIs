using ECommerce.Application.Abstractions;
using ECommerce.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
        public async Task<User> Authenticate(string UserName, string passwordText)
        {
            var user = await DC.Users.FirstOrDefaultAsync(X => X.UserName == UserName);
            if (user == null || user.Password == null)
                return null;
            
            if(!MatchPasswordHash(passwordText,user.Password, user.PasswordKey))
                return null;
            
            return user;
            

        }
        public void Register(string UserName, string password, string ComfirmPassword)
        {
            byte[] passwordHash, passwordKey;

            using (var hmac = new HMACSHA512())
            {
                passwordKey = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            }
            if (password != ComfirmPassword)
            {
                Console.WriteLine("Password In Valid ConfirmPassword ");
            }
            else
            {
                User user = new User();
                user.UserName = UserName;
                user.Password = passwordHash;
                user.ComfirmPassword = passwordHash;
                user.PasswordKey = passwordKey;
                user.CreateDate = DateTime.Now;
                
                DC.Users.Add(user);
            }
        }

        public async Task<bool> UserAlreadyExists(string UserName)
        {
            return await DC.Users.AnyAsync(x=>x.UserName == UserName);
        }

        //Passwordalgorithm
        private bool MatchPasswordHash(string passwordText, byte[] password, byte[] passwordKey)
        {
            using (var hmac = new HMACSHA512(passwordKey))
            {
                var passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(passwordText));

                for (int i = 0; i < passwordHash.Length; i++)
                {
                    if (passwordHash[i] != password[i])
                        return false;
                }

                return true;
            }
        }
    }
}
