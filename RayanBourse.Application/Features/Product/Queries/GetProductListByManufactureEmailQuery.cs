using MediatR;
using RayanBourse.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayanBourse.Application.Features.Product.Queries
{
    public class GetProductListByManufactureEmailQuery:IRequest<List<Domain.Entities.Product>>
    {
        public string Name { get; set; }

        public class GetProductListByManufactureEmailQueryHandler : IRequestHandler<GetProductListByManufactureEmailQuery, List<Domain.Entities.Product>>
        {
            private readonly IProductService _productService;

            public GetProductListByManufactureEmailQueryHandler(IProductService productService)
            {
                _productService = productService;
            }

            public async Task<List<Domain.Entities.Product>> Handle(GetProductListByManufactureEmailQuery query, CancellationToken cancellationToken)
            {
                return _productService.Find(x => x.Name == query.Name).ToList();
            }
        }

    }
}
