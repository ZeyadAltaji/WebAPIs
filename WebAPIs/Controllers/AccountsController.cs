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
            if (await uow.UserRepository.UserAlreadyExists(loginReq.Username)) return BadRequest("User already exists, please try different user name");
            if(ModelState.IsValid)
            {
                uow.UserRepository.Register(loginReq.Username, loginReq.Password, loginReq.ComfirmPassword);
                await uow.SaveChanges();
                return StatusCode(200);
            }
            else
            {
                if (loginReq.Username == null || loginReq.Password == null) return BadRequest("There is empty value in User Name Or Password !");
                //if (loginReq.Password != loginReq.ComfirmPassword) return BadRequest("Some Errors");

            }
            return StatusCode(200);
        }
        // Post: api/Accounts/Login

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginReqDto LoginReq)
        {
            var user = await uow.UserRepository.Authenticate(LoginReq.Username, LoginReq.Password);
            if (user == null) return Unauthorized(401);
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
