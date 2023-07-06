using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayanBourse.Application.Features.Product.Queries
{
    public partial class GetProductListByManufactureEmailQuery:IRequest<List<Domain.Entities.Product>>
    {
        public string Name { get; set; }

    }
}
