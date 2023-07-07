using MediatR;
using RayanBourse.Domain;
using RayanBourse.Infrastructure;

namespace RayanBourse.Application.Features.Product.Commands
{
    public partial class DeleteProductCommand
    {
        public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Domain.Entities.Product>
        {
            private IUnitOfWork _unitOfWork;

            public DeleteProductCommandHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
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

                    var databaseEntity = GetEntityByManufactorEmailAndProductData(product);

                    Validate(databaseEntity);

                    _unitOfWork.ProductRepository.Delete(databaseEntity);
                    return null;
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

            private void Validate(Domain.Entities.Product product)
            {
                try
                {
                    if (product == null)
                        throw new Exception("expected product does not exist in database");

                    if (product.IsAvailable != EnumYesNo.Yes)
                        throw new Exception("expected product is not available");
                }
                catch (Exception e)
                {

                    throw e;
                }

            }



            private Domain.Entities.Product? GetEntityByManufactorEmailAndProductData(Domain.Entities.Product product)
            {
                var res = _unitOfWork.ProductRepository.FindWithIncludes(x => x.ProduceDate == product.ProduceDate && x.ManufactureEmail.Trim() == product.ManufactureEmail, new[] { "User" });
                if (res != null) return res.FirstOrDefault();
                return null;
            }
        }
    }
}
