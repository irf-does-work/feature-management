using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FeatureToggle.Domain.ConfigurationModels;
using FeatureToggle.Domain.Entity.User_Schema;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace FeatureToggle.Application.Requests.Queries.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly UserManager<User> _userManager;  
        private readonly IOptionsMonitor<Authentication> _optionsMonitor;

        public LoginCommandHandler(UserManager<User> userManager, IOptionsMonitor<Authentication> optionsMonitor)
        {
            _userManager = userManager;
            _optionsMonitor = optionsMonitor;
        }
        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, request.Password))
            {
                var secretKey = _optionsMonitor.CurrentValue.JWTSecret;
                var signInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("UserID",user.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(signInKey,SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);

                return new LoginResponse { Token = token };
            }
            else
            {
                return new LoginResponse { ErrorMessage = "Incorrect Username or Password" };
            }
        }
    }

    public class LoginResponse
    {
        public string? Token { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
