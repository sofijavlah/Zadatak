using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zadatak.Models
{
    public class EmployeeDTO
    {

        public string FName { get; set; }

        public string LName { get; set; }

        public EmployeeDTO(Employee e)
        {
            FName = e.FirstName;
            LName = e.LastName;
        }
    }
}
