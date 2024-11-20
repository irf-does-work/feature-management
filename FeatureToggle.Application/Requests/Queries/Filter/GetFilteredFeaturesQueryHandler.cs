using FeatureToggle.Application.DTOs;
using FeatureToggle.Infrastructure.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
                    query = query.Where(bf => bf.IsEnabled == !request.IsDisabledFilter.Value);

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

            // Features in BusinessFeatureFlag
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
                .Where(result => result.BusinessFeatureFlag == null) 
                .Select(result => new FilteredFeatureDTO
                {
                    FeatureFlagId = 0, 
                    FeatureId = result.Feature.FeatureId,
                    FeatureName = result.Feature.FeatureName
                });

                var combinedQuery = featuresWithFlags.Concat(featuresWithoutFlags)
                                    .GroupBy(f =>f.FeatureId)  //
                                    .Select(x => x.First());  //
                return await combinedQuery.ToListAsync(cancellationToken);
            }
                
            var result = await featuresWithFlags
                                    .GroupBy(f => f.FeatureId)  //
                                    .Select(x => x.First())  //
                                    .ToListAsync(cancellationToken);
            return result;
            
        }

    }
}
