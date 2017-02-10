using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PATSOrder.Utility
{
    public enum RowGenre
    {
        [CellSorting(0, 0)]
        Functional = 0,
        [CellSorting(1, 1)]
        Statistics = 1,
        [CellSorting(2, 2)]
        Header = 2,
        [CellSorting(3, 9)]
        BP = 3,
        [CellSorting(8, 8)]
        MP = 4,
        [CellSorting(9, 3)]
        AP = 5
    }
    public enum ColGenre
    {
        Buy = 0,
        BL = 1,
        Price = 2,
        SL = 3,
        Sell = 4
    }
    /// <summary>
	/// Alert Box Button Style
	/// </summary>
	public enum AlertBoxButton
    {
        /// <summary>
        /// 出現OK, Caption正常
        /// </summary>
        Msg_OK,
        /// <summary>
        /// 出現OK, Caption為紅字
        /// </summary>
        Error_OK,
        /// <summary>
        /// 出現OK/Cancel
        /// </summary>
        OKCancel,
        /// <summary>
        /// 出現Yes/No
        /// </summary>
        YesNo
    }
}
