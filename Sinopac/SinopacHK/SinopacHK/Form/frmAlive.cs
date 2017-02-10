using System;
using OrderProcessor.SinoPac;
using SinopacHK.Class;
using Util.Extension.Class;
using WeifenLuo.WinFormsUI.Docking;

namespace SinopacHK
{
    public partial class frmAlive : DockContent
    {
        public frmAlive()
        {
            InitializeComponent();

            NotificationCenter.Instance.AddObserver(OnProductChanged, "PRODUCTCHANGED");
            NotificationCenter.Instance.AddObserver(OnOrderReply, "ORDERREPLY");
            NotificationCenter.Instance.AddObserver(OnMatchReply, "MATCHREPLY");
            Utility.LoadLayout("ALIVE", objectListView1);
        }

        private void objectListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (objectListView1.SelectedObject != null)
            {
                NotificationCenter.Instance.Post("SELECTEDORDER", new Notification(null, ((FIXOrder)objectListView1.SelectedObject).LastVaildClOrdID));
            }
        }
        private void objectListView1_DoubleClick(object sender, EventArgs e)
        {
            if (objectListView1.SelectedObject != null)
            {
                FIXOrder order = (FIXOrder)objectListView1.SelectedObject;
                Utility.Unit.Deal.Cancel(order.LastVaildClOrdID);
            }
        }

        #region Delegate
        private void OnProductChanged(Notification n)
        {
            objectListView1.Items.Clear();
            objectListView1.SetObjects(Utility.Unit.Deal.AliveOrders, true);
        }

        private void OnOrderReply(Notification n)
        {
            FIXOrder order = (FIXOrder)n.Message;
            if (order.isAlive)
            {
                objectListView1.AddObject(n.Message);
            }
            else
            {
                objectListView1.RemoveObject(n.Message);
            }
        }
        private void OnMatchReply(Notification n)
        {
            FIXOrder order = (FIXOrder)n.Message;
            if (!order.isAlive)
            {
                objectListView1.RemoveObject(order);
            }
            else
            {
                objectListView1.RefreshObject(order);
            }
        }
        #endregion

        public void SaveLayout()
        {
            Utility.SaveLayout("ALIVE", objectListView1);
        }
    }
}