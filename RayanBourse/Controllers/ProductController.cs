using MediatR;
using Microsoft.AspNetCore.Mvc;
using RayanBourse.Application.Features.Product.Queries;
using RayanBourse.Domain.Entities;
using RayanBourse.Models;

namespace RayanBourse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll(string manufactorEmail)
        {

            var products = await _mediator.Send(new GetAllProductQuery());


            return Ok(products);
        }

        [HttpPost]
        [Route("Get/{manufactorEmail}")]
        public async Task<IActionResult> GetByManufactorEmail(string manufactorEmail)
        {
            var products = new List<Product>();
            if (!string.IsNullOrWhiteSpace(manufactorEmail))
                products = await _mediator.Send(new GetProductListByManufactureEmailQuery() { Name = manufactorEmail });
            else
                return StatusCode(StatusCodes.Status400BadRequest, "manufactorEmail is empty!");


            return Ok(products);
        }
    }
}
