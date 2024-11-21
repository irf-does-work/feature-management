using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeatureToggle.Domain.Entity.FeatureManagementSchema;
using MediatR;

namespace FeatureToggle.Application.Requests.Commands.LogCommands
{
    public class AddLogCommand : IRequest<int>
    {
        public string UserId {  get; set; }
        public int? BusinessId {  get; set; } 
        public int FeatureId { get; set; }
        public Actions action { get; set; }
    }
}
