using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Zadatak.DTOs;
using Zadatak.Models;
using Zadatak.Repositories;

namespace Zadatak.Interfaces
{
    public interface IOfficeRepository : IRepository<Office>
    {
        Office GetOfficeEmployees(string description);

        //bool DeleteJustEmployees(string description);
    }
}
