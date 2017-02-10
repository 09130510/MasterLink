//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Runtime.InteropServices;

//namespace SKOSQuote
//{    // Define Call Back Function
//    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
//    public delegate void FOnConnect(int nKind, int nErrorCode);

//    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
//    public delegate void FOnOverseaProducts([MarshalAs(UnmanagedType.BStr)]string strProducts);

//    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
//    public delegate void FOnGetStockIdx(short nCode);

//    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
//    public delegate void FOnNotifyTicks(short sStockidx, int nPtr);

//    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
//    public delegate void FOnNotifyServerTime(short sHour, short sMinute, short sSecond);

//    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
//    public delegate void FOnNotifyKLineData([MarshalAs(UnmanagedType.BStr)]string strStockNo, [MarshalAs(UnmanagedType.BStr)]string strKData);

//    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
//    public delegate void FOnNotifyTicksGet(short sStockidx, int nPtr, int nTime, int nClose, int nQty);

//    public struct FOREIGN
//    {
//        public short m_sStockidx;				// 系統自行定義的股票代碼
//        public short m_sDecimal;				// 報價小數位數
//        public int m_nDenominator;			// 分母

//        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1)]
//        public string m_cMarketNo;              // 市場代號0x05海外商品

//        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
//        public string m_caExchangeNo;          // 交易所代號

//        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
//        public string m_caExchangeName;         // 交易所名稱

//        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
//        public string m_caStockNo;              // 股票代號

//        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
//        public string m_caStockName;                 // 股票名稱

//        public int m_nOpen;					// 開盤價
//        public int m_nHigh;					// 最高價
//        public int m_nLow;					// 最低價
//        public int m_nClose;				// 成交價
//        public int m_dSettlePrice;          //結算價
//        public int m_nTickQty;				// 單量
//        public int m_nRef;					// 昨收、參考價
//        public int m_nBid;					// 買價
//        public int m_nBc;					// 買量
//        public int m_nAsk;					// 賣價
//        public int m_nAc;					// 賣量
//        public int m_nTQty;					// 總量
//    }

//    public struct TICK
//    {
//        public int m_nPtr;			//Index
//        public int m_nTime;        // 時間
//        public int m_nClose;		//成交價
//        public int m_nQty;			//成交量
//    }
//    public struct BEST5
//    {
//        public int m_nBid1;
//        public int m_nBidQty1;
//        public int m_nBid2;
//        public int m_nBidQty2;
//        public int m_nBid3;
//        public int m_nBidQty3;
//        public int m_nBid4;
//        public int m_nBidQty4;
//        public int m_nBid5;
//        public int m_nBidQty5;
//        public int m_nAsk1;
//        public int m_nAskQty1;
//        public int m_nAsk2;
//        public int m_nAskQty2;
//        public int m_nAsk3;
//        public int m_nAskQty3;
//        public int m_nAsk4;
//        public int m_nAskQty4;
//        public int m_nAsk5;
//        public int m_nAskQty5;
//    }

//    enum ApiMessage
//    {
//        SK_SUCCESS = 0,
//        SK_FAIL = -1,
//        SK_ERROR_SERVER_NOT_CONNECTED = -3,
//        SK_ERROR_INITIALIZE_FAIL = -4,
//        SK_ERROR_ACCOUNT_NOT_EXIST = 1,
//        SK_ERROR_ACCOUNT_MARKET_NOT_MATCH = 2,
//        SK_ERROR_PERIOD_OUT_OF_RANGE = 3,
//        SK_ERROR_FLAG_OUT_OF_RANGE = 4,
//        SK_ERROR_BUYSELL_OUT_OF_RANGE = 5,
//        SK_ERROR_ORDER_SERVER_INVALID = 6,
//        SK_ERROR_PERMISSION_DENIED = 7,
//        SK_KLINE_DATA_TYPE_NOT_FOUND = 8,
//        SK_ERROR_PERMISSION_TIMEOUT = 9,

//        SK_SUBJECT_CONNECTION_CONNECTED = 100,
//        SK_SUBJECT_CONNECTION_DISCONNECT = 101,
//        SK_SUBJECT_QUOTE_PAGE_EXCEED = 200,
//        SK_SUBJECT_QUOTE_PAGE_INCORRECT = 201,
//        SK_SUBJECT_TICK_PAGE_EXCEED = 210,
//        SK_SUBJECT_TICK_PAGE_INCORRECT = 211,
//        SK_SUBJECT_TICK_STOCK_NOT_FOUND = 212,
//        SK_SUBJECT_BEST5_DATA_NOT_FOUND = 213,
//        SK_SUBJECT_QUOTEREQUEST_NOT_FOUND = 214,
//        SK_SUBJECT_SERVER_TIME_NOT_FOUND = 215,

//        SK_WARNING_LOGIN_ALREADY = 2003
//    }

//    class Functions
//    {
//        [DllImport("SKOSQuoteLib.dll", EntryPoint = "SKOSQuoteLib_Initialize", CharSet = CharSet.Ansi)]
//        public static extern int SKOSQuoteLib_Initialize(string pcUserName, string pcPassword);

//        [DllImport("SKOSQuoteLib.dll", EntryPoint = "SKOSQuoteLib_EnterMonitor", CharSet = CharSet.Ansi)]
//        public static extern int SKOSQuoteLib_EnterMonitor(short nConnectType);

//        [DllImport("SKOSQuoteLib.dll", EntryPoint = "SKOSQuoteLib_LeaveMonitor", CharSet = CharSet.Ansi)]
//        public static extern int SKOSQuoteLib_LeaveMonitor();

//        [DllImport("SKOSQuoteLib.dll", EntryPoint = "SKOSQuoteLib_RequestStocks", SetLastError = true, CharSet = CharSet.Ansi)]
//        public static extern int SKOSQuoteLib_RequestStocks(out int psPageNo, string pStockNos);

//        [DllImport("SKOSQuoteLib.dll", EntryPoint = "SKOSQuoteLib_RequestTicks", SetLastError = true, CharSet = CharSet.Ansi)]
//        public static extern int SKOSQuoteLib_RequestTicks(out int psPageNo, string pStockNos);

//        [DllImport("SKOSQuoteLib.dll", EntryPoint = "SKOSQuoteLib_GetStockByIndex", SetLastError = true, CharSet = CharSet.Ansi)]
//        public static extern int SKOSQuoteLib_GetStockByIndex(short sIndex, out FOREIGN TForeign);

//        [DllImport("SKOSQuoteLib.dll", EntryPoint = "SKOSQuoteLib_GetTick", SetLastError = true, CharSet = CharSet.Ansi)]
//        public static extern int SKQuoteLib_GetTick(short sIndex, int nPtr, out TICK TTick);

//        [DllImport("SKOSQuoteLib.dll", EntryPoint = "SKOSQuoteLib_GetBest5", SetLastError = true, CharSet = CharSet.Ansi)]
//        public static extern int SKOSQuoteLib_GetBest5(short sIndex, out BEST5 TBest5);

//        [DllImport("SKOSQuoteLib.dll", EntryPoint = "SKOSQuoteLib_RequestOverseaProducts", CharSet = CharSet.Ansi)]
//        public static extern int SKOSQuoteLib_RequestOverseaProducts();

//        [DllImport("SKOSQuoteLib.dll", EntryPoint = "SKOSQuoteLib_RequestServerTime", CharSet = CharSet.Ansi)]
//        public static extern int SKOSQuoteLib_RequestServerTime();

//        [DllImport("SKOSQuoteLib.dll", EntryPoint = "SKOSQuoteLib_RequestKLine", SetLastError = true, CharSet = CharSet.Ansi)]
//        public static extern int SKOSQuoteLib_RequestKLine(string pStockNos, short KLineType);




//        [DllImport("SKOSQuoteLib.dll", CallingConvention = CallingConvention.StdCall)]
//        public static extern int SKOSQuoteLib_AttachConnectCallBack(FOnConnect Connect);

//        [DllImport("SKOSQuoteLib.dll", CallingConvention = CallingConvention.StdCall)]
//        public static extern int SKOSQuoteLib_AttachQuoteCallBack(FOnGetStockIdx StockIdx);

//        [DllImport("SKOSQuoteLib.dll", CallingConvention = CallingConvention.StdCall)]
//        public static extern int SKOSQuoteLib_AttachTicksCallBack(FOnNotifyTicks StockIdx);

//        [DllImport("SKOSQuoteLib.dll", CallingConvention = CallingConvention.StdCall)]
//        public static extern int SKOSQuoteLib_AttachBest5CallBack(FOnGetStockIdx StockIdx);

//        [DllImport("SKOSQuoteLib.dll", CallingConvention = CallingConvention.StdCall)]
//        public static extern int SKOSQuoteLib_AttachOverseaProductsCallBack(FOnOverseaProducts OverseaProducts);

//        [DllImport("SKOSQuoteLib.dll", CallingConvention = CallingConvention.StdCall)]
//        public static extern int SKOSQuoteLib_AttachServerTimeCallBack(FOnNotifyServerTime ServerTime);

//        [DllImport("SKOSQuoteLib.dll", CallingConvention = CallingConvention.StdCall)]
//        public static extern int SKOSQuoteLib_AttachKLineDataCallBack(FOnNotifyKLineData KLineData);

//        [DllImport("SKOSQuoteLib.dll", CallingConvention = CallingConvention.StdCall)]
//        public static extern int SKOSQuoteLib_AttachHistoryTicksGetCallBack(FOnNotifyTicksGet TicksGet);

//        public static string GetApiCodeDefine(int nCode)
//        {
//            string strNCode = Enum.GetName(typeof(ApiMessage), nCode);

//            if (strNCode == "" || strNCode == null)
//            {
//                return nCode.ToString();
//            }
//            else
//            {
//                return strNCode;
//            }
//        }
//    }
//}
