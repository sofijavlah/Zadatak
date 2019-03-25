using System.Collections.Generic;

namespace Zadatak.Expressions
{
    /// <summary>
    /// 
    /// </summary>
    public class FilterInfo
    {
        /// <summary>
        /// Gets or sets the condition.
        /// </summary>
        /// <value>
        /// The condition.
        /// </value>
        public string Condition { get; set; }

        /// <summary>
        /// The rules
        /// </summary>
        public List<RuleInfo> Rules = new List<RuleInfo>();
    }
}
