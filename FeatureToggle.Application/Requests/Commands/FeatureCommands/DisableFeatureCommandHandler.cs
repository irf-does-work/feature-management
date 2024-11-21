using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeatureToggle.Domain.Entity.BusinessSchema;
using FeatureToggle.Infrastructure.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FeatureToggle.Application.Requests.Commands.FeatureCommands
{
    public class DisableFeatureCommandHandler(BusinessContext businessContext) : IRequestHandler<DisableFeatureCommand, int>
    {
        public async Task<int> Handle(DisableFeatureCommand request, CancellationToken cancellationToken)
        {

            Feature requiredFeature = await businessContext.Feature.FirstOrDefaultAsync(x => x.FeatureId == request.FeatureId);

            if (requiredFeature.FeatureTypeId == 2)
            {
                BusinessFeatureFlag selectedBusiness = await businessContext.BusinessFeatureFlag.FirstOrDefaultAsync(x => x.FeatureId == request.FeatureId && x.BusinessId == request.BusinessId, cancellationToken);

                if (selectedBusiness != null)
                {

                    selectedBusiness.UpdateIsenabled(false);

                    businessContext.BusinessFeatureFlag.Update(selectedBusiness);

                    return await businessContext.SaveChangesAsync(cancellationToken);

                }
                

            }
            return -1;


        }

    }
}
