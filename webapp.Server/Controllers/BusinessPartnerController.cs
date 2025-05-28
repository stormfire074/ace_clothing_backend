using Microsoft.AspNetCore.Mvc;
using webapp.Application;
using webapp.Domain;

namespace webapp.Server
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessPartnerController : ControllerBase
    {
        private readonly ISAPService<BusinessPartner> bpService;

        public BusinessPartnerController(ISAPService<BusinessPartner> bpService)
        {
            this.bpService = bpService;
        }
        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult> Create([FromBody] BusinessPartner request)
        {
            var response = await this.bpService.AddAsync(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("Update")]
        public async Task<ActionResult> Update([FromBody] BusinessPartner request)
        {
            await this.bpService.UpdateAsync(request);
            var response = await this.bpService.GetByIdAsync(request?.CardCode ?? "");
            return Ok(response);
        }

        [HttpGet]
        [Route("List")]
        public async Task<ActionResult> List()
        {
            var response = await this.bpService.GetAllAsync();
            return Ok(response);
        }

        [HttpGet]
        [Route("Retrieve")]
        public async Task<ActionResult> Retrieve([FromQuery] string Key)
        {
            var response = await this.bpService.GetByIdAsync(Key);
            return Ok(response);
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<ActionResult> Delete([FromBody] BusinessPartner request)
        {
            var response = await this.bpService.DeleteAsync(request);
            return Ok(response);
        }

    }
}
