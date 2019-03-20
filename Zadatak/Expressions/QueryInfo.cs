using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zadatak.Expressions
{
    public class QueryInfo
    {
        public int Skip { get; set; }

        public int Take { get; set; }

        public List<SortInfo> Sorters = new List<SortInfo>();

        public FilterInfo Filter { get; set; }
    }
}
