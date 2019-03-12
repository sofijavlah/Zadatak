using System;

namespace Zadatak.DTOs.Usage
{
    /// <summary>
    /// 
    /// </summary>
    public class UsageUserDto
    {
        /// <summary>
        /// Gets or sets the user function.
        /// </summary>
        /// <value>
        /// The user function.
        /// </value>
        public string UserFn { get; set; }

        /// <summary>
        /// Gets or sets the user ln.
        /// </summary>
        /// <value>
        /// The user ln.
        /// </value>
        public string UserLn { get; set; }

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
