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

        public List<Employee> Employees { get; set; } = new List<Employee>();

        public override bool Equals(object obj)
        {
            var office = obj as Office;
            if (office == null) return false;

            if (Description != office.Description) return false;

            return true;
        }
    }
}
