
using RayanBourse.Domain.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace RayanBourse.Infrastructure
{
    public class UnitOfWork :IUnitOfWork
    {

        protected readonly RayanBourseContext _context;
        public UnitOfWork(RayanBourseContext context)
        {
            _context = context;

        }





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
