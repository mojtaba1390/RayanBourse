using MediatR;
using RayanBourse.Application.Interfaces;

namespace RayanBourse.Application.Features.Product.Commands
{
    public partial class UpdateProductCommand
    {
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
