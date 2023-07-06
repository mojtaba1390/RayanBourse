using MediatR;
using RayanBourse.Application.Interfaces;

namespace RayanBourse.Application.Features.Product.Commands
{
    public partial class DeleteProductCommand
    {
        public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Domain.Entities.Product>
        {
            private readonly IProductService _productService;

            public DeleteProductCommandHandler(IProductService productService)
            {
                _productService = productService;
            }
            public async Task<Domain.Entities.Product> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var product = new Domain.Entities.Product()
                    {
                        ManufactureEmail = request.ManufactureEmail,
                        ProduceDate = request.ProduceDate,
                        UserId = request.UserId

                    };
                    _productService.Delete(product);
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
