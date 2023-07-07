using MediatR;
using RayanBourse.Infrastructure;

namespace RayanBourse.Application.Features.Product.Queries
{
    public partial class GetAllProductQuery
    {
        public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQuery, List<Domain.Entities.Product>>
        {
            private IUnitOfWork _unitOfWork;

            public GetAllProductQueryHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<List<Domain.Entities.Product>> Handle(GetAllProductQuery query, CancellationToken cancellationToken)
            {
                return _unitOfWork.ProductRepository.GetAll().ToList();
            }
        }
    }
}
