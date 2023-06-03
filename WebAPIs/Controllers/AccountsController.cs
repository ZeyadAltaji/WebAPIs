using AutoMapper;
using ECommerce.Application.Abstractions;
using ECommerce.Application.DTOs;
using ECommerce.Application.UnitOfWork;
using ECommerce.DataAccessLayer;
using ECommerce.DataAccessLayer.Repositories;
using ECommerce.Domain.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
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
        private readonly IWebHostEnvironment webHostEnvironment;
 
        public static ErrorsAPIs apiError = new ErrorsAPIs();


        public AccountsController(
            IConfiguration configuration,
            IUnitOfWork uow,
             IMapper mapper,
            IPhotoService photoService,
            IWebHostEnvironment webHostEnvironment
          )
         {
            this.configuration = configuration;
            this.uow = uow;
            this.mapper =mapper;
            this.photoService = photoService;
            this.webHostEnvironment= webHostEnvironment;
 
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(LoginReqDto loginReq)
        {
            if (loginReq.UserName == null || loginReq.Password == null)
            {
                apiError.Error_Code = BadRequest().StatusCode;
                apiError.Error_Messages = "User name or password can not be blank";
                return BadRequest(apiError);
            }
            if (await uow.UserRepository.UserAlreadyExists(loginReq.UserName,loginReq.Email))
            {
                apiError.Error_Code = BadRequest().StatusCode;
                apiError.Error_Messages = "User already exists, please try different user name";
                return BadRequest(apiError);
            }
            uow.UserRepository.Register(loginReq.UserName, loginReq.Frist_Name, loginReq.Last_Name, loginReq.Email, loginReq.Password, loginReq.ComfirmPassword, loginReq.Role);
            return StatusCode(200);
        }
        // Post: api/Accounts/BusinessAccount
        [HttpPost("BusinessAccount")]
        public async Task<IActionResult> BusinessAccount(LoginReqDto loginReq)
        {
            if (loginReq.UserName == null || loginReq.Password == null)
            {
                apiError.Error_Code = BadRequest().StatusCode;
                apiError.Error_Messages = "User name or password can not be blank";
                return BadRequest(apiError);
            }
            if (await uow.UserRepository.UserAlreadyExists(loginReq.UserName, loginReq.Email))
            {
                apiError.Error_Code = BadRequest().StatusCode;
                apiError.Error_Messages = "User already exists, please try different user name";
                return BadRequest(apiError);
            }
            uow.UserRepository.BusinessAccountRegister(loginReq.UserName, loginReq.Email, loginReq.Password, loginReq.ComfirmPassword, loginReq.Role);
            return StatusCode(200);
        }
        // Post: api/Accounts/Login
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginReqDto LoginReq)
        {
            var user = await uow.UserRepository.Authenticate(LoginReq.UserName, LoginReq.Password);
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
            var fullUser = mapper.Map<FullUserDTOs>(user);
            LoginRes.FullUser = fullUser;
            fullUser.Role = user.Role;

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
        [HttpPut("Users/Delete/{id}")]
        public async Task<IActionResult> DeleteUsers(int id)
        {
            var UsersDelete = await uow.UserRepository.FindByIdAsync(id);

            uow.UserRepository.DeleteAsync(id, UsersDelete);
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
        [HttpPost("NewUser")]
        public async Task<IActionResult> NewUser([FromForm] FullUserDTOs userDTOs)
        {
            var user = mapper.Map<User>(userDTOs);

            // Validate the model state
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model state");
            }

            // Check if the user already exists
            if (await uow.UserRepository.UserAlreadyExists(user.UserName,user.Email))
            {
                return BadRequest("User already exists, please try different user name");
            }
            user.Public_id = await SaveImage(userDTOs.Image_userUrl);
            user.CreateDate = DateTimeOffset.Now.LocalDateTime;
            var img = user.Image_userUrl;
            // Create the user and return the result
            await uow.UserRepository.Create(userDTOs,user, img);
            await uow.SaveChanges();
            return Ok(user);

        }
        [NonAction]
        public async Task<string> SaveImage(IFormFile imageFile)
        {
            string imagename = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', ' ');
            imagename = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
            string imagePath = Path.Combine(@"D:\project fianal\E-commerce\projects\dashboard\src\assets\image\Users", imagename);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            return imagename;
        }

        [HttpPut("Users/update")]
        public async Task<IActionResult> Updateuser(int id)
        {
            try
            {
                  var userDTOs = new FullUserDTOs();

                userDTOs.Id = id = int.Parse(HttpContext.Request.Form["id"].ToString());
                var img = HttpContext.Request.Form.Files["Image_userUrl"];
                userDTOs.Frist_Name = HttpContext.Request.Form["Frist_Name"].ToString();
                userDTOs.Last_Name = HttpContext.Request.Form["Last_Name"].ToString();
                userDTOs.UserName = HttpContext.Request.Form["UserName"].ToString();
                userDTOs.Email = HttpContext.Request.Form["Email"].ToString();
                userDTOs.Phone1 = HttpContext.Request.Form["Phone1"].ToString();
                userDTOs.Phone2 = HttpContext.Request.Form["Phone2"].ToString();
                userDTOs.Address = HttpContext.Request.Form["Address"].ToString();
                userDTOs.password = HttpContext.Request.Form["password"].ToString();
                userDTOs.comfirmPassword = HttpContext.Request.Form["comfirmPassword"].ToString();
                userDTOs.UserUpdate = HttpContext.Request.Form["userUpdate"].ToString();
                userDTOs.UpdateDate = DateTimeOffset.Now.LocalDateTime;
                if (!StringValues.IsNullOrEmpty(HttpContext.Request.Form["isActive"]))
                {
                    userDTOs.IsActive = bool.Parse(HttpContext.Request.Form["isActive"]);
                }

                if (!StringValues.IsNullOrEmpty(HttpContext.Request.Form["Role"]) && HttpContext.Request.Form["Role"] != "0")
                {
                    userDTOs.Role = int.Parse(HttpContext.Request.Form["Role"]);
                }
                if (id == userDTOs.Id)
                {
                    if (img != null && img.Length > 0)
                    {
                        // A new image is selected
                        var Update = await uow.UserRepository.UpdateAsync(id, userDTOs, img);
                        if (Update != null) return Ok();
                    }
                    else
                    {
                        // Preserve the existing image
                        var Update = await uow.UserRepository.UpdateAsync(id, userDTOs, null);
                        if (Update != null) return Ok();
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return BadRequest();
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
