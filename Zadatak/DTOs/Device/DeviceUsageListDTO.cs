using System.Collections.Generic;

namespace Zadatak.DTOs
{
    public class DeviceUsageListDTO
    {
        public string Name { get; set; }

        public List<UsageUserDTO> Usages = new List<UsageUserDTO>();
    }
}
