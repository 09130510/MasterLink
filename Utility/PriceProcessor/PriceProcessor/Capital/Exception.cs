using SKCOMLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PriceProcessor.Capital
{
    public class CapitalException : System.Exception
    {
        public CapitalException(ISKCenterLib lib, int code) :
            base(lib.SKCenterLib_GetReturnCodeMessage(code) + "  " + lib.SKCenterLib_GetLastLogInfo())
        { }
    }
}
