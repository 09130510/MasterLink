using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using BL;
using Bloomberglp.Blpapi;
using BLParser;
using BLPServer.Class;
using WeifenLuo.WinFormsUI.Docking;
using System.Linq;
using EventHandler = Bloomberglp.Blpapi.EventHandler;
using System.Collections.Generic;
using System.Diagnostics;

namespace BLPServer
{
    public partial class frmMain : Form
    {
        private string m_Channel;
        private Dictionary<string, Publish> m_Prices = new Dictionary<string, Publish>();

        public frmMain()
        {
            InitializeComponent();

            #region Version Info
            object[] attribute = this.GetType().Assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
            AssemblyDescriptionAttribute desc = (AssemblyDescriptionAttribute)(attribute[0]);
            attribute = this.GetType().Assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
            AssemblyTitleAttribute title = (AssemblyTitleAttribute)(attribute[0]);
            this.Text = $"[{ title.Title } - {Process.GetCurrentProcess().Id}]  {desc.Description}  V{Application.ProductVersion}";
            #endregion

            #region Form Layout            
            VS2003Theme t = new VS2003Theme();
            dockPanel1.Theme = t;
            //Utility.SubscribeForm.Show(dockPanel1, WeifenLuo.WinFormsUI.Docking.DockState.DockRight);
            //Utility.RequestForm.Show(Utility.SubscribeForm.Pane, Utility.SubscribeForm);
            //Utility.SDataForm.Show(dockPanel1);
            //Utility.RDataForm.Show(dockPanel1);
            Utility.SubDockForm.Show(dockPanel1);
            //Utility.ReqDockForm.Show(dockPanel1);
            Utility.SettingForm.Show(dockPanel1, DockState.DockRight);
            Utility.MsgForm.Show(dockPanel1, DockState.DockBottomAutoHide);
            #endregion

            Utility.PUB = new Pub(ConnectType.Bind, Utility.Load<int>("ZMQ", "PUBPORT"));
            Utility.REP = new Rep(ConnectType.Bind, Utility.Load<int>("ZMQ", "REPPORT"));
            Utility.REP.OnREQReceived += new Rep.OnREQReceivedDelegate(OnREQReceived);
            Utility.REP.Start();

            //Utility.LoadConfig(tsPublish);
            tsPublish.Checked = Utility.Load<bool>("REDIS", "PUBLISH");
            m_Channel = Utility.Load<string>("REDIS", "CHANNEL");
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Utility.Subscriber != null)
            {
                Utility.Subscriber.OnOtherReceived -= new EventHandler(OnOtherReceived);
                Utility.Subscriber.OnStatusReceived -= new EventHandler(OnStatusReceived);
                Utility.Subscriber.OnRequestReply -= new EventHandler(OnRequestReply);
                Utility.Subscriber.OnSubscriptionReply -= new EventHandler(OnSubscriptionReply);
                Utility.Subscriber.Close();
            }

            Utility.PUB.Dispose();
            Utility.REP.Dispose();
        }
        private void tsConnect_Click(object sender, EventArgs e)
        {
            if (Utility.Subscriber != null) { return; }
            tsStatus.ForeColor = Color.Crimson;
            Utility.Subscriber = BLSubscriber.Create();
            if (Utility.Subscriber == null)
            {
                MessageBox.Show("連線失敗！", "連線", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Utility.Subscriber.OnOtherReceived += new EventHandler(OnOtherReceived);
            Utility.Subscriber.OnStatusReceived += new EventHandler(OnStatusReceived);
            Utility.Subscriber.OnRequestReply += new EventHandler(OnRequestReply);
            Utility.Subscriber.OnSubscriptionReply += new EventHandler(OnSubscriptionReply);

            if (Utility.Display != null) { Utility.Display.Dispose(); }
            Utility.Display = new Display();
            Utility.SubResultForm.SetCell();
            //Utility.SDataForm.SetCell();
            tsStatus.ForeColor = Color.Green;
            Utility.SubSettingForm.DoSubscribe();
            //Utility.SubscribeForm.DoSubscribe();
        }
        private void dockPanel1_ActivePaneChanged(object sender, EventArgs e)
        {
            //DockPane Pane = ((DockPanel)sender).ActivePane;
            //IDockContent Content = ((DockPanel)sender).ActiveContent;
            //DockContent Active = null;

            //if (Pane == null || Content == null) { return; }

            //if (Pane.DockState == DockState.Document)
            //{
            //    Active = Content is frmRequestData ? (DockContent)Utility.RequestForm : (Content is frmSubscriptionData ? Utility.SubscribeForm : null);
            //}
            //else
            //{
            //    Active = Content is frmRequest ? (DockContent)Utility.RDataForm : ((Content is frmSubscribe) ? Utility.SDataForm : null);
            //}
            //if (Active != null && Active.Pane != null) { Active.Pane.ActiveContent = Active; }
        }
        private void tsSubscribe_Click(object sender, EventArgs e)
        {
            if (!Utility.SubDockForm.IsHidden)
            {
                Utility.SubDockForm.Activate();
            }
            else
            {
                Utility.SubDockForm.Show(dockPanel1);
            }
        }
        private void tsRequest_Click(object sender, EventArgs e)
        {
            if (!Utility.ReqDockForm.IsHidden)
            {
                Utility.ReqDockForm.Activate();
            }
            else
            {
                Utility.ReqDockForm.Show(dockPanel1);
            }
        }
        private void tsSetting_Click(object sender, EventArgs e)
        {
            Utility.SettingForm.Show(dockPanel1, DockState.DockRight);
        }
        private void tsMsg_Click(object sender, EventArgs e)
        {
            Utility.MsgForm.Show(dockPanel1, DockState.DockBottom);
        }
        private void tsPublish_CheckedChanged(object sender, EventArgs e)
        {
            Utility.Save("REDIS", "PUBLISH", tsPublish.Checked);
        }


        #region Delegate
        private void OnREQReceived(string Head, string Msg)
        {
            //Console.WriteLine("REQUEST" + Head + "  " + Msg);
            Utility.REP.Response("");
            if (String.IsNullOrEmpty(Head) || String.IsNullOrEmpty(Msg)) { return; }
            switch (Head)
            {
                case "QUERY":
                    string[] item = Msg.Split(';');
                    Utility.ReqSettingForm.SendRequest(item[0], item[1]);
                    //Utility.RequestForm.SendRequest(item[0], item[1]);
                    break;
                default:
                    Utility.SubSettingForm.Subscribe(Head, Msg, true);
                    //Utility.SubscribeForm.Subscribe(Head, Msg, true);
                    break;
            }

        }

        private void OnSubscriptionReply(Event eventObject, Session session)
        {
            //Console.WriteLine("1");
            Subscribe s = Subscribe.Create(eventObject);
            if (s != null)
            {
                //Console.WriteLine("2");
                if (tsPublish.Checked)
                {
                    //Console.WriteLine("3");
                    foreach (var security in s.Collection.Securities.Values)
                    {
                        if (!m_Prices.ContainsKey(security.Name))
                        {
                            m_Prices.Add(security.Name, new Publish(security));
                        }
                        m_Prices[security.Name].Update(security);
                        if (m_Prices[security.Name].MidP == 0) { continue; }
                        foreach (var redis in Utility.REDIS)
                        {
                            redis.Publish(Utility.Load<string>("REDIS", "CHANNEL") + security.Name, m_Prices[security.Name].MidP.ToString());
                        }
                        ////Console.WriteLine($"{security.Values["LAST_PRICE"]}; {security.Values["BID"]}; {security.Values["ASK"]}");                        
                        ////Console.WriteLine(value);

                        //double last = !security.Values.ContainsKey("LAST_PRICE") ? 0D : Convert.ToDouble(security.Values["LAST_PRICE"]);
                        //double bid = !security.Values.ContainsKey("BID") ? 0D : Convert.ToDouble(security.Values["BID"]);
                        //double ask = !security.Values.ContainsKey("ASK") ? 0D : Convert.ToDouble(security.Values["ASK"]);
                        //string value;

                        //if (bid == 0 || ask == 0)
                        //{
                        //    if (last == 0) { continue; }
                        //    value = last.ToString();
                        //}
                        //else
                        //{
                        //    value = ((ask + bid) / 2).ToString();
                        //}
                        //foreach (var redis in Utility.REDIS)
                        //{
                        //    redis.Publish(Utility.Load<string>("REDIS", "CHANNEL") + security.Name, value);
                        //}

                        ////var value = security.Values["LAST_PRICE"];
                        ////if (value != null)
                        ////{
                        ////    foreach (var redis in Utility.REDIS)
                        ////    {
                        ////        //Console.WriteLine(m_Channel + security.Name+" "+ value.ToString());

                        ////        redis.Publish(m_Channel + security.Name, value.ToString());
                        ////        //Console.WriteLine("4");
                        ////    }
                        ////    //Utility.REDIS.Publish(Utility.Load<string>("REDIS", "CHANNEL") + security.Name, value.ToString());
                        ////}
                    }
                }
                Utility.PUB.Send("Subscription", s);
            }
        }

        private void OnRequestReply(Event eventObject, Session session)
        {
            Response r = new Response(eventObject);
            //Console.WriteLine("HasSecurities" + r.Collection.HasSecurities);
            if (r != null)
            {
                Utility.PUB.Send("Request", r);
            }
        }

        private void OnStatusReceived(Event eventObject, Session session)
        {
            //foreach (var msg in eventObject)
            //{
            //    Utility.Log(this, "Status", (msg.NumCorrelationIDs > 0 ? msg.CorrelationID.ToString() : "") + "  " + msg);
            //    //Console.WriteLine(msg.CorrelationID + "  " + msg);
            //}
            ////Utility.PUB.Send("Status", eventObject);
        }

        private void OnOtherReceived(Event eventObject, Session session)
        {
            //foreach (var item in eventObject)
            //{
            //    Utility.Log(this, "Others", (item.CorrelationID != null ? item.CorrelationID.ToString() : "") + "  " + item);
            //}
            ////Utility.PUB.Send("Other", eventObject);
        }

        #endregion        
    }
}
