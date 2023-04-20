﻿using CloudinaryDotNet.Actions;
using ECommerce.Application.Abstractions;
using ECommerce.Application.DTOs;
using ECommerce.Domain.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.JavaScript;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ECommerce.DataAccessLayer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DBContext DC;
        [Obsolete]
        private readonly IWebHostEnvironment _environment;
        public UserRepository(DBContext dc , IWebHostEnvironment host)
        {
            DC = dc;
            _environment = host;

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
        public void Register(string UserName, string Frist_Name, string Last_Name, string Email, string password, string ComfirmPassword, int Role)
        {
            byte[] passwordHash, passwordSalt;
            createpasswordHash(password, out passwordHash, out passwordSalt);
            if (password != ComfirmPassword)
            {
                throw new ArgumentException("Password and Confirm Password do not match.");
            }
            else
            {
                User user = new User
                {
                    UserName = UserName,
                    Last_Name = Last_Name,
                    Frist_Name = Frist_Name,
                    Email = Email,
                    Password = passwordHash,
                    ComfirmPassword = passwordHash,
                    PasswordKey = passwordSalt,
                    CreateDate = DateTime.Now,
                    Role = 3
                };
                DC.Users.Add(user);
            }
            DC.SaveChanges();

        }

        public void BusinessAccountRegister(string UserName, string Email, string password, string ComfirmPassword, int Role)
        {
            byte[] passwordHash, passwordSalt;

            createpasswordHash(password, out passwordHash, out passwordSalt);

            if (password != ComfirmPassword)
            {
                throw new ArgumentException("Password and Confirm Password do not match.");
            }
            else
            {
                User user = new User();
                user.UserName = UserName;
                user.Email = Email;
                user.Password = passwordHash;
                user.ComfirmPassword = passwordHash;
                user.PasswordKey = passwordSalt;
                user.CreateDate = DateTime.Now;
                user.Role = 2;

                DC.Users.Add(user);
            }
            DC.SaveChanges();

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

        private void createpasswordHash(string password, out byte[] passwordHash, out byte[] passwordsalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordsalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
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
          
        public async Task<User> Create(FullUserDTOs userDtos, User user)
        {
            byte[] passwordHash, passwordSalt;
            createpasswordHash(userDtos.password, out passwordHash, out passwordSalt);
            // Set the password hash and salt
            var data = new User
            {
                UserName = userDtos.UserName,
                Last_Name = userDtos.Last_Name,
                Frist_Name = userDtos.Frist_Name,
                Email = userDtos.Email,
                Password = passwordHash,
                ComfirmPassword = passwordHash,
                PasswordKey = passwordSalt,
              
                CreateDate = DateTime.Now,
                Role = userDtos.Role,
                Phone1 = userDtos.Phone1,
                Phone2 = userDtos.Phone2,
                Address = userDtos.Address,
                Image_userUrl = userDtos.Image.FileName,




            };
            // Add user to the database and save changes
            DC.Users.Add(data);
            await DC.SaveChangesAsync();

            return data;
        }
    }
}
