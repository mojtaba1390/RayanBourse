using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RayanBourse.Application.Common
{
    public class LoginResponse
    {
        public JwtSecurityToken JwtSecurityToken { get; set; }
        public AuthenticationProperties AuthenticationProperties { get; set; }
        public ClaimsPrincipal ClaimsPrincipal { get; set; }



    }
}
