using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netizen.Text
{
    public static class ObjectListExtends
    {
        public static string FormatString(this List<object> list)
        {
            return string.Concat("[", string.Join(",", list.Select(i => i.ToString())), "]");
        }
    }
}
