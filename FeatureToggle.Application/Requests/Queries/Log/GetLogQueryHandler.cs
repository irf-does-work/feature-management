using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeatureToggle.Application.DTOs;
using FeatureToggle.Domain.Entity.FeatureManagementSchema;
using FeatureToggle.Infrastructure.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FeatureToggle.Application.Requests.Queries.Log
{
    public class GetLogQueryHandler(FeatureManagementContext featureManagementContext, BusinessContext businessContext, UserManager<User> userManager) : IRequestHandler<GetLogQuery, List<LogDTO>>
    {


        public async Task<List<LogDTO>> Handle(GetLogQuery request, CancellationToken cancellationToken)
        {

            var query = await featureManagementContext.Logs
                .Select(x => new LogDTO
                {
                    LogId = x.Id,
                    UserId = x.UserId,
                    UserName = x.UserName,
                    FeatureId = x.FeatureId,
                    FeatureName = x.FeatureName,
                    BusinessId = x.BusinessId,
                    BusinessName = x.BusinessName,
                    Time = x.Time,
                    Action = x.Action,

                })
                .OrderByDescending(x => x.Time)
            .ToListAsync(cancellationToken);

            return query;

        }
}
}


