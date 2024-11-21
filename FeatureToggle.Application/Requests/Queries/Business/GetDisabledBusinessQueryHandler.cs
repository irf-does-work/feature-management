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

namespace FeatureToggle.Application.Requests.Queries.Business
{
    public class GetDisabledBusinessQueryHandler(BusinessContext businessContext) : IRequestHandler<GetDisabledBusinessQuery, List<GetBusinessDTO>>
    {
        private readonly BusinessContext _businessContext = businessContext;
        public async Task<List<GetBusinessDTO>> Handle(GetDisabledBusinessQuery request, CancellationToken cancellationToken)
        {
            //var result = _businessContext.BusinessFeatureFlag
            //    .Where(x => x.FeatureId == request.FeatureId && x.IsEnabled == true)
            //    .
            //    .ToList();






            //var result = await _businessContext.Business
            //.GroupJoin(
            //    _businessContext.BusinessFeatureFlag,
            //    business => business.BusinessId,
            //    featureFlag => featureFlag.BusinessId,
            //    (business, featureFlag) => new { Business = business, FeatureFlag = featureFlag })
            //.Where(x => x.FeatureFlag.FeatureId == request.FeatureId && x.FeatureFlag.IsEnabled == true)
            //.Select(x => new GetBusinessDTO
            //{
            //    BusinessId = x.Business.BusinessId,
            //    BusinessName = x.Business.BusinessName
            //})
            //.ToList();

            //return result;





            //var result = _businessContext.Business
            //.GroupJoin(
            //    _businessContext.BusinessFeatureFlag.Where(ff => ff.FeatureId == request.FeatureId),
            //    business => business.BusinessId,
            //    featureFlag => featureFlag.BusinessId,
            //    (business, featureFlags) => new { Business = business, FeatureFlags = featureFlags })
            //.SelectMany(
            //    bf => bf.FeatureFlags.DefaultIfEmpty(), // Perform LEFT JOIN
            //    (bf, featureFlag) => new
            //    {
            //        bf.Business.BusinessId,
            //        bf.Business.BusinessName,
            //        IsEnabled = featureFlag != null && featureFlag.IsEnabled // Handle null featureFlag
            //    })
            //.Where(x => x.IsEnabled == true) // Only include enabled features
            //.Select(x => new GetBusinessDTO
            //{
            //    BusinessId = x.BusinessId,
            //    BusinessName = x.BusinessName
            //})
            //.ToList(); // Use async method for better performance in EF Core

            //return result;



            var result = await _businessContext.Business
            .Join(
                _businessContext.BusinessFeatureFlag.Where(ff => ff.FeatureId == request.FeatureId && ff.IsEnabled == true),
                business => business.BusinessId,
                featureFlag => featureFlag.BusinessId,
                (business, featureFlag) => new GetBusinessDTO
                {
                    BusinessId = business.BusinessId,
                    BusinessName = business.BusinessName
                })
            .ToListAsync();

            return result;


        }
    }
}
