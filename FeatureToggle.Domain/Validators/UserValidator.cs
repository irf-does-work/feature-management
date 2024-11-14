using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeatureToggle.Domain.Entity.User_Schema;
using FluentValidation;

namespace FeatureToggle.Domain.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.Email).NotEmpty()
                                 .EmailAddress()
                                 .Must(x => x.EndsWith("@geekywolf.com"))
                                 .WithMessage("Provided email is not part of GeekyWolf."); 
            
        }
    }
}
