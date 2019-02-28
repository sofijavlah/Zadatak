using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zadatak.DTOs
{
    public class DeviceUsageInfoDTO
    {
        public string FName { get; set; }

        public string LName { get; set; }

        public DateTime? To { get; set; }

        public DateTime From { get; set; }
    }
}
