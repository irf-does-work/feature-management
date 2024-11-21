using FeatureToggle.Application.Requests.Commands.FeatureCommands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FeatureToggle.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessFeatureFlagController(IMediator mediator) : ControllerBase
    {
        [HttpPost("release/enable")]
        public async Task<int> EnableRelease(EnableReleaseCommand command)
        {
           return await mediator.Send(command);
        }

        [HttpPost("release/disable")]
        public async Task<int> DisableRelease(DisableReleaseCommand command)
        {
            return await mediator.Send(command);
        }

        [HttpPost("feature/enable")]
        public async Task<int> EnableFeature(EnableFeatureCommand command)
        {
            return await mediator.Send(command);
        }

        [HttpPost("feature/disable")]
        public async Task<int> DisableFeature(DisableFeatureCommand command) 
        {
            return await mediator.Send(command);
        }

        [HttpPost("feature/update")]
        public async Task<int> UpdateFeature(UpdateToggleCommand command) 
        {
            return await mediator.Send(command);
        }
    }

}
