using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zadatak.Models;

namespace Zadatak.DTOs
{
    public class OfficeEmployeeListDTO
    {
        public string OfficeName { get; set; }

        public List<EmployeeDTO> Employees { get; set; }
    }
}
