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
    public class GetLogQueryHandler(FeatureManagementContext featureManagementContext, BusinessContext businessContext, UserManager<User> userManager) : IRequestHandler<GetLogQuery, PaginatedLogListDTO>
    {


        public async Task<PaginatedLogListDTO> Handle(GetLogQuery request, CancellationToken cancellationToken)
        {

            var query =  featureManagementContext.Logs
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
                .OrderByDescending(x => x.Time);

            //return query;


            var totalCount = query.Count();
            var page = request.Page;
            var pageSize = request.PageSize;
            var totalPages = (totalCount / pageSize) +1;
           

            var queryList = await query.Skip((page) * pageSize).Take(pageSize).ToListAsync(cancellationToken);


            var result = new PaginatedLogListDTO
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                CurrentPage = request.Page,
                PageSize = request.PageSize,
                Logs = queryList
            };

            return result;

        }
}
}


