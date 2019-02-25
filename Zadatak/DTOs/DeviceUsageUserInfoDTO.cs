using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zadatak.DTOs
{
    public class DeviceUsageUserInfoDTO
    {
        public string Name { get; set; }

        public IEnumerable<UsageDTO> ListOfUses { get; set; } = new List<UsageDTO>();

        public string User { get; set; }
    }
}
