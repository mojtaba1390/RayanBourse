using MediatR;
using RayanBourse.Infrastructure;

namespace RayanBourse.Application.Features.Product.Commands
{
    public partial class UpdateProductCommand
    {
        public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Domain.Entities.Product>
        {
            private IUnitOfWork _unitOfWork;

            public UpdateProductCommandHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
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

                    Validate(product);

                      _unitOfWork.ProductRepository.Update(product);

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
                    var databaseEntity = GetEntityByManufactorEmailAndProductData(product);

                    if (databaseEntity == null)
                        throw new Exception("Inserted product does not exist in database");

                    if (databaseEntity.UserId.Trim() != product.UserId)
                        throw new Exception("modifing product  allow just by user creation itself");

                    if (!Helper.IsValidMobileNumber(product.ManufacturePhone))
                        throw new Exception("phone number is invalid!");
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
