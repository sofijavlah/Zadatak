using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zadatak.Interfaces;

namespace Zadatak.Repositories
{
    public class IRepository <DomainType, IdType> where DomainType : IDomain
    {
        DomainType Find(IdType id);

        void Update(DomainType example);

        void Insert(DomainType example);

        void Delete(DomainType example);
    }
}
