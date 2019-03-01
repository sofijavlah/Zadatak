﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        [ForeignKey("OfficeId")]
        public Office Office { get; set; }

        public List<Device> Devices { get; set; } = new List<Device>();

        public List<DeviceUsage> UsageList { get; set; } = new List<DeviceUsage>();

        public override bool Equals(object obj)
        {
            var employee = obj as Employee;
            if (employee == null) return false;

            if (FirstName != employee.FirstName && LastName != employee.LastName) return false; //&& Office.Description != employee.Office.Description) ;

            return true;
        }

    }
}
