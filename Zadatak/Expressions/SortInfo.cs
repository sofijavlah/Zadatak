using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Zadatak.Expressions
{
    public class SortInfo
    {
        public string Property { get; set; }

        public char SortDirection { get; set; }
    }
}
