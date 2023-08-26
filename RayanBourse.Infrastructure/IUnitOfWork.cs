using System;
using System.Collections.Generic;
using System.Text;

namespace RayanBourse.Infrastructure
{
    public interface IUnitOfWork:IDisposable
    {


        int Complete();
    }
}
