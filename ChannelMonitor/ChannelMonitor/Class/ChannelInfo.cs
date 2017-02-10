using SourceCell;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ChannelMonitor.Class
{
    public class ChannelInfo : IDisposable
    {
        const int CHECKINTERVAL = 1000;

        #region Variable
        private frmMonitor m_Monitor;
        private Timer m_Timer;
        private TextCell c_Channel;
        private TextCell c_Item;
        private TextCell c_Value;
        private TextCell c_Delay;
        private TimeSpan m_UpdateTime;
        #endregion

        #region Property
        public TextCell Channel
        {
            get
            {
                if (c_Channel == null)
                {
                    c_Channel = new TextCell() { Value = "", TextAlignment = DevAge.Drawing.ContentAlignment.MiddleLeft, DefaultBackColor = Color.LightGray, HasBorder = true };
                }
                return c_Channel;
            }
        }
        public TextCell Item
        {
            get
            {
                if (c_Item == null)
                {
                    c_Item = new TextCell() { Value = "", TextAlignment = DevAge.Drawing.ContentAlignment.MiddleLeft, DefaultBackColor = Color.LightGray, HasBorder = true };
                }

                return c_Item;
            }
        }
        public TextCell Value
        {
            get
            {
                if (c_Value == null)
                {
                    c_Value = new TextCell() { Value = "", CellType = TextCell.TextType.String, TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight, DefaultBackColor = Color.White, DefaultFontColor = Color.Black, HasBorder = true };
                    c_Value.OnValueChanged += OnValueChanged;
                }
                return c_Value;
            }
        }
        public TextCell Delay
        {
            get
            {
                if (c_Delay == null)
                {
                    c_Delay = new TextCell() { CellType = TextCell.TextType.Int, TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight, DefaultBackColor = Color.White, HasBorder = true };
                }
                return c_Delay;
            }
        }
        public TimeSpan Interval { get; set; }
        public bool Exceed { get; private set; } = false;
        #endregion


        public ChannelInfo(frmMonitor monitor, int seconds, string channel, string item, string value)
        {
            m_Monitor = monitor;
            Interval = TimeSpan.FromSeconds(seconds);

            Channel.SetValue(channel);
            Item.SetValue(item);
            Value.SetValue(value);
            m_UpdateTime = new TimeSpan(DateTime.Now.Ticks);


            m_Timer = new Timer(CHECKINTERVAL);
            m_Timer.Elapsed += Timer_Elapsed;
            m_Timer.Start();
        }

        #region Delegate
        private void OnValueChanged(CellBase cell, EventArgs e)
        {
            m_UpdateTime = new TimeSpan(DateTime.Now.Ticks);
        }
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Exceed = new TimeSpan(DateTime.Now.Ticks) - m_UpdateTime > Interval;
            decimal sec = (new TimeSpan(DateTime.Now.Ticks) - m_UpdateTime).Ticks / 10000000;
            Delay.SetValueColor(sec, Exceed ? Color.Crimson : Value.DefaultBackColor, Exceed ? Color.White : Value.DefaultFontColor);
            m_Monitor.CountExceed();
        }

        #endregion

        public void Stop()
        {

            Delay.SetBackColor(Color.FromArgb(80, Color.DodgerBlue));
            Delay.SetFontColor(Delay.DefaultFontColor);
            Exceed = false;
            m_Timer.Stop();
        }
        public void Start()
        {
            Delay.SetBackColor(Delay.DefaultBackColor);
            Delay.SetFontColor(Delay.DefaultFontColor);
            m_UpdateTime = new TimeSpan(DateTime.Now.Ticks);
            m_Timer.Start();
        }

        #region IDisposable Support
        private bool disposedValue = false; // 偵測多餘的呼叫

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    m_Timer.Stop();
                }

                // TODO: 釋放 Unmanaged 資源 (Unmanaged 物件) 並覆寫下方的完成項。
                // TODO: 將大型欄位設為 null。

                disposedValue = true;
            }
        }

        // TODO: 僅當上方的 Dispose(bool disposing) 具有會釋放 Unmanaged 資源的程式碼時，才覆寫完成項。
        // ~ChannelInfo() {
        //   // 請勿變更這個程式碼。請將清除程式碼放入上方的 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 加入這個程式碼的目的在正確實作可處置的模式。
        public void Dispose()
        {
            // 請勿變更這個程式碼。請將清除程式碼放入上方的 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果上方的完成項已被覆寫，即取消下行的註解狀態。
            // GC.SuppressFinalize(this);
        }
        #endregion

    }
}