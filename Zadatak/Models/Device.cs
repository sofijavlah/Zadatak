using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zadatak.Models
{
    public class Device
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public long EmployeeId { get; set; }

        public Employee Employee { get; set; }
    }
}
