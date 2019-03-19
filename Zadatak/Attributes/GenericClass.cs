using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Zadatak.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class GenericClass : Attribute
    {
    }
}
