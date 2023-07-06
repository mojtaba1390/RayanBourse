using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using RayanBourse.Application.Common;
using RayanBourse.Application.Features.Authenticate.Commands;
using RayanBourse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RayanBourse.Application.Features.Authenticate.Queries
{
    public class LoginQuery:IRequest<JwtSecurityToken>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public class LoginQueryHandler : IRequestHandler<LoginQuery, JwtSecurityToken>
        {
            private readonly UserManager<ApplicationUser> _userService;

            public LoginQueryHandler(UserManager<ApplicationUser> userService)
            {
                _userService = userService;
            }
            public async Task<JwtSecurityToken> Handle(LoginQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var user =  await _userService.FindByNameAsync(request.Username);
                    if (user != null &&  await _userService.CheckPasswordAsync(user, request.Password))
                    {
                        var authClaims = new List<Claim>
                                        {
                                            new Claim(ClaimTypes.Name, user.UserName),
                                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                                        };

                        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTSettings.Secret));

                        var token = new JwtSecurityToken(
                            issuer: JWTSettings.Issuer,
                            audience: JWTSettings.Audiance,
                            expires: DateTime.Now.AddHours(JWTSettings.ExpireTime),
                            claims: authClaims,
                            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                            );



                        return token;
                    }

                    throw new Exception("Login Failed");
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }
    }
}
