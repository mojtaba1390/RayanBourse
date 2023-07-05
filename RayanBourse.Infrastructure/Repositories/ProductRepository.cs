using Microsoft.EntityFrameworkCore;
using RayanBourse.Domain.Context;
using RayanBourse.Domain.Entities;
using RayanBourse.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RayanBourse.Infrastructure.Repositories
{
    public class ProductRepository : Repository<Product>,IProductRepository
    {
        public ProductRepository(RayanBourseContext context) : base(context)
        {

        }

        public RayanBourseContext RayanBourseContext
        {
            get { return _dbContext as RayanBourseContext; }
        }
    }
}
