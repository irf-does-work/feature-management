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
                .Include(bf => bf.Feature)  // Include Feature details
                .ThenInclude(f => f.FeatureType)  // Include the FeatureType navigation for the feature
                .AsQueryable();

            // Filter by enabled/disabled state
            if (request.IsEnabled.HasValue)
            {
                query = query.Where(bf => bf.IsEnabled == request.IsEnabled.Value);
            }

            if (request.IsDisabled.HasValue)
            {
                query = query.Where(bf => bf.IsEnabled == !request.IsDisabled.Value || bf.IsEnabled==null);
                
            }

            // Filter by feature/release toggle type
            if (request.FeatureToggleType.HasValue)
            {
                query = query.Where(bf => bf.Feature.FeatureTypeId == request.FeatureToggleType.Value);
            }

            if (request.ReleaseToggleType.HasValue)
            {
                query = query.Where(bf => bf.Feature.FeatureTypeId == request.ReleaseToggleType.Value);
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
