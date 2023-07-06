using AutoMapper;
using Azure;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RayanBourse.Application.Features.Authenticate.Commands;
using RayanBourse.Application.Features.Authenticate.Queries;
using RayanBourse.Application.Features.Product.Queries;
using RayanBourse.Domain.Entities;
using RayanBourse.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RayanBourse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthenticateController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserModel model)
        {
            try
            {
                 await _mediator.Send(new RegisterCommand() { Username=model.Username,Password=model.Password });
                return Ok();

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }







        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserModel model)
        {
            try
            {
                var result = await _mediator.Send(new LoginQuery() { Username = model.Username, Password = model.Password });
                await HttpContext.SignInAsync(result.ClaimsPrincipal, result.AuthenticationProperties);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(result.JwtSecurityToken),
                    expiration = result.JwtSecurityToken.ValidTo
                });

            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }
    }
}
