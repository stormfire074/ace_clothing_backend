using Domain.Entities;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using webapp.Application;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
    }
}
