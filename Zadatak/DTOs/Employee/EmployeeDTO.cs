using Zadatak.DTOs.Office;

namespace Zadatak.DTOs.Employee
{
    /// <summary>
    /// 
    /// </summary>
    public class EmployeeDto
    {
        /// <summary>
        /// Gets or sets the employee identifier.
        /// </summary>
        /// <value>
        /// The employee identifier.
        /// </value>
        public long EmployeeId { get; set; }

        /// <summary>
        /// Gets or sets the name of the f.
        /// </summary>
        /// <value>
        /// The name of the f.
        /// </value>
        public string FName { get; set; }

        /// <summary>
        /// Gets or sets the name of the l.
        /// </summary>
        /// <value>
        /// The name of the l.
        /// </value>
        public string LName { get; set; }

        /// <summary>
        /// Gets or sets the office.
        /// </summary>
        /// <value>
        /// The office.
        /// </value>
        public OfficeDto Office { get; set; }
    }
}
