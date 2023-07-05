using MediatR;
using RayanBourse.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayanBourse.Application.Features.Product.Queries
{
    public class GetAllProductQuery : IRequest<List<Domain.Entities.Product>>
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
