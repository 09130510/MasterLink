using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace FixInitiator.Class.Public
{
    public class DataBase
    {
        private string connStringSEHK_ = string.Empty;
        private int poolSize_ = 100;
        private DataTable dtExecutionReport_ = new DataTable("ExecutionReport");
        
      
        public DataBase() { }
         
        public void SetParam( string server, string uid, string passwd)
        {
            connStringSEHK_ = string.Format("user id={0};server={1};persist security info=True;initial catalog=SEHK;password={2};Pooling=True;min Pool size=5;max Pool size={3}", uid, server, passwd, poolSize_);

        }

        public void FillExecutionReport(string system,DateTime tradeDate)
        {
            string commandText = string.Format("select  Symbol,Account,OrderQty, CumQty,OrderID,ExecID, ExecTransType,LastPx,LastShares,Side,ExecType,LeaveQty, AvgPx,ClOrdID, UpdateDate from dbo.ExecutionReport ");
            commandText += string.Format("Where System='{0}' and datediff(mi,TradeDate,'{1}') = 0 ", system, tradeDate.ToString ("yyyy/M/dd"));
            using (SqlConnection cn = new SqlConnection(connStringSEHK_))
            {
                SqlDataAdapter sda = new SqlDataAdapter(commandText, connStringSEHK_);
                sda.Fill(dtExecutionReport_);
            }
        }

        public void InsertExecutionReport(string system,DateTime tradeDate,int seqNo,string clOrdID, string orderID, string account, string symbol, double price,double orderQty, double cumQty, string execID, char execTransType, double lastPx, double lastShares, char side, char execType, double leaveQty, double avgPx)
        {

            using (SqlConnection cn = new SqlConnection(connStringSEHK_))
            {
                cn.Open();
                SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = cn;


                cmd.CommandText = "Insert into ExecutionReport (System,TradeDate,SeqNo,OrderID,ClOrdID, Account,Symbol,Price,OrderQty, CumQty,ExecID,ExecTransType,LastPx,LastShares,Side,ExecType,LeaveQty ,AvgPx) ";
                cmd.CommandText += " Values (@System,@TradeDate,@SeqNo,@OrderID,@ClOrdID, @Account,@Symbol,@Price,@OrderQty, @CumQty,@ExecID,@ExecTransType,@LastPx,@LastShares,@Side,@ExecType,@LeaveQty ,@AvgPx ) ";

                SqlParameter pSystem = new SqlParameter("@System", SqlDbType.Char, 2);
                SqlParameter pTradeDate = new SqlParameter("@TradeDate", SqlDbType.SmallDateTime);
                SqlParameter pSeqNo = new SqlParameter("@SeqNo", SqlDbType.Int);
                SqlParameter pOrderID = new SqlParameter("@OrderID", SqlDbType.VarChar, 20);
                SqlParameter pClOrdID = new SqlParameter("@ClOrdID", SqlDbType.VarChar, 30);
                SqlParameter pAccount = new SqlParameter("@Account", SqlDbType.VarChar, 20);
                SqlParameter pSymbol = new SqlParameter("@Symbol", SqlDbType.VarChar, 10);
                SqlParameter pPrice = new SqlParameter("@Price", SqlDbType.Float);
                SqlParameter pOrderQty = new SqlParameter("@OrderQty", SqlDbType.Float);
                SqlParameter pCumQty = new SqlParameter("@CumQty", SqlDbType.Float);
                SqlParameter pExecID = new SqlParameter("@ExecID", SqlDbType.VarChar, 2);
                SqlParameter pExecTransType = new SqlParameter("@ExecTransType", SqlDbType.Char ,1);
                SqlParameter pLastPx = new SqlParameter("@LastPx", SqlDbType.Float);
                SqlParameter pLastShares = new SqlParameter("@LastShares", SqlDbType.Float);
                SqlParameter pSide = new SqlParameter("@Side", SqlDbType.Char, 1);
                SqlParameter pExecType = new SqlParameter("@ExecType", SqlDbType.VarChar, 2);
                SqlParameter pLeaveQty = new SqlParameter("@LeaveQty", SqlDbType.Int);
                SqlParameter pAvgPx = new SqlParameter("@AvgPx", SqlDbType.Float);

                /*
                pSystem.Value = system;
                //pTradeDate.Value = tradeDate != null ? tradeDate.Value  : DBNull.Value; 
                pSeqNo.Value = seqNo;
                */

                pSystem.Value = system;
                pTradeDate.Value = tradeDate;
                pSeqNo.Value = seqNo;
                pClOrdID.Value = clOrdID;
                pAccount.Value = account;
                pSymbol.Value = symbol;
                pPrice.Value = price;
                pOrderQty.Value = orderQty;
                pCumQty.Value = cumQty;
                pOrderID.Value = orderID;
                pExecID.Value = execID;
                pExecTransType.Value = execTransType;
                pLastPx.Value = lastPx;
                pLastShares.Value = lastShares;
                pSide.Value = side;
                pExecType.Value = execType;
                pLeaveQty.Value = leaveQty;
                pAvgPx.Value = avgPx;
            

                cmd.Parameters.Clear();

                cmd.Parameters.Add(pSystem);
                cmd.Parameters.Add(pTradeDate);
                cmd.Parameters.Add(pSeqNo);
                cmd.Parameters.Add(pAccount);
                cmd.Parameters.Add(pAvgPx);
                cmd.Parameters.Add(pClOrdID);
                cmd.Parameters.Add(pCumQty);
                cmd.Parameters.Add(pExecID);
                cmd.Parameters.Add(pExecTransType);
                cmd.Parameters.Add(pLastPx);
                cmd.Parameters.Add(pLastShares);
                cmd.Parameters.Add(pOrderID);
                cmd.Parameters.Add(pPrice);
                cmd.Parameters.Add(pOrderQty);
                cmd.Parameters.Add(pSide);
                cmd.Parameters.Add(pSymbol);
                cmd.Parameters.Add(pExecType);
                cmd.Parameters.Add(pLeaveQty);
                

                try
                {
                    int cnt = cmd.ExecuteNonQuery();
                    if (cnt == 0)
                    {
                        UpdateExecutionReport(system, tradeDate, seqNo,clOrdID, orderID, account, symbol, price, orderQty, cumQty, execID, execTransType, lastPx, lastShares, side, execType, leaveQty, avgPx);
                    }
                }
                catch
                {
                    UpdateExecutionReport(system, tradeDate, seqNo,clOrdID, orderID, account, symbol,price, orderQty, cumQty, execID, execTransType, lastPx, lastShares, side, execType, leaveQty, avgPx);
                }
            }
              

        }

        public void UpdateExecutionReport(string system,DateTime tradeDate,int seqNo,string clOrdID, string orderID, string account, string symbol,double price, double orderQty, double cumQty, string execID, char execTransType, double lastPx, double lastShares, char side, char execType, double leaveQty, double avgPx)
        {

            using (SqlConnection cn = new SqlConnection(connStringSEHK_))
            {
                cn.Open();
                SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "Update ExecutionReport Set Account=@Account,Symbol=@Symbol,Price=@Price,OrderQty=@OrderQty,CumQty=@CumQty,OrderID=@OrderID,ExecID=@ExecID,ExecTransType=@ExecTransType,LastPx=@LastPx,LastShares=@LastShares,Side=@Side,ExecType=@ExecType,LeaveQty=@LeaveQty,AvgPx=@AvgPx,ClOrdID=@ClOrdID,UpdateDate=GetDate()";
                cmd.CommandText += " Where System=@System and TradeDate=@TradeDate and SeqNo=@SeqNo";

                SqlParameter pSystem = new SqlParameter("@System", SqlDbType.Char, 2);
                SqlParameter pTradeDate = new SqlParameter("@TradeDate", SqlDbType.SmallDateTime);
                SqlParameter pSeqNo = new SqlParameter("@SeqNo", SqlDbType.Int);
                SqlParameter pOrderID = new SqlParameter("@OrderID", SqlDbType.VarChar, 20);
                SqlParameter pClOrdID = new SqlParameter("@ClOrdID", SqlDbType.VarChar, 30);
                SqlParameter pAccount = new SqlParameter("@Account", SqlDbType.VarChar, 20);
                SqlParameter pSymbol = new SqlParameter("@Symbol", SqlDbType.VarChar, 10);
                SqlParameter pOrderQty = new SqlParameter("@OrderQty", SqlDbType.Float);
                SqlParameter pPrice = new SqlParameter("@Price", SqlDbType.Float);
                SqlParameter pCumQty = new SqlParameter("@CumQty", SqlDbType.Float);
                SqlParameter pExecID = new SqlParameter("@ExecID", SqlDbType.VarChar, 2);
                SqlParameter pExecTransType = new SqlParameter("@ExecTransType", SqlDbType.Char, 1);
                SqlParameter pLastPx = new SqlParameter("@LastPx", SqlDbType.Float);
                SqlParameter pLastShares = new SqlParameter("@LastShares", SqlDbType.Float);
                SqlParameter pSide = new SqlParameter("@Side", SqlDbType.Char, 1);
                SqlParameter pExecType = new SqlParameter("@ExecType", SqlDbType.VarChar, 2);
                SqlParameter pLeaveQty = new SqlParameter("@LeaveQty", SqlDbType.Int);
                SqlParameter pAvgPx = new SqlParameter("@AvgPx", SqlDbType.Float);


                pSystem.Value = system;
                pTradeDate.Value = tradeDate;
                pSeqNo.Value = seqNo;
                pClOrdID.Value = clOrdID;
                pAccount.Value = account;
                pSymbol.Value = symbol;
                pPrice.Value = price;
                pOrderQty.Value = orderQty;
                pCumQty.Value = cumQty;
                pOrderID.Value = orderID;
                pExecID.Value = execID;
                pExecTransType.Value = execTransType;
                pLastPx.Value = lastPx;
                pLastShares.Value = lastShares;
                pSide.Value = side;
                pExecType.Value = execType;
                pLeaveQty.Value = leaveQty;
                pAvgPx.Value = avgPx;

                cmd.Parameters.Clear();
                cmd.Parameters.Add(pSystem);
                cmd.Parameters.Add(pTradeDate);
                cmd.Parameters.Add(pSeqNo);
                cmd.Parameters.Add(pAccount);
                cmd.Parameters.Add(pAvgPx);
                cmd.Parameters.Add(pClOrdID);
                cmd.Parameters.Add(pCumQty);
                cmd.Parameters.Add(pExecID);
                cmd.Parameters.Add(pExecTransType);
                cmd.Parameters.Add(pLastPx);
                cmd.Parameters.Add(pLastShares);
                cmd.Parameters.Add(pOrderID);
                cmd.Parameters.Add(pPrice);
                cmd.Parameters.Add(pOrderQty);
                cmd.Parameters.Add(pSide);
                cmd.Parameters.Add(pSymbol);
                cmd.Parameters.Add(pExecType);
                cmd.Parameters.Add(pLeaveQty);
                

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch { }

            }

        }

        public void InsertDeal(string system, DateTime tradeDate, int seqNo, string clOrdID, string orderID, string account, string symbol, double lastPx, double lastShares, char side, DateTime transactTime)
        {

            using (SqlConnection cn = new SqlConnection(connStringSEHK_))
            {
                cn.Open();
                SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = cn;


                cmd.CommandText = "Insert into Deal(System,TradeDate,SeqNo,OrderID,ClOrdID, Account,Symbol, LastPx,LastShares,Side ) ";
                cmd.CommandText += " Values (@System,@TradeDate,@SeqNo,@OrderID,@ClOrdID, @Account,@Symbol, @LastPx,@LastShares,@Side )";

                SqlParameter pSystem = new SqlParameter("@System", SqlDbType.Char, 2);
                SqlParameter pTradeDate = new SqlParameter("@TradeDate", SqlDbType.SmallDateTime);
                SqlParameter pSeqNo = new SqlParameter("@SeqNo", SqlDbType.Int);
                SqlParameter pOrderID = new SqlParameter("@OrderID", SqlDbType.VarChar, 20);
                SqlParameter pClOrdID = new SqlParameter("@ClOrdID", SqlDbType.VarChar, 30);
                SqlParameter pAccount = new SqlParameter("@Account", SqlDbType.VarChar, 20);
                SqlParameter pSymbol = new SqlParameter("@Symbol", SqlDbType.VarChar, 10);
                SqlParameter pLastPx = new SqlParameter("@LastPx", SqlDbType.Float);
                SqlParameter pLastShares = new SqlParameter("@LastShares", SqlDbType.Float);
                SqlParameter pSide = new SqlParameter("@Side", SqlDbType.Char, 1);
                //SqlParameter pTransactTime = new SqlParameter("@TransactTime", SqlDbType.SmallDateTime);
              
                pSystem.Value = system;
                pTradeDate.Value = tradeDate;
                pSeqNo.Value = seqNo;
                pClOrdID.Value = clOrdID;
                pAccount.Value = account;
                pSymbol.Value = symbol;
                pOrderID.Value = orderID;
                pLastPx.Value = lastPx;
                pLastShares.Value = lastShares;
                pSide.Value = side;
                //pTransactTime.Value = transactTime;
                

                cmd.Parameters.Clear();
                cmd.Parameters.Add(pSystem);
                cmd.Parameters.Add(pTradeDate);
                cmd.Parameters.Add(pSeqNo);
                cmd.Parameters.Add(pAccount);
                cmd.Parameters.Add(pClOrdID);
                cmd.Parameters.Add(pLastPx);
                cmd.Parameters.Add(pLastShares);
                cmd.Parameters.Add(pOrderID);
                cmd.Parameters.Add(pSide);
                cmd.Parameters.Add(pSymbol);
                //cmd.Parameters.Add(pTransactTime);

                try
                {
                    int cnt = cmd.ExecuteNonQuery();
                    if (cnt == 0)
                    {
                        //UpdateDeal(system, tradeDate, seqNo, clOrdID, orderID, account, symbol, price, orderQty, cumQty, execID, execTransType, lastPx, lastShares, side, execType, leaveQty, avgPx);
                    }
                }
                catch (Exception ex)
                {
                   
                    //UpdateDeal(system, tradeDate, seqNo, clOrdID, orderID, account, symbol, price, orderQty, cumQty, execID, execTransType, lastPx, lastShares, side, execType, leaveQty, avgPx);
                }
            }


        }

       

        #region 屬性
        public DataTable dtExecutionReport
        {
            get
            {
                return dtExecutionReport_;
            }
        }
        #endregion
    }
}
