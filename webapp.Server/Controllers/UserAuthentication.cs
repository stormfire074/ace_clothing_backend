using B1SLayer;
using Microsoft.AspNetCore.Mvc;
using webapp.Application;
using webapp.Domain;

namespace SBODeskReact.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAuthentication : ControllerBase
    {
        ISLConnectionFactory iSLConnectionFactory;
        public UserAuthentication(ISLConnectionFactory iSLConnectionFactory)
        {
            this.iSLConnectionFactory = iSLConnectionFactory;

        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<SLLoginResponse>> Login([FromBody] Login login)
        {
            var response = await iSLConnectionFactory.Login(login);
            var connection = response.SLConnection;
            var Response = response.Response;
            return Ok(Response);
        }
        [HttpPost]
        [Route("Logout")]
        public async Task<ActionResult<bool>> Logout()
        {
            var response = await iSLConnectionFactory.Logout();

            return Ok(true);
        }
    }
}
