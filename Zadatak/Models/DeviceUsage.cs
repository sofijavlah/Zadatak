using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zadatak.Models
{
    public class DeviceUsage
    {
        public long Id { get; set; }

        public DateTime From { get; set; }

        public DateTime? To { get; set; }

        public Employee E { get; set; }

        public Device D { get; set; }
    }
}
