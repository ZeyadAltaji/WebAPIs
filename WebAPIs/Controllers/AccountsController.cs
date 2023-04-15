using AutoMapper;
using ECommerce.Application.Abstractions;
using ECommerce.Application.DTOs;
using ECommerce.Application.UnitOfWork;
using ECommerce.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPIs.Errors;
using WebAPIs.Services;

namespace WebAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        private readonly IPhotoService photoService;

        public AccountsController(
            IConfiguration configuration,
            IUnitOfWork uow,
             IMapper mapper,
            IPhotoService photoService
         )
         {
            this.configuration = configuration;
            this.uow = uow;
            this.mapper =mapper;
            this.photoService = photoService;
         }
        // Post: api/Accounts/register
        [HttpPost("register")]
        public async Task<IActionResult> Register(LoginReqDto loginReq)
        {
            ErrorsAPIs apiError = new ErrorsAPIs();
            if (loginReq.Username == null || loginReq.Password == null)
            {
                apiError.Error_Code = BadRequest().StatusCode;
                apiError.Error_Messages = "User name or password can not be blank";
                return BadRequest(apiError);
            }
            if (await uow.UserRepository.UserAlreadyExists(loginReq.Username))
            {
                apiError.Error_Code = BadRequest().StatusCode;
                apiError.Error_Messages = "User already exists, please try different user name";
                return BadRequest(apiError);
            }
            uow.UserRepository.Register(loginReq.Username, loginReq.Frist_Name, loginReq.Last_Name, loginReq.Email, loginReq.Password,loginReq.ComfirmPassword,loginReq.Role);
            await uow.SaveChanges();
            return StatusCode(200);
        }
        // Post: api/Accounts/BusinessAccount
       [HttpPost("BusinessAccount")]
        public async Task<IActionResult> BusinessAccount(LoginReqDto loginReq)
        {
            ErrorsAPIs apiError = new ErrorsAPIs();
            if (loginReq.Username == null || loginReq.Password == null)
            {
                apiError.Error_Code = BadRequest().StatusCode;
                apiError.Error_Messages = "User name or password can not be blank";
                return BadRequest(apiError);
            }
            if (await uow.UserRepository.UserAlreadyExists(loginReq.Username))
            {
                apiError.Error_Code = BadRequest().StatusCode;
                apiError.Error_Messages = "User already exists, please try different user name";
                return BadRequest(apiError);
            }
            uow.UserRepository.BusinessAccountRegister(loginReq.Username, loginReq.Email, loginReq.Password, loginReq.ComfirmPassword, loginReq.Role);
            await uow.SaveChanges();
            return StatusCode(200);
        }
        // Post: api/Accounts/Login
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginReqDto LoginReq)
        {
            var user = await uow.UserRepository.Authenticate(LoginReq.Username, LoginReq.Password);
            ErrorsAPIs apiError = new ErrorsAPIs();
            if (user == null)
            {
                apiError.Error_Code = Unauthorized().StatusCode;
                apiError.Error_Messages = "Invalid user name or password";
                apiError.Errors_Details = "This error appear when provided user id or password does not exists";
                return Unauthorized(apiError);
            }
            var LoginRes = new LoginResDto();
            LoginRes.UserName = user.UserName;
            LoginRes.Token =CreateJWT(user);
            return Ok(LoginRes);

        }
        [HttpGet("AllUsers")]
        public async Task<IActionResult>GetAllUsers()
        {
            var Users = await uow.UserRepository.GetAllUserAsync();
            if (Users == null) BadRequest("Users Not Found");

            var UserDTO = mapper.Map<IEnumerable<FullUserDTOs>>(Users);
            return Ok(UserDTO);

        }
        [HttpGet("AllUsers/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var GetUser = await uow.UserRepository.FindByIdAsync(id);
            if (id == null) BadRequest("User Not Found ! OR User Is Deleted");
            var GetByIdDtos = mapper.Map<FullUserDTOs>(GetUser);
            return Ok(GetByIdDtos);

        }
        [HttpPut("Users/update/{id}")]
        public async Task<IActionResult> UpdateUsers(int id, FullUserDTOs userDto)
        {
            if (id != userDto.Id) return BadRequest("Update not allowed");

             var user = await uow.UserRepository.FindByIdAsync(id);

            if (user == null) return BadRequest("User Not Found !");

            mapper.Map(userDto,user);
            await uow.SaveChanges();
            return StatusCode(200);
        }
        [HttpPut("Users/Delete/{id}")]
        public async Task<IActionResult> DeleteUsers(int id)
        {
            var UsersDelete = await uow.UserRepository.FindByIdAsync(id);

            uow.UserRepository.DeleteAsync(id, UsersDelete);
            await uow.SaveChanges();
            return Ok(id);
        }
        [HttpPost("ForGet-Password")]
        public async Task<IActionResult> ForGetPassword(LoginReqDto LoginReq)
        {
            var user = await uow.UserRepository.FindByEmailAsync(LoginReq.Email);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (user == null)
            {
                BadRequest("User not found with the provided email address");
            }

            // Update user's password
            LoginReq.Password = CreateJWT(user); // set to the new hashed password value
 
            await uow.SaveChanges();

            return Ok(user);

        }
        private string CreateJWT(User user)
        {

            var secretKey = configuration.GetSection("AppSettings:key").Value;
            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(secretKey));

            var claims = new Claim[] {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())
            };

            var signingCredentials = new SigningCredentials(
                    key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = signingCredentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        [HttpPost("UserProfile/UploadImages/{userId}")]
        public async Task<ActionResult<UserPhotoDtos>> UploadPhotoUser(int userId, IFormFile user_files)
        {
            var user = await uow.UserRepository.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var uploadResult = await photoService.UploadPhotoAsync(user_files);

            if (uploadResult == null || string.IsNullOrEmpty(uploadResult.PublicId) || uploadResult.SecureUrl == null)
            {
                return BadRequest("Invalid upload result");
            }

            user.Image_userUrl = uploadResult.SecureUrl.AbsoluteUri;
            user.Public_id = uploadResult.PublicId;

            uow.UserRepository.UpdateAsync(userId, user);


            await uow.SaveChanges();

            return Ok(user);
        }
       

    }
}
