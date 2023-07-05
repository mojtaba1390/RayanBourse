using AutoMapper;
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
        private readonly IMapper _mapper;

        public ProductController(IMediator mediator,IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll(string manufactorEmail)
        {
            var productModelList=new List<ProductModel>();
            var products = await _mediator.Send(new GetAllProductQuery());
            if (products.Any())
            {
                productModelList= _mapper.Map<List<ProductModel>>(products);
            }

            return Ok(productModelList);
        }

        [HttpPost]
        [Route("Get/{manufactorEmail}")]
        public async Task<IActionResult> GetByManufactorEmail(string manufactorEmail)
        {
            var productModelList = new List<ProductModel>();
            if (!string.IsNullOrWhiteSpace(manufactorEmail))
            {
                var products = await _mediator.Send(new GetProductListByManufactureEmailQuery() { Name = manufactorEmail });
                productModelList = _mapper.Map<List<ProductModel>>(products);

            }
            else
                return StatusCode(StatusCodes.Status400BadRequest, "manufactorEmail is empty!");


            return Ok(productModelList);
        }
    }
}
