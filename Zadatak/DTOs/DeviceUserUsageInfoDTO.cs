using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading.Tasks;

namespace Zadatak.DTOs
{
    public class DeviceUserUsageInfoDTO
    {
        public string Name { get; set; }

        public string User { get; set; }

        public DateTime? To { get; set; }

        public DateTime From { get; set; }
}
}
