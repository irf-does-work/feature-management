using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace FeatureToggle.Application.Requests.Commands.UserCommands
{
    public class AddUserCommand : IRequest<AddUserResponse>
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Name { get; set; }
    }
}
