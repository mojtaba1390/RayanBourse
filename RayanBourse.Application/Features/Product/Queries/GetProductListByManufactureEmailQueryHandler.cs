using MediatR;
using RayanBourse.Application.Interfaces;

namespace RayanBourse.Application.Features.Product.Queries
{
    public partial class GetProductListByManufactureEmailQuery
    {
        public class GetProductListByManufactureEmailQueryHandler : IRequestHandler<GetProductListByManufactureEmailQuery, List<Domain.Entities.Product>>
        {
            private readonly IProductService _productService;

            public GetProductListByManufactureEmailQueryHandler(IProductService productService)
            {
                _productService = productService;
            }

            public async Task<List<Domain.Entities.Product>> Handle(GetProductListByManufactureEmailQuery query, CancellationToken cancellationToken)
            {
                return _productService.Find(x => x.ManufactureEmail == query.Name).ToList();
            }
        }

    }
}
