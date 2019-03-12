using System.Collections.Generic;

namespace Zadatak.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Office
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the employees.
        /// </summary>
        /// <value>
        /// The employees.
        /// </value>
        public List<Employee> Employees { get; set; } = new List<Employee>();

        //public override bool Equals(object obj)
        //{
        //    var office = obj as Office;
            
        //    if (office == null) return false;

        //    if (Description != office.Description) return false;

        //    return true;
        //}

        //public override int GetHashCode()
        //{
        //    return Description.GetHashCode();
        //}
    }
}
