using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SinopacHK
{
    public enum RowType
    {
        None = 0,
        Functional = 1,
        Statistics = 2,
        //Manual,
        Header = 3,
        //UpLimit,
        BP = 4,
        MP = 5,
        AP = 6//,
        //DownLimit,        
    }
    public enum ColumnType
    {
        None = 0,
        Buy = 1,
        Price = 2,
        Sell = 3
    }
}