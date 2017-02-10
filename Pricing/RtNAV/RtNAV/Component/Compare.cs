//#define NET4

using RtNAV.Utility;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace RtNAV.Component
{
    public class Compare : Notify
    {
        #region Variable
        private Yuanta m_Yuanta;
        private string m_Channel;
        private decimal m_DiffLimit;
        private decimal m_Value;
        private SolidColorBrush m_AlertBrush;
        #endregion

        #region Property
        public string Channel
        {
            get { return m_Channel; }
            set
            {
                m_Channel = value;
#if NET4
                OnPropertyChanged(nameof(Channel));
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
        public decimal Value
        {
            get { return m_Value; }
            set
            {
                m_Value = value;
#if NET4
                OnPropertyChanged(nameof(Value));
#else
                OnPropertyChanged();
#endif
            }
        }
        public bool isOverLimit
        {
            get { return m_Yuanta.NAV != -1 && Diff > DiffLimit; }
        }
        public decimal Diff { get { return Math.Abs(m_Yuanta.NAV - Value); } }
        public SolidColorBrush AlertBrush
        {
            get { return m_AlertBrush; }
            set
            {
                m_AlertBrush = value;
#if NET4
                OnPropertyChanged(nameof(AlertBrush));
#else
                OnPropertyChanged();
#endif
            }
        }
        #endregion

        private Compare(Yuanta yt, SolidColorBrush alert, string channel, decimal limit)
        {
            m_Yuanta = yt;
            m_AlertBrush = alert;
            m_Channel = channel;
            m_DiffLimit = limit;
        }
        private Compare(Yuanta yt, string settingString)
        {
            string[] items = settingString.Split('-');
            m_Yuanta = yt;
            m_Channel = items[0];
            decimal.TryParse(items[1], out m_DiffLimit);
            m_AlertBrush = (SolidColorBrush)new BrushConverter().ConvertFromString(items[2]);
        }
        public static Compare Create(Yuanta yt, string setting)
        {
            if (string.IsNullOrEmpty(setting))
            {
                return null;
            }
            return new Compare(yt, setting);
        }
        public static Compare Create(Yuanta yt, SolidColorBrush alert, string channel, decimal limit)
        {
            return new Compare(yt, alert, channel, limit);
        }
        public override string ToString()
        {
            return $"{Channel}-{DiffLimit}-{AlertBrush}";
        }
    }
}