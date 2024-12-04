using FeatureToggle.Application.DTOs;
using FeatureToggle.Infrastructure.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FeatureToggle.Application.Requests.Queries.Business
{
    public class GetBusinessQueryHandler(BusinessContext businessContext) : IRequestHandler<GetBusinessQuery, List<GetBusinessDTO>>
    {
        public async Task<List<GetBusinessDTO>> Handle(GetBusinessQuery request, CancellationToken cancellationToken)
        {
            if (request.FeatureStatus)
            {
                // to Enable

                List<GetBusinessDTO> allBusinesses = await businessContext.Business
                    .Where(b =>
                        !b.BusinessFeatures!.Any(bf => bf.FeatureId == request.FeatureId)   // not in bff
                        || b.BusinessFeatures!.Any(bf => bf.FeatureId == request.FeatureId && bf.IsEnabled == false)   // disabled in bff
                    )
                    .Select(b => new GetBusinessDTO
                    {
                        BusinessId = b.BusinessId,
                        BusinessName = b.BusinessName
                    })
                    .ToListAsync(cancellationToken);

                return allBusinesses;

            }

            else
            {
                //  to Disable 
                List<GetBusinessDTO> result = await businessContext.Business
                    .Join(
                        businessContext.BusinessFeatureFlag.Where(ff => ff.FeatureId == request.FeatureId && ff.IsEnabled == true),
                        business => business.BusinessId,
                        featureFlag => featureFlag.BusinessId,
                        (business, featureFlag) => new GetBusinessDTO
                        {
                            BusinessId = business.BusinessId,
                            BusinessName = business.BusinessName
                        })
                    .ToListAsync(cancellationToken);

                return result;
            }

            
        }
    }
}
