using MediatR;
using RayanBourse.Application.Common;
using RayanBourse.Application.Features.Authenticate.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RayanBourse.Application.Features.Authenticate.Queries
{
    public partial class LoginQuery : IRequest<LoginResponse>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
