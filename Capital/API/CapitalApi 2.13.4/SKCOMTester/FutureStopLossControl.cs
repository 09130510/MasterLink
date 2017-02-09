using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SKCOMLib;

namespace SKOrderTester
{
    public partial class FutureStopLossControl : UserControl
    {

        #region Define Variable
        //----------------------------------------------------------------------
        // Define Variable
        //----------------------------------------------------------------------

        private int m_nCode;
        public string m_strMessage;

        public delegate void MyMessageHandler(string strType, int nCode, string strMessage);
        public event MyMessageHandler GetMessage;

        public delegate void OrderHandler(string strLogInID, bool bAsyncOrder, FUTUREORDER pOrder);
        public event OrderHandler OnFutureStopLossOrderSignal;


        public delegate void MovingOrderHandler(string strLogInID, bool bAsyncOrder, FUTUREORDER pOrder);
        public event MovingOrderHandler OnMovingStopLossOrderSignal;
        
        public delegate void OptionOrderHandler(string strLogInID, bool bAsyncOrder, FUTUREORDER pOrder);
        public event OptionOrderHandler OnOptionStopLossOrderSignal;

        public delegate void CancelFutrueOrderHandler(string strLogInID, bool bAsyncOrder, string strAccount, string strBookNo, string strSymbol, string strBuySell, string strPrice, string strQty, string strTriggerPrice, string strTradeType, string strDayTrade );
        public event CancelFutrueOrderHandler OnCancelFutureStopLossOrderSignal;

        public delegate void CancelMovingOrderHandler(string strLogInID, bool bAsyncOrder, string strAccount, string strBookNo, string strSymbol, string strBuySell, string strTriggerPrice, string strQty, string strMovingPoint, string strTradeType, string strDayTrade);
        public event CancelMovingOrderHandler OnCancelMovingStopLossOrderSignal;


        public delegate void CancelOptionOrderHandler(string strLogInID, bool bAsyncOrder, string strAccount, string strBookNo, string strSymbol, string strBuySell, string strPrice, string strQty, string strTriggerPrice, string strTradeType, string strDayTrade);
        public event CancelOptionOrderHandler OnCancelOptionStopLossOrderSignal;

        public delegate void StopLossReportHandler(string strLogInID, string strAccount, int nReportStatus, string strKind);
        public event StopLossReportHandler OnStopLossReportSignal;

        private string m_UserID = "";
        public string UserID
        {
            get { return m_UserID; }
            set { m_UserID = value; }
        }

        private string m_UserAccount = "";
        public string UserAccount
        {
            get { return m_UserAccount; }
            set { m_UserAccount = value; }
        }

        #endregion

        #region Initialize
        //----------------------------------------------------------------------
        // Initialize
        //----------------------------------------------------------------------
        public FutureStopLossControl()
        {
            InitializeComponent();
        }

        #endregion

        private void btnSendFutureStopOrder_Click(object sender, EventArgs e)
        {
            if (m_UserAccount == "")
            {
                MessageBox.Show("請選擇期貨帳號");
                return;
            }

            string strFutureNo;
            int nBidAsk;
            int nPeriod;
            int nFlag;
            string strPrice;
            string strTigger;
            int nQty;


            if (txtStockNo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼");
                return;
            }
            strFutureNo = txtStockNo.Text.Trim();

            if (boxBidAsk.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣別");
                return;
            }
            nBidAsk = boxBidAsk.SelectedIndex;

            if (boxPeriod.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託條件");
                return;
            }
            nPeriod = boxPeriod.SelectedIndex;

            if (boxFlag.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇當沖與否");
                return;
            }
            nFlag = boxFlag.SelectedIndex;

            double dPrice = 0.0;
            if (double.TryParse(txtPrice.Text.Trim(), out dPrice) == false)
            {
                MessageBox.Show("委託價請輸入數字");
                return;
            }
            strPrice = txtPrice.Text.Trim();

            if (int.TryParse(txtQty.Text.Trim(), out nQty) == false)
            {
                MessageBox.Show("委託量請輸入數字");
                return;
            }

            strTigger = txtTrigger.Text.Trim();

            FUTUREORDER pFutureOrder = new FUTUREORDER();

            pFutureOrder.bstrFullAccount = m_UserAccount;
            pFutureOrder.bstrPrice = strPrice;
            pFutureOrder.bstrStockNo = strFutureNo;
            pFutureOrder.nQty = nQty;
            pFutureOrder.sBuySell = (short)nBidAsk;
            pFutureOrder.sDayTrade = (short)nFlag;
            pFutureOrder.sTradeType = (short)nPeriod;
            pFutureOrder.bstrTrigger = strTigger;

            if (OnFutureStopLossOrderSignal != null)
            {
                OnFutureStopLossOrderSignal(m_UserID, false, pFutureOrder);
            }
        }

        private void btnSendFutureStopLossOrderAsync_Click(object sender, EventArgs e)
        {
            if (m_UserAccount == "")
            {
                MessageBox.Show("請選擇期貨帳號");
                return;
            }

            string strFutureNo;
            int nBidAsk;
            int nPeriod;
            int nFlag;
            string strPrice;
            string strTigger;
            int nQty;


            if (txtStockNo.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼");
                return;
            }
            strFutureNo = txtStockNo.Text.Trim();

            if (boxBidAsk.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣別");
                return;
            }
            nBidAsk = boxBidAsk.SelectedIndex;

            if (boxPeriod.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託條件");
                return;
            }
            nPeriod = boxPeriod.SelectedIndex;

            if (boxFlag.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇當沖與否");
                return;
            }
            nFlag = boxFlag.SelectedIndex;

            double dPrice = 0.0;
            if (double.TryParse(txtPrice.Text.Trim(), out dPrice) == false)
            {
                MessageBox.Show("委託價請輸入數字");
                return;
            }
            strPrice = txtPrice.Text.Trim();

            if (int.TryParse(txtQty.Text.Trim(), out nQty) == false)
            {
                MessageBox.Show("委託量請輸入數字");
                return;
            }

            strTigger = txtTrigger.Text.Trim();

            FUTUREORDER pFutureOrder = new FUTUREORDER();

            pFutureOrder.bstrFullAccount = m_UserAccount;
            pFutureOrder.bstrPrice = strPrice;
            pFutureOrder.bstrStockNo = strFutureNo;
            pFutureOrder.nQty = nQty;
            pFutureOrder.sBuySell = (short)nBidAsk;
            pFutureOrder.sDayTrade = (short)nFlag;
            pFutureOrder.sTradeType = (short)nPeriod;
            pFutureOrder.bstrTrigger = strTigger;

            if (OnFutureStopLossOrderSignal != null)
            {
                OnFutureStopLossOrderSignal(m_UserID, true, pFutureOrder);
            }
        }


        private void btnMovingStopLossOrder_Click(object sender, EventArgs e)
        {
            if (m_UserAccount == "")
            {
                MessageBox.Show("請選擇期貨帳號");
                return;
            }

            string strFutureNo;
            int nBidAsk;
            int nPeriod;
            int nFlag;
            string strMovingPint;
            string strTigger;
            int nQty;


            if (txtStockNo2.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼");
                return;
            }
            strFutureNo = txtStockNo2.Text.Trim();

            if (boxBidAsk2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣別");
                return;
            }
            nBidAsk = boxBidAsk2.SelectedIndex;

            if (boxPeriod2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託條件");
                return;
            }
            nPeriod = boxPeriod2.SelectedIndex;

            if (boxFlag2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇當沖與否");
                return;
            }
            nFlag = boxFlag2.SelectedIndex;

            double dPrice = 0.0;
            if (double.TryParse(txtMovingPoint.Text.Trim(), out dPrice) == false)
            {
                MessageBox.Show("移動點數請輸入數字");
                return;
            }
            strMovingPint = txtMovingPoint.Text.Trim();

            if (int.TryParse(txtQty2.Text.Trim(), out nQty) == false)
            {
                MessageBox.Show("委託量請輸入數字");
                return;
            }

            strTigger = txtTrigger2.Text.Trim();

            FUTUREORDER pFutureOrder = new FUTUREORDER();

            pFutureOrder.bstrFullAccount = m_UserAccount;
            //pFutureOrder.bstrPrice = strPrice;
            pFutureOrder.bstrStockNo = strFutureNo;
            pFutureOrder.nQty = nQty;
            pFutureOrder.sBuySell = (short)nBidAsk;
            pFutureOrder.sDayTrade = (short)nFlag;
            pFutureOrder.sTradeType = (short)nPeriod;
            pFutureOrder.bstrTrigger = strTigger;
            pFutureOrder.bstrMovingPoint = strMovingPint;

            if (OnMovingStopLossOrderSignal != null)
            {
                OnMovingStopLossOrderSignal(m_UserID, false, pFutureOrder);
            }
        }

        private void btnMovingStopLossOrderAsync_Click(object sender, EventArgs e)
        {
            if (m_UserAccount == "")
            {
                MessageBox.Show("請選擇期貨帳號");
                return;
            }

            string strFutureNo;
            int nBidAsk;
            int nPeriod;
            int nFlag;
            string strMovingPint;
            string strTigger;
            int nQty;


            if (txtStockNo2.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼");
                return;
            }
            strFutureNo = txtStockNo2.Text.Trim();

            if (boxBidAsk2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣別");
                return;
            }
            nBidAsk = boxBidAsk2.SelectedIndex;

            if (boxPeriod2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託條件");
                return;
            }
            nPeriod = boxPeriod2.SelectedIndex;

            if (boxFlag2.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇當沖與否");
                return;
            }
            nFlag = boxFlag2.SelectedIndex;

            double dPrice = 0.0;
            if (double.TryParse(txtMovingPoint.Text.Trim(), out dPrice) == false)
            {
                MessageBox.Show("移動點數請輸入數字");
                return;
            }
            strMovingPint = txtMovingPoint.Text.Trim();

            if (int.TryParse(txtQty2.Text.Trim(), out nQty) == false)
            {
                MessageBox.Show("委託量請輸入數字");
                return;
            }

            strTigger = txtTrigger2.Text.Trim();

            FUTUREORDER pFutureOrder = new FUTUREORDER();

            pFutureOrder.bstrFullAccount = m_UserAccount;
            //pFutureOrder.bstrPrice = strPrice;
            pFutureOrder.bstrStockNo = strFutureNo;
            pFutureOrder.nQty = nQty;
            pFutureOrder.sBuySell = (short)nBidAsk;
            pFutureOrder.sDayTrade = (short)nFlag;
            pFutureOrder.sTradeType = (short)nPeriod;
            pFutureOrder.bstrTrigger = strTigger;
            pFutureOrder.bstrMovingPoint = strMovingPint;

            if (OnMovingStopLossOrderSignal != null)
            {
                OnMovingStopLossOrderSignal(m_UserID, true, pFutureOrder);
            }
        }

        private void btnSendOptionOrder_Click(object sender, EventArgs e)
        {
            if (m_UserAccount == "")
            {
                MessageBox.Show("請選擇期貨帳號");
                return;
            }

            string strFutureNo;
            int nBidAsk;
            int nPeriod;
            int nFlag;
            string strPrice;
            string strTigger;
            int nQty;


            if (txtStockNo3.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼");
                return;
            }
            strFutureNo = txtStockNo3.Text.Trim();

            if (boxBidAsk3.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣別");
                return;
            }
            nBidAsk = boxBidAsk3.SelectedIndex;

            if (boxPeriod3.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託條件");
                return;
            }
            nPeriod = boxPeriod3.SelectedIndex;

            if (boxFlag3.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇當沖與否");
                return;
            }
            nFlag = boxFlag3.SelectedIndex;

            strPrice = txtPrice3.Text.Trim();

            if (int.TryParse(txtQty3.Text.Trim(), out nQty) == false)
            {
                MessageBox.Show("委託量請輸入數字");
                return;
            }
            strTigger = txtTrigger3.Text.Trim();

            FUTUREORDER pFutureOrder = new FUTUREORDER();

            pFutureOrder.bstrFullAccount = m_UserAccount;
            pFutureOrder.bstrPrice = strPrice;
            pFutureOrder.bstrStockNo = strFutureNo;
            pFutureOrder.nQty = nQty;
            pFutureOrder.sBuySell = (short)nBidAsk;
            pFutureOrder.sNewClose = (short)nFlag;
            pFutureOrder.sTradeType = (short)nPeriod;
            pFutureOrder.bstrTrigger = strTigger;

            if (OnOptionStopLossOrderSignal != null)
            {
                OnOptionStopLossOrderSignal(m_UserID, false, pFutureOrder);
            }
        }

        private void btnSendOptionOrderAsync_Click(object sender, EventArgs e)
        {
            if (m_UserAccount == "")
            {
                MessageBox.Show("請選擇期貨帳號");
                return;
            }

            string strFutureNo;
            int nBidAsk;
            int nPeriod;
            int nFlag;
            string strPrice;
            string strTigger;
            int nQty;


            if (txtStockNo3.Text.Trim() == "")
            {
                MessageBox.Show("請輸入商品代碼");
                return;
            }
            strFutureNo = txtStockNo3.Text.Trim();

            if (boxBidAsk3.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇買賣別");
                return;
            }
            nBidAsk = boxBidAsk3.SelectedIndex;

            if (boxPeriod3.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇委託條件");
                return;
            }
            nPeriod = boxPeriod3.SelectedIndex;

            if (boxFlag3.SelectedIndex < 0)
            {
                MessageBox.Show("請選擇當沖與否");
                return;
            }
            nFlag = boxFlag3.SelectedIndex;

            strPrice = txtPrice3.Text.Trim();

            if (int.TryParse(txtQty3.Text.Trim(), out nQty) == false)
            {
                MessageBox.Show("委託量請輸入數字");
                return;
            }
            strTigger = txtTrigger3.Text.Trim();

            FUTUREORDER pFutureOrder = new FUTUREORDER();

            pFutureOrder.bstrFullAccount = m_UserAccount;
            pFutureOrder.bstrPrice = strPrice;
            pFutureOrder.bstrStockNo = strFutureNo;
            pFutureOrder.nQty = nQty;
            pFutureOrder.sBuySell = (short)nBidAsk;
            pFutureOrder.sNewClose = (short)nFlag;
            pFutureOrder.sTradeType = (short)nPeriod;
            pFutureOrder.bstrTrigger = strTigger;

            if (OnOptionStopLossOrderSignal != null)
            {
                OnOptionStopLossOrderSignal(m_UserID, true, pFutureOrder);
            }
        }

        private void btnCancelFutureStopLoss_Click(object sender, EventArgs e)
        {
            if (OnCancelFutureStopLossOrderSignal != null)
            {
                OnCancelFutureStopLossOrderSignal("", true, m_UserAccount, txtCancelBookNo.Text.Trim(), txtCaneclSymbol.Text.Trim(), "", "", "", "", "", "");
            }
        }

        private void btnCancelMovingStopLoss_Click(object sender, EventArgs e)
        {
            if (OnCancelMovingStopLossOrderSignal != null)
            {
                OnCancelMovingStopLossOrderSignal(m_UserID, true, m_UserAccount, txtCancelBookNo.Text.Trim(), txtCaneclSymbol.Text.Trim(), "", "", "", "", "", "");
            }
        }

        private void btnCancelOptionStopLoss_Click(object sender, EventArgs e)
        {
            if (OnCancelOptionStopLossOrderSignal != null)
            {
                OnCancelOptionStopLossOrderSignal(m_UserID, true, m_UserAccount, txtCancelBookNo.Text.Trim(), txtCaneclSymbol.Text.Trim(), "", "", "", "", "", "");
            }
        }

        private void btnGetStopLossReport_Click(object sender, EventArgs e)
        {
            if (OnStopLossReportSignal != null)
            {
                OnStopLossReportSignal(m_UserID, m_UserAccount, boxTypeReport.SelectedIndex, boxKindReport.Text.Trim());
            }
        }
    }
}
