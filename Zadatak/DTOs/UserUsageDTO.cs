using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Zadatak.DTOs
{
    public class UserUsageDTO
    {
        public string UserFN { get; set; }

        public string UserLN { get; set; }

        public DateTime? To { get; set; }

        public DateTime From { get; set; }
    }
}
