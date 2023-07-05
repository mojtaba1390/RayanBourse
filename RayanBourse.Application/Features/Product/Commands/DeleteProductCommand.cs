using MediatR;
using RayanBourse.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayanBourse.Application.Features.Product.Commands
{
    public class DeleteProductCommand : IRequest<Domain.Entities.Product>
    {
        public string ManufactureEmail { get; set; }
        public DateTime ProduceDate { get; set; }

        public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Domain.Entities.Product>
        {
            private readonly IProductService _productService;

            public DeleteProductCommandHandler(IProductService productService)
            {
                _productService = productService;
            }
            public Task<Domain.Entities.Product> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var entity = _productService
                      .Find(x => x.ProduceDate == request.ProduceDate && x.ManufactureEmail.Trim() == request.ManufactureEmail).
                      FirstOrDefault();

                    if (entity == null)
                        return default;

                    _productService.Delete(entity);
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
