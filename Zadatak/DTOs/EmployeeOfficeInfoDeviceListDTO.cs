using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zadatak.Models;

namespace Zadatak.DTOs
{
    public class EmployeeOfficeInfoDeviceListDTO
    {
        public string FName { get; set; }

        public string LName { get; set; }

        public string OfficeName { get; set; }

        public IEnumerable<DeviceUsageInfoDTO> DeviceUsageList = new List<DeviceUsageInfoDTO>();
    }
}
