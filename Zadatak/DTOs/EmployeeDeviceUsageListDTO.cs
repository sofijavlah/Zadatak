using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zadatak.Models;

namespace Zadatak.DTOs
{
    public class EmployeeDeviceUsageListDTO
    {
        public string FName { get; set; }

        public string LName { get; set; }

        public List<UsageDeviceDTO> Usages { get; set; }
    }
}
