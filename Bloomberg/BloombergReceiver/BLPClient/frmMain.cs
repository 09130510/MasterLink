using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
//using BL;
//using Bloomberglp.Blpapi;
using BLParser;
using BLPClient.Class;
using WeifenLuo.WinFormsUI.Docking;
//using EventHandler = Bloomberglp.Blpapi.EventHandler;
using Util.Extension.Class;


namespace BLPClient
{
    public partial class frmMain : Form
    {

        public frmMain()
        {
            InitializeComponent();

            this.Text = Ext.VersionInfo(this);            

            #region Form Layout
            Utility.SettingForm.Show(dockPanel1, WeifenLuo.WinFormsUI.Docking.DockState.DockRight);
            Utility.SubscribeForm.Show(dockPanel1, WeifenLuo.WinFormsUI.Docking.DockState.DockRight);
            Utility.RequestForm.Show(Utility.SubscribeForm.Pane, Utility.SubscribeForm);
            Utility.SDataForm.Show(dockPanel1);
            Utility.RDataForm.Show(dockPanel1);
            Utility.MsgForm.Show(Utility.RDataForm.Pane, DockAlignment.Bottom, 0.25);
            #endregion

            //Utility.PUB = new Pub(ConnectType.Bind, Utility.Load<int>("ZMQ", "PUBSUBPORT"));
            //Utility.REP = new Rep(ConnectType.Bind, Utility.Load<int>("ZMQ", "REPREQPORT"));
            //Utility.REP.OnREQReceived += new Rep.OnREQReceivedDelegate(OnREQReceived);
            //Utility.REP.Start();
        }

        //private void OnREQReceived(string Head, string Msg)
        //{
        //    Console.WriteLine("REQUEST" + Head + "  " + Msg);
        //    Utility.REP.Response("");
        //    if (String.IsNullOrEmpty(Head) || String.IsNullOrEmpty(Msg)) { return; }
        //    switch (Head)
        //    {
        //        case "QUERY":
        //            string[] item = Msg.Split(';');
        //            Utility.RequestForm.SendRequest( item[0], item[1]);
        //            break;
        //        default:
        //            Utility.SubscribeForm.Subscribe(Head, Msg, true);
        //            break;
        //    }
            
        //}
        
        //private void tsConnect_Click(object sender, EventArgs e)
        //{
        //    if (Utility.Subscriber != null) { return; }
        //    Utility.Subscriber = BLSubscriber.Create();
        //    if (Utility.Subscriber != null)
        //    {
        //        Utility.Subscriber.OnOtherReceived += new EventHandler(OnOtherReceived);
        //        Utility.Subscriber.OnStatusReceived += new EventHandler(OnStatusReceived);
        //        Utility.Subscriber.OnRequestReply += new EventHandler(OnRequestReply);
        //        Utility.Subscriber.OnSubscriptionReply += new EventHandler(OnSubscriptionReply);

        //        if (Utility.Display != null) { Utility.Display.Dispose(); }
        //        Utility.Display = new Display();
        //        Utility.SDataForm.SetCell();
                
        //    }
        //    tsStatus.ForeColor = Utility.Subscriber != null ? Color.Green : Color.Crimson;

        //}
        private void dockPanel1_ActivePaneChanged(object sender, EventArgs e)
        {
            DockPane Pane = ((DockPanel)sender).ActivePane;
            IDockContent Content = ((DockPanel)sender).ActiveContent;
            DockContent Active = null;

            if (Pane == null || Content == null) { return; }

            if (Pane.DockState == DockState.Document)
            {
                Active = Content is frmRequestData ? (DockContent)Utility.RequestForm : (Content is frmSubscriptionData ? Utility.SubscribeForm : null);
            }
            else
            {
                Active = Content is frmRequest ? (DockContent)Utility.RDataForm : ((Content is frmSubscribe) ? Utility.SDataForm : null);
            }
            if (Active != null && Active.Pane != null) { Active.Pane.ActiveContent = Active; }
        }


        #region Delegate
        //private void OnSubscriptionReply(Event eventObject, Session session)
        //{
        //    Console.WriteLine("Main.OnSubscriptionReply");
        //    Subscribe s = Subscribe.Create(eventObject);
        //    Console.WriteLine("Main.OnSubscriptionReply Subscribe OK");
        //    if (s != null)
        //    {
        //        Utility.PUB.Send("Subscription", s);
        //        Console.WriteLine("Main.OnSubscriptionReply Send OK");
        //    }
        //}

        //private void OnRequestReply(Event eventObject, Session session)
        //{
        //    Response r = new Response(eventObject);
        //    Console.WriteLine("HasSecurities" + r.Collection.HasSecurities);
        //    if (r != null)
        //    {
        //        Utility.PUB.Send("Request", r);
        //    }
        //}

        //private void OnStatusReceived(Event eventObject, Session session)
        //{
        //    //Utility.PUB.Send("Status", eventObject);
        //}

        //private void OnOtherReceived(Event eventObject, Session session)
        //{
        //    //Utility.PUB.Send("Other", eventObject);
        //}
        #endregion

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Utility.PUB.Dispose();
            //Utility.REP.Dispose();
        }
    }
}
