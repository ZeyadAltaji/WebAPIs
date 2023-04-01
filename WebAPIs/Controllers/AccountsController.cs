using ECommerce.Application.DTOs;
using ECommerce.Application.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace WebAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IConfiguration Configuration;
        private readonly IUnitOfWork Uow;
        public AccountsController(IConfiguration configuration, IUnitOfWork uow)
        {
            Uow = uow;
            Configuration = configuration;
            
        }
        // Post: api/<AccountsController>
        [HttpPost("register")]
        public async Task<IActionResult> Register(LoginReqDto loginReq)
        {
            if (loginReq.Username==null ||  loginReq.Password==null) return BadRequest("There is empty value in User Name Or Password !");

            if (await Uow.UserRepository.UserAlreadyExists(loginReq.Username)) return StatusCode(401);

              Uow.UserRepository.Register(loginReq.Username, loginReq.Password);
            await Uow.SaveChanges();
            return StatusCode(200);
        }

        // GET api/<AccountsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AccountsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AccountsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AccountsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
