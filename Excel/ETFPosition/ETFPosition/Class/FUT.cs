using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETFPosition.Class
{
    public class FUT : Position
    {
        /// <summary>
        /// 期貨代號在I欄
        /// </summary>
        private const string COL_FUTID = "I";
        /// <summary>
        /// 期貨代號在L欄
        /// </summary>
        private const string COL_CURNCY = "L";

        public string YM { get; set; }
        public string CValueFormula
        {
            get
            {
                return $"VLOOKUP({COL_FUTID}{RowIndex},REF_CVALUE,2,0)";
            }

        }
        public string CurncyFormula
        {
            get
            {

                return $"VLOOKUP({COL_FUTID}{RowIndex}, REF_CVALUE, 3, 0)";
            }
        }
        public string FxRateFormula
        {
            get
            {
                return $"VLOOKUP({COL_CURNCY}{RowIndex},REF_FXRATE,2,0)";
            }
        }
    }
}
