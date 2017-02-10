using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BrightIdeasSoftware;
using Capital.Report.Class;
using OrderProcessor.Capital;
using Util.Extension.Class;
using WeifenLuo.WinFormsUI.Docking;
using System.Reflection;
using Util.Extension;
using Capital.Report.Properties;

namespace Capital.Report
{
    public partial class frmOrder : DockContent
    {
        public frmOrder()
        {
            InitializeComponent();
            Utility.LoadConfig(this);
            LoadLayout();

            NotificationCenter.Instance.AddObserver(OnCapitalInitialize, "isCapitalInit");
            NotificationCenter.Instance.AddObserver(OnChangeProduct, "CHANGEPRODUCT");

            Core.Instance.Order.OnOrderReply += new CapitalProcessor.OnReplyDelegate(OnReply);
            Core.Instance.Order.OnMatchReply += new CapitalProcessor.OnReplyDelegate(OnReply);
            Core.Instance.Order.OnCancelReply += new CapitalProcessor.OnReplyDelegate(OnReply);
            Core.Instance.Order.OnErrorReply += new CapitalProcessor.OnErrorDelegate(OnErrorReply);
        }
        private void frmOrder_FormClosing(object sender, FormClosingEventArgs e)
        {
            NotificationCenter.Instance.RemoveObserver(OnCapitalInitialize, "isCapitalInit");
            NotificationCenter.Instance.RemoveObserver(OnChangeProduct, "CHANGEPRODUCT");

            Core.Instance.Order.OnOrderReply -= new CapitalProcessor.OnReplyDelegate(OnReply);
            Core.Instance.Order.OnMatchReply -= new CapitalProcessor.OnReplyDelegate(OnReply);
            Core.Instance.Order.OnCancelReply -= new CapitalProcessor.OnReplyDelegate(OnReply);
            Core.Instance.Order.OnErrorReply -= new CapitalProcessor.OnErrorDelegate(OnErrorReply);
        }
        private void chkValid_CheckedChanged(object sender, EventArgs e)
        {
            _Order();
            Utility.SaveConfig(chkValid);
        }        
        private void olvOrder_CellRightClick(object sender, CellRightClickEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                olvOrder.ContextMenuStrip = null;
            }
            else
            {
                Order o = (Order)e.Item.RowObject;
                if (o.Qty!=0 && o.Qty-o.SumQty >0)
                {
                    _ChangeOrderMenu(o);
                    olvOrder.ContextMenuStrip = cmChangeOrder;
                }
                else
                {
                    olvOrder.ContextMenuStrip = null;
                }
                
            }            
        }
        private void olvOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            var radio = gupAction.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
            if (radio != radNone && olvOrder.SelectedObject != null)
            {
                NotificationCenter.Instance.Post("DataFilter", new Notification(radio == radFilter ? "Filter" : "Renderer", (olvOrder.SelectedObject as OrderProcessor.Capital.Order).OrdNo));
            }
            else
            {
                NotificationCenter.Instance.Post("DataFilter");
            }

            if (olvOrder.SelectedObject != null)
            {
                NotificationCenter.Instance.Post("SELECTEDORDER", new Notification(null, olvOrder.SelectedObject));
            }
        }
        private void cmBuy_Click(object sender, EventArgs e)
        {
            Order o = (Order)cmChangeOrder.Tag;
            string account = $"{o.BrokerID}{o.CustNo}";
            double price = double.Parse(((ToolStripMenuItem)sender).Text);

            if (!Core.Instance.DoCancel2(account, o.KeyNo))
            {
                return;
            }
            //下剩餘口數就好
            Core.Instance.DoOrder($"{o.BrokerID}{o.CustNo}", o.ExchangeID, o.OrderHead, o.YM, o.YM2,o.BuySell, price, o.Qty-o.SumQty);
            //string msg;
            //Core.Instance.Order.Cancel(account, o.KeyNo, out msg);
            //if (!string.IsNullOrEmpty(msg))
            //{
            //    AlertBox.AlertWithoutReply(this, AlertBoxButton.Error_OK, "刪單錯誤", msg);
            //    return;
            //}
            //if (Utility.Load<bool>("ORDERSETTING", "ORDERALERT"))
            //{
            //    //下單前詢問
            //    if (!AlertBox.Alert(null, AlertBoxButton.OKCancel, "下單",
            //        new MsgLine("Account", account),
            //        new MsgLine("Exch.", o.ExchangeID),
            //        new MsgLine("Symbol", o.OrderHead + o.YM),
            //        new MsgLine("Side", o.BuySell),
            //        new MsgLine("Qty", o.Qty.ToString("#,##0")),
            //        new MsgLine("Price", price)))
            //    {
            //        Utility.Log(this, "Order", "Cancel");
            //        return;
            //    }
            //}            
            //Core.Instance.Order.Order($"{o.BrokerID}{o.CustNo}", o.ExchangeID, o.OrderHead, o.YM, o.BuySell, o.Qty, price, out msg);
        }
        private void radNone_CheckedChanged(object sender, EventArgs e)
        {
            olvOrder_SelectedIndexChanged(this, EventArgs.Empty);
        }

        #region Delegate
        private void OnChangeProduct(Notification n)
        {
            PriceProcessor.Capital.Product p = (PriceProcessor.Capital.Product)n.Message;

            string filter = p == null ? string.Empty : p.OrderPID;
            if (string.IsNullOrEmpty(filter))
            {
                olvOrder.DefaultRenderer = null;
                return;
            }
            TextMatchFilter filter1 = TextMatchFilter.Contains(olvOrder, filter);
            filter1.Columns = new OLVColumn[] { this.olvcComID };
            olvOrder.DefaultRenderer = new HighlightTextRenderer(filter1);
            olvOrder.Refresh();
        }
        private void OnCapitalInitialize(Notification n)
        {
            _Order();
        }
        private void OnErrorReply(Order Ord, string Msg)
        {
            _Order();
        }

        private void OnReply(ReplyType ReplyType, Order Ord)
        {
            _Order();
        }
        #endregion

        #region Public
        public void ResetLayout()
        {
            Utility.ResetLayout("FORM", "ORDERGRIDLAYOUT");
            LoadLayout();
        }
        public void SaveLayout()
        {
            Utility.SaveLayout("FORM", "ORDERGRIDLAYOUT", olvOrder);
        }
        #endregion

        #region Private
        private void _Order()
        {
            olvOrder.InvokeIfRequired(() =>
            {
                var list = chkValid.Checked ? Core.Instance.Order.Valids() : Core.Instance.Order.Orders();
                if (list != null && list.Count() > 0)
                {
                    olvOrder.SetObjects(list, true);
                }
                else
                {
                    olvOrder.Items.Clear();
                }
            });
        }

        private void LoadLayout()
        {
            olvOrder.Items.Clear();
            Utility.LoadLayout("FORM", "ORDERGRIDLAYOUT", olvOrder);
            _Order();
        }

        private void _ChangeOrderMenu(Order o)
        {
            cmChangeOrder.Tag = o;
            cmPrice.Text = $"{o.ComID}  ${o.Price} @{o.Qty-o.SumQty}";
            if (o.BuySell== OrderProcessor.Side.B)
            {
                cmPrice.Image = Resources.buy;
                cmPrice.ForeColor = System.Drawing.Color.Crimson;
            }
            else
            {
                cmPrice.Image = Resources.sell;
                cmPrice.ForeColor = System.Drawing.Color.ForestGreen;
            }
            
            string tickname = PriceProcessor.Capital.CapitalProcessor.TickName($"{o.ExchangeID},{o.OrderHead}");
            var bids = PriceProcessor.Processor.ExtendBidByTickCount(tickname, 10, (decimal)o.Price);
            var asks = PriceProcessor.Processor.ExtendAskByTickCount(tickname, 10, (decimal)o.Price);
            for (int i = 0; i < 10; i++)
            {                
                ToolStripMenuItem miBuy = (ToolStripMenuItem)cmChangeOrder.Items[$"cmBuy{i + 1}"];
                miBuy.Text = bids[i].Price.ToString();                
                ToolStripMenuItem miSell = (ToolStripMenuItem)cmChangeOrder.Items[$"cmSell{i + 1}"];
                miSell.Text = asks[i].Price.ToString();
            }
        }
        #endregion       
    }
}
