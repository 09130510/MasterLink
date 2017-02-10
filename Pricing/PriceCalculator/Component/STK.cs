using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PriceCalculator.Utility;
using System.Data;
using PriceCalculator.Component;
using log4net;

namespace PriceCalculator.Component
{
    public class STK : Composition
    {
        private ILog m_Log = LogManager.GetLogger(typeof(STK));

        #region Property
        public override string BaseCurncy
        {
            get
            {
                switch (Exch)
                {
                    case Exch.SSE:
                    case Exch.SZSE:
                        return "CNY";
                    case Exch.TSE:
                    case Exch.OSE:
                        return "JPY";
                    case Exch.HKEx:
                        return "HKD";
                    case Exch.NYSE:
                    case Exch.NASDAQ:
                    case Exch.SGX:
                    case Exch.CME:
                    case Exch.CBT:
                    case Exch.NYM:
                        return "USD";
                    case Exch.NSE:
                        return "INR";
                    case Exch.EUREX:
                        return "EUR";
                    case Exch.KRX:
                        return "KRW";
                }
                return "TWD";
            }
        }
        public override decimal CValue { get { return 1M; } }
        public override string Capital
        {
            get
            {
                if (Exch == Exch.TWSE) { return PID; }
                return string.Empty;
            }
        }
        public override string Redis
        {
            get
            {
                if (Exch == Exch.TWSE) { return PID; }
                return string.Empty;
            }
        }
        public override string iPush
        {
            get
            {
                if (Exch == Exch.TWSE) { return PID; }
                return string.Empty;
            }
        }
        #endregion

        public STK(DataRow row)
            : base(row) { }

        public void LockYP(DateTime expired)
        {
            base.LockYP(expired, nameof(STK));
        }
    }
}