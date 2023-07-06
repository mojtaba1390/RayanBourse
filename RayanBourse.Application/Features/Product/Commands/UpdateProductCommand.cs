using MediatR;
using RayanBourse.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayanBourse.Application.Features.Product.Commands
{
    public partial class UpdateProductCommand : IRequest<Domain.Entities.Product>
    {
        public string Name { get; set; }
        public string ManufacturePhone { get; set; }
        public string ManufactureEmail { get; set; }
        public DateTime ProduceDate { get; set; }
        public EnumYesNo IsAvailable { get; set; }
        public string UserId { get; set; }
    }
}
