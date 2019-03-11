using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zadatak.DTOs;
using Zadatak.Interfaces;
using Zadatak.Models;

namespace Zadatak.Repositories
{
    public class OfficeRepository :  Repository<Office>, IOfficeRepository
    {

        protected readonly WorkContext _context;

        public OfficeRepository(WorkContext context) : base(context)
        {
            _context = context;
        }

        public Office GetOfficeEmployees(string description)
        {
            return _context.Offices.Include(x => x.Employees).First(x => x.Description == description);
        }

        public bool DeleteJustEmployees(string description)
        {
            var office = _context.Offices.Include(x => x.Employees).FirstOrDefault(x => x.Description == description);

            if (office == null) return false;

            office.Employees.RemoveRange(0, office.Employees.Count);

            _context.SaveChanges();

            return true;

        }

    }
}
