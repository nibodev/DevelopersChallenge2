using System.Threading.Tasks;
using DevelopersChallenge2.Domain;
using DevelopersChallenge2.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DevelopersChallenge2.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IBankListService service;
        
        public UploadController(IBankListService service)
        {
            this.service = service;
        }

        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await this.service.Get());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await this.service.GetById(id));
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post()
        {

            var result = await this.service.PostBankList(Request.Body);
            //this.service.Post(result);

            return Created(Request.Path, result);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] BANKTRANLIST value)
        {
            await this.service.Put(value);
            return NoContent();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await this.service.Delete(id);
            return NoContent();
        }
    }
}
