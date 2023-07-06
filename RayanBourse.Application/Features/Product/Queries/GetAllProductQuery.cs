using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RayanBourse.Application.Features.Product.Queries
{
    public partial class GetAllProductQuery : IRequest<List<Domain.Entities.Product>>
    {
    }
}
