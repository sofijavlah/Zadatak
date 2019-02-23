using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Zadatak.Models;

namespace Zadatak.DTOs
{
    public class EmployeeToOfficeDTO
    {
        //public long Id { get; set; }

        public string FName { get; set; }

        public string LName { get; set; }

        //public long OfficeId { get; set; }

        public string OfficeName { get; set; }

        //public EmployeeToOfficeDTO(Employee e)
        //{
        //    FName = e.FirstName;
        //    LName = e.LastName;
        //    OfficeName = e.Office.Description;
        //}

    }
}
