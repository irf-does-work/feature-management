using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeatureToggle.Domain.Entity.BusinessSchema;
using FeatureToggle.Infrastructure.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace FeatureToggle.Application.Requests.Commands.FeatureCommands
{
    public class DisableReleaseCommandHandler(BusinessContext businessContext) : IRequestHandler<DisableReleaseCommand, int>
    {
        public async Task<int> Handle(DisableReleaseCommand request, CancellationToken cancellationToken)
        {
            BusinessFeatureFlag selectBusiness = await businessContext.BusinessFeatureFlag.FirstOrDefaultAsync(x=> x.FeatureId == request.FeatureId, cancellationToken);

            if (selectBusiness != null)
            {
                selectBusiness.UpdateIsenabled(false);

                businessContext.BusinessFeatureFlag.Update(selectBusiness);
                return await businessContext.SaveChangesAsync(cancellationToken);
            }

            return -1;

        }
    }
}
