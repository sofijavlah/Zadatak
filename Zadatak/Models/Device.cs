using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Zadatak.Models
{
    public class Device
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public long EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }

        public List<DeviceUsage> UsageList { get; set; }
    }
}
