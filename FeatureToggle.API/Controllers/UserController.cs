using FeatureToggle.Application.DTOs;
using FeatureToggle.Application.Requests.Commands.UserCommands;
using FeatureToggle.Application.Requests.Queries.Login;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FeatureToggle.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IMediator mediator, CancellationToken cancellationToken) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        public async Task<AddUserResponseDTO> AddUser(AddUserCommand command)
        {
            return await _mediator.Send(command,cancellationToken);
        }
        
        
        
    }
}
