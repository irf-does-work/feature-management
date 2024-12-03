﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeatureToggle.Application.Requests.Commands.LogCommands;
using FeatureToggle.Domain.Entity.BusinessSchema;
using FeatureToggle.Domain.Entity.Enum;
using FeatureToggle.Domain.Entity.FeatureManagementSchema;
using FeatureToggle.Infrastructure.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FeatureToggle.Application.Requests.Commands.FeatureCommands
{
    public class DisableToggleCommandHandler(BusinessContext businessContext, FeatureManagementContext featureManagementContext, IMediator mediator) : IRequestHandler<DisableToggleCommand, int>
    {
        public async Task<int> Handle(DisableToggleCommand request, CancellationToken cancellationToken)
        {
            Feature feature = await businessContext.Feature.FirstAsync(x => x.FeatureId == request.FeatureId, cancellationToken);
            User user = await featureManagementContext.Users.FirstAsync(x => x.Id == request.UserId, cancellationToken);

            if (request.BusinessId is null) //checking if release toggle
            {
                BusinessFeatureFlag selectBusiness = await businessContext.BusinessFeatureFlag.FirstAsync(x => x.FeatureId == request.FeatureId, cancellationToken);

            
                //Disable release toggle
                if (selectBusiness is not null)
                {

                    selectBusiness.UpdateIsenabled(false);

                    businessContext.BusinessFeatureFlag.Update(selectBusiness);


                    AddLogCommand addLog = new AddLogCommand()
                    {
                        FeatureId = request.FeatureId,
                        FeatureName = feature.FeatureName,
                        BusinessId = null,
                        BusinessName = null,
                        UserId = request.UserId,
                        UserName = user.UserName,
                        action = Actions.Disabled
                    };

                    await mediator.Send(addLog, cancellationToken);

                    return await businessContext.SaveChangesAsync(cancellationToken);
                }

                return -1;

                

            }


            else // if feature toggle
            {
                //To get business Name for feature toggle
                Business business = await businessContext.Business.FirstAsync(x => x.BusinessId == request.BusinessId, cancellationToken);

                BusinessFeatureFlag? selectedBusiness = await businessContext.BusinessFeatureFlag.FirstOrDefaultAsync(x => x.FeatureId == request.FeatureId && x.BusinessId == request.BusinessId, cancellationToken);



            
                //Disable feature toggle
                
                Feature requiredFeature = await businessContext.Feature.FirstAsync(x => x.FeatureId == request.FeatureId);

                if (requiredFeature.FeatureTypeId == 2)
                {

                    if (selectedBusiness is not null)
                    {

                        selectedBusiness.UpdateIsenabled(false);

                        businessContext.BusinessFeatureFlag.Update(selectedBusiness);

                        AddLogCommand addLog = new()
                        {
                            FeatureId = request.FeatureId,
                            FeatureName = feature.FeatureName,
                            BusinessId = request.BusinessId,
                            BusinessName = business.BusinessName,
                            UserId = request.UserId,
                            UserName = user.UserName,
                            action = Actions.Disabled
                        };

                        await mediator.Send(addLog, cancellationToken);


                        return await businessContext.SaveChangesAsync(cancellationToken);

                    }


                }
                return -1;

                
            }

        }
    }
}