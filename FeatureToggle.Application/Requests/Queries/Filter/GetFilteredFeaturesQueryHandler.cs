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

            // Query for features with existing BusinessFeatureFlag
            var featuresWithFlags = query.Select(bf => new FilteredFeatureDTO
            {
                FeatureFlagId = bf.FeatureFlagId,
                FeatureId = bf.FeatureId,
                FeatureName = bf.Feature.FeatureName
            });

            if (request.IsDisabledFilter.HasValue || (request.IsEnabledFilter.HasValue && request.IsDisabledFilter.HasValue))
            {

                var featuresWithoutFlags = _businessContext.Feature
                .GroupJoin(
                    _businessContext.BusinessFeatureFlag,
                    feature => feature.FeatureId,
                    businessFeatureFlag => businessFeatureFlag.FeatureId,
                    (feature, businessFeatureFlags) => new { Feature = feature, BusinessFeatureFlags = businessFeatureFlags }
                )
                .SelectMany(
                    result => result.BusinessFeatureFlags.DefaultIfEmpty(),
                    (result, businessFeatureFlag) => new
                    {
                        Feature = result.Feature,
                        BusinessFeatureFlag = businessFeatureFlag
                    }
                )
                .Where(result => result.BusinessFeatureFlag == null) // Only features without flags
                .Select(result => new FilteredFeatureDTO
                {
                    FeatureFlagId = 0, // No FeatureFlagId since it doesn't exist
                    FeatureId = result.Feature.FeatureId,
                    FeatureName = result.Feature.FeatureName
                });
                // Combine both queries
                var combinedQuery = featuresWithFlags.Concat(featuresWithoutFlags);
                return await combinedQuery.ToListAsync(cancellationToken);
            }
            

            

            // Execute and return the result
            var result = await featuresWithFlags.ToListAsync(cancellationToken);
            return result;
            
        }

    }
}
