using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zadatak.Models
{
    public class Office
    {
        public long Id { get; set; }

        public string Description { get; set; }

        public List<Employee> Employees { get; set; }
    }
}
