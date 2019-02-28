using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zadatak.DTOs
{
    public class DeviceUsageListDTO
    {
        public string Name { get; set; }

        public List<UserUsageDTO> Usages = new List<UserUsageDTO>();
    }
}
