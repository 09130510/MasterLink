using System;
using OrderProcessor.SinoPac;
using SinopacHK.Class;
using Util.Extension.Class;
using WeifenLuo.WinFormsUI.Docking;

namespace SinopacHK
{
    public partial class frmMatch : DockContent
    {
        public frmMatch()
        {
            InitializeComponent();
                        
            //Utility.Processor.MatchReplyEvent += MatchReplyEvent;
            NotificationCenter.Instance.AddObserver(OnProductChanged, "PRODUCTCHANGED");
            NotificationCenter.Instance.AddObserver(OnMatchReply, "MATCHREPLY");
            Utility.LoadLayout("MATCH", objectListView1);
        }
        private void objectListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (objectListView1.SelectedObject != null)
            {
                NotificationCenter.Instance.Post("SELECTEDORDER", new Notification(null, ((FIXOrder)objectListView1.SelectedObject).LastVaildClOrdID));
            }
        }

        #region Delegate
        private void OnProductChanged(Notification n)
        {
            objectListView1.Items.Clear();
            objectListView1.SetObjects(Utility.Unit.Deal.Matchs, true);
        }
        private void OnMatchReply(Notification n)
        {
            objectListView1.AddObject(n.Message);
        }
        //private void MatchReplyEvent(SinoPacProcessor sender, SinoPacRPT reply)
        //{
        //    objectListView1.SetObjects(Utility.Unit.Deal.Matchs, true);
        //}
        #endregion

        public void SaveLayout()
        {
            Utility.SaveLayout("MATCH", objectListView1);
        }

        private void tsClear_Click(object sender, EventArgs e)
        {
            objectListView1.Items.Clear();
        }
    }
}
