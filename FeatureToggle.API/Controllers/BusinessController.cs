using FeatureToggle.Application.DTOs;
using FeatureToggle.Application.Requests.Queries.Business;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FeatureToggle.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessController(IMediator mediator) : ControllerBase
    {
        [HttpGet("Enable")]

        public async Task<List<GetBusinessDTO>> GetEnabledFeature([FromQuery] GetEnabledBusinessQuery query)
        {
            CancellationToken cancellationToken = HttpContext.RequestAborted;
            return await mediator.Send(query, cancellationToken);  
        }

        [HttpGet("Disable")]

        public async Task<List<GetBusinessDTO>> GetDisableFeature( [FromQuery] GetDisabledBusinessQuery query)
        {
            CancellationToken cancellationToken = HttpContext.RequestAborted;
            return await mediator.Send(query, cancellationToken); 
        }
    }
}
