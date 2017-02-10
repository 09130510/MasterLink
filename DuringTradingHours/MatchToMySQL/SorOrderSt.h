#ifndef SorOrderStH
#define SorOrderStH

   /// 下單要求流程,某一步驟處理過程.
   enum TReqStep
   {
      /// 委託要求處理中.
      TReqStep_Pending     = 0,

      /// 風控檢查中(遠端風控、圈存).
      TReqStep_ScChecking  = 2,
      /// 本機委託要求退回給使用者(或營業主管)確定or強迫.
      TReqStep_BackConfirm = 3,
      /// [交易所收單前] Qu單.
      TReqStep_BfQueuing   = 4,
      /// 本機委託要求排隊中.
      TReqStep_Queuing     = 5,
      /// 本機委託要求傳送中.
      TReqStep_Sending     = 6,
      /// 本機委託要求已送出(等回報).
      TReqStep_Sent        = 7,

      /// [另一台SOR]處理的過程.
      TReqStep_OtherHost        = 20,
      /// [另一台SOR]處理的委託要求退回給使用者(或營業主管)確定or強迫.
      TReqStep_OtherBackConfirm = TReqStep_OtherHost + TReqStep_BackConfirm,
      /// [另一台SOR] 的 [交易所收單前] Qu單.
      TReqStep_OtherBfQueuing   = TReqStep_OtherHost + TReqStep_BfQueuing,
      /// [另一台SOR]處理的委託要求排隊中.
      TReqStep_OtherQueuing     = TReqStep_OtherHost + TReqStep_Queuing,
      /// [另一台SOR]處理的委託要求傳送中.
      TReqStep_OtherSending     = TReqStep_OtherHost + TReqStep_Sending,
      /// [另一台SOR]處理的託要求已送出(等回報).
      TReqStep_OtherSent        = TReqStep_OtherHost + TReqStep_Sent,

      /// 當SorApi設定[處理過程不回報]時,若有暫時性的訊息,
      /// 例如: [下載有誤,重試中...]
      TReqStep_AckMessage = 40,
      
      /// 部份結束(失敗):例如:收到報價單的Bid失敗,Offer尚未回覆.
      TReqStep_PartReject = 88,
      /// 部份結束:例如收到報價單的其中一邊,還缺另一邊.
      TReqStep_PartFinish = 89,

      /// 委託要求已結束.
      TReqStep_Finished    = 90,
      /// 重複的[結束]回報,例如:報價單,先收到Bid成交,再收到Offer成功,此時Offer成功的回報就是TReqStep_DupFinished.
      /// 此次的委託異動,不會回覆給 SorApi.
      TReqStep_DupFinished = 91,

      /// 委託要求狀態不明(例如:送出後斷線).
      TReqStep_UnknownFail = 95,
      /// 委託要求拒絕.
      TReqStep_Reject      = 99,
      /// 先收到成交回報,所建立的新單(遺漏正常新單回報).
      /// 如果遺漏新單,先收到刪改,仍直接使用 [TReqKind_ChgXXX or TReqKind_Del] + [一般流程步驟].
      TReqStep_RptSuggestNew = 100,

      /// 配合 TReqKind_RptFilled: 部份成交
      TReqStep_RptPartFilled = 110,
      /// 配合 TReqKind_RptFilled: 全部成交.
      TReqStep_RptFullFilled = 111,

      /// 委託因IOC/FOK未成交而取消.
      TReqStep_RptExchgKilled = 120,
      /// 報價單收到另一邊的交易所刪單.
      TReqStep_RptExchgKilled2 = 121,

      /// 刪後改單結束.
      TReqStep_ADFinished = 125,

      /// 步驟已結束,但後續又有變動.
      /// 例如: FIX 的 ExecType=D(Restated).
      TReqStep_Restated   = 126,
   };

   /// 委託現在狀態.
   enum TOrderSt
   {
      /// 尚未處理.
      TOrderSt_NewPending = TReqStep_Pending,
      /// 新單要求退回給使用者(或營業主管)確定or強迫.
      TOrderSt_NewBackConfirm = TReqStep_BackConfirm,
      /// 新單 [交易所收單前] Qu單.
      TOrderSt_NewBfQueuing   = TReqStep_BfQueuing,
      /// 新單排隊中.
      TOrderSt_NewQueuing     = TReqStep_Queuing,
      /// 新單傳送中.
      TOrderSt_NewSending     = TReqStep_Sending,
      /// 新單已送出(等回報).
      TOrderSt_NewSent        = TReqStep_Sent,

      /// 新單[另一台SOR]處理的過程.
      TOrderSt_OtherHost           = TReqStep_OtherHost,
      /// [另一台SOR]新單要求退回給使用者(或營業主管)確定or強迫.
      TOrderSt_OtherNewBackConfirm = TOrderSt_OtherHost + TOrderSt_NewBackConfirm,
      /// [另一台SOR] 新單 [交易所收單前] Qu單.
      TOrderSt_OtherNewBfQueuing   = TOrderSt_OtherHost + TOrderSt_NewBfQueuing,
      /// [另一台SOR]新單排隊中.
      TOrderSt_OtherNewQueuing     = TOrderSt_OtherHost + TOrderSt_NewQueuing,
      /// [另一台SOR]新單傳送中.
      TOrderSt_OtherNewSending     = TOrderSt_OtherHost + TOrderSt_NewSending,
      /// [另一台SOR]新單已送出(等回報).
      TOrderSt_OtherNewSent        = TOrderSt_OtherHost + TOrderSt_NewSent,


      /// 新單要求(第一階段)已結束,例如:
      /// - 鉅額明細Leg主機已收到,但尚未送給期交所.
      /// - 鉅額議價申報已成功,但尚未送出明細.
      TOrderSt_NewFinished1 = 51,
      /// 新單要求(第二階段)已結束,例如:
      /// - 明細送出中.
      TOrderSt_NewFinished2 = 52,
      /// 以下 TOrderSt_NewFinished3..TOrderSt_NewFinished9 依此類推,新單各階段的結束.
      /// 由各個委託書自行定義解釋.
      /// 例如: 鉅額議價申報明細OK
      TOrderSt_NewFinished3 = 53,
      TOrderSt_NewFinished4 = 54,
      TOrderSt_NewFinished5 = 55,
      TOrderSt_NewFinished6 = 56,
      TOrderSt_NewFinished7 = 57,
      TOrderSt_NewFinished8 = 58,
      TOrderSt_NewFinished9 = 59,
      /// 一個特殊的[階段性結束]狀態,例如:
      /// - 鉅額議價申報,刪除明細後,回到等候明細狀態(狀態碼變小)
      /// - 或有類似 [狀態碼] 需要變小的情況, 則使用此狀態, 強制同步模組匯入同步資料.
      TOrderSt_NewBackFinished = 50,


      /// 內部刪除 [其他主機 NewQueuing] 的委託.
      /// 實際的刪單處理是在[原主機的同步模組]進行判斷.
      /// 避免原主幾已將新單送出(但尚未同步完成),
      /// 但另一主機卻收到刪單指令.
      TOrderSt_InternalCanceling = 81,
      /// 新單尚未送出就被刪單.
      TOrderSt_InternalCanceled = 91,


      /// 新單要求狀態不明(例如:送出後斷線).
      TOrderSt_NewUnknownFail = TReqStep_UnknownFail,
      /// 新單要求拒絕.
      TOrderSt_NewReject = TReqStep_Reject,

      /// 委託已接受(交易所已接受).
      TOrderSt_Accepted  = 101,
      /// 部份成交.
      TOrderSt_PartFilled = TReqStep_RptPartFilled,
      /// 全部成交.
      TOrderSt_FullFilled = TReqStep_RptFullFilled,
      /// 一般:委託因IOC/FOK未成交而取消.
      /// 報價:時間到自動刪單,有新報價舊報價自動刪單.
      TOrderSt_ExchgKilled = TReqStep_RptExchgKilled,

      /// 交易所將委託掛起, 此為大陸地區期交所特殊的狀態, 可透過[機活]操作變回可撮合狀態.
      TOrderSt_Suspended = 130,

      /// 今日交易結束: FIX 的 ExecType=3=DoneForDay.
      TOrderSt_DoneForDay = 140,
      /// 委託已取消, 若後續有收到成交, 依然不改變此狀態.
      /// 目前只有 FIX 的取消成功才會設定此狀態.
      TOrderSt_Canceled   = 141,
   };

#endif
