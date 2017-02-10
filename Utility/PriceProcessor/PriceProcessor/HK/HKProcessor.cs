using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Threading;

namespace PriceProcessor.HK
{
    public class HKProcessor : Processor
    {
        #region Event
        public delegate void MarketPriceDelegate(string PID, decimal Price);
        public event MarketPriceDelegate MarketPriceEvent;
        #endregion

        #region Variable
        private string m_Address = "http://www.etnet.com.hk/www/tc/stocks/realtime/quote_chart_interactive.php?code=";
        private string m_Address2 = "http://www.etnet.com.hk/www/tc/ashares/quote_chart_interactive.php?code=";
        private System.Timers.Timer m_timGetter;
        private WebClient m_WebClient;
        #endregion

        #region Property
        public Uri URL
        {
            get
            {
                if (m_PID.Length == 6)
                {
                    return new Uri(string.Format("{0}{1}", m_Address2, m_PID));
                }
                return new Uri(string.Format("{0}{1}", m_Address, m_PID));
            }
        }
        #endregion

        public HKProcessor(string PID, int Interval)
            : base()
        {
            m_PID = PID;

            m_timGetter = new System.Timers.Timer(Interval == 0 ? 5000 : Interval);
            m_timGetter.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);

            m_WebClient = new WebClient();
        }

        #region Delegate
        private void WebClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                string html = e.Result;
                int idx = html.IndexOf(@"<div class=""quotePrice", 0);
                if (idx != -1)
                {
                    int StartIdx = html.IndexOf(">", idx);
                    int EndIdx = html.IndexOf("<", StartIdx);

                    decimal price;
                    string quote = html.Substring(StartIdx + 1, EndIdx - StartIdx - 1).Trim();

                    if (decimal.TryParse(quote, out price))
                    {
                        OnMarketPrice(price);
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine(e.Error);
            }
        }
        private void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (m_WebClient.IsBusy) { return; }
            m_WebClient.DownloadStringAsync(URL);
        }
        private void OnMarketPrice(decimal Price)
        {
            if (MarketPriceEvent != null)
            {
                MarketPriceEvent(m_PID, Price);
            }
        }
        #endregion

        #region Public
        public void Start()
        {
            if (m_timGetter == null) { return; }
            m_WebClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(WebClient_DownloadStringCompleted);
            m_timGetter.Start();
            timer_Elapsed(this, null);
        }
        public void Stop()
        {
            if (m_timGetter == null) { return; }
            m_timGetter.Stop();
            m_WebClient.DownloadStringCompleted -= new DownloadStringCompletedEventHandler(WebClient_DownloadStringCompleted);
        }
        #endregion
    }
}