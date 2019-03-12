using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zadatak.Models
{
    /// <summary>
    /// Device model class
    /// </summary>
    public class Device
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the employee identifier.
        /// </summary>
        /// <value>
        /// The employee identifier.
        /// </value>
        public long EmployeeId { get; set; }

        /// <summary>
        /// Gets or sets the employee.
        /// </summary>
        /// <value>
        /// The employee.
        /// </value>
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }

        /// <summary>
        /// Gets or sets the usage list.
        /// </summary>
        /// <value>
        /// The usage list.
        /// </value>
        public List<DeviceUsage> UsageList { get; set; } = new List<DeviceUsage>();

        //public override bool Equals(object obj)
        //{
        //    var device = obj as Device;
        //    if (device == null) return false;

        //    if (Name != device.Name) return false;

        //    return true;
        //}
    }
}
