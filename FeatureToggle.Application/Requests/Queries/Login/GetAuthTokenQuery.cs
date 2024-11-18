using FeatureToggle.Application.DTOs;
using MediatR;

namespace FeatureToggle.Application.Requests.Queries.Login
{
    public class GetAuthTokenQuery : IRequest<LoginResponseDTO>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
