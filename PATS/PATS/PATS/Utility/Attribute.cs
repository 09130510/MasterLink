using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PATS.Utility
{
    [AttributeUsage(AttributeTargets.Field)]
    public class CellSortingAttribute : Attribute
    {
        public int AscendingNo { get; private set; }
        public int DescendingNo { get; private set; }

        public CellSortingAttribute(int ascending, int descending)
        {
            AscendingNo = ascending;
            DescendingNo = descending;
        }
    }
}
