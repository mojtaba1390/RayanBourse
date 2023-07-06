using MediatR;
using RayanBourse.Application.Interfaces;
using RayanBourse.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayanBourse.Application.Features.Product.Commands
{
    public class UpdateProductCommand : IRequest<Domain.Entities.Product>
    {
        public string Name { get; set; }
        public string ManufacturePhone { get; set; }
        public string ManufactureEmail { get; set; }
        public DateTime ProduceDate { get; set; }
        public EnumYesNo IsAvailable { get; set; }
        public string UserId { get; set; }


        public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Domain.Entities.Product>
        {
            private readonly IProductService _productService;

            public UpdateProductCommandHandler(IProductService productService)
            {
                _productService = productService;
            }
            public async Task<Domain.Entities.Product> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
            {
                try
                {

                    var product = new Domain.Entities.Product()
                    {
                        Name = request.Name,
                        ManufactureEmail = request.ManufactureEmail,
                        ManufacturePhone = request.ManufacturePhone,
                        ProduceDate = request.ProduceDate,
                        IsAvailable = request.IsAvailable,
                        UserId = request.UserId

                    };

                    _productService.Update(product);
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
