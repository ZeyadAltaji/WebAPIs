using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Sub_SliderController : ControllerBase
    {
        // GET: api/<Sub_SliderController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<Sub_SliderController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<Sub_SliderController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<Sub_SliderController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<Sub_SliderController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
