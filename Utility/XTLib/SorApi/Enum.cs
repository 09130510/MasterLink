using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace SorApi
{
    public enum CAErrCode
    {
        /// <summary>
        /// 找不到dll.
        /// </summary>
        [Description("找不到dll")]
        ERR_DLLNotFound = -2,
        /// <summary>
        /// dll沒提供必要的fn.
        /// </summary>
        [Description("dll沒提供必要的fn")]
        ERR_DLLUnknown = -1,
        /// <summary>
        /// 沒有錯誤.
        /// </summary>
        [Description("沒有錯誤")]
        ERR_Success = 0,
        /// <summary>
        /// CertConfig 格式錯誤.
        /// </summary>
        [Description("CertConfig 格式錯誤")]
        ERR_CertConfigFormat = 1,
        /// <summary>
        /// 無法開啟:憑證儲存區.
        /// </summary>
        [Description("無法開啟:憑證儲存區")]
        ERR_CertStore = 2,
        /// <summary>
        /// 找不到有效憑證: 找不到指定Subjects的憑證, 或超過有效日期.
        /// </summary>
        [Description("找不到有效憑證: 找不到指定Subjects的憑證, 或超過有效日期")]
        ERR_NoAvailCert = 3,
    }

    /// <summary>
    /// 可交易市場別.
    /// </summary>
    public enum SorMktFlags
    {
        /// <summary>
        /// 無可交易市場.
        /// </summary>
        [Description("無可交易市場")]
        None = 0,
        /// <summary>
        /// 台灣證券.
        /// </summary>
        [Description("台灣證券")]
        TwStk = 1,
        /// <summary>
        /// 台灣期權.
        /// </summary>
        [Description("台灣期權")]
        TwFuo = 2,
        /// <summary>
        /// 國外證券.
        /// </summary>
        [Description("國外證券")]
        FrStk = 4,
        /// <summary>
        /// 國外期權.
        /// </summary>
        [Description("國外期權")]
        FrFuo = 8,
        /// <summary>
        /// 台灣期權報價.
        /// </summary>
        [Description("台灣期權報價")]
        TwfQuot = 0x10,
        /// <summary>
        /// 大陸期權.
        /// </summary>
        [Description("大陸期權")]
        CnFuo = 0x20,
    }

    /// <summary>
    /// SorClient現在狀態
    /// </summary>
    public enum SorClientState
    {
        /// <summary>
        /// 切斷連線: 呼叫 Disconnect() 斷線.
        /// </summary>
        [Description("切斷連線: 呼叫 Disconnect() 斷線")]
        SorClientState_Disconnected = -1,
        /// <summary>
        /// 連線失敗: 網路層無法建立與SORS的連線.
        /// </summary>
        [Description("連線失敗: 網路層無法建立與SORS的連線")]
        SorClientState_LinkFail = -2,
        /// <summary>
        /// 連線後斷線: 網路層斷線.
        /// </summary>
        [Description("連線後斷線: 網路層斷線")]
        SorClientState_LinkBroken = -3,
        /// <summary>
        /// 網路層可連線,但對方不是SORS服務.
        /// </summary>
        [Description("網路層可連線,但對方不是SORS服務")]
        SorClientState_UnknownServer = -4,
        /// <summary>
        /// 登入失敗.
        /// </summary>
        [Description("登入失敗")]
        SorClientState_SignonError = -5,
        /// <summary>
        /// 連線失敗: 主機拒絕連線訊息.
        /// </summary>
        [Description("連線失敗: 主機拒絕連線訊息")]
        SorClientState_ConnectError = -6,
        /// <summary>
        /// 連線中斷: 送出的 Heartbeat 主機沒有回覆.
        /// </summary>
        [Description("連線中斷: 送出的 Heartbeat 主機沒有回覆")]
        SorClientState_HeartbeatTimeout = -7,
        /// <summary>
        /// 建構後, 尚未進行連線.
        /// </summary>
        [Description("建構後, 尚未進行連線")]
        SorClientState_Idle = 0,
        /// <summary>
        /// 網路層連線中.
        /// </summary>
        [Description("網路層連線中")]
        SorClientState_Linking = 1,
        /// <summary>
        /// 已與SORS建立已連線: 已初步溝通完畢:已取得SORS服務端名稱.
        /// </summary>
        [Description("已與SORS建立已連線: 已初步溝通完畢:已取得SORS服務端名稱")]
        SorClientState_Connected = 2,
        /// <summary>
        /// 已登入券商系統, 可以進行下單或其他操作.
        /// </summary>
        [Description("已登入券商系統, 可以進行下單或其他操作")]
        SorClientState_ApReady = 3,
    }

    public enum RecoverType
    {
        /// <summary>
        /// 補全部委託,含成交明細
        /// </summary>
        [Description("D")]
        AllIncludeDealDetail,
        /// <summary>
        /// 回補全部，不包含成交明細
        /// </summary>
        [Description("")]
        AllNoDealDetail,
        /// <summary>
        /// 回補全部有剩餘量，並包含成交明細
        /// </summary>
        [Description("Dw")]
        RemainIncludeDealDetail,
        /// <summary>
        /// 補有成交(或UserID相同)的委託,含成交明細
        /// </summary>
        [Description("M")]
        DealWithIDIncludeDealDetail,
        /// <summary>
        /// 補有成交(或UserID相同)的委託,不含成交明細
        /// </summary>
        [Description("m")]
        DealWithIDNoDealDetail,
        /// <summary>
        /// 僅補有成交(不考慮UserID)的委託,含成交明細
        /// </summary>
        [Description("M0")]
        AllDealIncludeDealDetail,
        /// <summary>
        /// 僅補有成交(不考慮UserID)的委託,不含成交明細
        /// </summary>
        [Description("m0")]
        AllDealNoDealDetail,
        /// <summary>
        /// 僅回補還在 [交易所委託簿] 的委託: 還有剩餘量的委託
        /// </summary>
        [Description("w")]
        Remain,
        /// <summary>
        /// 不回補,且不收任何回報
        /// </summary>
        [Description("")]
        NotReceive,
        /// <summary>
        /// 不回補、且不收委託回報，僅收成交回報
        /// </summary>
        [Description("m")]
        OnlyDeal
    }

    public enum TableType
    {
        head,
        Accs,
        CurUsers,
        REQ,
        ORD,
        RPT,
        DDS
    }
    public enum OrderSt
    {
        /// <summary>
        /// 尚未處理.
        /// </summary>        
        NewPending = 0,
        /// <summary>
        /// 新單要求退回給使用者(或營業主管)確定or強迫.
        /// </summary>
        NewBackConfirm = 3,
        /// <summary>
        /// 新單 [交易所收單前] Qu單.
        /// </summary>
        NewBfQueuing = 4,
        /// <summary>
        /// 新單排隊中.
        /// </summary>
        NewQueuing = 5,
        /// <summary>
        /// 新單傳送中.
        /// </summary>
        NewSending = 6,
        /// <summary>
        /// 新單已送出(等回報).
        /// </summary>
        NewSent = 7,
        /// <summary>
        /// 新單[另一台SOR]處理的過程.
        /// </summary>
        OtherHost = 20,
        /// <summary>
        /// [另一台SOR]新單要求退回給使用者(或營業主管)確定or強迫.
        /// </summary>
        OtherNewBackConfirm = 23,
        /// <summary>
        /// [另一台SOR] 新單 [交易所收單前] Qu單.
        /// </summary>
        OtherNewBfQueuing = 24,
        /// <summary>
        /// [另一台SOR]新單排隊中.
        /// </summary>
        OtherNewQueuing = 25,
        /// <summary>
        /// [另一台SOR]新單傳送中.
        /// </summary>
        OtherNewSending = 26,
        /// <summary>
        /// [另一台SOR]新單已送出(等回報).
        /// </summary>
        OtherNewSent = 27,
        /// <summary>
        /// 新單要求(第一階段)已結束,例如:
        /// - 鉅額明細Leg主機已收到,但尚未送給期交所.
        /// - 鉅額議價申報已成功,但尚未送出明細.
        /// </summary>
        NewFinished1 = 51,
        /// <summary>
        /// 新單要求(第二階段)已結束,例如:
        /// - 明細送出中.
        /// </summary>
        NewFinished2 = 52,
        /// <summary>
        /// 以下 NewFinished3..NewFinished9 依此類推,新單各階段的結束.
        /// 由各個委託書自行定義解釋.
        /// 例如: 鉅額議價申報明細OK
        /// </summary>
        NewFinished3 = 53,
        NewFinished4 = 54,
        NewFinished5 = 55,
        NewFinished6 = 56,
        NewFinished7 = 57,
        NewFinished8 = 58,
        NewFinished9 = 59,
        /// <summary>
        /// 一個特殊的[階段性結束]狀態,例如:
        /// - 鉅額議價申報,刪除明細後,回到等候明細狀態(狀態碼變小)
        /// - 或有類似 [狀態碼] 需要變小的情況, 則使用此狀態, 強制同步模組匯入同步資料.
        /// </summary>
        NewBackFinished = 50,
        /// <summary>
        /// 內部刪除 [其他主機 NewQueuing] 的委託.
        /// 實際的刪單處理是在[原主機的同步模組]進行判斷.
        /// 避免原主幾已將新單送出(但尚未同步完成),
        /// 但另一主機卻收到刪單指令.
        /// </summary>
        InternalCanceling = 81,
        /// <summary>
        /// 新單尚未送出就被刪單
        /// </summary>
        InternalCanceled = 91,
        /// <summary>
        /// 新單要求狀態不明(例如:送出後斷線).
        /// </summary>
        NewUnknownFail = 95,
        /// <summary>
        /// 新單要求拒絕.
        /// </summary>
        NewReject = 99,
        /// <summary>
        /// 委託已接受(交易所已接受).
        /// </summary>
        Accepted = 101,
        /// <summary>
        /// 部份成交.
        /// </summary>
        PartFilled = 110,
        /// <summary>
        /// 全部成交.
        /// </summary>
        FullFilled = 111,
        /// <summary>
        /// 一般:委託因IOC/FOK未成交而取消.
        /// 報價:時間到自動刪單,有新報價舊報價自動刪單.
        /// </summary>
        ExchgKilled = 120,
        /// <summary>
        /// 交易所將委託掛起, 此為大陸地區期交所特殊的狀態, 可透過[機活]操作變回可撮合狀態.
        /// </summary>
        Suspended = 130,
        /// <summary>
        /// 今日交易結束: FIX 的 ExecType=3=DoneForDay.
        /// </summary>
        DoneForDay = 140,
        /// <summary>
        /// 委託已取消, 若後續有收到成交, 依然不改變此狀態.
        /// 目前只有 FIX 的取消成功才會設定此狀態.
        /// </summary>
        Canceled = 141,
    }
    //public enum REQ
    //{
    //    TwsNew,
    //    TwsChgQty,
    //    TwsChgAD,
    //    GNew,
    //    GChg
    //}
    //public enum ORD
    //{
    //    TwsOrd,
    //    GOrd
    //}
    //public enum RPT
    //{
    //    TwsNew,
    //    TwsChg,
    //    TwsDeal,
    //    TwsChgAD,
    //    GNew,
    //    GChg
    //}
    //public enum DDS
    //{
    //    TwsOrd,
    //    GOrd
    //}
}