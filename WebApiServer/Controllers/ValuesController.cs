using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data_Access_Layer;
using Data_Access_Layer.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApiServer.Services;

namespace WebApiServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private IOptions<JwtConfiguration> _jwtConfiguration;

        public ValuesController(
            LogService service,
            IRepository<ProductAttribute> parepository,
            ProductRepository aa,
            AttributeRepository bb,
            IOptions<JwtConfiguration> jwtConfiguration)
        {
            _jwtConfiguration = jwtConfiguration;
        }


        [HttpGet]
        public IActionResult Get()
        {
            return new ObjectResult(_jwtConfiguration.Value);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
