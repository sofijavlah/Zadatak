﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zadatak.DTOs
{
    public class DeviceUsageListDTO
    {
        public string Name { get; set; }

        public List<UsageDTO> Usages = new List<UsageDTO>();
    }
}
