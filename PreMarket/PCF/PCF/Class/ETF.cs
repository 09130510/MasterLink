using HtmlAgilityPack;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;


namespace PCF.Class
{    
    public abstract class ETF
    {
        public static string DATE = "yyyy/MM/dd";        
        public const string CONTENT = "CONTENT";
        public const string TR = "tr";
        public const string TD = "td";
        public const string TH = "th";

        #region Variable
        protected ILog m_Log;
        protected static DateTime m_Today = DateTime.Now;
        protected DateTime m_DataDate;
        protected string m_Address;
        protected Uri m_Url = null;
        protected WebClient m_Web = new WebClient();        
        private string m_Name;
        private Broker m_Broker;        
        /// <summary>
        /// 用來算期貨價格的匯率
        /// (開盤時間不同的期貨商品, 用到的匯率不同)
        /// </summary>
        protected Dictionary<string, PCF_FxRate> m_FutFXs = new Dictionary<string, PCF_FxRate>();        
        #endregion

        #region Property        
        public string ETFCode { get; set; }
        public string Name
        {
            get { return m_Name; }
            set
            {
                m_Name = value.Trim();
                Node.Text = $"{ETFCode} [{Broker}-{m_Name}]";
            }
        }
        public Market Market { get; set; }
        public ETFType ETFType { get; set; }
        public Broker Broker {
            get { return m_Broker; }
            set
            {
                m_Broker = value;
                Node.Text = $"{ETFCode} [{Broker}-{m_Name}]";
            }
        }
        public virtual string Address { get; set; }
        public virtual string SettingItem { get; set; }
        public virtual bool YstFutPrice { get; set; }
        public string Channel { get; set; }
        //沒用到
        //public abstract string Method { get; }

        public virtual Uri URL { get { return m_Url; } }
        public virtual Dictionary<DataKind, Uri> URLs { get { return null; } }
        public List<string> ProcessMsg { get; private set; }
        public System.Windows.Forms.TreeNode Node { get; private set; }
        #endregion

        public ETF()
        {
            m_Web.Encoding = Encoding.UTF8;
            m_Web.DownloadStringCompleted += new DownloadStringCompletedEventHandler(DownloadStringCompleted);
            ProcessMsg = new List<string>();

            Node = new System.Windows.Forms.TreeNode() { Tag = this, Checked = false };
        }
        
        protected abstract void DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e);        

        #region Private
        protected string INI(string session, string key)
        {
            if (string.IsNullOrEmpty(session) || string.IsNullOrEmpty(key)) { return null; }
            return Utility.INI[session][key];
        }
        protected string NODE(HtmlDocument doc, string xPath)
        {
            if (string.IsNullOrEmpty(xPath) || doc == null) { return string.Empty; }
            HtmlNode node = doc.DocumentNode.SelectSingleNode(xPath);
            if (node == null) { return string.Empty; }
            return node.InnerText.Trim();
        }
        protected void MSG(string Msg)
        {
            if (ProcessMsg == null) { ProcessMsg = new List<string>(); }
            if (string.IsNullOrEmpty(Msg))
            {
                ProcessMsg.Add(string.Empty);
                return;
            }
            lock (ProcessMsg)
            {
                ProcessMsg.Add($"{DateTime.Now.ToString("HH:mm:ss.fff")}   {Msg}");
            }            
            m_Log.Info($"{ETFCode} {Msg}");
        }
        protected void CHANNEL()
        {
#if !DEBUG
            int db = int.Parse(Utility.INI["SYS"]["CHANNELDB"]);
            string channel = Utility.INI["SYS"]["CHANNELNAME"];
            MSG($"[Redis]   DB:{db} Channel:{channel}");
            foreach (var redis in Utility.Redis)
            {
                redis.HashSet(db, ETFCode, $"{channel}.{ETFCode}", "0");
            }
#endif
        }
        protected abstract void _DataKind();
        protected double Rate(Dictionary<string,PCF_FxRate>dict,  string BaseCurncy, string QuotedCurncy)
        {
            if (BaseCurncy == QuotedCurncy) { return 1.0; }
            //if (m_FXs.ContainsKey(QuotedCurncy) && BaseCurncy == "USD")
            if (dict.ContainsKey(QuotedCurncy) && BaseCurncy == "USD")
            {
                //return m_FXs[QuotedCurncy].Value;
                return dict[QuotedCurncy].Value;
            }
            else
            {
                //if (!m_FXs.ContainsKey(BaseCurncy)) { return 1.0; }
                //var b = m_FXs[BaseCurncy].Value;
                //var q = m_FXs[QuotedCurncy].Value;
                if (!dict.ContainsKey(BaseCurncy)) { return 1.0; }
                var b = dict[BaseCurncy].Value;
                var q = dict[QuotedCurncy].Value;
                var re = q / b;
                return re;
            }
        }
        protected string ForeignFUTMonth(string month)
        {
            switch (month)
            {
                case "F":
                    return "01";
                case "G":
                    return "02";
                case "H":
                    return "03";
                case "J":
                    return "04";
                case "K":
                    return "05";
                case "M":
                    return "06";
                case "N":
                    return "07";
                case "Q":
                    return "08";
                case "U":
                    return "09";
                case "V":
                    return "10";
                case "X":
                    return "11";
                case "Z":
                    return "12";
                default:
                    return string.Empty;
            }
        }
        protected Exch ExchParse(string PID, string splitor = " ")
        {
            string[] pid = PID.Split(new string[] { splitor }, StringSplitOptions.RemoveEmptyEntries);
            if (pid.Length < 2) { return Exch.TWSE; }
            switch (pid[1])
            {
                case "UN":
                case "N":
                    return Exch.NYSE;
                case "UQ":
                case "US":
                case "OQ":
                    return Exch.NASDAQ;
                case "JP":
                case "JT":
                    return Exch.TSE;
                case "HK":
                    return Exch.HKEx;
                case "IS":
                    return Exch.NSE;
                case "CH":
                    string first = pid[0].Substring(0, 1);
                    if (first == "6")
                    {
                        return Exch.SSE;
                    }
                    else
                    {
                        return Exch.SZSE;
                    }
                case "BB":
                case "FP":
                case "GY":
                case "NA":
                case "SM":
                case "IM":
                case "FH":
                case "ID":
                    return Exch.EUREX;
                case "KS":
                    return Exch.KRX;
            }
            return Exch.TWSE;
        }        
        
        #endregion

        #region Public        
        public virtual void DoParse(DateTime? today =null)
        {
            if (URL == null)
            {
                MSG("[Error]    沒有設定網址");
                return;
            }
            if (today != null)
            {
                //Reset Date, Message, Address
                m_Today = (DateTime)today;
                ProcessMsg = new List<string>();
                Address = Address;
            }
            MSG("[Download]    開始進行下載...");
            m_Web.DownloadStringAsync(URL, DataKind.HEAD);

            //string v = m_Web.DownloadString(URL);

            //HttpWebRequest req = HttpWebRequest.Create(URL) as HttpWebRequest;           
            //using (WebResponse response = req.GetResponse())
            //{
            //    StreamReader sr = new StreamReader(response.GetResponseStream());
            //    var s  = sr.ReadToEnd();
            //    sr.Close();
            //}
            //HttpWebRequest req = REQ("https://www.cathaysite.com.tw/funds/etf/etf_ws.asmx/GetPCFByStock", "_stock_code=00636");            
            //req.BeginGetResponse(new AsyncCallback(FinishWebRequest), req);
        }
        //private void FinishWebRequest(IAsyncResult result)
        //{
        //    string Err;
        //    string rep = REP(result, out Err);            
        //}

        //protected HttpWebRequest REQ(Uri url, string param = "")
        //{
        //    HttpWebRequest req = WebRequest.Create(url) as HttpWebRequest;
        //    req.Method = Method;
        //    if (!string.IsNullOrEmpty(param))
        //    {
        //        req.ContentType = "application/x-www-form-urlencoded";
        //        byte[] paramByte = Encoding.UTF8.GetBytes(param);
        //        req.ContentLength = paramByte.Length;
        //        using (Stream s = req.GetRequestStream())
        //        {
        //            s.Write(paramByte, 0, paramByte.Length);
        //            s.Close();
        //        }
        //    }
        //    return req;
        //}
        //protected HttpWebRequest REQ(string url, string param = "")
        //{
        //    return REQ(new Uri(url), param);
        //}
        //protected string REP(IAsyncResult result , out string ErrMsg)
        //{
        //    ErrMsg = string.Empty;
        //    try
        //    {
        //        using (HttpWebResponse response = (result.AsyncState as HttpWebRequest).EndGetResponse(result) as HttpWebResponse)
        //        {
        //            using (StreamReader sr = new StreamReader(response.GetResponseStream()))
        //            {
        //                return sr.ReadToEnd();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrMsg = ex.Message;
        //        return string.Empty;
        //    }            
        //}
        public void SetValue(string etfcode, string name, Market m, ETFType t, Broker b, string address, string setting, bool ystFutPrice)
        {
            ETFCode = etfcode;
            Name = name;
            Market = m;
            ETFType = t;
            Broker = b;
            Address = address;
            SettingItem = setting;
            YstFutPrice = ystFutPrice;
        }
        
        public override string ToString()
        {
            return $"[{ETFCode,-10}]{Name}({Broker}-{Market})   {Address}";

            //return string.Format("{0,-10}{1}    - {2};{3}", "[" + ETFCode + "]", (Name+"("+ Broker+")").PadRight(10,' '), Market, Address);
        }
        #endregion
    }
}