using System.ComponentModel;

namespace PriceLib.PATS
{

    public enum EnvironmentType
    {
        ptGateway = 'G',
        ptClient = 'C',
        ptTestClient = 'T',
        ptTestGateway = 'g',
        ptDemoClient = 'D',
        // VM 03/10/2008 new enviroments for Broker Intervention        
        ptBroker = 'B',
        ptTestBroker = 'b'
    }
    public enum ErrorCode
    {
        [Description("Success")]
        ptSuccess = 0,
        [Description("ptInitialise not run yet")]
        ptErrNotInitialised = 1,
        [Description("some callback addresses nil")]
        ptErrCallbackNotSet = 2,
        [Description("unknown callback ID")]
        ptErrUnknownCallback = 3,
        [Description("user has not successfully logged on")]
        ptErrNotLoggedOn = 4,
        [Description("old pwd incorrect on ptSetPassword")]
        ptErrInvalidPassword = 5,
        [Description("password may not be blank")]
        ptErrBlankPassword = 6,
        [Description("user/accnt not enabled for this action")]
        ptErrNotEnabled = 7,
        [Description("index provided to ptGet<xxx> is invalid")]
        ptErrInvalidIndex = 8,
        [Description("trader account not found")]
        ptErrUnknownAccount = 9,
        [Description("could not find any data to return")]
        ptErrNoData = 10,
        [Description("generic value not set/known error")]
        ptErrFalse = 11,
        [Description("*** NOT USED - Spare return code ***")]
        ptErrUnknownError = 12,
        [Description("mismatch between application and API")]
        ptErrWrongVersion = 13,
        [Description("Msg Type not ptAlert or ptNormal")]
        ptErrBadMsgType = 14,
        [Description("msg ID sequence no. not found")]
        ptErrUnknownMsgID = 15,
        [Description("not enough room to write report")]
        ptErrBufferOverflow = 16,
        [Description("new password was not recognisable text")]
        ptErrBadPassword = 17,
        [Description("not connected to host or price feed")]
        ptErrNotConnected = 18,
        [Description("currency not recognised")]
        ptErrUnknownCurrency = 19,
        [Description("no matching report for report type")]
        ptErrNoReport = 20,
        [Description("order type not known by API")]
        ptErrUnknownOrderType = 21,
        [Description("contractname/date unknown")]
        ptErrUnknownContract = 22,
        [Description("commodity name not known")]
        ptErrUnknownCommodity = 23,
        [Description("required price to entered for new order")]
        ptErrPriceRequired = 24,
        [Description("specified order ID not valid")]
        ptErrUnknownOrder = 25,
        [Description("order is not in valid state for action")]
        ptErrInvalidState = 26,
        [Description("supplied price string is invalid")]
        ptErrInvalidPrice = 27,
        [Description("price specified and should not be")]
        ptErrPriceNotRequired = 28,
        [Description("volume (lots) is not valid")]
        ptErrInvalidVolume = 29,
        [Description("amend not enabled for exch. (use cancel/add)")]
        ptErrAmendDisabled = 30,
        [Description("ORPI query not enabled for exch.")]
        ptErrQueryDisabled = 31,
        [Description("that exchange not known")]
        ptErrUnknownExchange = 32,
        [Description("fill ID not for valid fill")]
        ptErrUnknownFill = 33,
        [Description("Trader is View Only")]
        ptErrNotTradable = 34,
        [Description("Transaction server is not connected")]
        ptErrTASUnavailable = 35,
        [Description("MDS not connected")]
        ptErrMDSUnavailable = 36,
        [Description("new password was not alpha-numeric")]
        ptErrNotAlphaNumeric = 37,
        [Description("invalid underlying contract for strategy")]
        ptErrInvalidUnderlying = 38,
        [Description("user is not allowed to trade with selected order type")]
        ptErrUntradableOType = 39,
        [Description("returned when the user has no preallocated orders remaining and the request for more ids has been rejected.")]
        ptErrNoPreallocOrders = 40,
        [Description("Crossing Error - the contracts are in different markets")]
        ptErrDifferentMarkets = 41,
        [Description("Crossing Error - the orders are different types")]
        ptErrDifferentOrderTypes = 42,
        [Description("user is not allowed to trade with selected order type")]
        ptOrderAlreadyReceived = 43,
        [Description("the user has tried to retrieve an invalid Variable Tick Size")]
        ptVTSItemInvalid = 44,
        [Description("The user has tried to add an order to an invalid parent order - OMI")]
        ptErrInvalidOrderParent = 45,
        [Description("The user has tried to set an order to DoneForDay that isn't an Aggrgate Order - OMI")]
        ptErrNotAggOrder = 46,
        [Description("The order has already been passed to the Core Components.  It will be held in a queue until a valid state is returned")]
        ptErrOrderAlreadyAmending = 47,
        [Description("The user does not have permission to access this contract information")]
        ptErrNotTradableContract = 48,
        [Description("Unable to decompress contract data")]
        ptErrFailedDecompress = 49,
        [Description("TGE Specific error - the user will get an error if the order cannot be amended")]
        ptErrAmendMarketSuspended = 50,
        [Description("GT Message used to identify orders that have been cancelled due to the exchange closing.")]
        ptErrGTOrderCancelled = 51,
        [Description("This Order cannot be amended to this Order Type")]
        ptErrInvalidAmendOrderType = 52,
        //VM 05/12/2007
        [Description("invalid algo string")]
        ptErrInvalidAlgoXML = 53,
        //VM 09/01/2008
        [Description("Invalid IPAddress")]
        ptErrInvalidIPAddress = 54,
        [Description("Keep this in sync with last error number. Should be same as highest error number (except 99)v")]
        ptErrLast = 54,
        [Description("unexpected error trapped - routine aborted")]
        ptErrUnexpected = 99
    }
    public enum CallbackType
    {
        ptHostLinkStateChange = 1,
        ptPriceLinkStateChange = 2,
        ptLogonStatus = 3,
        ptMessage = 4,
        ptOrder = 5,
        ptForcedLogout = 6,
        ptDataDLComplete = 7,
        ptPriceUpdate = 8,
        ptFill = 9,
        ptStatusChange = 10,
        ptContractAdded = 11,
        ptContractDeleted = 12,
        ptExchangeRate = 13,
        ptConnectivityStatus = 14,
        ptOrderCancelFailure = 15,
        ptAtBestUpdate = 16,
        ptTickerUpdate = 17,
        ptMemoryWarning = 18,
        ptSubscriberDepthUpdate = 19,
        ptVTSCallback = 20,
        ptDOMUpdate = 21,
        ptSettlementCallback = 22,
        ptStrategyCreateSuccess = 23,
        ptStrategyCreateFailure = 24,
        ptAmendFailureCallback = 25,
        // Eurodollar changes
        ptGenericPriceUpdate = 26,
        ptBlankPrice = 27,
        // Jan release callbacks
        ptOrderSentFailure = 28,
        ptOrderQueuedFailure = 29,
        ptOrderBookReset = 30,
        // Global Trading Changes
        ptExchangeUpdate = 31,
        ptCommodityUpdate = 32,
        ptContractDateUpdate = 33,

        ptPurgeCompleted = 36,
        ptTraderAdded = 37,
        ptOrderTypeUpdate = 38
    }
    public enum SocketLinkState
    {
        ptLinkOpened = 1,
        ptLinkConnecting = 2,
        ptLinkConnected = 3,
        ptLinkClosed = 4,
        ptLinkInvalid = 5
    }
    public enum MessageType
    {
        ptAlert = 1,
        ptNormal = 2
    }
    public enum GroupType
    {
        ptFillGroup = 0,
        ptLegsGroup = 1,
        ptOrderGroup = 2
    }
    public enum LogonState
    {
        ptLogonFailed = 0,
        ptLogonSucceeded = 1,
        ptForcedOut = 2,
        ptObsoleteVers = 3,
        ptWrongEnv = 4,
        ptDatabaseErr = 5,
        ptInvalidUser = 6,
        ptLogonRejected = 7,
        ptInvalidAppl = 8,
        ptLoggedOn = 9,
        ptInvalidLogonState = 99
    }
    public enum FillType
    {
        ptNormalFill = 1,
        ptExternalFill = 2,
        ptNettedFill = 3,
        ptRetainedFill = 5,
        ptBlockLegFill = 52
    }
    public enum OrderType
    {
        ptOrderTypeMarket = 1,
        ptOrderTypeLimit = 2,
        ptOrderTypeLimitFAK = 3,
        ptOrderTypeLimitFOK = 4,
        ptOrderTypeStop = 5,
        ptOrderTypeSynthStop = 6,
        ptOrderTypeSynthStopLimit = 7,
        ptOrderTypeMIT = 8,
        ptOrderTypeSynthMIT = 9,
        ptOrderTypeMarketFOK = 10,
        ptOrderTypeMOO = 11,
        ptOrderTypeIOC = 12,
        ptOrderTypeStopRise = 13,
        ptOrderTypeStopFall = 14,
        ptOrderTypeRFQ = 15,
        ptOrderTypeStopLoss = 16,
        ptLimitAtOpen = 17,
        ptMLM = 18,
        ptAggregateOrder = 25,
        ptCustomerRequest = 26,
        ptRFQi = 27,
        ptRFQt = 28,
        ptCrossingBatchType = 42,
        ptBasisBatchType = 43,
        ptBlockBatchType = 44,
        ptAABatchType = 45,
        ptCrossFaKBatchType = 46,
        ptGTCMarket = 50,
        ptGTCLimit = 51,
        ptGTCStop = 52,
        ptGTDMarket = 53,
        ptGTDLimit = 54,
        ptGTDStop = 55,
        ptSETSRepenter = 90,
        ptSETSRepcancel = 91,
        ptSETSRepprerel = 92,
        ptSETSSectDel = 93,
        ptSETSInstDel = 94,
        ptSETSCurDel = 95,
        ptIceberg = 130,
        ptGhost = 131,
        ptProtected = 132,
        ptStop = 133
    }
    public enum OrderState
    {
        ptQueued = 1,
        ptSent = 2,
        ptWorking = 3,
        ptRejected = 4,
        ptCancelled = 5,
        ptBalCancelled = 6,
        ptPartFilled = 7,
        ptFilled = 8,
        ptCancelPending = 9,
        ptAmendPending = 10,
        ptUnconfirmedFilled = 11,
        ptUnconfirmedPartFilled = 12,
        ptHeldOrder = 13,
        ptCancelHeldOrder = 14,
        ptTransferred = 20,
        ptExternalCancelled = 24 // added for GT
    }
    public enum OrderSubState
    {
        ptSubStatePending = 1,
        ptSubStateTriggered = 2
    }
    public enum PriceMovement
    {
        ptPriceSame = 0,
        ptPriceRise = 1,
        ptPriceFall = 2
    }
    public enum GenericPrice
    {
        [Description("The RFQ is a buy order")]
        ptBuySide = 1,
        [Description("The RFQ is a sell order")]
        ptSellSide = 2,
        [Description("The RFQ is a for both sides")]
        ptBothSide = 3,
        [Description("This is for crossing RFQs")]
        PtCrossSide = 4
    }
    public enum FillSubType
    {
        ptFillSubTypeSettlement = 1,
        ptFillSubTypeMinute = 2,
        ptFillSubTypeUnderlying = 3,
        ptFillSubTypeReverse = 4
    }
    public enum SettlementPriceType
    {
        ptStlLegacyPrice = 0,
        ptStlCurPrice = 7,
        ptStlLimitUp = 21,
        ptStlLimitDown = 22,
        ptStlExecDiff = 23,

        // VM 27/11/2008 changed because these are the values according to what stas send to us...
        //ptStlNewPrice   = 24;
        //ptStlYDSPPrice  = 25;

        ptStlYDSPPrice = 24,
        ptStlNewPrice = 25,

        ptStlRFQiPrice = 26,
        ptStlRFQtPrice = 27,
        ptStlIndicative = 28,
        ptEFPVolume = 33,
        ptEFSVolume = 34,
        ptBlockVolume = 35,
        ptEFPCummVolume = 36,
        ptEFSCummVolume = 37,
        ptBlockCummVolume = 38
    }
    public enum PriceChange
    {
        ptChangeBid = 0x00000001,
        ptChangeOffer = 0x00000002,
        ptChangeImpliedBid = 0x00000004,
        ptChangeImpliedOffer = 0x00000008,
        ptChangeRFQ = 0x00000010,
        ptChangeLast = 0x00000020,
        ptChangeTotal = 0x00000040,
        ptChangeHigh = 0x00000080,
        ptChangeLow = 0x00000100,
        ptChangeOpening = 0x00000200,
        ptChangeClosing = 0x00000400,
        ptChangeBidDOM = 0x00000800,
        ptChangeOfferDOM = 0x00001000,
        ptChangeTGE = 0x00002000,
        ptChangeSettlement = 0x00004000,
        ptChangeIndic = 0x00008000,
        [Description("Mask for Cleared Prices")]
        ptChangeClear = 0x0000181F
    }
    public enum ContractDateMarketStatus
    {
        ptStateUndeclared = -0x0001,
        ptStateNormal = 0x0000,
        ptStateExDiv = 0x0001,
        ptStateAuction = 0x0002,
        ptStateSuspended = 0x0004,
        ptStateClosed = 0x0008,
        ptStatePreOpen = 0x0010,
        ptStatePreClose = 0x0020,
        ptStateFastMarket = 0x0040
    }
    public enum PreallocateState
    {
        ptIDsNull = -1,
        ptIDsReceived = 0,
        ptIDsRejected = 1,
        ptIDsRequested = 2
    }
    public enum StrategyCreationCode
    {
        ptFUT_CALENDAR = 'E',
        ptFUT_BUTTERFLY = 'B',
        ptFUT_CONDOR = 'W',
        ptFUT_STRIP = 'M',
        ptFUT_PACK = 'O',
        ptFUT_BUNDLE = 'Y',
        ptFUT_RTS = 'Z',
        ptOPT_BUTTERFLY = 'B',
        ptOPT_SPREAD = 'D',
        ptOPT_CALENDAR_SPREAD = 'E',
        ptOPT_DIAG_CALENDAR_SPREAD = 'F',
        ptOPT_GUTS = 'G',
        ptOPT_RATIO_SPREAD = 'H',
        ptOPT_IRON_BUTTERFLY = 'I',
        ptOPT_COMBO = 'J',
        ptOPT_STRANGLE = 'K',
        ptOPT_LADDER = 'L',
        ptOPT_STRADDLE_CALENDAR_SPREAD = 'N',
        ptOPT_DIAG_STRADDLE_CALENDAR_SPREAD = 'P',
        ptOPT_STRADDLE = 'S',
        ptOPT_CONDOR = 'W',
        ptOPT_BOX = 'X',
        ptOPT_SYNTHETIC_CONVERSION_REVERSAL = 'r',
        ptOPT_CALL_SPREAD_VS_PUT = 'x',
        ptOPT_PUT_SPREAD_VS_CALL = 'y',
        ptOPT_STRADDLE_VS_OPTION = 'z',
        ptVOL_REVERSAL_CONVERSION = 'R',
        ptVOL_OPTION = 'V',
        ptVOL_LADDER = 'a',
        ptVOL_CALL_SPREAD_VS_PUT = 'c',
        ptVOL_SPREAD = 'd',
        ptVOL_COMBO = 'j',
        ptVOL_PUT_SPREAD_VS_CALL = 'p',
        ptVOL_STRADDLE = 's',
        ptDIV_C_CALENDAR = 'I',
        ptDIV_C_SPREAD = 'H',
        ptDIV_CONVERSION = 'G',
        ptDIV_F_SPREAD = 'E',
        ptDIV_P_CALENDAR = 'A',
        ptDIV_P_SPREAD = 'B',
        ptDIV_STRADDLE = 'D',
        ptDIV_STRANGLE = 'J'
    }
}