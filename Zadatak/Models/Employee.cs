using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zadatak.Models
{
    public class Employee
    {
        public long Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }


        public long OfficeId { get; set; }

        public Office Office { get; set; }

        public List<Device> Devices { get; set; }

    }
}
