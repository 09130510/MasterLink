using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PriceCalculator
{
    public enum FutBaseCalcType
    {
        TYPE0,
        TYPE1,
        TYPE2
    }    
    public enum SubscribeType
    {
        NONE,
        PATS,
        CAPITAL,        
        REDIS,
        IPUSH
    }
    public enum Month
    {
        [ForeignLetter("F")]
        JAN = 1,
        [ForeignLetter("G")]
        FEB = 2,
        [ForeignLetter("H")]
        MAR = 3,
        [ForeignLetter("J")]
        APR = 4,
        [ForeignLetter("K")]
        MAY = 5,
        [ForeignLetter("M")]
        JUN = 6,
        [ForeignLetter("N")]
        JUL = 7,
        [ForeignLetter("Q")]
        AUG = 8,
        [ForeignLetter("U")]
        SEP = 9,
        [ForeignLetter("V")]
        OCT = 10,
        [ForeignLetter("X")]
        NOV = 11,
        [ForeignLetter("Z")]
        DEC = 12
    }
    public enum Substitute
    {
        None,
        /// <summary>
        /// 西元年
        /// </summary>
        AD,
        /// <summary>
        /// 民國年
        /// </summary>
        TW,
        /// <summary>
        /// 數字月
        /// </summary>
        DG,
        /// <summary>
        /// 國外月; 1F,2G,3H,4J,5K,6M,7N,8Q,9U,10V,11X,12Z
        /// </summary>
        FL,
        /// <summary>
        /// 國外月; 1JAN,2FEB,3MAR,4APR,5MAY,6JUN,7JUL,8AUG,9SEP10OCT,11NOV,12DEC
        /// </summary>
        ML,
        /// <summary>
        /// 台灣月; 1A,2B,3C,4D,5E,6F,7G,8H,9I,10J,11K,12L
        /// </summary>
        TL
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
