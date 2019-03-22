using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Zadatak.Expressions
{
    public class RuleInfo
    {
        public string Property { get; set; }

        public string Operator { get; set; }

        public string Value { get; set; }
    }
}
