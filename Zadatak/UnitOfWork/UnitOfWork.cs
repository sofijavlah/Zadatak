using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using Zadatak.Interfaces;
using Zadatak.Models;
using Zadatak.Repositories;

namespace Zadatak.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WorkContext Context;

        private IDbContextTransaction transaction;

        public UnitOfWork(WorkContext context)
        {
            Context = context;
        }

        public void Start()
        {
            transaction = Context.Database.BeginTransaction();
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        public void Commit()
        {
            transaction.Commit();
        }

        public void Dispose()
        {
            transaction?.Dispose();
        }


    }
}
