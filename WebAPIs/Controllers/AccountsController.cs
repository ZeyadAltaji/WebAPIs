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

namespace WebAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IUnitOfWork uow;
         public AccountsController(
            IConfiguration configuration,
            IUnitOfWork uow
           )
         {
            this.configuration = configuration;
            this.uow = uow;
 
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
            uow.UserRepository.BusinessAccountRegister(loginReq.Username,loginReq.Email, loginReq.Password, loginReq.ComfirmPassword, loginReq.Role);
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


    }
}
