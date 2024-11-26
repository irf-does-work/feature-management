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
        // Base query
        IQueryable<Feature> query = _businessContext.Feature.Include(f => f.BusinessFeatures);

        // Apply filters
        if (request.IsEnabledFilter.HasValue || request.IsDisabledFilter.HasValue)
        {
            bool isEnabled = request.IsEnabledFilter ?? false;
            query = query.Where(f =>
                f.BusinessFeatures != null &&
                f.BusinessFeatures.Any(bf => bf.IsEnabled == isEnabled));
        }

        if (request.ReleaseToggleFilter.HasValue)
        {
            query = query.Where(f => f.FeatureTypeId == 1);
        }

        if (request.FeatureToggleFilter.HasValue)
        {
            query = query.Where(f => f.FeatureTypeId == 2);
        }

        // Combine features with flags and features without flags
        var combinedQuery = query
            .Select(f => new FilteredFeatureDTO
            {
                FeatureFlagId = f.BusinessFeatures != null && f.BusinessFeatures.Any()
                    ? f.BusinessFeatures.First().FeatureFlagId
                    : 0, // No feature flag
                FeatureId = f.FeatureId,
                FeatureName = f.FeatureName,
                FeatureType = f.FeatureTypeId,
                isEnabled = f.BusinessFeatures != null && f.BusinessFeatures.Any()
                    ? f.BusinessFeatures.First().IsEnabled
                    : null
            });

        // Apply search query
        if (!string.IsNullOrEmpty(request.SearchQuery))
        {
            combinedQuery = combinedQuery.Where(cq => cq.FeatureName.Contains(request.SearchQuery, StringComparison.OrdinalIgnoreCase));
        }

        // Pagination
        var featureList = await combinedQuery
            .Skip(pageSize * request.PageNumber)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PaginatedFeatureListDTO
        {
            FeatureCount = await combinedQuery.CountAsync(cancellationToken),
            TotalPages = ((await combinedQuery.CountAsync(cancellationToken)) + pageSize - 1) / pageSize,
            PageSize = pageSize,
            FeatureList = featureList
        };
    }
}