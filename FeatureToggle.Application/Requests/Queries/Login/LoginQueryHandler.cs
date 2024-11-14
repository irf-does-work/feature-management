using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FeatureToggle.Domain.Entity.User_Schema;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace FeatureToggle.Application.Requests.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, LoginResponse>
    {
        private UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public LoginQueryHandler(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        public async Task<LoginResponse> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, request.Password))
            {
                var secretKey = _configuration["AppSettings:JWTSecret"];
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
