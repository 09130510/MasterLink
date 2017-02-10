using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AxIPUSHXLib;
using AxXQRECOVERLib;
using log4net.Config;
using log4net;

namespace PriceLib.iPush
{
    public partial class iPushLib : UserControl
    {
        public enum Action
        {
            Subject,
            Unsubject
        }

        #region Event
        public delegate void MessageEventHandler(object sender, MessageEventArgs e);
        public event MessageEventHandler OnMessage;
        public event OnMktPriceUpdateDelegate OnMktPriceUpdate;
        #endregion

        #region Variable
        private AxiPushX m_iPushServer;
        private Dictionary<string, MktPrice> m_MktPrices;
        private Dictionary<string, int> m_Subjects;
        private ILog m_Log;
        #endregion       

        public iPushLib()
        {
            InitializeComponent();
            
            m_MktPrices = new Dictionary<string, MktPrice>();
            m_Subjects = new Dictionary<string, int>();
            XmlConfigurator.Configure(new System.IO.FileInfo("./LogConfig.xml"));
            m_Log = LogManager.GetLogger(typeof(iPushLib));
        }

        #region Delegate
        private void iPushServer_ConnectReady(object sender, _DiPushXEvents_ConnectReadyEvent e)
        {
            OnMessage?.Invoke(this, new MessageEventArgs(DateTime.Now, "Connected|"));
        }
        private void iPushServer_ConnectFail(object sender, _DiPushXEvents_ConnectFailEvent e)
        {
            OnMessage?.Invoke(this, new MessageEventArgs(DateTime.Now, $"Fail| {e.nStatus}"));
        }
        private void iPushServer_ConnectLost(object sender, EventArgs e)
        {
            OnMessage?.Invoke(this, new MessageEventArgs(DateTime.Now, "Lost|"));
        }
        private void iPushServer_CommandMsg(object sender, _DiPushXEvents_CommandMsgEvent e)
        {
            OnMessage?.Invoke(this, new MessageEventArgs(DateTime.Now, $"iPush Command: {e.strMsg}[RCode: {e.nCode}]"));
        }
        private void iPushServer_SetData3(object sender, _DiPushXEvents_SetData3Event e)
        {
            int ReturnData = axXQuote.SetData3(e.vChannel, e.vData);
            //股&期
            //if (ReturnData != 133 && ReturnData != 147) { return; }
            if (ReturnData > 0)
            {
                try
                {
                    int multiple = ReturnData == 133 ? 10 : 1;
                    int refp = axXQuote.GetIntValue(9);
                    int mktp = axXQuote.GetIntValue(16);
                    int ap = axXQuote.GetIntValue(40);
                    int aq = axXQuote.GetIntValue(45);
                    int bp = axXQuote.GetIntValue(30);
                    int bq = axXQuote.GetIntValue(35);
                    MktPrice mkt = new MktPrice(axXQuote.GetStrValue(0),
                        (refp < 0 ? -1 : refp * multiple),
                        (mktp < 0 ? -1 : mktp * multiple),
                        (ap < 0 ? -1 : ap * multiple),
                        (bp < 0 ? -1 : bp * multiple),
                        aq, bq,
                        1000);
                    if (m_MktPrices.ContainsKey(mkt.ID))
                    {
                        m_MktPrices[mkt.ID].Join(mkt);
                    }
                    else
                    {
                        m_MktPrices.Add(mkt.ID, mkt);
                    }
                    RaiseMktPrice(mkt);
                }
                catch (Exception) { }
            }
        }
        #endregion

        #region Public
        public bool Connect(string ip, string port, string comp, string prod, string user, string pwd)
        {
            try
            {
                string key = $"{ip}{port}{comp}{prod}{user}{pwd}";

                #region Init iPush
                ComponentResourceManager resources = new ComponentResourceManager(typeof(iPushLib));
                m_iPushServer = new AxiPushX();
                SuspendLayout();
                ((ISupportInitialize)m_iPushServer).BeginInit();
                m_iPushServer.Enabled = true;
                m_iPushServer.Location = new System.Drawing.Point(5, 0);
                m_iPushServer.Name = key;
                m_iPushServer.OcxState = ((AxHost.State)(resources.GetObject("m_iPushServer.OcxState")));
                m_iPushServer.Size = new System.Drawing.Size(40, 40);
                m_iPushServer.TabIndex = 0;
                m_iPushServer.Hide();
                Controls.Add(m_iPushServer);
                ((ISupportInitialize)(m_iPushServer)).EndInit();
                ResumeLayout(false);
                PerformLayout();
                #endregion

                m_iPushServer.ConnectReady += new _DiPushXEvents_ConnectReadyEventHandler(iPushServer_ConnectReady);
                m_iPushServer.ConnectFail += new _DiPushXEvents_ConnectFailEventHandler(iPushServer_ConnectFail);
                m_iPushServer.ConnectLost += new EventHandler(iPushServer_ConnectLost);
                m_iPushServer.CommandMsg += new _DiPushXEvents_CommandMsgEventHandler(iPushServer_CommandMsg);
                m_iPushServer.SetData3 += new _DiPushXEvents_SetData3EventHandler(iPushServer_SetData3);
                m_iPushServer.usingSetData = 3;

                m_iPushServer.ipuship = ip;
                m_iPushServer.ipushport = Convert.ToInt32(port);
                m_iPushServer.company = comp;
                m_iPushServer.product = prod;
                m_iPushServer.username = user;
                m_iPushServer.password = pwd;
                m_iPushServer.ipushConnect();
            }
            catch (Exception) { return false; }
            return true;
        }
        public bool Disconnect()
        {
            try
            {
                m_iPushServer.ipushDisconnect();
                m_Subjects.Clear();
                m_MktPrices.Clear();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
            return true;
        }
        public void Subscribe(params string[] IDs)
        {
            foreach (var id in IDs)
            {
                Command(Action.Subject, id);
                MktPrice mkt = m_MktPrices.ContainsKey(id) ? m_MktPrices[id] : null;
                RaiseMktPrice(mkt);
                if (!m_Subjects.ContainsKey(id)) { m_Subjects.Add(id, 0); }
                m_Subjects[id] += 1;
            }
        }
        public void Unsubscribe(params string[] args)
        {
            foreach (string item in args)
            {
                if (!m_Subjects.ContainsKey(item)) { continue; }
                m_Subjects[item] -= 1;
                if (m_Subjects[item] <= 0)
                {
                    Command(Action.Unsubject, item);
                    m_Subjects.Remove(item);
                }
            }
        }
        public void UnsubscribeAll()
        {
            foreach (var item in m_Subjects.Keys)
            {
                Command(Action.Unsubject, item);
            }
            m_Subjects.Clear();
        }
        #endregion

        #region Private
        private bool Command(Action action, string args)
        {
            if (m_iPushServer == null) { return false; }

            switch (action)
            {
                case Action.Subject:
                    m_iPushServer.ipushSub("x" + args);
                    break;
                case Action.Unsubject:
                    m_iPushServer.ipushUnsub("x" + args);
                    break;
            }
            return true;
        }
        private void RaiseMktPrice(MktPrice mkt)
        {
            if (OnMktPriceUpdate != null && mkt != null && !(mkt.YP == MktPrice.NULLVALUE && mkt.MP == MktPrice.NULLVALUE))
            {
                OnMktPriceUpdate(mkt);
            }
        }
        #endregion
    }

    public class MessageEventArgs : EventArgs
    {
        public DateTime trigDatetime;
        public string message;
        public MessageEventArgs(DateTime trigDatetime, string message)
        {
            this.trigDatetime = trigDatetime;
            this.message = message;
        }
    }

    public class RecoverPriceEventArgs : EventArgs
    {
        public string ID { get; private set; }
        public int YP { get; private set; }
        public int MP { get; private set; }
        public RecoverPriceEventArgs(string id, int ystPrice, int marketPrice)
        {
            this.ID = id;
            this.YP = ystPrice;
            this.MP = marketPrice;
        }
    }
}