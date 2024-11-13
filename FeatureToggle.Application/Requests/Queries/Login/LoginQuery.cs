using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace FeatureToggle.Application.Requests.Queries.Login
{
    public class LoginQuery : IRequest<int>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
