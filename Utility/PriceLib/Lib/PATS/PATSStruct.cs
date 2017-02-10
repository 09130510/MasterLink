using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace PriceLib.PATS
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public unsafe struct ExchangeStruct
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string Name;
        [MarshalAs(UnmanagedType.I1, SizeConst = PATSLib.SIZE_OF_CHAR + 1)]
        public char QueryEnabled;
        [MarshalAs(UnmanagedType.I1, SizeConst = PATSLib.SIZE_OF_CHAR + 1)]
        public char AmendEnabled;
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int Strategy;
        [MarshalAs(UnmanagedType.I1, SizeConst = PATSLib.SIZE_OF_CHAR + 1)]
        public char CustomDecs;
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int Decimals;
        [MarshalAs(UnmanagedType.I1, SizeConst = PATSLib.SIZE_OF_CHAR + 1)]
        public char TicketType;
        [MarshalAs(UnmanagedType.I1, SizeConst = PATSLib.SIZE_OF_CHAR + 1)]
        public char RFQA;
        [MarshalAs(UnmanagedType.I1, SizeConst = PATSLib.SIZE_OF_CHAR + 1)]
        public char RFQT;
        [MarshalAs(UnmanagedType.I1, SizeConst = PATSLib.SIZE_OF_CHAR + 1)]
        public char EnableBlock;
        [MarshalAs(UnmanagedType.I1, SizeConst = PATSLib.SIZE_OF_CHAR + 1)]
        public char EnableBasis;
        [MarshalAs(UnmanagedType.I1, SizeConst = PATSLib.SIZE_OF_CHAR + 1)]
        public char EnableAA;
        [MarshalAs(UnmanagedType.I1, SizeConst = PATSLib.SIZE_OF_CHAR + 1)]
        public char EnableCross;
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int GTStatus;
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public unsafe struct TraderAcctStruct
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string TraderAccount;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string BackOfficeID;
        [MarshalAs(UnmanagedType.I1, SizeConst = PATSLib.SIZE_OF_CHAR + 1)]
        public char Tradable;
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int LossLimit;

        public string AcctNo { get { return TraderAccount; } }
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public unsafe struct OrderTypeStruct
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string OrderType;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string ExchangeName;
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int OrderTypeID;
        [MarshalAs(UnmanagedType.U1, SizeConst = PATSLib.SIZE_OF_BYTE)]
        public byte NumPricesReqd;
        [MarshalAs(UnmanagedType.U1, SizeConst = PATSLib.SIZE_OF_BYTE)]
        public byte NumVolumesReqd;
        [MarshalAs(UnmanagedType.U1, SizeConst = PATSLib.SIZE_OF_BYTE)]
        public byte NumDatesReqd;
        [MarshalAs(UnmanagedType.I1, SizeConst = PATSLib.SIZE_OF_CHAR + 1)]
        public char AutoCreated;
        [MarshalAs(UnmanagedType.I1, SizeConst = PATSLib.SIZE_OF_CHAR + 1)]
        public char TimeTriggered;
        [MarshalAs(UnmanagedType.I1, SizeConst = PATSLib.SIZE_OF_CHAR + 1)]
        public char RealSynthetic;
        [MarshalAs(UnmanagedType.I1, SizeConst = PATSLib.SIZE_OF_CHAR + 1)]
        public char GTCFlag;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY2 + 1)]
        public string TicketType;
        [MarshalAs(UnmanagedType.I1, SizeConst = PATSLib.SIZE_OF_CHAR + 1)]
        public char PatsOrderType;
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int AmendOTCount;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY50 + 1)]
        public string AlgoXML;
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public unsafe struct CommodityStruct
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string ExchangeName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string ContractName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string Currency;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string Group;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string OnePoint;
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int TicksPerPoint;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string TickSize;
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int GTStatus;
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public unsafe struct ContractStruct
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string ContractName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY50 + 1)]
        public string ContractDate;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string ExchangeName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY8 + 1)]
        public string ExpiryDate;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY8 + 1)]
        public string LastTradeDate;
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int NumberOfLegs;
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int TicksPerPoint;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string TickSize;
        [MarshalAs(UnmanagedType.I1, SizeConst = PATSLib.SIZE_OF_CHAR + 1)]
        public char Tradable;
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int GTStatus;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string Margin;
        [MarshalAs(UnmanagedType.I1, SizeConst = PATSLib.SIZE_OF_CHAR + 1)]
        public char ESATemplate;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY16 + 1)]
        public string MarketRef;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string lnExchangeName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string lnContractName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY50 + 1)]
        public string lnContractDate;
        //[MarshalAs(UnmanagedType.ByValArray, SizeConst = PATSLib.SIZE_OF_ARRAY10*(4 + 1))]
        //      LegStruct ExternalID[2];
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public ExternalID[] LegStruct;

        public string Key { get { return $"{ExchangeName},{ContractName},{ContractDate}"; } }
        
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public unsafe struct ExternalID
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = PATSLib.SIZE_OF_ARRAY5 + 1)]
        public StratLegStruct[] Leg;
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public unsafe struct ExtendedContractStruct
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string ContractName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY50 + 1)]
        public string ContractDate;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string ExchangeName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY8 + 1)]
        public string ExpiryDate;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY8 + 1)]
        public string LastTradeDate;
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int NumberOfLegs;
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int TicksPerPoint;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string TickSize;
        [MarshalAs(UnmanagedType.U1, SizeConst = PATSLib.SIZE_OF_BYTE)]
        public byte Tradable;
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int GTStatus;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string Margin;
        [MarshalAs(UnmanagedType.I1, SizeConst = PATSLib.SIZE_OF_CHAR + 1)]
        public char ESATemplate;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY16 + 1)]
        public string MarketRef;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string lnExchangeName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string lnContractName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY50 + 1)]
        public string lnContractDate;
        //LegStruct ExternalID[16];
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public ExternalID[] LegStruct;
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public unsafe struct LogonStatusStruct
    {
        [MarshalAs(UnmanagedType.U1, SizeConst = PATSLib.SIZE_OF_BYTE)]
        public byte Status;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY60 + 1)]
        public string Reason;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string DefaultTraderAccount;
        [MarshalAs(UnmanagedType.I1, SizeConst = PATSLib.SIZE_OF_CHAR + 1)]
        public char ShowReason;
        [MarshalAs(UnmanagedType.I1, SizeConst = PATSLib.SIZE_OF_CHAR + 1)]
        public char DOMEnabled;
        [MarshalAs(UnmanagedType.I1, SizeConst = PATSLib.SIZE_OF_CHAR + 1)]
        public char PostTradeAmend;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY255 + 1)]
        public string UserName;
        [MarshalAs(UnmanagedType.I1, SizeConst = PATSLib.SIZE_OF_CHAR + 1)]
        public char GTEnabled;
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public unsafe struct LogonStruct
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY255 + 1)]
        public string UserID;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY255 + 1)]
        public string Password;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY255 + 1)]
        public string NewPassword;
        [MarshalAs(UnmanagedType.I1, SizeConst = PATSLib.SIZE_OF_CHAR + 1)]
        public char Reset;
        [MarshalAs(UnmanagedType.I1, SizeConst = PATSLib.SIZE_OF_CHAR + 1)]
        public char Reports;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string OTPassword;
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public unsafe struct MessageStruct
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string MsgID;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY500 + 1)]
        public string MsgText;
        [MarshalAs(UnmanagedType.I1, SizeConst = PATSLib.SIZE_OF_CHAR + 1)]
        public char IsAlert;
        [MarshalAs(UnmanagedType.I1, SizeConst = PATSLib.SIZE_OF_CHAR + 1)]
        public char Status;
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public unsafe struct LinkStateStruct
    {
        [MarshalAs(UnmanagedType.U1, SizeConst = PATSLib.SIZE_OF_BYTE)]
        public byte OldState;
        [MarshalAs(UnmanagedType.U1, SizeConst = PATSLib.SIZE_OF_BYTE)]
        public byte NewState;
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public unsafe struct ContractUpdStruct
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string ExchangeName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string ContractName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY50 + 1)]
        public string ContractDate;
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public unsafe struct CommodityUpdStruct
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string ExchangeName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string ContractName;
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public unsafe struct ExchangeUpdStruct
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string ExchangeName;
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public unsafe struct DOMUpdStruct
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string ExchangeName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string ContractName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY50 + 1)]
        public string ContractDate;
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public unsafe struct PriceUpdStruct
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string ExchangeName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string ContractName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY50 + 1)]
        public string ContractDate;

        public string Key { get { return $"{ExchangeName},{ContractName},{ContractDate}"; } }
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public unsafe struct AtBestUpdStruct
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string ExchangeName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string ContractName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY50 + 1)]
        public string ContractDate;
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public unsafe struct SubscriberDepthUpdStruct
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string ExchangeName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string ContractName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY50 + 1)]
        public string ContractDate;
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public unsafe struct StatusUpdStruct
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string ExchangeName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string ContractName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY50 + 1)]
        public string ContractDate;
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int Status;
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public unsafe struct FillUpdStruct
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string OrderID;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY70 + 1)]
        public string FillID;
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public unsafe struct OrderUpdStruct
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string OrderID;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string OldOrderID;
        [MarshalAs(UnmanagedType.U1, SizeConst = PATSLib.SIZE_OF_BYTE)]
        public byte OrderStatus;
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int OFSeqNumber;
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int OrderTypeId;
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public unsafe struct VTSUpdStruct
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string Exchange;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string Commodity;
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int Count;
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public unsafe struct SettlementPriceStruct
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string ExchangeName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string ContractName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY50 + 1)]
        public string ContractDate;
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int SettlementType;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string Price;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY6 + 1)]
        public string Time;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY8 + 1)]
        public string Date;
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public unsafe struct StrategyCreateSuccessStruct
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string UserName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string ExchangeName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string ContractName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY50 + 1)]
        public string ReqContractDate;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY50 + 1)]
        public string GenContractDate;
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public unsafe struct StrategyCreateFailureStruct
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string UserName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string ExchangeName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string ContractName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY50 + 1)]
        public string ContractDate;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY60 + 1)]
        public string Text;
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public unsafe struct BlankPriceStruct
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string ExchangeName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string ContractName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY50 + 1)]
        public string ContractDate;
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public unsafe struct ExchangeRateUpdStruct
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string Currency;
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public unsafe struct ConnectivityStatusUpdStruct
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY36 + 1)]
        public string DeviceLabel;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY3 + 1)]
        public string DeviceType;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY3 + 1)]
        public string Status;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY3 + 1)]
        public string Severity;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string DeviceName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY255 + 1)]
        public string Commentary;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string ExchangeID;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string Owner;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY14 + 1)]
        public string TimeStamp;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string SystemID;
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public unsafe struct AtBestStruct
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY3 + 1)]
        public string Firm;
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int Volume;
        [MarshalAs(UnmanagedType.I1, SizeConst = PATSLib.SIZE_OF_CHAR + 1)]
        public char BestType;
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public unsafe struct AtBestPriceStruct
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string BidPrice;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string OfferPrice;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY3 + 1)]
        public string LastBuyer;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY3 + 1)]
        public string LastSeller;
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public unsafe struct TickerUpdStruct
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string ExchangeName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string ContractName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY50 + 1)]
        public string ContractDate;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string BidPrice;
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int BidVolume;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string OfferPrice;
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int OfferVolume;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string LastPrice;
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int LastVolume;
        [MarshalAs(UnmanagedType.I1, SizeConst = PATSLib.SIZE_OF_CHAR + 1)]
        public char Bid;
        [MarshalAs(UnmanagedType.I1, SizeConst = PATSLib.SIZE_OF_CHAR + 1)]
        public char Offer;
        [MarshalAs(UnmanagedType.I1, SizeConst = PATSLib.SIZE_OF_CHAR + 1)]
        public char Last;

        public string Key { get { return $"{ExchangeName},{ContractName},{ContractDate}"; } }
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public unsafe struct SubscriberDepthStruct
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string Price;
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int Volume;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY3 + 1)]
        public string Firm;
        [MarshalAs(UnmanagedType.I1, SizeConst = PATSLib.SIZE_OF_CHAR + 1)]
        public char DepthType;
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public unsafe struct FillStruct
    {
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int Index;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY70 + 1)]
        public string FillId;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string ExchangeName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string ContractName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY50 + 1)]
        public string ContractDate;
        [MarshalAs(UnmanagedType.I1, SizeConst = PATSLib.SIZE_OF_CHAR + 1)]
        public char BuyOrSell;
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int Lots;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string Price;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string OrderID;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY8 + 1)]
        public string DateFilled;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY6 + 1)]
        public string TimeFilled;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY8 + 1)]
        public string DateHostRecd;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY6 + 1)]
        public string TimeHostRecd;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY30 + 1)]
        public string ExchOrderId;
        [MarshalAs(UnmanagedType.U1, SizeConst = PATSLib.SIZE_OF_BYTE)]
        public byte FillType;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string TraderAccount;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string UserName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY70 + 1)]
        public string ExchangeFillID;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string ExchangeRawPrice;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY70 + 1)]
        public string ExecutionID;
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int GTStatus;
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int SubType;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string CounterParty;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY2 + 1)]
        public string Leg;

        public string Key { get { return $"{ExchangeName},{ContractName},{ContractDate}"; } }
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public unsafe struct PositionStruct
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string Profit;
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int Buys;
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int Sells;
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public unsafe struct PriceDetailStruct
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string Price;
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int Volume;
        [MarshalAs(UnmanagedType.U1, SizeConst = PATSLib.SIZE_OF_BYTE)]
        public byte AgeCounter;
        [MarshalAs(UnmanagedType.U1, SizeConst = PATSLib.SIZE_OF_BYTE)]
        public byte Direction;
        [MarshalAs(UnmanagedType.U1, SizeConst = PATSLib.SIZE_OF_BYTE)]
        public byte Hour;
        [MarshalAs(UnmanagedType.U1, SizeConst = PATSLib.SIZE_OF_BYTE)]
        public byte Minute;
        [MarshalAs(UnmanagedType.U1, SizeConst = PATSLib.SIZE_OF_BYTE)]
        public byte Second;
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public unsafe struct PriceStruct
    {
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct Bid;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct Offer;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct ImpliedBid;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct ImpliedOffer;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct RFQ;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct Last0;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct Last1;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct Last2;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct Last3;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct Last4;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct Last5;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct Last6;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct Last7;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct Last8;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct Last9;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct Last10;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct Last11;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct Last12;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct Last13;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct Last14;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct Last15;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct Last16;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct Last17;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct Last18;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct Last19;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct Total;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct High;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct Low;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct Opening;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct Closing;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct BidDOM0;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct BidDOM1;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct BidDOM2;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct BidDOM3;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct BidDOM4;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct BidDOM5;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct BidDOM6;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct BidDOM7;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct BidDOM8;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct BidDOM9;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct BidDOM10;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct BidDOM11;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct BidDOM12;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct BidDOM13;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct BidDOM14;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct BidDOM15;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct BidDOM16;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct BidDOM17;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct BidDOM18;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct BidDOM19;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct OfferDOM0;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct OfferDOM1;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct OfferDOM2;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct OfferDOM3;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct OfferDOM4;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct OfferDOM5;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct OfferDOM6;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct OfferDOM7;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct OfferDOM8;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct OfferDOM9;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct OfferDOM10;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct OfferDOM11;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct OfferDOM12;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct OfferDOM13;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct OfferDOM14;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct OfferDOM15;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct OfferDOM16;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct OfferDOM17;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct OfferDOM18;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct OfferDOM19;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct LimitUp;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct LimitDown;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct ExecutionUp;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct ExecutionDown;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct ReferencePrice;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct pvCurrStl;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct pvSODStl;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct pvNewStl;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct pvIndBid;
        [MarshalAs(UnmanagedType.Struct, SizeConst = PATSLib.SIZE_OF_ARRAY30)]
        public PriceDetailStruct pvIndOffer;
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int Status;
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int ChangeMask;
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int PriceStatus;

        private PriceDetailStruct[] m_BidDOMs;
        public PriceDetailStruct[] BidDOMs
        {
            get
            {
                if (m_BidDOMs == null)
                {
                    m_BidDOMs = new PriceDetailStruct[20] { BidDOM0, BidDOM1, BidDOM2, BidDOM3, BidDOM4, BidDOM5, BidDOM6, BidDOM7, BidDOM8, BidDOM9, BidDOM10, BidDOM11, BidDOM12, BidDOM13, BidDOM14, BidDOM15, BidDOM16, BidDOM17, BidDOM18, BidDOM19 };
                }
                return m_BidDOMs;
            }
        }
        private PriceDetailStruct[] m_OfferDOMs;
        public PriceDetailStruct[] OfferDOMs
        {
            get
            {
                if (m_OfferDOMs == null)
                {
                    m_OfferDOMs = new PriceDetailStruct[20] { OfferDOM0, OfferDOM1, OfferDOM2, OfferDOM3, OfferDOM4, OfferDOM5, OfferDOM6, OfferDOM7, OfferDOM8, OfferDOM9, OfferDOM10, OfferDOM11, OfferDOM12, OfferDOM13, OfferDOM14, OfferDOM15, OfferDOM16, OfferDOM17, OfferDOM18, OfferDOM19 };
                }
                return m_OfferDOMs;
            }
        }
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public unsafe struct NewAggOrderStruct
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string TraderAccount;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string ExchangeName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string ContractName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY50 + 1)]
        public string ContractDate;
        [MarshalAs(UnmanagedType.I1, SizeConst = PATSLib.SIZE_OF_CHAR + 1)]
        public char BuyOrSell;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string AveragePrice;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY25 + 1)]
        public string Reference;
        [MarshalAs(UnmanagedType.I1, SizeConst = PATSLib.SIZE_OF_CHAR + 1)]
        public char DoneForDay;
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int Xref;
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public unsafe struct NewCustReqStruct
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string TraderAccount;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string ExchangeName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string ContractName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY50 + 1)]
        public string ContractDate;
        [MarshalAs(UnmanagedType.I1, SizeConst = PATSLib.SIZE_OF_CHAR + 1)]
        public char BuyOrSell;
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int ActualAmount;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string OrderType;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string Price;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string Price2;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string ParentID;
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int TotalVolume;
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int CumulativeVol;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string AveragePrice;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY25 + 1)]
        public string Reference;
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int Xref;
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public unsafe struct NewOrderStruct
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string TraderAccount;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string OrderType;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string ExchangeName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string ContractName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY50 + 1)]
        public string ContractDate;
        [MarshalAs(UnmanagedType.I1, SizeConst = PATSLib.SIZE_OF_CHAR + 1)]
        public char BuyOrSell;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string Price;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string Price2;
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int Lots;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string LinkedOrder;
        [MarshalAs(UnmanagedType.I1, SizeConst = PATSLib.SIZE_OF_CHAR + 1)]
        public char OpenOrClose;
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int Xref;
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int XrefP;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY8 + 1)]
        public string GoodTillDate;
        [MarshalAs(UnmanagedType.I1, SizeConst = PATSLib.SIZE_OF_CHAR + 1)]
        public char TriggerNow;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY25 + 1)]
        public string Reference;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY50 + 1)]
        public string ESARef;
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int Priority;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY8 + 1)]
        public string TriggerDate;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY6 + 1)]
        public string TriggerTime;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string BatchID;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string BatchType;
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int BatchCount;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string BatchStatus;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string ParentID;
        [MarshalAs(UnmanagedType.I1, SizeConst = PATSLib.SIZE_OF_CHAR + 1)]
        public char DoneForDay;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY255 + 1)]
        public string BigRefField;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY32 + 1)]
        public string SenderLocationID;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string Rawprice;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string Rawprice2;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY70 + 1)]
        public string ExecutionID;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string ClientID;
        [MarshalAs(UnmanagedType.I1, SizeConst = PATSLib.SIZE_OF_CHAR + 1)]
        public char APIM;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string APIMUser;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string YDSPAudit;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string ICSNearLegPrice;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string ICSFarLegPrice;
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int MinClipSize;
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int MaxClipSize;
        [MarshalAs(UnmanagedType.I1, SizeConst = PATSLib.SIZE_OF_CHAR + 1)]
        public char Randomise;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY2 + 1)]
        public string TicketType;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY3 + 1)]
        public string TicketVersion;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string ExchangeField;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string BOFID;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY5 + 1)]
        public string Badge;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string LocalUserName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string LocalTrader;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string LocalBOF;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string LocalOrderID;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string LocalExAcct;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string RoutingID1;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string RoutingID2;
        [MarshalAs(UnmanagedType.I1, SizeConst = PATSLib.SIZE_OF_CHAR + 1)]
        public char Inactive;
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public unsafe struct ProtectionStruc
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string Pr1_Price;
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int Pr1_Volume;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string Pr2_Price;
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int Pr2_Volume;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string Pr3_Price;
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int Pr3_Volume;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string St_Type;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string St_Price;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string St_Step_1;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string St_Step_2;
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public unsafe struct BEPNewOrderStruct
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public NewOrderStruct[] NewOrders;
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public unsafe struct BEPOrderIDStruct
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string OrderIDs;
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public unsafe struct AmendOrderStruct
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string Price;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string Price2;
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int Lots;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string LinkedOrder;
        [MarshalAs(UnmanagedType.I1, SizeConst = PATSLib.SIZE_OF_CHAR + 1)]
        public char OpenOrClose;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string Trader;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY25 + 1)]
        public string Reference;
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int Priority;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY8 + 1)]
        public string TriggerDate;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY6 + 1)]
        public string TriggerTime;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string BatchID;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string BatchType;
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int BatchCount;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string BatchStatus;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string ParentID;
        [MarshalAs(UnmanagedType.I1, SizeConst = PATSLib.SIZE_OF_CHAR + 1)]
        public char DoneForDay;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY255 + 1)]
        public string BigRefField;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY32 + 1)]
        public string SenderLocationID;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string Rawprice;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string Rawprice2;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY70 + 1)]
        public string ExecutionID;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string ClientID;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY50 + 1)]
        public string ESARef;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string YDSPAudit;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string ICSNearLegPrice;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string ICSFarLegPrice;
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int MaxClipSize;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string LocalUserName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string LocalTrader;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string LocalBOF;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string LocalOrderID;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string LocalExAcct;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string RoutingID1;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string RoutingID2;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string AmendOrderType;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string TargetUserName;
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public unsafe struct AmendingOrderStruct
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string OrderID;
        /// <summary>
        /// utInvalid = 0,
        /// utAmend = 1,
        /// utCancel = 2,
        /// utDeactivate = 3
        /// </summary>
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int UpdateType;
        [MarshalAs(UnmanagedType.Struct)]
        public AmendOrderStruct AmendDetails;
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int CancelTimeOut;
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public unsafe struct CrossingOrderIDs
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string PrimaryOrder;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string SecondaryOrder;
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public unsafe struct OrderDetailStruct
    {
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int Index;
        [MarshalAs(UnmanagedType.I1, SizeConst = PATSLib.SIZE_OF_CHAR + 1)]
        public char Historic;
        [MarshalAs(UnmanagedType.I1, SizeConst = PATSLib.SIZE_OF_CHAR + 1)]
        public char Checked;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string OrderID;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string DisplayID;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY30 + 1)]
        public string ExchOrderID;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string UserName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string TraderAccount;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string OrderType;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string ExchangeName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string ContractName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY50 + 1)]
        public string ContractDate;
        [MarshalAs(UnmanagedType.I1, SizeConst = PATSLib.SIZE_OF_CHAR + 1)]
        public char BuyOrSell;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string Price;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string Price2;
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int Lots;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string LinkedOrder;
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int AmountFilled;
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int NoOfFills;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string AveragePrice;
        [MarshalAs(UnmanagedType.U1, SizeConst = PATSLib.SIZE_OF_BYTE)]
        public byte Status;
        [MarshalAs(UnmanagedType.I1, SizeConst = PATSLib.SIZE_OF_CHAR + 1)]
        public char OpenOrClose;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY8 + 1)]
        public string DateSent;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY6 + 1)]
        public string TimeSent;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY8 + 1)]
        public string DateHostRecd;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY6 + 1)]
        public string TimeHostRecd;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY8 + 1)]
        public string DateExchRecd;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY6 + 1)]
        public string TimeExchRecd;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY8 + 1)]
        public string DateExchAckn;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY6 + 1)]
        public string TimeExchAckn;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY60 + 1)]
        public string NonExecReason;
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int Xref;
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int XrefP;
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int UpdateSeq;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY8 + 1)]
        public string GoodTillDate;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY25 + 1)]
        public string Reference;
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int Priority;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY8 + 1)]
        public string TriggerDate;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY6 + 1)]
        public string TriggerTime;
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int SubState;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string BatchID;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string BatchType;
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int BatchCount;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string BatchStatus;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string ParentID;
        [MarshalAs(UnmanagedType.I1, SizeConst = PATSLib.SIZE_OF_CHAR + 1)]
        public char DoneForDay;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY255 + 1)]
        public string BigRefField;
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int Timeout;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY120 + 1)]
        public string QuoteID;
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int LotsPosted;
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int ChildCount;
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int ActLots;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY32 + 1)]
        public string SenderLocationID;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string Rawprice;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string Rawprice2;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY70 + 1)]
        public string ExecutionID;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string ClientID;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY50 + 1)]
        public string ESARef;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string ISINCode;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string CashPrice;
        [MarshalAs(UnmanagedType.I1, SizeConst = PATSLib.SIZE_OF_CHAR + 1)]
        public char Methodology;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string BasisRef;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY8 + 1)]
        public string EntryDate;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY6 + 1)]
        public string EntryTime;
        [MarshalAs(UnmanagedType.I1, SizeConst = PATSLib.SIZE_OF_CHAR + 1)]
        public char APIM;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string APIMUser;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string ICSNearLegPrice;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string ICSFarLegPrice;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY8 + 1)]
        public string CreationDate;
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int OrderHistorySeq;
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int MinClipSize;
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int MaxClipSize;
        [MarshalAs(UnmanagedType.I1, SizeConst = PATSLib.SIZE_OF_CHAR + 1)]
        public char Randomise;
        [MarshalAs(UnmanagedType.I1, SizeConst = PATSLib.SIZE_OF_CHAR + 1)]
        public char ProfitLevel;
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int OFSeqNumber;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string ExchangeField;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string BOFID;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY5 + 1)]
        public string Badge;
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int GTStatus;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string LocalUserName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string LocalTrader;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string LocalBOF;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string LocalOrderID;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string LocalExAcct;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string RoutingID1;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string RoutingID2;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string FreeTextField1;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string FreeTextField2;
        [MarshalAs(UnmanagedType.I1, SizeConst = PATSLib.SIZE_OF_CHAR + 1)]
        public char Inactive;


        public string Key { get { return $"{ExchangeName},{ContractName},{ContractDate}"; } }
        public OrderState State { get { return (OrderState)Enum.Parse(typeof(OrderState), Status.ToString()); } }
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public unsafe struct VTSDetailStruct
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string Exchange;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string ContractName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string LowerLim;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string UpperLim;
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int Multiplier;
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public unsafe struct StratLegStruct
    {
        [MarshalAs(UnmanagedType.I1, SizeConst = PATSLib.SIZE_OF_CHAR + 1)]
        public char ContractType;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY50 + 1)]
        public string ContractDate;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string Price;
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int Ratio;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string ContractName;
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public unsafe struct StrategyLegsStruct
    {
        [MarshalAs(UnmanagedType.Struct, SizeConst = 79)]
        public StratLegStruct Leg0;
        [MarshalAs(UnmanagedType.Struct, SizeConst = 79)]
        public StratLegStruct Leg1;
        [MarshalAs(UnmanagedType.Struct, SizeConst = 79)]
        public StratLegStruct Leg2;
        [MarshalAs(UnmanagedType.Struct, SizeConst = 79)]
        public StratLegStruct Leg3;
        [MarshalAs(UnmanagedType.Struct, SizeConst = 79)]
        public StratLegStruct Leg4;
        [MarshalAs(UnmanagedType.Struct, SizeConst = 79)]
        public StratLegStruct Leg5;
        [MarshalAs(UnmanagedType.Struct, SizeConst = 79)]
        public StratLegStruct Leg6;
        [MarshalAs(UnmanagedType.Struct, SizeConst = 79)]
        public StratLegStruct Leg7;
        [MarshalAs(UnmanagedType.Struct, SizeConst = 79)]
        public StratLegStruct Leg8;
        [MarshalAs(UnmanagedType.Struct, SizeConst = 79)]
        public StratLegStruct Leg9;
        [MarshalAs(UnmanagedType.Struct, SizeConst = 79)]
        public StratLegStruct Leg10;
        [MarshalAs(UnmanagedType.Struct, SizeConst = 79)]
        public StratLegStruct Leg11;
        [MarshalAs(UnmanagedType.Struct, SizeConst = 79)]
        public StratLegStruct Leg12;
        [MarshalAs(UnmanagedType.Struct, SizeConst = 79)]
        public StratLegStruct Leg13;
        [MarshalAs(UnmanagedType.Struct, SizeConst = 79)]
        public StratLegStruct Leg14;
        [MarshalAs(UnmanagedType.Struct, SizeConst = 79)]
        public StratLegStruct Leg15;
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public unsafe struct LegPriceStruct
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string Leg0Price;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string Leg1Price;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string Leg2Price;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string Leg3Price;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string Leg4Price;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string Leg5Price;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string Leg6Price;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string Leg7Price;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string Leg8Price;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string Leg9Price;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string Leg10Price;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string Leg11Price;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string Leg12Price;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string Leg13Price;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string Leg14Price;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string Leg15Price;
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public unsafe struct BasisOrderStruct
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string ISINCode;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string CashPrice;
        [MarshalAs(UnmanagedType.I1)]
        public char Methodology;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY20 + 1)]
        public string Reference;
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public unsafe struct GenericPriceStruct
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string ExchangeName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY10 + 1)]
        public string ContractName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = PATSLib.SIZE_OF_ARRAY50 + 1)]
        public string ContractDate;
        [MarshalAs(UnmanagedType.I4, SizeConst = PATSLib.SIZE_OF_INT)]
        public int PriceType;
        [MarshalAs(UnmanagedType.I1, SizeConst = PATSLib.SIZE_OF_CHAR + 1)]
        public char BuyOrSell;
    }
}