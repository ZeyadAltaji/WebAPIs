using ECommerce.Application.DTOs;
using ECommerce.Domain.Models;
using Microsoft.AspNetCore.Http;
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
        void Register(string UserName, string Frist_Name, string Last_Name, string Email, string password, string ComfirmPassword, int Role);
        void BusinessAccountRegister(string UserName, string Email, string password, string ComfirmPassword, int Role);
        Task<bool> UserAlreadyExists(string UserName, string Email);
        Task<User> FindByEmailAsync(string Email);
        Task<IEnumerable<User>> GetAllUserAsync();
        Task<User> FindByIdAsync(int id);
        void DeleteAsync(int id,User user);
         Task<User> UpdateAsync(int id, object entity, IFormFile img);

        Task<User> Create(FullUserDTOs userDtos,User user,IFormFile img);
    }
}
