using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace PriceLib.PATS
{
    public partial class PATSLib
    {
        //#if DEBUG
        //private const string DLLPath = "DEMOAPI.DLL";
        //#else
        private const string DLLPath = "PATSAPI.DLL";
        //#endif
        public const int SIZE_OF_CHAR = 1;
        public const int SIZE_OF_INT = 4;
        public const int SIZE_OF_BYTE = 1;

        public const int SIZE_OF_ARRAY2 = 2;
        public const int SIZE_OF_ARRAY3 = 3;
        public const int SIZE_OF_ARRAY6 = 6;
        public const int SIZE_OF_ARRAY5 = 5;
        public const int SIZE_OF_ARRAY8 = 8;
        public const int SIZE_OF_ARRAY10 = 10;
        public const int SIZE_OF_ARRAY14 = 14;
        public const int SIZE_OF_ARRAY16 = 16;
        public const int SIZE_OF_ARRAY20 = 20;
        public const int SIZE_OF_ARRAY25 = 25;
        public const int SIZE_OF_ARRAY30 = 30;
        public const int SIZE_OF_ARRAY32 = 32;
        public const int SIZE_OF_ARRAY36 = 36;
        public const int SIZE_OF_ARRAY50 = 50;
        public const int SIZE_OF_ARRAY60 = 60;
        public const int SIZE_OF_ARRAY70 = 70;
        public const int SIZE_OF_ARRAY120 = 120;
        public const int SIZE_OF_ARRAY250 = 250;
        public const int SIZE_OF_ARRAY255 = 255;
        public const int SIZE_OF_ARRAY500 = 500;


        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void ProcAddr();
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void LinkProcAddr(ref LinkStateStruct Data);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        //private delegate void MsgProcAddr(ref MsgIDPtr MsgID);
        private delegate void MsgProcAddr(ref string MsgID);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void PriceProcAddr(ref PriceUpdStruct PriceUpdate);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void DOMProcAddr(ref DOMUpdStruct DOMUpdate);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void AtBestProcAddr(ref AtBestUpdStruct AtBestUpdate);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void SubscriberDepthProcAddr(ref SubscriberDepthUpdStruct SubscriberDepthUpdate);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void OrderProcAddr(ref OrderUpdStruct Order);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void FillProcAddr(ref FillUpdStruct Fill);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void ContractProcAddr(ref ContractUpdStruct Contract);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void CommodityProcAddr(ref CommodityUpdStruct Commodity);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void ExchangeProcAddr(ref ExchangeUpdStruct Exchange);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void StatusProcAddr(ref StatusUpdStruct Status);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void ExchangeRateProcAddr(ref ExchangeRateUpdStruct ExchangeRate);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void ConStatusProcAddr(ref ConnectivityStatusUpdStruct ConStatus);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void TickerUpdateProcAddr(ref TickerUpdStruct TickerUpdate);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void AmendFailureProcAddr(ref OrderUpdStruct Order);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void GenericPriceProcAddr(ref GenericPriceStruct Price);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void SettlementProcAddr(ref SettlementPriceStruct SettlementPrice);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void StrategyCreateSuccessProcAddr(ref StrategyCreateSuccessStruct Data);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void StrategyCreateFailureProcAddr(ref StrategyCreateFailureStruct Data);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void BlankPriceProcAddr(ref BlankPriceStruct Data);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void OrderBookResetProcAddr();
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void TraderAddedProcAddr(ref TraderAcctStruct TraderAccount);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate void OrderTypeUpdateAddr(ref OrderTypeStruct OrderType);

        #region Registering Callbacks
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptRegisterCallback(int callbackID, ProcAddr CBackProc);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptRegisterLinkStateCallback(int callbackID, LinkProcAddr CBackProc);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptRegisterMsgCallback(int callbackID, MsgProcAddr CBackProc);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptRegisterOrderCallback(int callbackID, OrderProcAddr CBackProc);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptRegisterOrderTypeUpdateCallback(int callbackID, OrderTypeUpdateAddr CBackProc);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptRegisterFillCallback(int callbackID, FillProcAddr CBackProc);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptRegisterPriceCallback(int callbackID, PriceProcAddr CBackProc);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptRegisterBlankPriceCallback(int callbackID, BlankPriceProcAddr CBackProc);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptRegisterGenericPriceCallback(int callbackID, PriceProcAddr CBackProc);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptRegisterDOMCallback(int callbackID, DOMProcAddr CBackProc);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptRegisterContractCallback(int callbackID, ContractProcAddr CBackProc);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptRegisterCommodityCallback(int callbackID, CommodityProcAddr CBackProc);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptRegisterExchangeCallback(int callbackID, ExchangeProcAddr CBackProc);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptRegisterStatusCallback(int callbackID, StatusProcAddr CBackProc);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptRegisterExchangeRateCallback(int callbackID, ExchangeRateProcAddr CBackProc);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptRegisterConStatusCallback(int callbackID, ConStatusProcAddr CBackProc);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptRegisterOrderCancelFailureCallback(int callbackID, OrderProcAddr CBackProc);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptRegisterOrderSentFailureCallback(int callbackID, OrderProcAddr CBackProc);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptRegisterOrderQueuedFailureCallback(int callbackID, OrderProcAddr CBackProc);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptRegisterAtBestCallback(int callbackID, AtBestProcAddr CBackProc);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptRegisterTickerCallback(int callbackID, TickerUpdateProcAddr CBackProc);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptRegisterAmendFailureCallback(int callbackID, AmendFailureProcAddr CBackProc);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptRegisterSubscriberDepthCallback(int callbackID, SubscriberDepthProcAddr CBackProc);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptRegisterSettlementCallback(int callbackID, SettlementProcAddr CBackProc);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptRegisterStrategyCreateSuccess(int callbackID, StrategyCreateSuccessProcAddr CBackProc);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptRegisterStrategyCreateFailure(int callbackID, StrategyCreateFailureProcAddr CBackProc);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptRegisterOrderBookReset(int callbackID, OrderBookResetProcAddr CBackProc);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptRegisterTraderAdded(int callbackID, TraderAddedProcAddr CBackProc);
        #endregion

        #region Obtain Reference Data
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptCountExchanges(ref int Count);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptGetExchange(int Index, ref ExchangeStruct ExchangeDetails);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]        
        private static extern int ptGetExchangeByName(string ExchangeName, out ExchangeStruct ExchangeDetails);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]        
        private static extern int ptExchangeExists(string ExchangeName);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptCountTraders(out int Count);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptGetTrader(int Index, out TraderAcctStruct TraderDetails);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]        
        private static extern int ptGetTraderByName(string TraderAccount, out TraderAcctStruct TraderDetails);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]        
        private static extern int ptTraderExists(string TraderAccount);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptCountOrderTypes(out int Count);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]        
        private static extern int ptGetOrderType(int Index, out OrderTypeStruct OrderTypeRec, out string AmendOrderTypes);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]        
        private static extern int ptGetExchangeRate(string Index, out string ExchRate);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptCountReportTypes(ref int Count);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        //private static extern int ptGetReportType(int Index, ref ReportTypePtr ReportType);
        private static extern int ptGetReportType(int Index, ref string ReportType);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        //private static extern int ptReportTypeExists(ReportTypePtr ReportType);
        private static extern int ptReportTypeExists(string ReportType);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        //private static extern int ptGetReportSize(ReportTypePtr ReportType, ref int ReportSize);
        private static extern int ptGetReportSize(string ReportType, ref int ReportSize);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        //private static extern int ptGetReport(ReportTypePtr ReportType, int BufferSize, IntPtr BufferAddr);
        private static extern int ptGetReport(string ReportType, int BufferSize, IntPtr BufferAddr);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptCountCommodities(ref int Count);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptGetCommodity(int Index, ref CommodityStruct Commodity);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        //private static extern int ptCommodityExists(ExchNamePtr ExchangeName, ConNamePtr ContractName);
        private static extern int ptCommodityExists(string ExchangeName, string ContractName);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        //private static extern int ptGetCommodityByName(ExchNamePtr ExchangeName, ConNamePtr ContractName, ref CommodityStruct Commodity);
        private static extern int ptGetCommodityByName(string ExchangeName, string ContractName, ref CommodityStruct Commodity);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptCountContracts(out int Count);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptGetContract(int Index, out ContractStruct Contract);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        //private static extern int ptGetContractByName(ExchNamePtr ExchangeName, ConNamePtr ContractName, ConDatePtr ContractDate, ref ContractStruct Contract);
        private static extern int ptGetContractByName(string ExchangeName, string ContractName, string ContractDate, out ContractStruct Contract);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptGetContractByExternalID(ContractStruct ContractIn, ref ContractStruct ContractOut);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptGetExtendedContract(int Inex, ref ExtendedContractStruct ExtContract);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        //private static extern int ptGetExtendedContractByName(ExchNamePtr ExchangeName, ConNamePtr ContractName, ConDatePtr ContractDate,ref  ExtendedContractStruct ExtContract);
        private static extern int ptGetExtendedContractByName(string ExchangeName, string ContractName, string ContractDate, ref ExtendedContractStruct ExtContract);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        //private static extern int ptContractExists(ExchNamePtr ExchangeName, ConNamePtr ContractName, ConDatePtr ContractDate);
        private static extern int ptContractExists(string ExchangeName, string ContractName, string ContractDate);
        #endregion

        #region Setting Up The API
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptInitialise(char Env, string APIversion, string ApplicID, string ApplicVersion, string License, bool InitReset);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]        
        private static extern int ptGetAPIBuildVersion(ref string APIVersion);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptReady();
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptPurge(string PDate, string PTime);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptDisconnect();
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptSetHostAddress(String IPaddress, String IPSocket);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptSetPriceAddress(String IPaddress, String IPSocket);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern void ptEnable(int Code);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern void ptDisable(int Code);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]        
        private static extern void ptLogString(string DebugStr);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptOMIEnabled(char Enabled);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptNotifyAllMessages(char Enabled);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptSetHostReconnect(int Interval);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptSetPriceReconnect(int Interval);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptSetPriceAgeCounter(int MaxAge);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]        
        private static extern int ptSubscribePrice(string ExchangeName, string ContractName, string ContractDate);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]        
        private static extern int ptSubscribeToMarket(string ExchangeName, string ContractName, string ContractDate);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]        
        private static extern int ptUnsubscribePrice(string ExchangeName, string ContractName, string ContractDate);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]        
        private static extern int ptUnsubscribeToMarket(string ExchangeName, string ContractName, string ContractDate);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]        
        private static extern int ptPriceSnapshot(string ExchangeName, string ContractName, string ContractDate, int Wait);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern void ptSetEncryptionCode(char Code);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptSetHandShakePeriod(int Seconds);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptSetHostHandShake(int Interval, int TimeOut);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptSetPriceHandShake(int Interval, int TimeOut);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptSetInternetUser(char Enabled);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern void ptSetClientPath(string Path);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern string ptGetErrorMessage(int Error);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptDumpLastError();
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern char ptSnapdragonEnabled();
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]        
        private static extern int ptSubscribeBroadcast(string ExchangeName);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]        
        private static extern int ptUnsubscribeBroadcast(string ExchangeName);
        #endregion

        #region Trading
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptCountFills(out int Count);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptGetFill(int Index, out FillStruct Fill);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]        
        private static extern int ptGetFillByID(string FillID, out FillStruct Fill);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]        
        private static extern int ptGetContractPosition(string ExchangeName, string ContractName, string ContractDate, string TraderAccount, out PositionStruct Position);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]        
        private static extern int ptGetOpenPosition(string ExchangeName, string ContractName, string ContractDate, string TraderAccount, out PositionStruct Position);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]        
        private static extern int ptGetTotalPosition(string TraderAccount, out PositionStruct Position);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]        
        private static extern int ptGetAveragePrice(string ExchangeName, string ContractName, string ContractDate, string TraderAccount, out string Price);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptCountOrders(out int Count);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptGetOrder(int Index, out OrderDetailStruct OrderDetail);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]        
        private static extern int ptGetOrderByID(string OrderID, out OrderDetailStruct OrderDetail, int OFSequenceNumber = 0);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]        
        private static extern int ptGetOrderIndex(string OrderID, out int Index);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern void ptBlankPrices();
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptGetPrice(int Index, out PriceStruct CurrentPrice);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]        
        private static extern int ptGetPriceForContract(string ExchangeName, string ContractName, string ContractDate, out PriceStruct CurrentPrice);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]        
        private static extern int ptGetGenericPrice(string ExchangeName, string ContractName, string ContractDate, int PriceType, int Side, out PriceStruct Price);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]        
        private static extern int ptAddOrder(NewOrderStruct NewOrder,out  string OrderID);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]        
        private static extern int ptAddAlgoOrder(NewOrderStruct NewOrder, int BuffSize, string AlgoBuffer, string OrderID);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptGetOrderEx(int Index, ref OrderDetailStruct OrderDetail, ref string AlgoDetail, ref int AlgoSize);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]        
        private static extern int ptGetOrderByIDEx(string OrderID, ref OrderDetailStruct OrderDetail, ref string AlgoDetail, ref int AlgoSize, int OFSequenceNumber = 0);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptGetOrderHistoryEx(int Index, int Position, ref OrderDetailStruct OrderDetail, ref string AlgoDetail, ref int AlgoSize);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]        
        private static extern int ptAddProtectionOrder(NewOrderStruct NewOrder, ProtectionStruc Protection, string OrderID);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptAddBatchOrder(int OrderCount, BEPNewOrderStruct BEPNewOrder, BEPOrderIDStruct BEPOrderIDs);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]        
        private static extern int ptAddAggregateOrder(NewAggOrderStruct NewAggOrder, string OrderID);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]        
        private static extern int ptAddCustRequest(NewCustReqStruct NewCustReq, string OrderID);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]        
        private static extern int ptReParent(string OrderID, string DestParentID);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]        
        private static extern int ptDoneForDay(string OrderID);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptAddCrossingOrder(NewOrderStruct PrimaryOrder, NewOrderStruct SecondaryOrder, LegPriceStruct LegPrices, CrossingOrderIDs OrderIDs, char FAK = 'L');
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptAddBlockOrder(NewOrderStruct PrimaryOrder, NewOrderStruct SecondaryOrder, LegPriceStruct LegPrices, CrossingOrderIDs OrderIDs);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptAddBasisOrder(NewOrderStruct PrimaryOrder, NewOrderStruct SecondaryOrder, BasisOrderStruct BasisOrder, CrossingOrderIDs OrderID);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]        
        private static extern int ptAddAAOrder(NewOrderStruct PrimaryOrder, NewOrderStruct SecondaryOrder, string BidUser, string OfferUser, CrossingOrderIDs OrderID);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]        
        private static extern int ptGetConsolidatedPosition(string Exchange, string ContractName, string ContractDate, string TraderAccount, int PositionType, ref FillStruct fill);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        //private static extern int ptAmendOrder(OrderIDPtr OrderID, AmendOrderStruct NewDetails);
        private static extern int ptAmendOrder(string OrderID, ref AmendOrderStruct NewDetails);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        //private static extern int ptAmendAlgoOrder(OrderIDPtr OrderID, int BuffSize, ref string AlgoBuffer, AmendOrderStruct NewDetails);
        private static extern int ptAmendAlgoOrder(string OrderID, int BuffSize, ref string AlgoBuffer, ref AmendOrderStruct NewDetails);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        //private static extern int ptCancelOrder(OrderIDPtr OrderID);
        private static extern int ptCancelOrder(string OrderID);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        //private static extern int ptActivateOrder(OrderIDPtr OrderID);
        private static extern int ptActivateOrder(string OrderID);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        //private static extern int ptDeactivateOrder(OrderIDPtr OrderID);
        private static extern int ptDeactivateOrder(string OrderID);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        //private static extern int ptCancelBuys(TraderPtr TraderAccount, ExchNamePtr ExchangeName, ConNamePtr ContractName, ConDatePtr ContractDate);
        private static extern int ptCancelBuys(string TraderAccount, string ExchangeName, string ContractName, string ContractDate);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        //private static extern int ptCancelSells(TraderPtr TraderAccount, ExchNamePtr ExchangeName, ConNamePtr ContractName, ConDatePtr ContractDate);
        private static extern int ptCancelSells(string TraderAccount, string ExchangeName, string ContractName, string ContractDate);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        //private static extern int ptCancelOrders(TraderPtr TraderAccount, ExchNamePtr ExchangeName, ConNamePtr ContractName, ConDatePtr ContractDate);
        private static extern int ptCancelOrders(string TraderAccount, string ExchangeName, string ContractName, string ContractDate);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        //private static extern int ptCancelAll(TraderPtr TraderAccount);
        private static extern int ptCancelAll(string TraderAccount);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        //private static extern int ptOrderChecked(OrderIDPtr OrderID, char Checked);
        private static extern int ptOrderChecked(string OrderID, char Checked);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        //private static extern int ptQueryOrderStatus(OrderIDPtr OrderID);
        private static extern int ptQueryOrderStatus(string OrderID);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptCountOrderHistory(int Index, ref int Count);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptGetOrderHistory(int Index, int Position, ref OrderDetailStruct OrderDetail);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptSetUserIDFilter(char Enable);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptNextOrderSequence(ref int Sequence);
        #endregion

        #region User Level Requests
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptLogOn(ref LogonStruct LogonDetails);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptLogOff();
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptGetLogonStatus(ref LogonStatusStruct LogonStatus);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptDOMEnabled();
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptPostTradeAmendEnabled();
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptEnabledFunctionality(ref int FunctionalityEnabled, ref int SoftwareEnabled);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptCountUsrMsgs(ref int Count);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptGetUsrMsg(int Index, ref MessageStruct UserMsg);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        //private static extern int ptGetUsrMsgByID(MsgIDPtr MsgID, MessageStruct UserMsg);
        private static extern int ptGetUsrMsgByID(string MsgID, ref MessageStruct UserMsg);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        //private static extern int ptAcknowledgeUsrMsg(MsgIDPtr MsgID);
        private static extern int ptAcknowledgeUsrMsg(string MsgID);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern double ptPriceStep(double Price, double TickSize, int NumSteps, int TicksPerPoint);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        //private static extern int ptPLBurnRate(ExchNamePtr ExchangeName, ConNamePtr ContractName, ConDatePtr ContractDate, TraderPtr TraderAccount, ref double BurnRate);
        private static extern int ptPLBurnRate(string ExchangeName, string ContractName, string ContractDate, string TraderAccount, ref double BurnRate);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        //private static extern int ptOpenPositionExposure(ExchNamePtr ExchangeName, ConNamePtr ContractName, ConDatePtr ContractDate, TraderPtr TraderAccount, ref double Exposure);
        private static extern int ptOpenPositionExposure(string ExchangeName, string ContractName, string ContractDate, string TraderAccount, ref double Exposure);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        //private static extern int ptBuyingPowerRemaining(ExchNamePtr ExchangeName, ConNamePtr ContractName, ConDatePtr ContractDate, TraderPtr TraderAccount, ref double BPRemaining);
        private static extern int ptBuyingPowerRemaining(string ExchangeName, string ContractName, string ContractDate, string TraderAccount, ref double BPRemaining);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        //private static extern int ptBuyingPowerUsed(ExchNamePtr ExchangeName, ConNamePtr ContractName, ConDatePtr ContractDate, TraderPtr TraderAccount, ref double BPUsed);
        private static extern int ptBuyingPowerUsed(string ExchangeName, string ContractName, string ContractDate, string TraderAccount, ref double BPUsed);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        //private static extern int ptTotalMarginForTrade(ExchNamePtr ExchangeName, ConNamePtr ContractName, ConDatePtr ContractDate, TraderPtr TraderAccount, ref int Lots, OrderIDPtr OrderID, PricePtr Price, ref double MarginReqd);
        private static extern int ptTotalMarginForTrade(string ExchangeName, string ContractName, string
            ContractDate, string TraderAccount, ref int Lots, string OrderID, string Price, ref double MarginReqd);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        //private static extern int ptMarginForTrade(ExchNamePtr ExchangeName, ConNamePtr ContractName, ConDatePtr ContractDate, TraderPtr TraderAccount, ref int Lots, OrderIDPtr OrderID, PricePtr Price, ref double MarginReqd);
        private static extern int ptMarginForTrade(string ExchangeName, string ContractName, string ContractDate, string TraderAccount, ref int Lots, string OrderID, string Price, ref double MarginReqd);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern void ptSetOrderCancelFailureDelay(int Code);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern void ptSetOrderSentFailureDelay(int Code);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern void ptSetOrderQueuedFailureDelay(int Code);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]        
        private static extern int ptCountContractAtBest(string ExchangeName, string ContractName, string ContractDate, ref int Count);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]        
        private static extern int ptGetContractAtBest(string ExchangeName, string ContractName, string ContractDate, int Index, ref AtBestStruct AtBest);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]        
        private static extern int ptGetContractAtBestPrices(string ExchangeName, string ContractName, string ContractDate, ref AtBestStruct AtBest);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]        
        private static extern int ptCountContractSubscriberDepth(string ExchangeName, string ContractName, string ContractDate, ref int Count);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]        
        private static extern int ptGetContractSubscriberDepth(string ExchangeName, string ContractName, string ContractDate, int Index, ref SubscriberDepthStruct SubscriberDepth);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptSuperTASEnabled();
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptSetSSL(char Enable);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptSetPDDSSL(char Enable);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]        
        private static extern int ptSetMDSToken(string MDSToken);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]        
        private static extern int ptSetSSLCertificateName(string CertName);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]        
        private static extern int ptSetPDDSSLCertificateName(string CertName);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]        
        private static extern int ptSetSSLClientAuthName(string CertName);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]        
        private static extern int ptSetPDDSSLClientAuthName(string CertName);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptSetSuperTAS(char Enable);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptSetMemoryWarning(int MemAmount);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]        
        private static extern int ptCreateStrategy(char StrategyCode, int NoOfLegs, string ExchangeName, string ContractName, StrategyLegsStruct Legs);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]        
        private static extern int ptGetOptionPremium(string ExchangeName, string ContractName, string ContractDate, char BuySell, string Price, int Lots, ref double OPr);
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptLockUpdates();
        [DllImport(DLLPath, CharSet = CharSet.Ansi)]
        private static extern int ptUnlockUpdates();
        #endregion
    }
}