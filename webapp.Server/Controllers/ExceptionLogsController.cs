using Microsoft.AspNetCore.Mvc;
using webapp.Application;
using webapp.Domain;

namespace webapp.Server
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
        public async Task<ActionResult> List(ListRequest<ExceptionLogs> request)
        {
            var response = await this.exceptionLogsService.GetAllAsync(request);
            return Ok(response);
        }

    }
}
