using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zadatak.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the office identifier.
        /// </summary>
        /// <value>
        /// The office identifier.
        /// </value>
        public long OfficeId { get; set; }

        /// <summary>
        /// Gets or sets the office.
        /// </summary>
        /// <value>
        /// The office.
        /// </value>
        [ForeignKey("OfficeId")]
        public Office Office { get; set; }

        /// <summary>
        /// Gets or sets the devices.
        /// </summary>
        /// <value>
        /// The devices.
        /// </value>
        public List<Device> Devices { get; set; } = new List<Device>();

        /// <summary>
        /// Gets or sets the usage list.
        /// </summary>
        /// <value>
        /// The usage list.
        /// </value>
        public List<DeviceUsage> UsageList { get; set; } = new List<DeviceUsage>();

        //public override bool Equals(object obj)
        //{
        //    var employee = obj as Employee;
        //    if (employee == null) return false;

        //    if (Id != employee.Id) return false; //&& Office.Description != employee.Office.Description) ;

        //    return true;
        //}

    }
}
