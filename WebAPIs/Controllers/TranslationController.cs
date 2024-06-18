using ECommerce.Application.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace WebAPIs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TranslationController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        public TranslationController (IUnitOfWork uow)
        {
            this.uow = uow;
        }
        [HttpGet("GetLanguage")]
        public async Task<IActionResult> GetLanguage ()
        {
            var lLanguage = await uow.repositoryLanguage.GetAll();
            return Ok(lLanguage);

        }
        [HttpGet("GetMessages")]
        public async Task<IActionResult> GetMessages ()
        {
            var lMessages = await uow.repositoryMessages.GetAll();
            return Ok(lMessages);
        }
    }
}
