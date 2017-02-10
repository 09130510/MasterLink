using System;
using System.Collections.Generic;
using System.Text;
using QuickFix;
using System.Threading;
using FixInitiator.Class;
using System.Drawing;
using System.Reflection;
using FixInitiator.Class;


namespace FixInitiator.Class
{
    class ClientInitiator : QuickFix.Application
    {
        public void onCreate(QuickFix.SessionID value)
        {
            string s = string.Format("{0}\t{1}:{2},SessionID:{3}", DateTime.Now.ToString("hh:mm:ss.ffff"), MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name, value);
            MainClass.mainClass.logs.Add(s);
            MainClass.mainClass.ShowMessage(s);
        }

        public void onLogon(QuickFix.SessionID value)
        {
            MainClass.mainClass.isLogon = true;
            string s = string.Format("{0}\t{1}:{2},SessionID:{3}", DateTime.Now.ToString("hh:mm:ss.ffff"), MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name, value);
            MainClass.mainClass.logs.Add(s);
            MainClass.mainClass.ShowMessage(s);
            if (MainClass.mainClass.isResetSeq)
                MainClass.mainClass.isResetSeq = false;
            
        }

        public void onLogout(QuickFix.SessionID value)
        {
            MainClass.mainClass.isLogon = false;
            string s = string.Format("{0}\t{1}:{2},SessionID:{3}", DateTime.Now.ToString("hh:mm:ss.ffff"), MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name, value);
            MainClass.mainClass.logs.Add(s);
            MainClass.mainClass.ShowMessage(s);
        }

        public void toAdmin(QuickFix.Message value, QuickFix.SessionID session)
        {
            if (MainClass.mainClass.isResetSeq)   //重設序號
            {
                value.setString(ResetSeqNumFlag.FIELD, "Y");
            }
           
            string s = string.Format("{0}\t{1}:{2},SessionID:{3}", DateTime.Now.ToString("hh:mm:ss.ffff"), MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name,session);
            MainClass.mainClass.logs.Add(s);
            MainClass.mainClass.ShowMessage(s);
            s = string.Format("Message:{0}",  value);
            MainClass.mainClass.logs.Add(s);
            MainClass.mainClass.ShowMessage(s);
          
        }

        public void toApp(QuickFix.Message value, QuickFix.SessionID session)
        {
            string s = string.Format("{0}\t{1}:{2},SessionID:{3},Message:{4}", DateTime.Now.ToString("hh:mm:ss.ffff"), MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name, session, value);
            MainClass.mainClass.logs.Add(s);
            MainClass.mainClass.ShowMessage(s);
        }

        public void fromAdmin(QuickFix.Message value, SessionID session)
        {
            string s = string.Format("{0}\t{1}:{2},SessionID:{3},Message:{4}", DateTime.Now.ToString("hh:mm:ss.ffff"), MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name, session, value);
            MainClass.mainClass.logs.Add(s);
            MainClass.mainClass.ShowMessage(s);
        }
         /*
                     char timeInForce = er.getTimeInForce().getValue();
                    if (er.isSetTimeInForce())
                    {
                        er.getTimeInForce().getValue();
                    }
                    er.getTimeInForce().getValue(); 
                     */
        public void fromApp(QuickFix.Message value, SessionID session)
        {
            string s = string.Empty;
            if (value is QuickFix42.ExecutionReport)
            {
                QuickFix42.ExecutionReport er = (QuickFix42.ExecutionReport)value;
                ExecType et = (ExecType)er.getExecType();

                /*
                Order order = new Order( 
                                                                er.getClOrdID().getValue(),
                                                                er.getOrderID().getValue(),
                                                                er.getAccount().getValue(),
                                                                er.getSymbol().getValue(),
                                                                er.getOrderQty().getValue(),
                                                                er.getCumQty().getValue(),
                                                                er.getExecID().getValue(),
                                                                er.getExecTransType().getValue(),
                                                                er.getLastPx().getValue(),
                                                                er.getLastShares().getValue(),
                                                                er.getSide().getValue(),
                                                                er.getExecType().getValue(),
                                                                er.getLeavesQty().getValue(),
                                                                er.getAvgPx().getValue());
                 */
                
                string clOrdID =er.getExecType().getValue() == ExecType.CANCELED? er.getOrigClOrdID ().getValue ():er.getClOrdID().getValue();
               
                string system = string.Empty;
                DateTime tradeDate = DateTime.MinValue;
                int seqNo = -1;
                try
                {
                    system = clOrdID.Substring(0, 2);
                    tradeDate = DateTime.Parse(clOrdID.Substring(2, 10));
                    seqNo = int.Parse(clOrdID.Substring(12, 6));
                    DateTime transactTime = DateTime.MinValue;
                    if (er.isSetTransactTime ())
                        transactTime = er.getTransactTime().getValue();
                  
                   
                    MainClass.mainClass.dataBase.InsertExecutionReport(system,
                                                                tradeDate,
                                                                seqNo,
                                                                clOrdID,
                                                                //er.getClOrdID().getValue(),
                                                                er.getOrderID().getValue(),
                                                                er.getAccount().getValue(),
                                                                er.getSymbol().getValue(),
                                                                er.getPrice().getValue(),
                                                                er.getOrderQty().getValue(),
                                                                er.getCumQty().getValue(),
                                                                er.getExecID().getValue(),
                                                                er.getExecTransType().getValue(),
                                                                er.getLastPx().getValue(),
                                                                er.getLastShares().getValue(),
                                                                er.getSide().getValue(),
                                                                er.getExecType().getValue(),
                                                                er.getLeavesQty().getValue(),
                                                                er.getAvgPx().getValue());
                    


                    if (er.getExecType().getValue() == ExecType.FILL || er.getExecType().getValue() == ExecType.PARTIAL_FILL)  //成回另外寫檔
                    {
                         MainClass.mainClass.dataBase.InsertDeal(system, tradeDate, seqNo, clOrdID,er.getOrderID().getValue(), er.getAccount().getValue(), er.getSymbol().getValue(), er.getLastPx().getValue(), er.getLastShares().getValue(), er.getSide().getValue(), transactTime);
                    }
                   
                     
                                                                   
                }
                catch (Exception ex)
                {
                    s = string.Format("{0}\t{1}:{2},Source:{3},Message:{4},SessionID:{5},Message:{6}", DateTime.Now.ToString("hh:mm:ss.ffff"), MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name, ex.Source, ex.Message, session, value);
                    MainClass.mainClass.logs.Add(s);
                    MainClass.mainClass.ShowMessage(s);
                }
                 
                 
            }

            s = string.Format("{0}\t{1}:{2},SessionID:{3},Message:{4}", DateTime.Now.ToString("hh:mm:ss.ffff"), MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name, session, value);
            MainClass.mainClass.logs.Add(s);
            MainClass.mainClass.ShowMessage(s);
        }
    }
}
