using FeatureToggle.Application.DTOs;
using FeatureToggle.Application.Requests.Queries.Log;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FeatureToggle.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController(IMediator mediator) : ControllerBase
    {
        [HttpGet]

        public async Task<PaginatedLogListDTO> GetLogs(
            [FromQuery] int page, 
            [FromQuery] int pageSize
            )
        {
            GetLogQuery query = new GetLogQuery()
            {
                Page = page,
                PageSize = pageSize
            };
            return await mediator.Send(query);
        }
    }
}
