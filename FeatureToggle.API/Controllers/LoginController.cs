using FeatureToggle.Application.DTOs;
using FeatureToggle.Application.Requests.Queries.Login;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FeatureToggle.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly IMediator _mediator;

        public LoginController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<LoginResponseDTO> SignIn(GetAuthTokenQuery command)
        {
            return await _mediator.Send(command);
        }
    }
}
