using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Zadatak.Attributes
{
    /// <summary>
    /// Attribute for defining which services to add
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Class)]
    public class DefineScopeType : Attribute
    {
        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public EnumScopeType Type { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefineScopeType"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        public DefineScopeType(EnumScopeType type = EnumScopeType.Transient)
        {
            Type = type;
        }
    }
}
