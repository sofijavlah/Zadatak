using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zadatak.Models
{
    public class OfficeDTO
    {
        public long Id { get; set; }

        public string Description { get; set; }

        

        public OfficeDTO(Office o)
        {
            Id = o.Id;
            Description = o.Description;
        }

    }
}
