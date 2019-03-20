using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zadatak.Expressions
{
    public class FilterInfo
    {
        public string Condition { get; set; }

        public List<RuleInfo> Rules = new List<RuleInfo>();
    }
}
