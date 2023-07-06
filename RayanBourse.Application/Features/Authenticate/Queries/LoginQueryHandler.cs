using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using RayanBourse.Application.Common;
using RayanBourse.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RayanBourse.Application.Features.Authenticate.Queries
{
    public partial class LoginQuery
    {
        public class LoginQueryHandler : IRequestHandler<LoginQuery, LoginResponse>
        {
            private readonly UserManager<ApplicationUser> _userService;

            public LoginQueryHandler(UserManager<ApplicationUser> userService)
            {
                _userService = userService;
            }
            public async Task<LoginResponse> Handle(LoginQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var user = await _userService.FindByNameAsync(request.Username);
                    if (user != null && await _userService.CheckPasswordAsync(user, request.Password))
                    {
                        var authClaims = new List<Claim>
                                        {
                                            new Claim(ClaimTypes.Name, user.UserName),
                                            new Claim(type: "UserId", value: user.Id),
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

                        var identity = new ClaimsIdentity(authClaims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal = new ClaimsPrincipal(identity);

                        var peraperties = new AuthenticationProperties()
                        {
                            IsPersistent = true

                        };


                        return  new LoginResponse()
                        {
                            JwtSecurityToken = token,
                            AuthenticationProperties = peraperties,
                            ClaimsPrincipal=principal

                        };




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
