using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zadatak.Models;
using Zadatak.Repositories;

namespace Zadatak.Interfaces
{
    public interface IOfficeRepository : IRepository<Office, long>
    {
    }
}
