using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace FeatureToggle.Application.Requests.Commands.UserCommands
{
    public class AddUserCommandValidator : AbstractValidator<AddUserCommand>
    {
        public AddUserCommandValidator() {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Email).NotEmpty()
                                 .EmailAddress()
                                 .Must(x => x.EndsWith("@geekywolf.com"))
                                 .WithMessage("Provided email is not part of GeekyWolf.");
        }
    }
}
