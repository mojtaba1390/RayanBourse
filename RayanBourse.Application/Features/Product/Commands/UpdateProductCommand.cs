using MediatR;
using RayanBourse.Application.Interfaces;
using RayanBourse.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayanBourse.Application.Features.Product.Commands
{
    public class UpdateProductCommand : IRequest<Domain.Entities.Product>
    {
        public string Name { get; set; }
        public string ManufacturePhone { get; set; }
        public string ManufactureEmail { get; set; }
        public DateTime ProduceDate { get; set; }
        public EnumYesNo IsAvailable { get; set; }


        public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Domain.Entities.Product>
        {
            private readonly IProductService _productService;

            public UpdateProductCommandHandler(IProductService productService)
            {
                _productService = productService;
            }
            public async Task<Domain.Entities.Product> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var entity = _productService
                      .Find(x => x.ProduceDate == request.ProduceDate && x.ManufactureEmail.Trim() == request.ManufactureEmail).
                      FirstOrDefault();

                    if (entity == null)
                        return default;


                    entity.Name = request.Name;
                    entity.IsAvailable = request.IsAvailable;
                    entity.ManufacturePhone = request.ManufacturePhone;

                    _productService.Update(entity);
                    return null;
                }
                catch (Exception ex)
                {

                    throw ex;
                }


            }
        }
    }
}
