using MediatR;
using Microsoft.AspNetCore.Identity;
using RayanBourse.Application.Features.Product.Commands;
using RayanBourse.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayanBourse.Application.Features.Authenticate.Commands
{
    public class RegisterCommand:IRequest<ApplicationUser>
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ApplicationUser>
        {
            private readonly UserManager<ApplicationUser> _userService;

            public RegisterCommandHandler(UserManager<ApplicationUser> userService)
            {
                _userService = userService;
            }
            public async Task<ApplicationUser> Handle(RegisterCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var userExists = await _userService.FindByNameAsync(request.Username);
                    if (userExists != null)
                        throw new Exception("User already exists!");

                    ApplicationUser user = new ApplicationUser()
                    {
                        SecurityStamp = Guid.NewGuid().ToString(),
                        UserName = request.Username
                    };

                    var result = await  _userService.CreateAsync(user, request.Password);

                    if (!result.Succeeded)
                        throw new Exception(string.Join(',', result.Errors.Select(x=>x.Description).ToList()));

                    return null;
                }
                catch (Exception ex)
                {

                    throw ex;
                }

            }
        }
    }
}
