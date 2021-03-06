﻿//using System;
//using System.Runtime.InteropServices;
//using System.Text;

//namespace Capital.Report.Class
//{
//    //----------------------------------------------------------------------
//    // SKOrderLib
//    //----------------------------------------------------------------------

//    //GetUserAccount、GetRealBalanceReport    Call Back Function Used
//    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
//    public delegate void FOnGetBSTR([MarshalAs(UnmanagedType.BStr)]string strAccount);

//    //OnOrderAsyncReport Call Back Function
//    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
//    public delegate void FOnOrderAsyncReport(int nThreadID, int nCode, [MarshalAs(UnmanagedType.BStr)]string strAccount);

//    //OnTSFilledOrder Call Back Function
//    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
//    public delegate void FOnTSFilledOrder([MarshalAs(UnmanagedType.BStr)]string bstrSymbol,
//                                            [MarshalAs(UnmanagedType.BStr)]string bstrDescription,
//                                            [MarshalAs(UnmanagedType.BStr)]string bstrOrderType,
//                                            [MarshalAs(UnmanagedType.BStr)]string bstrOrder,
//                                            int lFillPrice,
//                                            int lSlippage,
//                                            double dTimePlace,
//                                            double dTimeFilled,
//                                            [MarshalAs(UnmanagedType.BStr)]string bstrStrategy,
//                                            [MarshalAs(UnmanagedType.BStr)]string bstrSignal,
//                                            [MarshalAs(UnmanagedType.BStr)]string bstrWorkspace,
//                                            [MarshalAs(UnmanagedType.BStr)]string bstrInterval,
//                                            [MarshalAs(UnmanagedType.BStr)]string bstrPositionNumber,
//                                            [MarshalAs(UnmanagedType.BStr)]string bstrOrderNumber);

//    //OnTSActiveOrder Call Back Function
//    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
//    public delegate void FOnTSActiveOrder([MarshalAs(UnmanagedType.BStr)]string bstrSymbol,
//                                          [MarshalAs(UnmanagedType.BStr)]string bstrDescription,
//                                          [MarshalAs(UnmanagedType.BStr)]string bstrOrderType,
//                                          [MarshalAs(UnmanagedType.BStr)]string bstrOrder,
//                                          int lLastPrice,
//                                          double dTimePlaced,
//                                          [MarshalAs(UnmanagedType.BStr)]string bstrStrategy,
//                                          [MarshalAs(UnmanagedType.BStr)]string bstrSignal,
//                                          [MarshalAs(UnmanagedType.BStr)]string bstrWorkspace,
//                                          [MarshalAs(UnmanagedType.BStr)]string bstrInterval,
//                                          [MarshalAs(UnmanagedType.BStr)]string bstrPositionNumber,
//                                          [MarshalAs(UnmanagedType.BStr)]string bstrOrderNumber);

//    //OnTSCanceledOrder Call Back Function
//    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
//    public delegate void FOnTSCanceledOrder([MarshalAs(UnmanagedType.BStr)]string bstrSymbol,
//                                            [MarshalAs(UnmanagedType.BStr)]string bstrDescription,
//                                            [MarshalAs(UnmanagedType.BStr)]string bstrOrderType,
//                                            [MarshalAs(UnmanagedType.BStr)]string bstrOrder,
//                                            double dTimePlaced,
//                                            double dTimeCanceled,
//                                            [MarshalAs(UnmanagedType.BStr)]string bstrStrategy,
//                                            [MarshalAs(UnmanagedType.BStr)]string bstrSignal,
//                                            [MarshalAs(UnmanagedType.BStr)]string bstrWorkspace,
//                                            [MarshalAs(UnmanagedType.BStr)]string bstrInterval,
//                                            [MarshalAs(UnmanagedType.BStr)]string bstrPositionNumber,
//                                            [MarshalAs(UnmanagedType.BStr)]string bstrOrderNumber,
//                                            [MarshalAs(UnmanagedType.BStr)]string bstrCanceledNumber);

//    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
//    public delegate void FOnOverseaFutureOpenInterest([MarshalAs(UnmanagedType.BStr)]string strData);

//    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
//    public delegate void FOnGetExecutionReoprt([MarshalAs(UnmanagedType.BStr)]string strAccount);

//    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
//    public delegate void FOnOverseaProducts([MarshalAs(UnmanagedType.BStr)]string strProducts);

//    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
//    public delegate void FOnGetStockIdx(short nCode);

//    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
//    public delegate void FOnNotifyTicks(short sStockidx, int nPtr);

//    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
//    public delegate void FOnNotifyServerTime(short sHour , short sMinute,short sSecond);

//    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
//    public delegate void FOnNotifyKLineData([MarshalAs(UnmanagedType.BStr)]string strStockNo,[MarshalAs(UnmanagedType.BStr)]string strKData);

//    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
//    public delegate void FOnNotifyTicksGet(short sStockidx, int nPtr, int nTime, int nClose, int nQty);

//    public struct FOREIGN
//    {
//        /// <summary>
//        /// 系統自行定義的股票代碼
//        /// </summary>
//        public short m_sStockidx;
//        /// <summary>
//        /// 報價小數位數
//        /// </summary>
//        public short m_sDecimal;
//        /// <summary>
//        /// 分母
//        /// </summary>
//        public int m_nDenominator;
//        /// <summary>
//        /// 市場代號0x05海外商品
//        /// </summary>
//        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1)]
//        public string m_cMarketNo;
//        /// <summary>
//        /// 交易所代號
//        /// </summary>
//        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
//        public string m_caExchangeNo;
//        /// <summary>
//        /// 交易所名稱
//        /// </summary>
//        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
//        public string m_caExchangeName;
//        /// <summary>
//        /// 股票代號
//        /// </summary>
//        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
//        public string m_caStockNo;
//        /// <summary>
//        /// 股票名稱
//        /// </summary>
//        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
//        public string m_caStockName; 
//        /// <summary>
//        /// 開盤價
//        /// </summary>
//        public int m_nOpen;
//        /// <summary>
//        /// 最高價
//        /// </summary>
//        public int m_nHigh;
//        /// <summary>
//        /// 最低價
//        /// </summary>
//        public int m_nLow;
//        /// <summary>
//        /// 成交價
//        /// </summary>
//        public int m_nClose;
//        /// <summary>
//        /// 結算價
//        /// </summary>
//        public int m_dSettlePrice;
//        /// <summary>
//        /// 單量
//        /// </summary>
//        public int m_nTickQty;
//        /// <summary>
//        /// 昨收、參考價
//        /// </summary>
//        public int m_nRef;
//        /// <summary>
//        /// 買價
//        /// </summary>
//        public int m_nBid;
//        /// <summary>
//        /// 買量
//        /// </summary>
//        public int m_nBc;
//        /// <summary>
//        /// 賣價
//        /// </summary>
//        public int m_nAsk;
//        /// <summary>
//        /// 賣量
//        /// </summary>
//        public int m_nAc;
//        /// <summary>
//        /// 總量
//        /// </summary>
//        public int m_nTQty;
//    }

//    public struct TICK
//    {
//        /// <summary>
//        /// Index
//        /// </summary>
//        public int m_nPtr;
//        /// <summary>
//        /// 時間
//        /// </summary>
//        public int m_nTime;
//        /// <summary>
//        /// 成交價
//        /// </summary>
//        public int m_nClose;
//        /// <summary>
//        /// 成交量
//        /// </summary>
//        public int m_nQty;
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

//    //OverSea Product Struct
//    public struct SOfComProduct
//    {
//        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10)]
//        public string strExchange;

//        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10)]
//        public string strProductNo;

//        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10)]
//        public string strYearMonth;

//        public int sSpecialTradeType;

//        public double dMinJump;

//        public int nDenominator;

//        public int sDayTrade;
//    }

//    //----------------------------------------------------------------------
//    // SKReplyLib
//    //----------------------------------------------------------------------

//    // Define Connect Disconnect Call Back Function
//    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
//    public delegate void FOnConnect([MarshalAs(UnmanagedType.BStr)]string strAccount, int nErrorCode);

//    //Define OnData Call Back
//    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
//    public delegate void FOnData(IntPtr strData);
//    //public delegate void FOnData([MarshalAs(UnmanagedType.BStr)]string strData);

//    //Define OnComplete Call Back
//    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
//    public delegate void FOnComplete(int nComplete);

//    //Reply Data Struct
//    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
//    public struct DataItem
//    {
//        /// <summary>
//        /// 委託序號
//        /// </summary>
//        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
//        public string strKeyNo;
//        /// <summary>
//        /// 市場類別
//        /// </summary>
//        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 2)]
//        public string strMarketType;
//        /// <summary>
//        /// 委託種類
//        /// </summary>
//        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1)]
//        public string strType;
//        /// <summary>
//        /// 錯誤
//        /// </summary>
//        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1)]
//        public string strOrderErr;
//        /// <summary>
//        /// 上手代碼
//        /// </summary>
//        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 7)]
//        public string strBroker;
//        /// <summary>
//        /// 交易帳號
//        /// </summary>
//        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 7)]
//        public string strCustNo;
//        /// <summary>
//        /// 買賣別/價別/新平倉別
//        /// </summary>
//        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 3)]
//        public string strBuySell;
//        /// <summary>
//        /// 交易所
//        /// </summary>
//        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 7)]
//        public string strExchangeID;
//        /// <summary>
//        /// 商品代碼
//        /// </summary>
//        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
//        public string strComId;
//        /// <summary>
//        /// 履約價
//        /// </summary>  
//        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
//        public string strStrikePrice;
//        /// <summary>
//        /// 委託書號
//        /// </summary>
//        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
//        public string strOrderNo;
//        /// <summary>
//        /// 價格
//        /// </summary>
//        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
//        public string strPrice;
//        /// <summary>
//        /// 價格小數分子
//        /// </summary>
//        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
//        public string strNumerator;
//        /// <summary>
//        /// 價格小數分母
//        /// </summary>
//        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
//        public string strDenominator;
//        /// <summary>
//        /// 價格1
//        /// </summary>
//        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
//        public string strPrice1;
//        /// <summary>
//        /// 價格1小數分子
//        /// </summary>
//        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
//        public string strNumerator1;
//        /// <summary>
//        /// 價格1小數分母
//        /// </summary>
//        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
//        public string strDenominator1;
//        /// <summary>
//        /// 價格2
//        /// </summary>
//        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
//        public string strPrice2;
//        /// <summary>
//        /// 價格2小數分子
//        /// </summary>
//        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 5)]
//        public string strNumerator2;
//        /// <summary>
//        /// 價格2小數分母
//        /// </summary>
//        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
//        public string strDenominator2;
//        /// <summary>
//        /// 口數
//        /// </summary>
//        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
//        public string strQty;
//        /// <summary>
//        /// 異動前數量
//        /// </summary>
//        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
//        public string strBeforeQty;
//        /// <summary>
//        /// 異動後數量
//        /// </summary>
//        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
//        public string strAfterQty;
//        /// <summary>
//        /// 交易日期
//        /// </summary>
//        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
//        public string strDate;
//        /// <summary>
//        /// 交易時間
//        /// </summary>
//        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 6)]
//        public string strTime;
//        /// <summary>
//        /// 成交序號
//        /// </summary>
//        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
//        public string strOkSeq;
//        /// <summary>
//        /// 子帳帳號
//        /// </summary>
//        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 7)]
//        public string strSubID;
//        /// <summary>
//        /// 營業員編號
//        /// </summary>
//        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
//        public string strSaleNo;
//        /// <summary>
//        /// 委託界面
//        /// </summary>
//        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1)]
//        public string strAgent;
//    }
//    public enum MarketType
//    {
//        /// <summary>
//        /// 證券
//        /// </summary>
//        TS,
//        /// <summary>
//        /// 盤後
//        /// </summary>
//        TA,
//        /// <summary>
//        /// 零股
//        /// </summary>
//        TL,
//        /// <summary>
//        /// 期貨
//        /// </summary>
//        TF,
//        /// <summary>
//        /// 選擇權
//        /// </summary>
//        TO,
//        /// <summary>
//        /// 海外期貨
//        /// </summary>
//        OF
//    }
//    public enum OrderType
//    {
//        /// <summary>
//        /// 委託
//        /// </summary>
//        N,
//        /// <summary>
//        /// 取消
//        /// </summary>
//        C,
//        /// <summary>
//        /// 改量
//        /// </summary>
//        U,
//        /// <summary>
//        /// 成交
//        /// </summary>
//        D,
//        /// <summary>
//        /// 報價
//        /// </summary>
//        Q,
//        /// <summary>
//        /// 改價
//        /// </summary>
//        P
//    }
//    public enum OrderErr
//    {
//        /// <summary>
//        /// 失敗
//        /// </summary>
//        Y,
//        /// <summary>
//        /// 逾時
//        /// </summary>
//        T,
//        /// <summary>
//        /// 正常
//        /// </summary>
//        N
//    }
//    public enum BuySell
//    {
//        /// <summary>
//        /// 買
//        /// </summary>
//        B,
//        /// <summary>
//        /// 賣
//        /// </summary>
//        S
//    }
//    public enum PriceType
//    {
//        /// <summary>
//        /// 市價
//        /// </summary>
//        Market = 1,
//        /// <summary>
//        /// 限價
//        /// </summary>
//        Limit = 2,
//        /// <summary>
//        /// 停損
//        /// </summary>
//        Stop = 3,
//        /// <summary>
//        /// 停損限價
//        /// </summary>
//        StopLimit = 4,
//        /// <summary>
//        /// 收市
//        /// </summary>
//        CloseMarket = 5
//    }


//    enum ApiMessage
//    {
//        SK_SUCCESS = 0,
//        SK_ERROR_INITIALIZE_FAIL = 1000,
//        SK_ERROR_ACCOUNT_NOT_EXIST = 1001,
//        SK_ERROR_ACCOUNT_MARKET_NOT_MATCH = 1002,
//        SK_ERROR_PERIOD_OUT_OF_RANGE = 1003,
//        SK_ERROR_FLAG_OUT_OF_RANGE = 1004,
//        SK_ERROR_BUYSELL_OUT_OF_RANGE = 1005,
//        SK_ERROR_ORDER_SERVER_INVALID = 1006,
//        SK_ERROR_PERMISSION_DENIED = 1007,
//        SK_ERROR_TRADE_TYPR_OUT_OF_RANGE = 1008,
//        SK_ERROR_DAY_TRADE_OUT_OF_RANGE = 1009,
//        SK_ERROR_ORDER_SIGN_INVALID = 1010,
//        SK_ERROR_NEW_CLOSE_OUT_OF_RANGE = 1011,
//        SK_ERROR_PRODUCT_INVALID = 1012,
//        SK_ERROR_QTY_INVALID = 1013,
//        SK_ERROR_DAYTRADE_DENIED = 1014,
//        SK_ERROR_SPCIAL_TRADE_TYPE_INVALID = 1015,
//        SK_ERROR_PRICE_INVALID = 1016,
//        SK_ERROR_INDEX_OUT_OF_RANGE = 1017,
//        SK_ERROR_QUERY_IN_PROCESSING = 1018,
//        SK_ERROR_LOGIN_INVALID = 1019,
//        SK_ERROR_REGISTER_CALLBACK = 1020,
//        SK_ERROR_FUNCTION_PERMISSION_DENIED = 1021,
//        SK_ERROR_MARKET_OUT_OF_RANGE = 1022,
//        SK_ERROR_PERMISSION_TIMEOUT = 1023,
//        SK_ERROR_FOREIGNSTOCK_PRICE_OUT_OF_RANGE = 1024,
//        SK_ERROR_FOREIGNSTOCK_UNDEFINE_COINTYPE = 1025,
//        SK_ERROR_FOREIGNSTOCK_SAME_COINSTYPE = 1026,
//        SK_ERROR_FOREIGNSTOCK_SALE_SHOULD_ORIGINAL_COIN = 1027,
//        SK_ERROR_FOREIGNSTOCK_TRADE_UNIT_INVALID = 1028,
//        SK_ERROR_FOREIGNSTOCK_STOCKNO_INVALID = 1029,
//        SK_ERROR_FOREIGNSTOCK_ACCOUNTTYPE_INVALID = 1030,
//        SK_ERROR_FOREIGNSTOCK_INITIALIZE_FAIL = 1031,
//        SK_ERROR_TS_INITIALIZE_FAIL = 1032,
//        SK_ERROR_OVERSEA_TRADE_PRODUCT_FAIL = 1033,
//        SK_ERROR_OVERSEA_TRADE_DATA_NOT_COMPLETE = 1034,
//        SK_ERROR_CERT_VERIFY_CN_INVALID = 1035,
//        SK_ERROR_CERT_VERIFY_SERVER_REJECT = 1036,
//        SK_ERROR_CERT_NOT_VERIFIED = 1037,
//        SK_ERROR_SERVER_NOT_CONNECTED = 1038,
//        SK_ERROR_ORDER_LOCK = 1039,
//        SK_ERROR_DID_NOT_LOCK = 1040,
//        SK_WARNING_OF_COM_DATA_MISSING = 2001,
//        SK_WARNING_TS_READY = 2002,
//        SK_WARNING_LOGIN_ALREADY = 2003,
//        SK_WARNING_LOGIN_SPECIAL_ALREADY = 2004,
//        SK_FAIL = 3001
//    }
//    class Functions
//    {
//        #region SKOrderLib
//        //----------------------------------------------------------------------
//        // SKOrderLib
//        //----------------------------------------------------------------------

//        [DllImport("SKOrderLib.dll", EntryPoint = "SKOrderLib_Initialize", CharSet = CharSet.Ansi)]
//        public static extern int SKOrderLib_Initialize(string pcUserName, string pcPassword);

//        [DllImport("SKOrderLib.dll", EntryPoint = "SKOrderLib_InitializeTS", CharSet = CharSet.Ansi)]
//        public static extern int SKOrderLib_InitializeTS(string lpszTSCOMName);

//        [DllImport("SKOrderLib.dll", EntryPoint = "SKOrderLib_ReadCertByID", CharSet = CharSet.Ansi)]
//        public static extern int SKOrderLib_ReadCertByID(string lpszUserID);

//        [DllImport("SKOrderLib.dll")]
//        public static extern int GetUserAccount();

//        [DllImport("SKOrderLib.dll", EntryPoint = "SendStockOrder", SetLastError = true, CharSet = CharSet.Ansi)]
//        public static extern int SendStockOrder(string lpszAccount, string lpszStockNo, int usPeriod, int usFlag, int usBuySell, string lpszPrice, int nQty, [MarshalAs(UnmanagedType.LPStr)]StringBuilder buf, out Int32 pnMessageBufferSize);

//        [DllImport("SKOrderLib.dll", EntryPoint = "SendStockOrderAsync", CharSet = CharSet.Ansi)]
//        public static extern int SendStockOrderAsync(string lpszAccount, string lpszStockNo, int usPeriod, int usFlag, int usBuySell, string lpszPrice, int nQty);

//        [DllImport("SKOrderLib.dll", EntryPoint = "CancelOrderByStockNo", CharSet = CharSet.Ansi)]
//        public static extern int CancelOrderByStockNo(string lpszAccount, string lpszStockNo, [MarshalAs(UnmanagedType.LPStr)]StringBuilder buf, out Int32 pnMessageBufferSize);

//        [DllImport("SKOrderLib.dll", EntryPoint = "CancelOrderBySeqNo", CharSet = CharSet.Ansi)]
//        public static extern int CancelOrderBySeqNo(string lpszAccount, string lpszStockNo, [MarshalAs(UnmanagedType.LPStr)]StringBuilder buf, out Int32 pnMessageBufferSize);

//        [DllImport("SKOrderLib.dll", EntryPoint = "GetRealBalanceReport", CharSet = CharSet.Ansi)]
//        public static extern int GetRealBalanceReport(string lpszAccount);

//        [DllImport("SKOrderLib.dll", EntryPoint = "SendFutureOrder", SetLastError = true, CharSet = CharSet.Ansi)]
//        public static extern int SendFutureOrder(string lpszAccount, string lpszFutureNo, int usTradeType, int usDayTrade, int usBuySell, string lpszPrice, int nQty, [MarshalAs(UnmanagedType.LPStr)]StringBuilder buf, out Int32 pnMessageBufferSize);

//        [DllImport("SKOrderLib.dll", EntryPoint = "SendFutureOrderAsync", SetLastError = true, CharSet = CharSet.Ansi)]
//        public static extern int SendFutureOrderAsync(string lpszAccount, string lpszFutureNo, int usTradeType, int usDayTrade, int usBuySell, string lpszPrice, int nQty);

//        [DllImport("SKOrderLib.dll", EntryPoint = "GetOpenInterest", CharSet = CharSet.Ansi)]
//        public static extern int GetOpenInterest(string lpszAccount);

//        [DllImport("SKOrderLib.dll", EntryPoint = "SendOptionOrder", SetLastError = true, CharSet = CharSet.Ansi)]
//        public static extern int SendOptionOrder(string lpszAccount, string lpszFutureNo, int usTradeType, int usNewClose, int usBuySell, string lpszPrice, int nQty, [MarshalAs(UnmanagedType.LPStr)]StringBuilder buf, out Int32 pnMessageBufferSize);

//        [DllImport("SKOrderLib.dll", EntryPoint = "SendOptionOrderAsync", SetLastError = true, CharSet = CharSet.Ansi)]
//        public static extern int SendOptionOrderAsync(string lpszAccount, string lpszFutureNo, int usTradeType, int usNewClose, int usBuySell, string lpszPrice, int nQty);

//        [DllImport("SKOrderLib.dll", EntryPoint = "SendForeignStockOrder", SetLastError = true, CharSet = CharSet.Ansi)]
//        public static extern int SendForeignStockOrder(string lpszAccount, string lpszStockNo, string lpszExchangeNo, int usBuySell, string lpszPrice, int nQty, string lpszCurrency1, string lpszCurrency2, string lpszCurrency3, int iAccountType, [MarshalAs(UnmanagedType.LPStr)]StringBuilder buf, out Int32 pnMessageBufferSize);

//        [DllImport("SKOrderLib.dll", EntryPoint = "SendForeignStockOrderAsync", SetLastError = true, CharSet = CharSet.Ansi)]
//        public static extern int SendForeignStockOrderAsync(string lpszAccount, string lpszStockNo, string lpszExchangeNo, int usBuySell, string lpszPrice, int nQty, string lpszCurrency1, string lpszCurrency2, string lpszCurrency3, int iAccountType, [MarshalAs(UnmanagedType.LPStr)]StringBuilder buf, out Int32 pnMessageBufferSize);

//        [DllImport("SKOrderLib.dll", EntryPoint = "CancelForeignStockOrderByBookNo", SetLastError = true, CharSet = CharSet.Ansi)]
//        public static extern int CancelForeignStockOrderByBookNo(string lpszAccount, string lpszBookNo, string lpszExchangeNo, [MarshalAs(UnmanagedType.LPStr)]StringBuilder buf, out Int32 pnMessageBufferSize);

//        [DllImport("SKOrderLib.dll", EntryPoint = "CancelForeignStockOrderBySeqNo", SetLastError = true, CharSet = CharSet.Ansi)]
//        public static extern int CancelForeignStockOrderBySeqNo(string lpszAccount, string lpszSeqNo, string lpszExchangeNo, [MarshalAs(UnmanagedType.LPStr)]StringBuilder buf, out Int32 pnMessageBufferSize);

//        [DllImport("SKOrderLib.dll", EntryPoint = "SendOverseaFutureOrder", SetLastError = true, CharSet = CharSet.Ansi)]
//        public static extern int SendOverseaFutureOrder(string lpszAccount, string lpszTradeName, string lpszStockNo, string lpszYearMonth, int usBuySell, int usNewClose, int usDayTrade, int usTradeType, int usSpecialTradeType, int nQty, string lpszOrder, string lpszOrderNumerator, string lpszTrigger, string lpszTriggerNumerator, [MarshalAs(UnmanagedType.LPStr)]StringBuilder buf, out Int32 pnMessageBufferSize);

//        [DllImport("SKOrderLib.dll", EntryPoint = "SendOverseaFutureOrderAsync", SetLastError = true, CharSet = CharSet.Ansi)]
//        public static extern int SendOverseaFutureOrderAsync(string lpszAccount, string lpszTradeName, string lpszStockNo, string lpszYearMonth, int usBuySell, int usNewClose, int usDayTrade, int usTradeType, int usSpecialTradeType, int nQty, string lpszOrder, string lpszOrderNumerator, string lpszTrigger, string lpszTriggerNumerator);

//        [DllImport("SKOrderLib.dll", EntryPoint = "GetOverseaCount", SetLastError = true, CharSet = CharSet.Ansi)]
//        public static extern int GetOverseaCount();

//        [DllImport("SKOrderLib.dll", EntryPoint = "GetOverseaProducts", SetLastError = true, CharSet = CharSet.Ansi)]
//        public static extern int GetOverseaProducts(int nIndext, out SOfComProduct product);

//        [DllImport("SKOrderLib.dll", EntryPoint = "GetOverseaFutures", SetLastError = true, CharSet = CharSet.Ansi)]
//        public static extern int GetOverseaFutures();

//        [DllImport("SKOrderLib.dll", EntryPoint = "ReloadOverseaProducts", SetLastError = true, CharSet = CharSet.Ansi)]
//        public static extern int ReloadOverseaProducts();

//        [DllImport("SKOrderLib.dll", EntryPoint = "OverseaCancelOrderBySeqNo", CharSet = CharSet.Ansi)]
//        public static extern int OverseaCancelOrderBySeqNo(string lpszAccount, string lpszSeqNo, [MarshalAs(UnmanagedType.LPStr)]StringBuilder buf, out Int32 pnMessageBufferSize);

//        [DllImport("SKOrderLib.dll", EntryPoint = "OverseaDecreaseOrderBySeqNo", CharSet = CharSet.Ansi)]
//        public static extern int OverseaDecreaseOrderBySeqNo(string lpszAccount, string lpszSeqNo, int nDecreaseQty, [MarshalAs(UnmanagedType.LPStr)]StringBuilder buf, out Int32 pnMessageBufferSize);

//        [DllImport("SKOrderLib.dll", EntryPoint = "DecreaseOrderBySeqNo", CharSet = CharSet.Ansi)]
//        public static extern int DecreaseOrderBySeqNo(string lpszAccount, string lpszSeqNo, int nDecreaseQty, [MarshalAs(UnmanagedType.LPStr)]StringBuilder buf, out Int32 pnMessageBufferSize);

//        [DllImport("SKOrderLib.dll", EntryPoint = "SendFutureStopLoss", CharSet = CharSet.Ansi)]
//        public static extern int SendFutureStopLoss(string lpszAccount, string lpszFutureNo, int usTradeType, int usDayTrade, int usBuySell, string lpszPrice, int nQty, string lpszTriggerPrice, [MarshalAs(UnmanagedType.LPStr)]StringBuilder buf, out Int32 pnMessageBufferSize);

//        [DllImport("SKOrderLib.dll", EntryPoint = "CancelFutureStopLoss", CharSet = CharSet.Ansi)]
//        public static extern int CancelFutureStopLoss(string lpszAccount, string lpszBookNo, string lpszSymbol, string lpszBuySell, string lpszPrice, string lpszQty, string lpszTriggerPrice, string lpszTradeType, string lpszDayTrade, [MarshalAs(UnmanagedType.LPStr)]StringBuilder buf, out Int32 pnMessageBufferSize);

//        [DllImport("SKOrderLib.dll", EntryPoint = "GetStopLossReport", CharSet = CharSet.Ansi)]
//        public static extern int GetStopLossReport(string lpszAccount, int nReportStatus, string lpszType);

//        [DllImport("SKOrderLib.dll", EntryPoint = "SendMovingStopLoss", CharSet = CharSet.Ansi)]
//        public static extern int SendMovingStopLoss(string lpszAccount, string lpszFutureNo, int usTradeType, int usDayTrade, int usBuySell, string lpszPrice, int nQty, string lpszMovingPoint, [MarshalAs(UnmanagedType.LPStr)]StringBuilder buf, out Int32 pnMessageBufferSize);

//        [DllImport("SKOrderLib.dll", EntryPoint = "CancelMovingStopLoss", CharSet = CharSet.Ansi)]
//        public static extern int CancelMovingStopLoss(string lpszAccount, string lpszBookNo, string lpszSymbol, string lpszBuySell, string lpszPrice, string lpszQty, string lpszMovingPoint, string lpszTradeType, string lpszDayTrade, [MarshalAs(UnmanagedType.LPStr)]StringBuilder buf, out Int32 pnMessageBufferSize);

//        [DllImport("SKOrderLib.dll", EntryPoint = "SendOptionStopLoss", CharSet = CharSet.Ansi)]
//        public static extern int SendOptionStopLoss(string lpszAccount, string lpszFutureNo, int usTradeType, int usNewClose, int usBuySell, string lpszPrice, int nQty, string lpszMovingPoint, [MarshalAs(UnmanagedType.LPStr)]StringBuilder buf, out Int32 pnMessageBufferSize);

//        [DllImport("SKOrderLib.dll", EntryPoint = "CancelOptionStopLoss", CharSet = CharSet.Ansi)]
//        public static extern int CancelOptionStopLoss(string lpszAccount, string lpszBookNo, string lpszSymbol, string lpszBuySell, string lpszPrice, string lpszQty, string lpszMovingPoint, string lpszTradeType, string lpszDayTrade, [MarshalAs(UnmanagedType.LPStr)]StringBuilder buf, out Int32 pnMessageBufferSize);

//        [DllImport("SKOrderLib.dll", EntryPoint = "GetOverseaFutureOpenInterest", CharSet = CharSet.Ansi)]
//        public static extern int GetOverseaFutureOpenInterest(string lpszAccount);

//        [DllImport("SKOrderLib.dll", EntryPoint = "GetLastLogMessage", CharSet = CharSet.Ansi)]
//        public static extern int GetLastLogMessage([MarshalAs(UnmanagedType.LPStr)]StringBuilder buf, out Int32 pnMessageBufferSize);

//        [DllImport("SKOrderLib.dll", EntryPoint = "UnlockOrder", CharSet = CharSet.Ansi)]
//        public static extern int UnlockOrder(string strMarketType);

//        [DllImport("SKOrderLib.dll", EntryPoint = "GetExecutionReport", CharSet = CharSet.Ansi)]
//        public static extern int GetExecutionReport(string lpszAccount, string lpszStockNo, int nMarket, int nBuySell, int nDataNum, int nType);

//        [DllImport("SKOrderLib.dll", EntryPoint = "CorrectPriceBySeqNo", CharSet = CharSet.Ansi)]
//        public static extern int CorrectPriceBySeqNo(string lpszAccount, string lpszSeqNo, string lpszPrice, int nTradeType, [MarshalAs(UnmanagedType.LPStr)]StringBuilder buf, out Int32 pnMessageBufferSize);

//        [DllImport("SKOrderLib.dll", EntryPoint = "CorrectPriceByBookNo", CharSet = CharSet.Ansi)]
//        public static extern int CorrectPriceByBookNo(string lpszAccount, string lpszMarketType, string lpszBookNo, string lpszPrice, int nTradeType, [MarshalAs(UnmanagedType.LPStr)]StringBuilder buf, out Int32 pnMessageBufferSize);



//        [DllImport("SKOrderLib.dll", CallingConvention = CallingConvention.StdCall)]
//        public static extern int RegisterOnAccountCallBack(FOnGetBSTR Account);

//        [DllImport("SKOrderLib.dll", CallingConvention = CallingConvention.StdCall)]
//        public static extern int RegisterOnOrderAsyncReportCallBack(FOnOrderAsyncReport OrderAsync);

//        [DllImport("SKOrderLib.dll", CallingConvention = CallingConvention.StdCall)]
//        public static extern int RegisterOnRealBalanceReportCallBack(FOnGetBSTR RealBalanceReport);

//        [DllImport("SKOrderLib.dll", CallingConvention = CallingConvention.StdCall)]
//        public static extern int RegisterOnOpenInterestCallBack(FOnGetBSTR OnOpenInterest);

//        [DllImport("SKOrderLib.dll", CallingConvention = CallingConvention.StdCall)]
//        public static extern int RegisterOnOverseaFuturesCallBack(FOnGetBSTR OnOpenInterest);

//        [DllImport("SKOrderLib.dll", CallingConvention = CallingConvention.StdCall)]
//        public static extern int RegisterOnTSFilledOrderCallBack(FOnTSFilledOrder OnTSFilledOrder);

//        [DllImport("SKOrderLib.dll", CallingConvention = CallingConvention.StdCall)]
//        public static extern int RegisterOnTSActiveOrderCallBack(FOnTSActiveOrder OnTSActiveOrder);

//        [DllImport("SKOrderLib.dll", CallingConvention = CallingConvention.StdCall)]
//        public static extern int RegisterOnTSCanceledOrderCallBack(FOnTSCanceledOrder OnTSCanceledOrder);

//        [DllImport("SKOrderLib.dll", CallingConvention = CallingConvention.StdCall)]
//        public static extern int RegisterOnStopLossReportCallBack(FOnGetBSTR OnTSCanceledOrder);

//        [DllImport("SKOrderLib.dll", CallingConvention = CallingConvention.StdCall)]
//        public static extern int RegisterOnOverseaFutureOpenInterestCallBack(FOnOverseaFutureOpenInterest OnOverseaFutureOpenInterest);

//        [DllImport("SKOrderLib.dll", CallingConvention = CallingConvention.StdCall)]
//        public static extern int RegisterOnExecutionReportCallBack(FOnGetExecutionReoprt ExecutionReport);

//        #endregion

//        #region SKReplyLib
//        //----------------------------------------------------------------------
//        // SKReplyLib
//        //----------------------------------------------------------------------

//        [DllImport("SKReplyLib.dll", EntryPoint = "SKReplyLib_Initialize", CharSet = CharSet.Ansi)]
//        public static extern int SKReplyLib_Initialize(string pcUserName, string pcPassword);

//        [DllImport("SKReplyLib.dll", EntryPoint = "SKReplyLib_ConnectByID", CharSet = CharSet.Ansi)]
//        public static extern int SKReplyLib_ConnectByID(string pcUserID);

//        [DllImport("SKReplyLib.dll", EntryPoint = "SKReplyLib_IsConnectedByID", CharSet = CharSet.Ansi)]
//        public static extern int SKReplyLib_IsConnectedByID(string pcUserID);

//        [DllImport("SKReplyLib.dll", EntryPoint = "SKReplyLib_CloseByID", CharSet = CharSet.Ansi)]
//        public static extern int SKReplyLib_CloseByID(string pcUserID);


//        [DllImport("SKReplyLib.dll", CallingConvention = CallingConvention.StdCall)]
//        public static extern int RegisterOnConnectCallBack(FOnConnect Connect);

//        [DllImport("SKReplyLib.dll", CallingConvention = CallingConvention.StdCall)]
//        public static extern int RegisterOnDisconnectCallBack(FOnConnect Connect);

//        [DllImport("SKReplyLib.dll", CallingConvention = CallingConvention.StdCall)]
//        public static extern int RegisterOnDataCallBack(FOnData Data);


//        [DllImport("SKReplyLib.dll", CallingConvention = CallingConvention.StdCall)]
//        public static extern int RegisterOnCompleteCallBack(FOnComplete Complete);


//        #endregion
//    }
//}
