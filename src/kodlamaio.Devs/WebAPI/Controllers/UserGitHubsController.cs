using Application.Features.ProgrammingTechnologies.Commands.CreateProgrammingTechnology;
using Application.Features.ProgrammingTechnologies.Commands.DeleteProgrammingTechnology;
using Application.Features.ProgrammingTechnologies.Commands.UpdateProgrammingTechnology;
using Application.Features.ProgrammingTechnologies.Dtos;
using Application.Features.ProgrammingTechnologies.Models;
using Application.Features.ProgrammingTechnologies.Queries.GetListProgrammingTechnology.GetListProgrammingTechnologyByDynamic;
using Application.Features.ProgrammingTechnologies.Queries.GetListProgrammingTechnology;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Features.UserGitHubs.Queries.GetListUserGitHub;
using Application.Features.UserGitHubs.Models;
using Application.Features.Users.Commands.CreateUserGitHub;
using Application.Features.UserGitHubs.Dtos;
using Application.Features.UserGitHubs.Commands.UpdateUserGitHub;
using Application.Features.UserGitHubs.Commands.DeleteUserGitHub;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserGitHubsController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListUserGitHubQuery getListUserGitHubQuery = new() { PageRequest = pageRequest };
            UserGitHubListModel result = await Mediator.Send(getListUserGitHubQuery);
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateUserGitHubCommand createUserGitHubCommand)
        {
            CreatedUserGitHubDto result = await Mediator.Send(createUserGitHubCommand);
            return Created("", result);
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] UpdateUserGitHubCommand updateUserGitHubCommand)
        {
            UpdatedUserGitHubDto result = await Mediator.Send(updateUserGitHubCommand);
            return Created("", result);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromBody] DeleteUserGitHubCommand deleteUserGitHubCommand)
        {
            DeletedUserGitHubDto result = await Mediator.Send(deleteUserGitHubCommand);
            return Created("", result);
        }
    }
}
