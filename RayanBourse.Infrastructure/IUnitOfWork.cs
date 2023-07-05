using RayanBourse.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RayanBourse.Infrastructure
{
    public interface IUnitOfWork:IDisposable
    {

        IProductRepository ProductRepository { get; }

        int Complete();
    }
}
