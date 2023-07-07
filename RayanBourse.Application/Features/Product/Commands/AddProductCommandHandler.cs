using MediatR;
using Microsoft.EntityFrameworkCore;
using RayanBourse.Domain;
using RayanBourse.Domain.Entities;
using RayanBourse.Infrastructure;

namespace RayanBourse.Application.Features.Product.Commands
{
    public partial class AddProductCommand
    {
        public class AddProductCommandHandler : IRequestHandler<AddProductCommand, Domain.Entities.Product>
        {
            private IUnitOfWork _unitOfWork;

            public AddProductCommandHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
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
                        UserId = request.UserId

                    };

                    Validate(product);

                    var res=await _unitOfWork.ProductRepository.Save(product);

                    return res;
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

                    if (databaseEntity != null)
                        throw new Exception("product is existed in database");

                    if (!Helper.IsValidEmail(product.ManufactureEmail))
                        throw new Exception("email format is invalid!");

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
