using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zadatak.Models
{
    public class OfficeDTO
    {
        //public long Id { get; set; }

        public string OfficeName { get; set; }
        
        public OfficeDTO(Office o)
        {
            OfficeName = o.Description;
        }

    }
}
