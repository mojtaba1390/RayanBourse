using MediatR;
using Microsoft.AspNetCore.Identity;
using RayanBourse.Domain.Entities;

namespace RayanBourse.Application.Features.Authenticate.Commands
{
    public partial class RegisterCommand
    {
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
