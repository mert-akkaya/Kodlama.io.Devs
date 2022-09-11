using Application.Features.Auths.Commands.Login;
using Application.Features.Auths.Commands.Register;
using Application.Features.Auths.Dtos;
using Core.Security.JWT;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand registerCommand)
        {
            AccessToken result = await Mediator.Send(registerCommand);
            return Created("", result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand loginCommand)
        {
            LoginedDto result = await Mediator.Send(loginCommand);
            if (result == null)
            {
                return BadRequest("error");
            }
            return Ok(result);
        }
    }
}
