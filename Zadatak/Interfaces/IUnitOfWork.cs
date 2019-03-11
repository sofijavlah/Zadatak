using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zadatak.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {

        void Save();

        void Start();

        void Commit();

        void Dispose();
    }
}
