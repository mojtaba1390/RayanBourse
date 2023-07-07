using MediatR;
using RayanBourse.Infrastructure;

namespace RayanBourse.Application.Features.Product.Queries
{
    public partial class GetProductListByManufactureEmailQuery
    {
        public class GetProductListByManufactureEmailQueryHandler : IRequestHandler<GetProductListByManufactureEmailQuery, List<Domain.Entities.Product>>
        {
            private IUnitOfWork _unitOfWork;

            public GetProductListByManufactureEmailQueryHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<List<Domain.Entities.Product>> Handle(GetProductListByManufactureEmailQuery query, CancellationToken cancellationToken)
            {
                return _unitOfWork.ProductRepository.Find(x => x.ManufactureEmail == query.Name).ToList();
            }
        }

    }
}
