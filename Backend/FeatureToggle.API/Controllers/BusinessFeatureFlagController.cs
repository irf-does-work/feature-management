using FeatureToggle.API.Identity;
using FeatureToggle.Application.Requests.Commands.FeatureCommands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FeatureToggle.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessFeatureFlagController(IMediator mediator) : ControllerBase
    {
        [HttpPost("toggle/enable")]
        public async Task<int> EnableFeature(UpdateToggleCommand command, CancellationToken cancellationToken = default) 
        {
            command.EnabledOrDisabled = true;
            return await mediator.Send(command, cancellationToken);
        }

        [Authorize(Policy = IdentityData.AdminUserPolicyName)]
        [HttpPost("toggle/disable")]
        public async Task<int> DisableFeature(UpdateToggleCommand command, CancellationToken cancellationToken = default)
        {
            command.EnabledOrDisabled = false;
            return await mediator.Send(command, cancellationToken);
        }
    }

}
