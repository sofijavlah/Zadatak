using System;

namespace Zadatak.DTOs.Usage
{
    /// <summary>
    /// 
    /// </summary>
    public class UsageDeviceDto
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets from.
        /// </summary>
        /// <value>
        /// From.
        /// </value>
        public DateTime From { get; set; }

        /// <summary>
        /// Gets or sets to.
        /// </summary>
        /// <value>
        /// To.
        /// </value>
        public DateTime? To { get; set; }
    }
}
