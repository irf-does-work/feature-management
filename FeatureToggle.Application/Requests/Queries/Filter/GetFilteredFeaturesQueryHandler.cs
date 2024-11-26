using FeatureToggle.Application.DTOs;
using FeatureToggle.Application.Requests.Queries.Filter;
using FeatureToggle.Domain.Entity.BusinessSchema;
using FeatureToggle.Infrastructure.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class GetFilteredFeaturesQueryHandler(BusinessContext businessContext) : IRequestHandler<GetFilteredFeaturesQuery, PaginatedFeatureListDTO>
{
    private readonly BusinessContext _businessContext = businessContext;
    private int pageSize = 12;

    public async Task<PaginatedFeatureListDTO> Handle(GetFilteredFeaturesQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Feature> baseQuery = _businessContext.Feature.Include(f => f.BusinessFeatures);

        // no filters 
        if (!request.FeatureToggleFilter.HasValue && !request.ReleaseToggleFilter.HasValue &&
            !request.IsEnabledFilter.HasValue && !request.IsDisabledFilter.HasValue)
        {
            var allFeatures = await baseQuery.Select(f => new FilteredFeatureDTO
            {
                FeatureFlagId = f.BusinessFeatures != null && f.BusinessFeatures.Any()
                    ? f.BusinessFeatures.FirstOrDefault().FeatureFlagId
                    : 0,
                FeatureId = f.FeatureId,
                FeatureName = f.FeatureName,
                FeatureType = f.FeatureTypeId,
                isEnabled = f.BusinessFeatures != null && f.BusinessFeatures.Any()
                    ? f.BusinessFeatures.FirstOrDefault().IsEnabled
                    : null
            }).ToListAsync(cancellationToken);

            return new PaginatedFeatureListDTO
            {
                FeatureCount = allFeatures.Count,
                TotalPages = (allFeatures.Count + pageSize - 1) / pageSize,
                PageSize = pageSize,
                FeatureList = allFeatures.Skip(request.PageNumber * pageSize).Take(pageSize).ToList()
            };
        }

        // Apply feature toggle filter
        if (request.FeatureToggleFilter.HasValue)
        {
            baseQuery = baseQuery.Where(f => f.FeatureTypeId == 2); // Feature toggles
        }

        // Apply release toggle filter
        if (request.ReleaseToggleFilter.HasValue)
        {
            baseQuery = baseQuery.Where(f => f.FeatureTypeId == 1); // Release toggles

            if (request.IsEnabledFilter.HasValue && request.IsDisabledFilter.HasValue)
            {
                baseQuery = baseQuery.Where(f =>
                    f.BusinessFeatures.Any(bf => bf.IsEnabled) ||   // Enabled toggles
                    f.BusinessFeatures.All(bf => !bf.IsEnabled) ||  // Disabled toggles
                    !f.BusinessFeatures.Any()                      // Toggles without flags
                );
            }
            else if (request.IsEnabledFilter.HasValue)
            {
                baseQuery = baseQuery.Where(f =>
                    f.BusinessFeatures.Any(bf => bf.IsEnabled == true)); // Enabled toggles
            }
            else if (request.IsDisabledFilter.HasValue)
            {
                baseQuery = baseQuery.Where(f =>
                    f.BusinessFeatures.All(bf => !bf.IsEnabled) ||      // Disabled toggles
                    !f.BusinessFeatures.Any());                        // Toggles without flags
            }
        }

        var combinedQuery = baseQuery.Select(f => new FilteredFeatureDTO
        {
            FeatureFlagId = f.BusinessFeatures != null && f.BusinessFeatures.Any()
                ? f.BusinessFeatures.FirstOrDefault().FeatureFlagId
                : 0,
            FeatureId = f.FeatureId,
            FeatureName = f.FeatureName,
            FeatureType = f.FeatureTypeId,
            isEnabled = f.BusinessFeatures != null && f.BusinessFeatures.Any()
                ? f.BusinessFeatures.FirstOrDefault().IsEnabled
                : null
        });

        if (!string.IsNullOrWhiteSpace(request.SearchQuery))
        {
            combinedQuery = combinedQuery.Where(cq => cq.FeatureName.Contains(request.SearchQuery, StringComparison.OrdinalIgnoreCase));
        }

        var featureList = await combinedQuery
            .Skip(request.PageNumber * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        int totalCount = await combinedQuery.CountAsync(cancellationToken);

        return new PaginatedFeatureListDTO
        {
            FeatureCount = totalCount,
            TotalPages = (totalCount + pageSize - 1) / pageSize,
            PageSize = pageSize,
            FeatureList = featureList
        };
    }
}