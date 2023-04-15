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
            if (user == null || user.Password == null) return null;
            if (!MatchPasswordHash(passwordText, user.Password, user.PasswordKey)) return null;
            if (user.IsDelete) throw new Exception("User has been deleted.");
            else if (!user.IsActive) throw new Exception("User is inactive.");
            return user;

        }
        public void BusinessAccountRegister(string UserName, string Email, string password, string ComfirmPassword, int Role)
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
                user.Email = Email;
                user.Password = passwordHash;
                user.ComfirmPassword = passwordHash;
                user.PasswordKey = passwordKey;
                user.CreateDate = DateTime.Now;
                user.Role = 2;

                DC.Users.Add(user);
            }
        }
        public async Task<User> FindByEmailAsync(string Email)
        {
            var user = await DC.Users.FirstOrDefaultAsync(u => u.Email == Email);
            if (user == null)
            {
                Console.WriteLine("User not found.");
                return null; // or throw an exception if appropriate
            }
            DC.Users.Update(user);
            await DC.SaveChangesAsync();
            return user;
        }
        public async Task<User> FindByIdAsync(int id)
        {
            return await DC.Users.FindAsync(id);
        }
        public async Task<bool> UserAlreadyExists(string UserName)
        {
            return await DC.Users.AnyAsync(x => x.UserName == UserName);

        }
        public void Register(string UserName, string Frist_Name, string Last_Name, string Email, string password, string ComfirmPassword, int Role)
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
                user.Frist_Name = Frist_Name;
                user.Last_Name = Last_Name;
                user.Email = Email;
                user.Password = passwordHash;
                user.ComfirmPassword = passwordHash;
                user.PasswordKey = passwordKey;
                user.CreateDate = DateTime.Now;
                user.Role = 3;

                DC.Users.Add(user);
            }
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

        public async Task<IEnumerable<User>> GetAllUserAsync()
        {
            return await DC.Users.ToListAsync();
        }

        public void DeleteAsync(int id, User user)
        {
            user.IsDelete = true;
            user.IsActive = false;
            DC.Users.Update(user);
            DC.SaveChangesAsync();
        }

        public void UpdateAsync(int id, User user)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            DC.Users.Update(user);
            DC.SaveChanges();
        }
    }
}
