using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zadatak.Attributes
{
    /// <summary>
    /// Values for DefineScopeType attribute
    /// </summary>
    public enum EnumScopeType
    {
        /// <summary>
        /// The scoped ype
        /// </summary>
        Scoped = 0,
        /// <summary>
        /// The transient type
        /// </summary>
        Transient = 1,
        /// <summary>
        /// The singleton type
        /// </summary>
        Singleton = 2
    }
}
