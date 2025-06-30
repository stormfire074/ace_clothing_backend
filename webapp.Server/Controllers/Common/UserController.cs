using Domain.Entities;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webapp.Application;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles="admin")]
    public class UserController : ControllerBase
    {
        private readonly UserService userService;

        public UserController(UserService userService)
        {
            this.userService = userService;
        }
        [HttpPost]
        [Route("List")]
        public async Task<ActionResult> List(ListRequest request)
        {
            var response = await this.userService.ListUsers(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<ActionResult> Add(User_AddEdit request)
        {
            var response = await this.userService.AddUser(request);
            return Ok(response);
        }
        [HttpPost]
        [Route("Update")]
        public async Task<ActionResult> Update(User_AddEdit request)
        {
            var response = await this.userService.UpdateUser(request);
            return Ok(response);
        }

        [HttpDelete]
        [Route("Delete/{Id}")]
        public async Task<ActionResult> Delete(Guid Id)
        {
            var response = await this.userService.DeleteAsync(Id);
            return Ok(response);
        }

        [HttpGet]
        [Route("Retrieve/{Id}")]
        public async Task<ActionResult> Retrieve(Guid Id)
        {
            var response = await this.userService.RetrieveUser(Id);
            return Ok(response);
        }
    }
}
