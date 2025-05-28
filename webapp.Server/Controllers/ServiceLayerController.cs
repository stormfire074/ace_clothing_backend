using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Nodes;
using webapp.Application;

namespace SBODeskReact.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceLayerController : ControllerBase
    {
        private readonly IServiceLayerService _serviceLayerService;

        public ServiceLayerController(IServiceLayerService serviceLayerService)
        {
            _serviceLayerService = serviceLayerService;
        }

        // POST: api/ServiceLayer
        [HttpPost("Add")]
        public async Task<IActionResult> AddEntity([FromBody] object entity)
        {
            if (entity == null)
                return BadRequest("Entity data is required.");

            try
            {
                var result = await _serviceLayerService.AddAsync(entity.ToString());
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET: api/ServiceLayer/{document}/{key}
        [HttpGet("Retrieve/{document}/{key}")]
        public async Task<IActionResult> GetEntityById(string document, string key)
        {
            var entity = new JsonObject
            {
                ["Document"] = document,
                ["Key"] = key
            };



            try
            {
                var result = await _serviceLayerService.GetByIdAsync(entity);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET: api/ServiceLayer/{document}
        [HttpGet("List/{document}")]
        public async Task<IActionResult> GetAllEntities(string document, [FromQuery] int pageSize = 10, [FromQuery] int pageNumber = 1)
        {
            var entity = new JsonObject
            {
                ["Document"] = document
            };

            try
            {
                var result = await _serviceLayerService.GetAllAsync(entity, pageSize, pageNumber);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        // PUT: api/ServiceLayer/{document}/{key}
        [HttpPut("Edit/{document}/{key}")]
        public async Task<IActionResult> EditEntity(string document, string key, [FromBody] JsonObject entity)
        {
            entity["Document"] = document;
            entity["Key"] = key;

            try
            {
                var result = await _serviceLayerService.UpdateAsync(entity);
                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE: api/ServiceLayer/{document}/{key}
        [HttpDelete("Delete/{document}/{key}")]
        public async Task<IActionResult> DeleteEntity(string document, string key)
        {
            var entity = new JsonObject
            {
                ["Document"] = document,
                ["Key"] = key
            };


            var response = await _serviceLayerService.DeleteAsync(entity);
            return Ok(response);

        }
    }
}
