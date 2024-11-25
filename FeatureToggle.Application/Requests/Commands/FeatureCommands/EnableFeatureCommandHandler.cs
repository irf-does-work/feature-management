using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure;
using FeatureToggle.Domain.Entity.BusinessSchema;
using FeatureToggle.Infrastructure.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FeatureToggle.Application.Requests.Commands.FeatureCommands
{
    public class EnableFeatureCommandHandler(BusinessContext businessContext) : IRequestHandler<EnableFeatureCommand, int>
    {
        public async Task<int> Handle(EnableFeatureCommand request, CancellationToken cancellationToken)
        {
            BusinessFeatureFlag selectedBusiness = await businessContext.BusinessFeatureFlag.FirstOrDefaultAsync(x => x.FeatureId == request.FeatureId && x.BusinessId == request.BusinessId, cancellationToken);

            if (selectedBusiness != null)
            {
                if (selectedBusiness.IsEnabled == false)
                {
                    selectedBusiness.UpdateIsenabled(true);
                    businessContext.BusinessFeatureFlag.Update(selectedBusiness);
                }

                return await businessContext.SaveChangesAsync(cancellationToken);
            }
            else 
            {

                Feature requiredFeature = await businessContext.Feature.FirstOrDefaultAsync(x => x.FeatureId == request.FeatureId);

                if (requiredFeature.FeatureTypeId == 2 )
                {
                    Business requiredBusiness = await businessContext.Business.FirstOrDefaultAsync(x => x.BusinessId == request.BusinessId);

                    BusinessFeatureFlag newBusinessFlag = new BusinessFeatureFlag(requiredFeature, requiredBusiness);

                    await businessContext.AddAsync(newBusinessFlag, cancellationToken);

                    return await businessContext.SaveChangesAsync(cancellationToken);
                }

                return -1;

                

            }

            

        }
    }
}
