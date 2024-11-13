using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeatureToggle.Domain.Entity.User_Schema;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FeatureToggle.Application.Requests.Commands.UserCommands
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, AddUserResponse>
    {
        private readonly UserManager<User> _userManager;

        public AddUserCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<AddUserResponse> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            
            User newUser = new User(request.Email,request.Name);

            if (!request.Email.EndsWith("@geekywolf.com"))
            {
                return new AddUserResponse
                {
                    Success = false,
                    Message = "Failed to create user",
                    Errors = new List<string> { "EMAIL IS NOT A GW MAIL!" }
                };
            }

            var result = await _userManager.CreateAsync(newUser, request.Password);
            
            
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
        public string Message { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }
}
