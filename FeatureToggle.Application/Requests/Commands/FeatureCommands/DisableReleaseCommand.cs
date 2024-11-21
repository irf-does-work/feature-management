using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace FeatureToggle.Application.Requests.Commands.FeatureCommands
{
    public class DisableReleaseCommand : IRequest<int>
    {
        public int FeatureId { get; set; }
    }
}
