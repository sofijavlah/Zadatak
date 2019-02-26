using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Zadatak.Models;

namespace Zadatak.DTOs
{
    public class EmployeeOfficeInfoDTO
    {
        public string FName { get; set; }

        public string LName { get; set; }
        
        public string OfficeName { get; set; }
    }
}
