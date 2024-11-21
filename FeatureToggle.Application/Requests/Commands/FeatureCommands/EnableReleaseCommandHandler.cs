using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using FeatureToggle.Domain.Entity.BusinessSchema;
using FeatureToggle.Infrastructure.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FeatureToggle.Application.Requests.Commands.FeatureCommands
{
    public class EnableReleaseCommandHandler(BusinessContext businessContext) : IRequestHandler<EnableReleaseCommand, int>
    {
        public async Task<int> Handle(EnableReleaseCommand request, CancellationToken cancellationToken)
        {
            BusinessFeatureFlag selectBusiness = await businessContext.BusinessFeatureFlag.FirstOrDefaultAsync(x => x.FeatureId == request.FeatureId, cancellationToken);

            if (selectBusiness != null) {

                if (selectBusiness.IsEnabled == false)
                {
                    selectBusiness.UpdateIsenabled(true);
                    businessContext.BusinessFeatureFlag.Update(selectBusiness);

                }
                return await businessContext.SaveChangesAsync(cancellationToken);

            }
            else
            {
                Feature requiredFeature = await businessContext.Feature.FirstOrDefaultAsync(x => x.FeatureId == request.FeatureId, cancellationToken: cancellationToken);

                BusinessFeatureFlag newBusinessFlag = new BusinessFeatureFlag(requiredFeature);
                
                businessContext.AddAsync(newBusinessFlag, cancellationToken);

                return await businessContext.SaveChangesAsync(cancellationToken);
            }

           
        }
    }
}
