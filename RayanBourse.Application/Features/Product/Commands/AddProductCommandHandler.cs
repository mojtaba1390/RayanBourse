using MediatR;
using RayanBourse.Application.Interfaces;

namespace RayanBourse.Application.Features.Product.Commands
{
    public partial class AddProductCommand
    {
        public class AddProductCommandHandler : IRequestHandler<AddProductCommand, Domain.Entities.Product>
        {
            private readonly IProductService _productService;

            public AddProductCommandHandler(IProductService productService)
            {
                _productService = productService;
            }
            public async Task<Domain.Entities.Product> Handle(AddProductCommand request, CancellationToken cancellationToken)
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
                        UserId=request.UserId

                    };
                    _productService.Save(product);
                    return product;
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }
    }
}
