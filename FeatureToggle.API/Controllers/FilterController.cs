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
            [FromQuery] bool? featureToggleType
           )
        {
            // Create the request object with query parameters
            var query = new GetFilteredFeaturesQuery
            {
                IsEnabledFilter = isEnabled,
                IsDisabledFilter = isDisabled,
                ReleaseToggleFilter = releaseToggleType,
                FeatureToggleFilter = featureToggleType
            };

            // Execute the query handler and get the filtered result
            return await mediator.Send(query);

            // Return the result (OK status with data)
           // return Ok(filteredFeatures);
        }

    }
}
