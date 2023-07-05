
using RayanBourse.Infrastructure.Interfaces;
using RayanBourse.Domain.Context;
using System;
using System.Collections.Generic;
using System.Text;
using RayanBourse.Infrastructure.Repositories;

namespace RayanBourse.Infrastructure
{
    public class UnitOfWork :IUnitOfWork
    {

        protected readonly RayanBourseContext _context;
        public UnitOfWork(RayanBourseContext context)
        {
            _context = context;
            ProductRepository = new ProductRepository(_context);

        }


        public IProductRepository ProductRepository { get; set; }



        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
