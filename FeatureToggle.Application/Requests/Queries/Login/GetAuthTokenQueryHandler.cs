using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FeatureToggle.Application.DTOs;
using FeatureToggle.Domain.ConfigurationModels;
using FeatureToggle.Domain.Entity.FeatureManagementSchema;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace FeatureToggle.Application.Requests.Queries.Login
{
    public class GetAuthTokenQueryHandler : IRequestHandler<GetAuthTokenQuery, LoginResponseDTO>
    {
        private readonly UserManager<User> _userManager;  
        private readonly IOptionsMonitor<Authentication> _optionsMonitor;

        public GetAuthTokenQueryHandler(UserManager<User> userManager, IOptionsMonitor<Authentication> optionsMonitor)
        {
            _userManager = userManager;
            _optionsMonitor = optionsMonitor;
        }
        public async Task<LoginResponseDTO> Handle(GetAuthTokenQuery request, CancellationToken cancellationToken)
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
                        new Claim("UserID",user.Id.ToString()),
                        new Claim("IsAdmin",user.IsAdmin.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(signInKey,SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);

                return new LoginResponseDTO { Token = token };
            }
            else
            {
                return new LoginResponseDTO { ErrorMessage = "Incorrect Username or Password" };
            }
        }
    }

    
}
