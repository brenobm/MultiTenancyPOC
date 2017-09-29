using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenancy.Integration
{
    public interface IGenericUnitOfWork
    {
        void Save();

        void Dispose();
    }
}
