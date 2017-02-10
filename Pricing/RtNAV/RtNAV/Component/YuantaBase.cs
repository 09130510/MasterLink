#define NOTHREADING
//#define NET4

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RtNAV.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;

namespace RtNAV.Component
{
    public class YuantaBase : Notify
    {
        private const string ADDRESS = "http://www.yuantaetfs.com/api/RtNav";
        private const int STARTTIME = 083000;
        private const int STOPTIME = 170000;

        #region Variable
        private Dictionary<string, Yuanta> m_List = new Dictionary<string, Yuanta>();
        private System.Timers.Timer m_Timer = new System.Timers.Timer(15000);

        private bool m_isCorrection = false;
        private bool m_isStart = false;
        private bool m_inCorrection = false;
        #endregion

        #region Property
        public bool isStart
        {
            get { return m_isStart; }
            set
            {
                m_isStart = value;
#if NET4
                OnPropertyChanged(nameof(isStart));
#else
                OnPropertyChanged();
#endif
            }
        }
        /// <summary>
        /// 時間校正
        /// </summary>        
        public bool isCorrection
        {
            get { return m_isCorrection; }
            set
            {
                m_isCorrection = value;
#if NET4
                OnPropertyChanged(nameof(isCorrection));
#else
                OnPropertyChanged();
#endif
            }
        }
        public double Interval
        {
            get { return m_Timer.Interval; }
            set
            {
                m_Timer.Interval = value;
#if NET4
                OnPropertyChanged(nameof(Interval));
#else
                OnPropertyChanged();
#endif
            }
        }
        public Yuanta this[string etfcode]
        {
            get
            {
                if (m_List.ContainsKey(etfcode))
                {
                    return m_List[etfcode];
                }
                return null;
            }
        }
        public int Count { get { return m_List.Count; } }
        public Dictionary<string, Yuanta> List { get { return m_List; } }
        #endregion

        public YuantaBase()
        {
            _LoadSetting();

            m_Timer.Elapsed += Timer_Elapsed;
        }

        #region Delegate
        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (!m_inCorrection)
            {
                _Download(_HourMinSec(DateTime.Now));
            }
        }
        private void DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
#if !NOTHREADING
            ThreadPool.QueueUserWorkItem((args) =>
            {
                DownloadStringCompletedEventArgs evt = (DownloadStringCompletedEventArgs)args;
#else
            DownloadStringCompletedEventArgs evt = e;
#endif
            int updateHMS = -1;
            JArray array = JArray.Parse(evt.Result.ToString());
            foreach (JObject obj in array)
            {
                JSON.YT ytJson = JsonConvert.DeserializeObject<JSON.YT>(obj.ToString());
                if (m_List.ContainsKey(ytJson.etfId) /*&& m_List[ytJson.etfId].isPublish*/)
                {
                    m_List[ytJson.etfId].Update(ytJson);
                    if (updateHMS == -1)
                    {
                        updateHMS = _HourMinSec(m_List[ytJson.etfId].UpdateTime);
                    }
                    else
                    {
                        updateHMS = Math.Min(_HourMinSec(m_List[ytJson.etfId].UpdateTime), updateHMS);
                    }
                }
            }
            if (isStart && updateHMS != -1 && isCorrection)
            {
                var now = DateTime.Now;
                int current = int.Parse(string.Format("{0:00}{1:00}{2:00}", now.Hour, now.Minute, now.Second));
                if ((int)e.UserState > updateHMS && (current > STARTTIME && current < STOPTIME))
                {
                    Console.WriteLine("******");
                    m_inCorrection = true;
                    _Download((int)e.UserState);
                }
                else
                {
                    m_inCorrection = false;
                }
            }

#if !NOTHREADING
            }, e);
#endif   
        }
        #endregion

        #region Public
        public void Start(double interval = 15000D)
        {
            Interval = interval;

            if (isCorrection)
            {
                double sec = (interval / 1000);
                while (DateTime.Now.Second % sec != 0)
                {
                    Thread.Sleep(1);
                }
                m_Timer.Start();
            }
            else
            {
                m_Timer.Start();
            }
            isStart = true;
        }
        public void Stop()
        {
            m_Timer.Stop();
            isStart = false;
        }
        public void Save()
        {
            string[] YTs = m_List.Values.Select(e => e.ToString()).ToArray();
            MainWindow.INI["YUANTA"]["ITEMS"] = $"{ string.Join(";", YTs)}";
            MainWindow.INI["YUANTA"]["INTERVAL"] = Interval.ToString();
            MainWindow.INI["YUANTA"]["ISCORRECTION"] = isCorrection.ToString();
            MainWindow.WriteConfig();
        }
        #endregion

        #region Private
        private void _LoadSetting()
        {
            double interval;
            bool timecorrection;
            double.TryParse(MainWindow.INI["YUANTA"]["INTERVAL"], out interval);
            bool.TryParse(MainWindow.INI["YUANTA"]["ISCORRECTION"], out timecorrection);
            Interval = interval;
            isCorrection = timecorrection;

            var list = MainWindow.SQL.Query<Yuanta>("SELECT DISTINCT ETFCODE FROM TBLETF WHERE BROKER='YT' ORDER BY ETFCODE ");
            string[] settings = MainWindow.INI["YUANTA"]["ITEMS"].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var etf in list)
            {
                if (!m_List.ContainsKey(etf.ETFCode))
                {
                    m_List.Add(etf.ETFCode, etf);
                    foreach (var setting in settings)
                    {
                        if (setting.Contains($"{etf.ETFCode}|"))
                        {
                            string[] items = setting.Split('|');
                            etf.PublishChannel = items[1];
                            string[] channels = items[2].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (var channel in channels)
                            {
                                //string[] channelitems = channel.Split('-');
                                //var compare = new Compare(etf, (SolidColorBrush)new BrushConverter().ConvertFromString(channelitems[2]), channelitems[0], decimal.Parse(channelitems[1]));
                                var comp = Compare.Create(etf, channel);
                                etf.CompareChannel.Add(comp.Channel, comp);
                            }
                            etf.isPublish = bool.Parse(items[3]);
                            break;
                        }
                    }
                }
            }
        }
        private void _Download(int getMinSecond/* = -1*/)
        {
            //Console.WriteLine(DateTime.Now);
            WebClient client = new WebClient();
            client.Encoding = Encoding.UTF8;
            client.DownloadStringCompleted += DownloadStringCompleted;

            //if (getMinSecond == -1) { getMinSecond = _HourMinSec(DateTime.Now); }
            Console.WriteLine(getMinSecond);
            client.DownloadStringAsync(new Uri(ADDRESS), getMinSecond);
        }
        private int _HourMinSec(DateTime time)
        {
            return time.Hour * 10000 + time.Minute * 100 + time.Second;
        }
        #endregion
    }
}