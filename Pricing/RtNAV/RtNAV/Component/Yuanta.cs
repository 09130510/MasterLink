//#define NET4

using RtNAV.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;

using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;


namespace RtNAV.Component
{
    public class Yuanta : Notify
    {
        #region Variable
        private bool m_isPublish = true;
        private string m_PublishChanngel = "RtNAV";
        private Dictionary<string, Compare> m_CompareChannel = new Dictionary<string, Compare>();
        private decimal m_DiffLimit = 0.02M;

        private string m_ETFCode;
        private string m_Name;
        private JSON.YT m_Json;
        private decimal m_YstNAV = -1;
        private decimal m_NAV = -1;
        private decimal m_Price = -1;
        private DateTime m_UpdateTime = DateTime.Today;
        private SolidColorBrush m_Background = Brushes.White;
        private SolidColorBrush m_Foreground = Brushes.Black;
        #endregion

        #region Property
        public bool isPublish
        {
            get { return m_isPublish; }
            set
            {
                m_isPublish = value;
#if NET4
                OnPropertyChanged(nameof(isPublish));
#else
                OnPropertyChanged();
#endif

            }
        }
        public string PublishChannel
        {
            get { return m_PublishChanngel; }
            set
            {
                m_PublishChanngel = value;
#if NET4
                OnPropertyChanged(nameof(PublishChannel));                
#else
                OnPropertyChanged();

#endif
                OnPropertyChanged(nameof(Channel));
            }
        }
        public string Channel
        {
            get
            {
                return string.IsNullOrEmpty(PublishChannel) ? string.Empty : $"{PublishChannel}.{ETFCode}";
            }
        }
        public Dictionary<string, Compare> CompareChannel
        {
            get { return m_CompareChannel; }
            set
            {
                m_CompareChannel = value;
#if NET4
                OnPropertyChanged(nameof(CompareChannel));
#else
                OnPropertyChanged();

#endif
            }
        }
        public decimal DiffLimit
        {
            get { return m_DiffLimit; }
            set
            {
                m_DiffLimit = value;
#if NET4
                OnPropertyChanged(nameof(DiffLimit));
#else
                OnPropertyChanged();

#endif

            }
        }
        public string ETFCode
        {
            get { return m_ETFCode; }
            set
            {
                m_ETFCode = value;
#if NET4
                OnPropertyChanged(nameof(ETFCode));
#else
                OnPropertyChanged();

#endif
                OnPropertyChanged(nameof(Channel));
            }
        }
        public string Name
        {
            get { return m_Name; }
            set
            {
                m_Name = value;
#if NET4
                OnPropertyChanged(nameof(Name));
#else
                OnPropertyChanged();

#endif
            }
        }
        public decimal YstNAV
        {
            get { return m_YstNAV; }
            set
            {
                m_YstNAV = value;
#if NET4
                OnPropertyChanged(nameof(YstNAV));
#else
                OnPropertyChanged();

#endif

            }
        }
        public decimal NAV
        {
            get { return m_NAV; }
            set
            {
                if (value == m_NAV) { return; }
                m_NAV = value;
                if (isPublish && !string.IsNullOrEmpty(Channel))
                {
                    MainWindow.PublishLib.Publish(Channel, m_NAV.ToString());
                    if (MainWindow.PublishLib.HashGet(1, ETFCode, Channel) == null)
                    {
                        MainWindow.PublishLib.Publish("ETFN.NAV", $"I|{ETFCode}|{Channel}");
                    }
                    MainWindow.PublishLib.HashSet(1, ETFCode, Channel, m_NAV.ToString());
                }
#if NET4
                OnPropertyChanged(nameof(NAV));
#else
                OnPropertyChanged();

#endif

                //Change Color
                _Alert();
            }
        }
        private void _Alert()
        {
            foreach (var item in CompareChannel.Values)
            {
                if (item.isOverLimit)
                {
                    Background = item.AlertBrush;
                    Foreground = Brushes.White;

                    if (isPublish)
                    {
                        var diff = string.Format("{0:0.####}", item.Diff);
                        MainWindow.PublishLib.Publish($"ETFALERT.{ETFCode}", $"[{item.Channel}] 與預估淨值相差 {diff}");
                    }
                    return;
                }
            }
            Background = Brushes.White;
            Foreground = Brushes.Black;
        }
        public decimal Price
        {
            get { return m_Price; }
            set
            {
                m_Price = value;
#if NET4
                OnPropertyChanged(nameof(Price));
#else
                OnPropertyChanged();
#endif
            }
        }
        public DateTime UpdateTime
        {
            get { return m_UpdateTime; }
            set
            {
                m_UpdateTime = value;
#if NET4
                OnPropertyChanged(nameof(UpdateTime));
#else
                OnPropertyChanged();
#endif
            }
        }
        public SolidColorBrush Background
        {
            get { return m_Background; }
            set
            {
                m_Background = value;
#if NET4
                OnPropertyChanged(nameof(Background));
#else
                OnPropertyChanged();
#endif
            }
        }
        public SolidColorBrush Foreground
        {
            get { return m_Foreground; }
            set
            {
                m_Foreground = value;
#if NET4
                OnPropertyChanged(nameof(Foreground));
#else
                OnPropertyChanged();
#endif
            }
        }
        #endregion

        public Yuanta()
        {
            //CompareChannel.Add("UAT", new Compare(this, Brushes.Orange) { Channel = "UAT", DiffLimit = DiffLimit });
            //CompareChannel.Add("NAV", new Compare(this, Brushes.Crimson) { Channel = "NAV", DiffLimit = DiffLimit });
            MainWindow.PublishLib.OnValueUpdated += PublishLib_OnValueUpdated;
        }
        public Yuanta(JSON.YT json)
        {
            ETFCode = json.etfId;
            Name = json.name;

            Update(json);
        }

        private void PublishLib_OnValueUpdated(string channel, string item, string value)
        {
            if (item != ETFCode || !m_CompareChannel.ContainsKey(channel)) { return; }
            decimal decValue;
            decimal.TryParse(value, out decValue);
            m_CompareChannel[channel].Value = decValue;
            _Alert();
            //if (m_CompareChannel[channel].isOverLimit)
            //{
            //    Background = m_CompareChannel[channel].AlertBrush;
            //    Foreground = Brushes.White;
            //}
            //else if(Background== m_CompareChannel[channel].AlertBrush)
            //{
            //    Foreground = Brushes.Black;
            //    Background = Brushes.White;
            //}
        }

        #region Public
        public void Update(JSON.YT json)
        {
            m_Json = json;

            _Set(typeof(Yuanta).GetProperty(nameof(YstNAV)), json.yestNav);
            _Set(typeof(Yuanta).GetProperty(nameof(NAV)), json.nav);
            _Set(typeof(Yuanta).GetProperty(nameof(Price)), json.price);
            _Set(typeof(Yuanta).GetProperty(nameof(UpdateTime)), json.updateTime);

            Console.WriteLine($"{ETFCode} {NAV} {UpdateTime.ToString("HHmmss.fff")}");
        }
        public void AddCompare(string channel, SolidColorBrush alert, decimal limit)
        {
            if (!CompareChannel.ContainsKey(channel))
            {
                Compare comp = Compare.Create(this, alert, channel, DiffLimit);
                CompareChannel.Add(channel, comp);
            }
        }
        public void RemoveCompare(string channel)
        {
            if (CompareChannel.ContainsKey(channel))
            {
                CompareChannel.Remove(channel);
            }
        }
        public override string ToString()
        {
            string[] compares = m_CompareChannel.Values.Select(e => e.ToString()).ToArray();
            return $"{ETFCode}|{PublishChannel}|{string.Join(",", compares)}|{isPublish}";
        }
        #endregion

        #region Private
        private void _Set(PropertyInfo pinfo, string value)
        {
            switch (pinfo.PropertyType.FullName)
            {
                case "System.Decimal":
                    decimal d;
                    decimal.TryParse(value, out d);
#if NET4
                    pinfo.SetValue(this, d, null);
#else
                    pinfo.SetValue(this, d);
#endif
                    break;
                case "System.DateTime":
                    DateTime t;
                    DateTime.TryParse(value, out t);
#if NET4
                    pinfo.SetValue(this, t, null);
#else
                    pinfo.SetValue(this, t);
#endif

                    break;
                default:
#if NET4
                    pinfo.SetValue(this, value, null);
#else
                    pinfo.SetValue(this, value);
#endif

                    break;
            }
        }
        #endregion

    }
}
