using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeatureToggle.Domain.Entity.User_Schema;
using FeatureToggle.Domain.Validators;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FeatureToggle.Application.Requests.Commands.UserCommands
{
    public class AddUserCommandHandler(UserManager<User> userManager, IValidator<AddUserCommand> userValidator) : IRequestHandler<AddUserCommand, AddUserResponse>
    {
        public async Task<AddUserResponse> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            
            User newUser = new User(request.Email, request.Name);  //use '!' ??

            var validationResult = await userValidator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return new AddUserResponse
                {
                    Success = false,
                    Message = "Failed to create user",
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }


            var result = await userManager.CreateAsync(newUser, request.Password);
            
            
            if (result.Succeeded)
            {
                
                return new AddUserResponse
                {
                    Success = true,
                    Message = "User created successfully",
                    
                };
            }
            else
            {
              
                return new AddUserResponse
                {
                    Success = false,
                    Message = "Failed to create user",
                    Errors = result.Errors.Select(e => e.Description).ToList()
                };
            }
        }
    }

    public class AddUserResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }
}
