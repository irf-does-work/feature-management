using FeatureToggle.Application.Requests.Commands.LogCommands;
using FeatureToggle.Domain.Entity.BusinessSchema;
using FeatureToggle.Domain.Entity.Enum;
using FeatureToggle.Domain.Entity.FeatureManagementSchema;
using FeatureToggle.Infrastructure.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FeatureToggle.Application.Requests.Commands.FeatureCommands
{
    public class UpdateToggleCommandHandler(BusinessContext businessContext, FeatureManagementContext featureManagementContext, IMediator mediator) : IRequestHandler<UpdateToggleCommand, int>
    {
        public async Task<int> Handle(UpdateToggleCommand request, CancellationToken cancellationToken)
        {
            Feature feature = await businessContext.Feature.FirstAsync(x => x.FeatureId == request.FeatureId, cancellationToken);
            User user = await featureManagementContext.Users.FirstAsync(x => x.Id == request.UserId, cancellationToken);


            if (feature.FeatureTypeId == 1) //checking if release toggle
            {
                BusinessFeatureFlag? selectBusiness = await businessContext.BusinessFeatureFlag.FirstOrDefaultAsync(x => x.FeatureId == request.FeatureId, cancellationToken);

                if (request.EnabledOrDisabled) //Enable release toggle
                {
                    if (selectBusiness is not null)
                    {

                        selectBusiness.UpdateIsenabled(true);
                        businessContext.BusinessFeatureFlag.Update(selectBusiness);


                        AddLogCommand addLog = new()
                        {
                            FeatureId = request.FeatureId,
                            FeatureName = feature.FeatureName,
                            BusinessId = null,
                            BusinessName = null,
                            UserId = request.UserId,
                            UserName = user.UserName,
                            action = Actions.Enabled
                        };

                        await mediator.Send(addLog, cancellationToken);
                        
                        return await businessContext.SaveChangesAsync(cancellationToken);

                    }
                    else
                    {
                        BusinessFeatureFlag newBusinessFlag = new(feature);

                        await businessContext.AddAsync(newBusinessFlag, cancellationToken);

                        AddLogCommand addLog = new()
                        {
                            FeatureId = request.FeatureId,
                            FeatureName = feature.FeatureName,
                            BusinessId = null,
                            BusinessName = null,
                            UserId = request.UserId,
                            UserName = user.UserName,
                            action = Actions.Enabled
                        };

                        await mediator.Send(addLog, cancellationToken);

                        return await businessContext.SaveChangesAsync(cancellationToken);
                    }
                }


                else //Disable release toggle
                {
                    selectBusiness!.UpdateIsenabled(false);

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

            }

            else // if feature toggle
            {
                //To get business Name for feature toggle
                Business business = await businessContext.Business.FirstAsync(x => x.BusinessId == request.BusinessId, cancellationToken);

                BusinessFeatureFlag? selectedBusiness = await businessContext.BusinessFeatureFlag.FirstOrDefaultAsync(x => x.FeatureId == request.FeatureId
                                                                                                                        && x.BusinessId == request.BusinessId,
                           
                                                                                                                        cancellationToken);

                if (request.EnabledOrDisabled)
                {
                    //Enable feature toggle
                    if (selectedBusiness is not null)
                    {

                        selectedBusiness.UpdateIsenabled(true);
                        businessContext.BusinessFeatureFlag.Update(selectedBusiness);

                        AddLogCommand addLog = new()
                        {
                            FeatureId = request.FeatureId,
                            FeatureName = feature.FeatureName,
                            BusinessId = request.BusinessId,
                            BusinessName = business.BusinessName,
                            UserId = request.UserId,
                            UserName = user.UserName,
                            action = Actions.Enabled
                        };

                        await mediator.Send(addLog, cancellationToken);

                        return await businessContext.SaveChangesAsync(cancellationToken);


                    }
                    else
                    {

                        BusinessFeatureFlag newBusinessFlag = new(feature, business!);

                        await businessContext.AddAsync(newBusinessFlag, cancellationToken);

                        AddLogCommand addLog = new()
                        {
                            FeatureId = request.FeatureId,
                            FeatureName = feature.FeatureName,
                            BusinessId = request.BusinessId,
                            BusinessName = business.BusinessName,
                            UserId = request.UserId,
                            UserName = user.UserName,
                            action = Actions.Enabled
                        };

                        await mediator.Send(addLog, cancellationToken);

                        return await businessContext.SaveChangesAsync(cancellationToken);

                    }
                }

                else //Disable feature toggle
                {
                 
                    selectedBusiness!.UpdateIsenabled(false);

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

        }

    }
}
