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

        public async Task<List<LogDTO>> GetLogs()
        {
            GetLogQuery query = new GetLogQuery();
            return await mediator.Send(query);
        }
    }
}
