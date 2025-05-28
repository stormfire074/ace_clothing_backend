using Microsoft.AspNetCore.Mvc;
using webapp.Application;
using webapp.Domain;

namespace SBODeskReact.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SAPDatabasesController : ControllerBase
    {

        private readonly IService<SAPDatabases> sapDatabaseService;

        public SAPDatabasesController(IService<SAPDatabases> sapDatabaseService)
        {
            this.sapDatabaseService = sapDatabaseService;
        }
        [HttpGet]
        [Route("List")]
        public async Task<ActionResult> List(ListRequest<SAPDatabases> request)
        {
            var response = await this.sapDatabaseService.GetAllAsync(request);
            return Ok(response);
        }
        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult> Create([FromBody] SAPDatabases request)
        {
            return Ok(await this.sapDatabaseService.AddAsync(request));
        }
        [HttpPost]
        [Route("Update")]
        public async Task<ActionResult> Update([FromBody] SAPDatabases request)
        {
            await this.sapDatabaseService.UpdateAsync(request);

            return Ok(await this.sapDatabaseService.GetByIdAsync((int)request.Id));

        }
        [HttpGet]
        [Route("Retrieve")]
        public async Task<ActionResult> Retrieve([FromQuery] SAPDatabases request)
        {

            return Ok(await this.sapDatabaseService.GetByIdAsync((int)request.Id));

        }
        [HttpDelete]
        [Route("Delete")]
        public async Task<ActionResult> Delete([FromBody] SAPDatabases request)
        {
            var response = await this.sapDatabaseService.DeleteAsync(request);
            return Ok(response);

        }
    }
}
