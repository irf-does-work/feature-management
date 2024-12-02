using FeatureToggle.Application.DTOs;
using FeatureToggle.Application.Requests.Queries.Filter;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FeatureToggle.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FilterController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<PaginatedFeatureListDTO> GetFilteredFeatures([FromQuery] GetFilteredFeaturesQuery query)
        { 
            return await mediator.Send(query);
        }

    }
}
