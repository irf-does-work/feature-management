using FeatureToggle.Application.DTOs;
using FeatureToggle.Application.Requests.Queries.Filter;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FeatureToggle.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilterController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<FilteredFeatureDTO>>> GetFilteredFeatures(
            [FromQuery] bool? isEnabled,
            [FromQuery] bool? isDisabled,
            [FromQuery] bool? releaseToggleType,
            [FromQuery] bool? featureToggleType,
            [FromQuery] string? searchQuery
           )
        {
            var query = new GetFilteredFeaturesQuery
            {
                IsEnabledFilter = isEnabled,
                IsDisabledFilter = isDisabled,
                ReleaseToggleFilter = releaseToggleType,
                FeatureToggleFilter = featureToggleType,
                SearchQuery = searchQuery
            };

            return await mediator.Send(query);
        }

    }
}
