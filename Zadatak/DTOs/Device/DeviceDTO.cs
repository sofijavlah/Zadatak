using Zadatak.DTOs.Employee;

namespace Zadatak.DTOs.Device
{
    /// <summary>
    /// 
    /// </summary>
    public class DeviceDto
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
        /// Gets or sets the employee.
        /// </summary>
        /// <value>
        /// The employee.
        /// </value>
        public EmployeeDto Employee { get; set; }
    }
}
