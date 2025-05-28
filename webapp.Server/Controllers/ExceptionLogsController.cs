using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using webapp.Application;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExceptionLogsController : ControllerBase
    {

        private readonly IService<ExceptionLogs> exceptionLogsService;

        public ExceptionLogsController(IService<ExceptionLogs> exceptionLogsService)
        {
            this.exceptionLogsService = exceptionLogsService;
        }
        [HttpGet]
        [Route("List")]
        public async Task<ActionResult> List(ListRequest request)
        {
            var response = await this.exceptionLogsService.GetAllAsync(request);
            return Ok(response);
        }

    }
}
