using FeatureToggle.Application.DTOs;
using MediatR;

namespace FeatureToggle.Application.Requests.Commands.UserCommands
{
    public class AddUserCommand : IRequest<AddUserResponseDTO>
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string Name { get; set; }
    }
}
