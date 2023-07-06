using MediatR;
using RayanBourse.Application.Interfaces;

namespace RayanBourse.Application.Features.Product.Queries
{
    public partial class GetAllProductQuery
    {
        public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQuery, List<Domain.Entities.Product>>
        {
            private readonly IProductService _productService;

            public GetAllProductQueryHandler(IProductService productService)
            {
                _productService = productService;
            }

            public async Task<List<Domain.Entities.Product>> Handle(GetAllProductQuery query, CancellationToken cancellationToken)
            {
                return _productService.GetAll().ToList();
            }
        }
    }
}
