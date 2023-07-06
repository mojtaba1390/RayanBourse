using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RayanBourse.Application.Features.Product.Commands;
using RayanBourse.Application.Features.Product.Queries;
using RayanBourse.Domain.Entities;
using RayanBourse.Models;
using System.Security.Claims;

namespace RayanBourse.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly string userId;
        public ProductController(IMediator mediator,IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;


        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var productModelList=new List<ProductModel>();
            var products = await _mediator.Send(new GetAllProductQuery());
            if (products.Any())
            {
                productModelList= _mapper.Map<List<ProductModel>>(products);
            }

            return Ok(productModelList);
        }

        [HttpGet]
        [Route("GetByManufactorEmail/{manufactorEmail}")]
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


        [HttpPost]
        [Authorize]
        [Route("Add")]
        public async Task<IActionResult> Add([FromBody] ProductModel model)
        {
            var userId = HttpContext.User.FindFirstValue("UserId");

            try
            {
                await _mediator.Send(new AddProductCommand()
                {
                    Name = model.Name,
                    ManufactureEmail = model.ManufactureEmail,
                    ProduceDate = model.ProduceDate,
                    ManufacturePhone = model.ManufacturePhone,
                    IsAvailable = model.IsAvailable,
                    UserId = userId
                });
                return Ok();
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }




            return Ok();
        }



        [HttpPost]
        [Authorize]
        [Route("Edit")]
        public async Task<IActionResult> Edit([FromBody] ProductModel model)
        {
            var userId = HttpContext.User.FindFirstValue("UserId");

            try
            {
                await _mediator.Send(new UpdateProductCommand()
                {
                    Name = model.Name,
                    ManufactureEmail = model.ManufactureEmail,
                    ProduceDate = model.ProduceDate,
                    ManufacturePhone = model.ManufacturePhone,
                    IsAvailable = model.IsAvailable,
                    UserId = userId
                });
                return Ok();
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }




            return Ok();
        }



        [HttpPost]
        [Authorize]
        [Route("Delete")]
        public async Task<IActionResult> Delete([FromBody] ProductModel model)
        {
            var userId = HttpContext.User.FindFirstValue("UserId");

            try
            {
                await _mediator.Send(new DeleteProductCommand()
                {
                    ManufactureEmail = model.ManufactureEmail,
                    ProduceDate = model.ProduceDate,
                    UserId = userId
                });
                return Ok();
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }

            return Ok();
        }
    }
}
