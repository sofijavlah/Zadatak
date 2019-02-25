using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zadatak.DTOs
{
    public class DeviceUsageInfoDTO
    {
        public string Name { get; set; }
        
        public DateTime From { get; set; }

        public DateTime? To { get; set; }
    }
}
