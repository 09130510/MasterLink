using SKCOMLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PriceLib.Capital
{
    public partial class OSCapitalLib
    {
        public class CapitalException : Exception
        {
            public CapitalException(ISKCenterLib lib, int code) : base(lib.SKCenterLib_GetReturnCodeMessage(code))
            {

            }
        }
    }
}
