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
        public EnumScopeType Type { get; }

        public DefineScopeType(EnumScopeType type = EnumScopeType.Transient)
        {
            Type = type;
        }
    }
}
