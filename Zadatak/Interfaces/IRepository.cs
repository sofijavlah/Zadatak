using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Zadatak.Interfaces;

namespace Zadatak.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {

        IEnumerable<TEntity> GetAll();

        TEntity Get(long id);

        void Add(TEntity example);

        void Update(TEntity example);
        
        void Remove(TEntity entity);
    }
}
