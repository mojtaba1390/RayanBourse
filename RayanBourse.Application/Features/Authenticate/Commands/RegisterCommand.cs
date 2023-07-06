using MediatR;
using RayanBourse.Application.Features.Product.Commands;
using RayanBourse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayanBourse.Application.Features.Authenticate.Commands
{
    public partial class RegisterCommand:IRequest<ApplicationUser>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
