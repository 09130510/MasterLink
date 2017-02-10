using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using log4net;
using PriceLib;
using PriceLib.Capital;
using PriceLib.iPush;
using PriceLib.PATS;
using PriceLib.Redis;
using PriceCalculator.Component;
using PriceCalculator.Utility;
using static PriceLib.Capital.OSCapitalLib;
using PriceCalculator.Properties;

namespace PriceCalculator
{
    internal class Calculator : IDisposable
    {
        #region Variable
        private ILog m_Log = LogManager.GetLogger(typeof(Calculator));
        private bool m_disposed = false;
        private Dictionary<string, ETF> m_CalcETFs;
        private ToolStripStatusLabel[] m_StatusLabels;
        private Source m_Source = Source.None;
        private bool m_ConnectToPATS = false;

        private RedisPriceLib m_Redis;
        private iPushLib m_iPush;
        private OSCapitalLib m_OSCapital;
        private PATSLib m_PATS;
        #endregion

        #region Property
        public bool SendToChannel { get; set; }
        public Source Source
        {
            get { return m_Source; }
            set
            {
                if (value == m_Source) { return; }
                m_Source = value;
                switch (m_Source)
                {
                    case Source.iPush:
                        _RedisDispose();
                        _iPushInit();
                        break;
                    case Source.Redis:
                        _iPushDispose();
                        _RedisInit();
                        break;
                    default:
                        _RedisDispose();
                        _iPushDispose();
                        break;
                }
            }
        }
        public bool ConnectToPATS
        {
            get { return m_ConnectToPATS; }
            set
            {
                if (value == m_ConnectToPATS) { return; }
                m_ConnectToPATS = value;
                if (m_ConnectToPATS)
                {
                    _PATSInit();
                }
                else
                {
                    _PATSDispose();
                }
            }
        }
        public Dictionary<string, ETF> IIVs { get { return m_CalcETFs; } }
        #endregion

        public Calculator(ToolStripStatusLabel[] statusLabels)
        {
            Util.Info(m_Log, nameof(Calculator), "Init Calcularot");
            m_CalcETFs = new Dictionary<string, ETF>();
            m_StatusLabels = statusLabels;

            Util.FXRates.OnFXRateUpdate += (sender, e) =>
            {
                ThreadPool.QueueUserWorkItem((args) =>
                {
                    FX fx = (FX)sender;
                    if (m_CalcETFs.ContainsKey(fx.ETFCode)) { _UpdateFX(m_CalcETFs[fx.ETFCode]); }
                }, sender);
            };
            ThreadPool.QueueUserWorkItem((args) =>
            {
                _OSCapitalInit();
                lock (m_CalcETFs)
                {
                    string[] CalcETFs = Util.INI["SYS"]["CALCETF"].Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var etf in CalcETFs)
                    {
                        Util.ETFSelectForm.Add(etf);
                    }
                }
            });
            ToolStripLabel lbl = m_StatusLabels.FirstOrDefault(entry => entry.Tag.ToString() == "OSCapital");
            lbl.ToolTipText = "斷線後Double Click重連";
            if (lbl != null)
            {
                lbl.DoubleClick += (sender, e) =>
                {
                    try
                    {
                        if (m_OSCapital.Status != StatusEnum.Connected)
                        {
                            m_OSCapital.Connect(Util.INI["CAPITAL"]["USR"], Util.INI["CAPITAL"]["PWD"]);
                        }
                    }
                    catch (CapitalException ex)
                    {
                        Util.Error(m_Log, nameof(Calculator) + "_OSCapitalUnsub", ex.Message);
                    }
                };
            }
        }

        #region Public
        public void Add(ETF etf)
        {
            Util.Info(m_Log, nameof(Add), $"{etf.ETFCode}");
            if (etf == null || m_CalcETFs.ContainsKey(etf.ETFCode)) { return; }
            m_CalcETFs.Add(etf.ETFCode, etf);
            Util.AddRow(etf);

            _Sub(etf);
            _ForeignSub(etf, true);
            _UpdateFX(etf);
        }
        public string Remove(string etfcode)
        {
            Util.Info(m_Log, nameof(Remove), etfcode);
            if (!m_CalcETFs.ContainsKey(etfcode)) { return string.Empty; }
            ETF etf = m_CalcETFs[etfcode];
            _UnSub(etf);
            _ForeignUnsub(etf);
            etf.Dispose();
            m_CalcETFs.Remove(etfcode);
            Util.RemoveRow(etf);
            return etf.ToString();
        }
        public void Publish()
        {
            if (!SendToChannel) { return; }
            foreach (var e in m_CalcETFs.Values)
            {
                Util.PublishToMiddle(e.ETFCode, e.IIV);
            }
        }
        public ETF GetETF(string etfcode)
        {
            if (m_CalcETFs.ContainsKey(etfcode))
            {
                return m_CalcETFs[etfcode];
            }
            return null;
        }
        public void CapitalReconnect()
        {
            if (m_OSCapital != null )
            {
                m_OSCapital.Connect(Util.INI["CAPITAL"]["USR"], Util.INI["CAPITAL"]["PWD"]);
            }            
        }
        #endregion

        #region Redis Callback
        private void _RedisInit()
        {
            lock (m_CalcETFs)
            {
                if (m_Redis == null)
                {
                    string ip = Util.INI["REDIS"]["IP"];
                    int port = int.Parse(Util.INI["REDIS"]["PORT"]);
                    Util.Info(m_Log, nameof(_RedisInit), "Init:" + ip + ":" + port);
                    m_Redis = new RedisPriceLib(ip, port);
                    m_Redis.OnMktPriceUpdate += new OnMktPriceUpdateDelegate(Redis_OnMktPriceUpdate);
                    //m_StatusLabels.FirstOrDefault(e => e.Tag.ToString() == "Redis").ForeColor = Color.Green;
                    m_StatusLabels.FirstOrDefault(e => e.Tag.ToString() == "Redis").Image = Resources.check;
                }
                if (m_CalcETFs.Count > 0) { _ResetCalcETF(false); }
            }
        }
        private void _RedisDispose()
        {
            Util.Info(m_Log, nameof(_RedisDispose), "Dispose");
            if (m_Redis == null) { return; }
            m_Redis.UnsubscribeAll();
            m_Redis.Close();
        }
        private void _RedisSub(SubscribeType stype, Dictionary<string, Composition> items)
        {

            if (items == null || items.Count <= 0 || m_Redis == null) { return; }
            string[] SUBs = items.Values.Where(e => !string.IsNullOrEmpty(e.Redis)).Select(e => e.Redis).ToArray();
            Util.Info(m_Log, nameof(_RedisSub), $"Subscribe Type:{stype} {SUBs.Length}");
            m_Redis.Subscribe(stype, SUBs);
        }
        private void _RedisUnsub(SubscribeType stype, Dictionary<string, Composition> items)
        {

            if (items == null || m_Redis == null) { return; }
            //2016/06/07 取消訂閱的話 就會不能同一台開多隻
            //foreach (var item in items.Values)
            //{
            //    m_Redis.Unsubscribe(stype, item.Redis);
            //}
            string[] UNSUBs = items.Values.Where(e => !string.IsNullOrEmpty(e.Redis)).Select(e => e.Redis).ToArray();
            Util.Info(m_Log, nameof(_RedisUnsub), $"Subscribe Type:{stype} {UNSUBs.Length}");
            m_Redis.Unsubscribe(stype, UNSUBs);
        }

        private void Redis_OnSubscribed(object sender, EventArgs e) { }
        private void Redis_OnUnsubscribed(object sender, EventArgs e) { }
        private void Redis_OnMktPriceUpdate(MktPrice mkt)
        {
            ThreadPool.QueueUserWorkItem((args) =>
            {
                MktPrice price = (MktPrice)args;
                if (Util.STK[CollectionKey.Redis, price.ID] != null)
                {
                    _UpdatePrice(Util.STK[CollectionKey.Redis, price.ID].Values, price);
                }
                if (Util.FUND[CollectionKey.Redis, price.ID] != null)
                {
                    _UpdatePrice(Util.FUND[CollectionKey.Redis, price.ID].Values, price);
                }
                else if (Util.FUT[CollectionKey.Redis, price.ID] != null)
                {
                    _UpdatePrice(Util.FUT[CollectionKey.Redis, price.ID].Values, price);
                }
            }, mkt);
        }
        #endregion
        #region iPush Callback
        private void _iPushInit()
        {
            Util.Info(m_Log, nameof(_iPushInit), "Init");
            lock (m_CalcETFs)
            {
                if (m_iPush != null) { return; }
                m_iPush = new iPushLib();
                m_iPush.OnMessage += new iPushLib.MessageEventHandler(iPush_OnMessage);
                m_iPush.OnMktPriceUpdate += new PriceLib.OnMktPriceUpdateDelegate(iPush_OnMktPriceUpdate);
                m_iPush.Connect(Util.INI["IPUSH"]["IP"], Util.INI["IPUSH"]["PORT"], Util.INI["IPUSH"]["COMP"], Util.INI["IPUSH"]["PROD"], Util.INI["IPUSH"]["USER"], Util.INI["IPUSH"]["PWD"]);

                if (m_CalcETFs.Count > 0) { _ResetCalcETF(false); }
            }
        }
        private void _iPushDispose()
        {
            Util.Info(m_Log, nameof(_iPushDispose), "Dispose");
            if (m_iPush == null) { return; }
            m_iPush.UnsubscribeAll();
            m_iPush.Disconnect();
            m_iPush.OnMktPriceUpdate -= new OnMktPriceUpdateDelegate(iPush_OnMktPriceUpdate);
            m_iPush = null;
        }
        private void _iPushSub(IEnumerable<Composition> items)
        {
            if (items == null || m_iPush == null) { return; }
            string[] SUBs = items.Where(e => !string.IsNullOrEmpty(e.iPush)).Select(e => e.iPush).ToArray();
            Util.Info(m_Log, nameof(_iPushSub), "iPush Subscribe:" + SUBs.Length); ;
            m_iPush.Subscribe(SUBs);
        }
        private void _iPushUnsub(IEnumerable<Composition> items)
        {
            if (items == null || m_iPush == null) { return; }
            string[] UNSUBs = items.Where(e => !string.IsNullOrEmpty(e.iPush)).Select(e => e.iPush).ToArray();
            Util.Info(m_Log, nameof(_iPushUnsub), "iPush Unsubscribe:" + UNSUBs.Length);
            m_iPush.Unsubscribe(UNSUBs);
        }

        private void iPush_OnMktPriceUpdate(MktPrice mkt)
        {
            Util.Debug(m_Log, nameof(iPush_OnMktPriceUpdate), mkt.ID);

            if (Util.STK[CollectionKey.iPush, mkt.ID] != null)
            {
                _UpdatePrice(Util.STK[CollectionKey.iPush, mkt.ID].Values, mkt);
            }
            if (Util.FUND[CollectionKey.iPush, mkt.ID] != null)
            {
                _UpdatePrice(Util.FUND[CollectionKey.iPush, mkt.ID].Values, mkt);
            }
            else if (Util.FUT[CollectionKey.iPush, mkt.ID] != null)
            {
                _UpdatePrice(Util.FUT[CollectionKey.iPush, mkt.ID].Values, mkt);
            }
        }
        private void iPush_OnMessage(object sender, MessageEventArgs e)
        {
            Util.Info(m_Log, nameof(iPush_OnMessage), e.message);
            string[] msg = e.message.Split('|');
            ToolStripStatusLabel lbl = m_StatusLabels.FirstOrDefault(entry => entry.Tag.ToString() == "iPush");
            if (lbl == null) { return; }
            lbl.InvokeIfRequired(() =>
            {
                switch (msg[0])
                {
                    case "Connected":
                        //lbl.ForeColor = Color.Green;
                        lbl.Image = Resources.check;
                        break;
                    case "Fail":
                        //lbl.ForeColor = Color.Crimson;
                        lbl.Image = Resources.logout;
                        break;
                    case "Lost":
                        //lbl.ForeColor = Color.Yellow;
                        lbl.Image = Resources.logout;
                        break;
                }
            });
        }
        #endregion
        #region OSCapital Callback
        private void _OSCapitalInit()
        {
            try
            {
                lock (m_CalcETFs)
                {
                    Util.Info(m_Log, nameof(_OSCapitalInit), "Init");
                    if (m_OSCapital != null) { return; }

                    m_OSCapital = new OSCapitalLib();
                    m_OSCapital.OnStatusChange += new EventHandler(OSCapital_OnStatusChange);
                    m_OSCapital.OnMktPriceUpdate += new OnMktPriceUpdateDelegate(OSCapital_OnMktPriceUpdate);
                    m_OSCapital.Connect(Util.INI["CAPITAL"]["USR"], Util.INI["CAPITAL"]["PWD"]);

                    if (m_CalcETFs.Count > 0) { _ResetCalcETF(false); }
                }
            }
            catch (CapitalException ex)
            {
                Util.Error(this.m_Log, nameof(_OSCapitalUnsub), ex.Message);
            }
        }
        private void _OSCapitalDispose()
        {
            try
            {
                Util.Info(m_Log, nameof(_OSCapitalDispose), "Dispose");
                if (m_OSCapital == null || m_OSCapital.Status != StatusEnum.Connected) { return; }
                m_OSCapital.UnsubscribeAll();
                m_OSCapital.Disconnect();
                m_OSCapital = null;
            }
            catch (CapitalException ex)
            {
                Util.Error(this.m_Log, nameof(_OSCapitalUnsub), ex.Message);
            }
        }
        private void _OSCapitalSub(Dictionary<string, Composition> items)
        {
            if (items == null || m_OSCapital == null) { return; }
            string[] SUBs = items.Values.Where(e => !String.IsNullOrEmpty(e.Capital) && e.Exch != Exch.TAIFEX).Select(e => e.Capital).ToArray();
            Util.Info(m_Log, nameof(_OSCapitalSub), "Subscribe:" + SUBs.Length);
            try
            {
                m_OSCapital.Subscribe(SUBs);
            }
            catch (CapitalException ex)
            {
                Util.Error(m_Log, nameof(_OSCapitalSub), ex.Message);
            }
        }
        private void _OSCapitalUnsub(Dictionary<string, Composition> items)
        {
            if (items == null || m_OSCapital == null) { return; }
            string[] UNSUBs = items.Values.Where(e => !string.IsNullOrEmpty(e.Capital) && e.Exch != Exch.TAIFEX).Select(e => e.Capital).ToArray();
            Util.Info(m_Log, nameof(_OSCapitalUnsub), "Unsubscribe:" + UNSUBs.Length);
            try
            {
                m_OSCapital.Unsubscribe(UNSUBs);
            }
            catch (CapitalException ex)
            {
                Util.Error(m_Log, nameof(_OSCapitalUnsub), ex.Message);
            }
        }

        private void OSCapital_OnMktPriceUpdate(MktPrice mkt)
        {
            try
            {
                Util.Debug(m_Log, nameof(OSCapital_OnMktPriceUpdate), mkt.ID);
                if (m_PATS == null || !m_PATS.isConnected)
                {
                    if (Util.FUT.Contains(CollectionKey.Capital, mkt.ID))
                    {
                        _UpdatePrice(Util.FUT[CollectionKey.Capital, mkt.ID].Values, mkt);
                    }
                }
                else if (m_PATS.isConnected)
                {
                    _UpdatePrice(Util.FUT[CollectionKey.Capital, mkt.ID].Values.Where(e => string.IsNullOrEmpty(e.PATS)).ToList(), mkt);
                }
            }
            catch (CapitalException ex)
            {
                Util.Error(m_Log, nameof(OSCapital_OnMktPriceUpdate), ex.Message);
            }
        }
        private void OSCapital_OnStatusChange(object sender, EventArgs e)
        {
            try
            {
                StatusEnum status = (StatusEnum)sender;
                Util.Info(m_Log, nameof(OSCapital_OnStatusChange), $"{status}");
                ToolStripLabel lbl = m_StatusLabels.FirstOrDefault(entry => entry.Tag.ToString() == "OSCapital");
                if (lbl == null) { return; }
                lbl.InvokeIfRequired(() =>
                {
                    switch (status)
                    {
                        case StatusEnum.InitializeSuccess:
                        case StatusEnum.Initialized:
                        case StatusEnum.ConnectFail:
                            //lbl.ForeColor = Color.Yellow;
                            lbl.Image = Resources.process;
                            break;
                        case StatusEnum.Connected:
                            //lbl.ForeColor = Color.Green;
                            lbl.Image = Resources.check;
                            break;
                        case StatusEnum.InitializeFail:
                        case StatusEnum.Disconnected:
                        default:
                            //lbl.ForeColor = Color.Crimson;
                            lbl.Image = Resources.logout;
                            break;
                    }
                });
            }
            catch (CapitalException ex)
            {
                Util.Error(m_Log, nameof(OSCapital_OnStatusChange), ex.Message);
            }
        }
        #endregion
        #region PATS Callback
        private void _PATSInit()
        {
            lock (m_CalcETFs)
            {
                if (m_PATS != null) { return; }
                Util.Info(m_Log, nameof(_PATSInit), "Init");
                m_PATS = new PATSLib(Util.INI["PATS"]["HOSTIP"], Util.INI["PATS"]["HOSTPORT"], Util.INI["PATS"]["PRICEIP"], Util.INI["PATS"]["PRICEPORT"], Util.INI["PATS"]["USER"], Util.INI["PATS"]["PWD"]);
                m_PATS.OnConnectStateChanged += new EventHandler<ConnectStateEventArgs>(PATS_OnConnectStateChanged);
                m_PATS.OnMktPriceUpdate += new OnMktPriceUpdateDelegate(PATS_OnMktPriceUpdate);
                m_PATS.Connect();

                if (m_CalcETFs.Count > 0) { _ResetCalcETF(true); }
            }
        }
        private void _PATSDispose()
        {
            Util.Info(m_Log, nameof(_PATSDispose), "Dispose");
            if (m_PATS == null) { return; }
            m_PATS.UnsubscribeAll();
            m_PATS.Disconnect();
            m_PATS = null;
        }
        private void _PATSSub(Dictionary<string, Composition> items)
        {
            if (items == null || m_PATS == null) { return; }
            string[] SUBs = items.Values.Where(e => !string.IsNullOrEmpty(e.PATS)).Select(e => e.PATS).ToArray();
            Util.Info(m_Log, nameof(_PATSSub), "Subscribe:" + SUBs.Length);

            foreach (var sub in SUBs)
            {
                m_PATS.Subscribe(sub);
            }
        }
        private void _PATSUnsub(Dictionary<string, Composition> items)
        {
            if (items == null || m_PATS == null) { return; }
            string[] UNSUBs = items.Values.Where(e => !string.IsNullOrEmpty(e.PATS)).Select(e => e.PATS).ToArray();
            Util.Info(m_Log, nameof(_PATSUnsub), "Unsubscribe:" + UNSUBs.Length);

            foreach (var unsub in UNSUBs)
            {
                m_PATS.Unsubscribe(unsub);
            }
        }

        private void PATS_OnMktPriceUpdate(MktPrice mkt)
        {
            Util.Debug(m_Log, nameof(PATS_OnMktPriceUpdate), mkt.ID);
            if (Util.FUT.Contains(CollectionKey.PATS, mkt.ID))
            {
                _UpdatePrice(Util.FUT[CollectionKey.PATS, mkt.ID].Values, mkt);
            }
        }
        private void PATS_OnConnectStateChanged(object sender, ConnectStateEventArgs e)
        {
            Util.Info(m_Log, nameof(PATS_OnConnectStateChanged), $"Host:{e.HostState} Logon:{e.LogonState} Price:{e.PriceState} isConnect:{e.isConnected}");
            ToolStripLabel lbl = m_StatusLabels.FirstOrDefault(entry => entry.Tag.ToString() == "PATS");
            if (lbl == null) { return; }
            lbl.InvokeIfRequired(() =>
            {
                if (e.isConnected)
                {
                    //lbl.ForeColor = Color.Green;
                    //lbl.Text = "█";
                    lbl.Image = Resources.check;
                    return;
                }
                else if (e.HostState == SocketLinkState.ptLinkConnected && e.PriceState == SocketLinkState.ptLinkConnected)
                {
                    //lbl.ForeColor = Color.Yellow;
                    lbl.Image = Resources.process;
                }
                else
                {
                    lbl.Image = Resources.logout;
                    //lbl.ForeColor = Color.Crimson;
                }
                lbl.Text = $"{e.LogonState} {e.HostState} {e.PriceState}";
            });
        }
        #endregion

        private void _Sub(ETF etf)
        {
            Util.Info(m_Log, nameof(_Sub), etf.ETFCode);
            switch (Source)
            {
                case Source.iPush:
                    _iPushSub(Util.Union(CollectionKey.ETFCode, etf.ETFCode));
                    break;
                case Source.Redis:
                    //不能Union起來跑, 會佔太多連線後當掉
                    _RedisSub(SubscribeType.Stock, Util.STK[CollectionKey.ETFCode, etf.ETFCode]);
                    _RedisSub(SubscribeType.Future, Util.FUT[CollectionKey.ETFCode, etf.ETFCode]);
                    _RedisSub(SubscribeType.Stock, Util.FUND[CollectionKey.ETFCode, etf.ETFCode]);
                    break;
            }
        }
        private void _ForeignSub(ETF etf, bool isReplace)
        {
            Util.Info(m_Log, nameof(_ForeignSub), $"{etf.ETFCode}");
            _OSCapitalSub(Util.FUT[CollectionKey.ETFCode, etf.ETFCode]);
            if (m_PATS != null) { _PATSSub(Util.FUT[CollectionKey.ETFCode, etf.ETFCode]); }
        }
        private void _UnSub(ETF etf)
        {
            Util.Info(m_Log, nameof(_UnSub), etf.ETFCode);
            switch (Source)
            {
                case Source.iPush:
                    _iPushUnsub(Util.Union(CollectionKey.ETFCode, etf.ETFCode));
                    break;
                case Source.Redis:
                    _RedisUnsub(SubscribeType.Stock, Util.STK[CollectionKey.ETFCode, etf.ETFCode]);
                    _RedisUnsub(SubscribeType.Future, Util.FUT[CollectionKey.ETFCode, etf.ETFCode]);
                    _RedisUnsub(SubscribeType.Stock, Util.FUND[CollectionKey.ETFCode, etf.ETFCode]);
                    break;
            }
            Util.RemoveRow(etf);
        }
        private void _ForeignUnsub(ETF etf)
        {
            Util.Info(m_Log, nameof(_ForeignUnsub), etf.ETFCode);
            _OSCapitalUnsub(Util.FUT[CollectionKey.ETFCode, etf.ETFCode]);
            if (m_PATS != null) { _PATSUnsub(Util.FUT[CollectionKey.ETFCode, etf.ETFCode]); }
        }
        private void _ResetCalcETF(bool isReplace)
        {
            Util.Info(m_Log, nameof(_ResetCalcETF), "Replace:" + isReplace);
            List<string> subedETF = new List<string>();
            foreach (var etf in m_CalcETFs.Values)
            {
                subedETF.Add(etf.ETFCode);
                _ForeignUnsub(etf);
                _UnSub(etf);
                etf.Dispose();
            }
            m_CalcETFs.Clear();
            foreach (var item in subedETF)
            {
                ETF e = new ETF(item, DateTime.Now);
                Add(e);
            }
        }
        private void _UpdatePrice(IEnumerable<Composition> items, MktPrice mkt)
        {
            if (items == null || items.Count() <= 0) { return; }
            foreach (var item in items)
            {
                if (!m_CalcETFs.ContainsKey(item.ETFCode)) { continue; }
                Util.Debug(m_Log, nameof(_UpdatePrice), $"ETF:{item.ETFCode} ID:{mkt.ID}");
                ETF e = m_CalcETFs[item.ETFCode];
                if (mkt.YP != MktPrice.NULLVALUE) { item.YP = mkt.YP; }
                if (mkt.MP != MktPrice.NULLVALUE) { item.MP = mkt.MP; }
                if (e.Calc(item.PID, _CalcVariation(item)))
                {
                    if (SendToChannel)
                    {
                        Util.PublishToMiddle(e.ETFCode, e.IIV);
                        e.Sended = true;
                    }
                    e.cIIV.SetValue(e.IIV);
                }
            }
        }
        private void _UpdateFX(ETF etf)
        {
            //2016/12/27 能不能改成一次算完??? 再想想
            if (etf == null) { return; }
            IEnumerable<Composition> compositions = Util.Union(CollectionKey.ETFCode, etf.ETFCode);
            Util.Debug(m_Log, nameof(_UpdateFX), $"{etf.ETFCode} Composition Update By FX Changed");
            if (compositions != null)
            {
                foreach (var item in compositions)
                {
                    if (etf.Calc(item.PID, _CalcVariation(item)))
                    {
                        if (SendToChannel)
                        {
                            Util.PublishToMiddle(etf.ETFCode, etf.IIV);
                            etf.Sended = true;
                        }
                        etf.cIIV.SetValue(etf.IIV);
                    }
                }
            }

            IEnumerable<Asset> assets = Util.Union(AssetKey.ETFCode, etf.ETFCode);
            Util.Debug(m_Log, nameof(_UpdateFX), $"{etf.ETFCode} Asset Update By FX Changed");
            if (assets != null)
            {
                foreach (Asset asset in assets)
                {
                    if (etf.Calc(asset.Key, _CalcVariation(asset)))
                    {
                        if (SendToChannel)
                        {
                            Util.PublishToMiddle(etf.ETFCode, etf.IIV);
                            etf.Sended = true;
                        }
                        etf.cIIV.SetValue(etf.IIV);
                    }
                }
            }
        }
        private decimal _CalcVariation(Asset item)
        {
            if (item.Amount <= 0M) { return 0M; }
            Util.Debug(m_Log, nameof(_CalcVariation), $"{item.ETFCode}-{item.BaseCrncy} Calc");
            return ((item.Amount * (item.FXRate - item.YstRate)) * item.Direction);

        }
        private decimal _CalcVariation(Composition item)
        {
            if (item.Shares <= 0 || item.MP == MktPrice.NULLVALUE || item.YP == MktPrice.NULLVALUE) { return 0M; }
            Util.Debug(m_Log, nameof(_CalcVariation), $"{item.ETFCode}-{item.PID} Calc");
            return (item.Units * (item.MP - item.YP) * item.CValue * item.FXRate);
        }

        #region IDisposable 成員
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this); //要求系統不要呼叫指定物件的完成項。
        }
        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="IsDisposing"></param>
        protected void Dispose(bool IsDisposing)
        {
            if (m_disposed) return;

            if (IsDisposing) { DoDispose(); }
            m_disposed = true;
        }
        /// <summary>
        /// Do something when disposing
        /// </summary>
        protected virtual void DoDispose()
        {
            if (m_Redis != null) { _RedisDispose(); }
            if (m_iPush != null) { _iPushDispose(); }
            try
            {
                if (m_OSCapital != null) { _OSCapitalDispose(); }
            }
            catch (CapitalException ex)
            {
                Util.Error(m_Log, nameof(DoDispose), ex.Message);
            }
            if (m_PATS != null) { _PATSDispose(); }
        }
        #endregion
    }
}