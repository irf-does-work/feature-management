using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeatureToggle.Application.DTOs;
using FeatureToggle.Domain.Entity.BusinessSchema;
using FeatureToggle.Infrastructure.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace FeatureToggle.Application.Requests.Queries.Filter
{
    public class GetFilteredFeaturesQueryHandler(BusinessContext businessContext) : IRequestHandler<GetFilteredFeaturesQuery, List<FilteredFeatureDTO>>
    {
        private readonly BusinessContext _businessContext = businessContext;

        public async Task<List<FilteredFeatureDTO>> Handle(GetFilteredFeaturesQuery request, CancellationToken cancellationToken)
        {
            var query = _businessContext.BusinessFeatureFlag
                .Include(bf => bf.Feature)  
                .ThenInclude(f => f.FeatureType)  
                .AsQueryable();

            // Filter by enabled/disabled state
            if (request.IsEnabledFilter.HasValue && request.IsDisabledFilter.HasValue)
            {
                // Both Enabled and Disabled
            }
            else
            {
                if (request.IsEnabledFilter.HasValue)
                {
                    query = query.Where(bf => bf.IsEnabled == request.IsEnabledFilter.Value);
                }

                 if (request.IsDisabledFilter.HasValue)
                {
                    query = query.Where(bf => bf.IsEnabled == !request.IsDisabledFilter.Value /*|| bf.IsEnabled==null*/);

                }
            }
            // Filter by feature/release toggle type
            if (request.ReleaseToggleFilter.HasValue && request.FeatureToggleFilter.HasValue)
            {
                // Both Release and Feature
            }
            else
            {
                if (request.ReleaseToggleFilter.HasValue)
                {
                    query = query.Where(bf => bf.Feature.FeatureTypeId == 1);
                }

                if (request.FeatureToggleFilter.HasValue)
                {
                    query = query.Where(bf => bf.Feature.FeatureTypeId == 2);
                }
            }

            // Project the result into a list of FilteredFeatureDTO
            var result = await query
                .Select(bf => new FilteredFeatureDTO
                {
                    FeatureFlagId = bf.FeatureFlagId,
                    FeatureId = bf.FeatureId,
                    FeatureName = bf.Feature.FeatureName
                })
                .ToListAsync(cancellationToken);

            return result;
        }

    }
}
