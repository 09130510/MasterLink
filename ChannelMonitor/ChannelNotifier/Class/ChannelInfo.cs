using CustomUIControls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;


namespace Notifier.Class
{
    public class ChannelInfo : IDisposable
    {
        const int CHECKINTERVAL = 1000;

        #region Variable        
        private Timer m_Timer;
        //private string m_Channel;
        //private string m_Item;
        private string m_Value;
        //private string m_Delay;
        private TimeSpan m_UpdateTime;
        private NotifyBox m_Notify;        
        #endregion

        #region Property
        public ServerInfo Server { get; private set; }
        public string Channel { get; private set; }
        public string Item { get; private set; }
        public string Value
        {
            get { return m_Value; }
            set
            {
                m_Value = value;
                m_UpdateTime = new TimeSpan(DateTime.Now.Ticks);
            }
        }
        public ChannelStyle Style { get; private set; }
        public int Delay { get; private set; }
        public TimeSpan Interval { get; set; }
        public bool Exceed { get; private set; } = false;
        public bool Alerted { get; private set; } = false;
        #endregion


        public ChannelInfo(ServerInfo server, ChannelStyle style, int intervalSeconds, string channel, string item, string value)
        {
            Server = server;            
            Style = style;
            Interval = TimeSpan.FromSeconds(intervalSeconds);
            Channel = channel;
            Item = item;
            Value = value;
            m_UpdateTime = new TimeSpan(DateTime.Now.Ticks);

            _InitNotifyBox();
            m_Timer = new Timer(CHECKINTERVAL);
            m_Timer.Elapsed += Timer_Elapsed;
            m_Timer.Start();
        }

        #region Delegate        
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Exceed = new TimeSpan(DateTime.Now.Ticks) - m_UpdateTime > Interval;
            decimal sec = (new TimeSpan(DateTime.Now.Ticks) - m_UpdateTime).Ticks / 10000000;
            Delay = (int)sec;
            if (Exceed & !Alerted)
            {
                //m_TaskNotify.InvokeIfRequired(() => { 
                try
                {
                    Alerted = true;
                    Program.SettingForm.InvokeIfRequired(() =>
                    {
                        m_Notify.Show(string.Empty, $"{Server.Name}\r\n{Channel}.{Item}\r\nLast Value: {Value}\r\nStoped:{Delay} Sec", 100, 5000, 1000);
                    });
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                //});
            }
        }
        private void CloseClick(object sender, EventArgs e)
        {
            m_UpdateTime =new TimeSpan( DateTime.Now.Ticks);
            Alerted = false;
        }
        private void ContentClick(object sender, EventArgs e)
        {
            System.Windows.Forms.Clipboard.SetDataObject(((NotifyBox)sender).ContentText);
        }
        #endregion

        public void Stop()
        {
            Exceed = false;
            m_Timer.Stop();
        }
        public void Start()
        {
            m_UpdateTime = new TimeSpan(DateTime.Now.Ticks);
            m_Timer.Start();
        }
        public void UnAlert()
        {
            Alerted = false;
        }

        private void _InitNotifyBox()
        {
            Program.SettingForm.InvokeIfRequired(() =>
            {
                m_Notify = new NotifyBox(Program.SettingForm);
                
                m_Notify.SetBackgroundBitmap(new Bitmap(GetType(), $"{Style}.png"),Color.FromArgb(255, 0, 255));
                //m_Notify.SetCloseBitmap(new Bitmap(GetType(), $"{Style}Close.png"), Color.FromArgb(255, 0, 255), new Point(450, 5));
                m_Notify.SetCloseBitmap(new Bitmap(GetType(), $"{Style}Close2.png"), Color.FromArgb(255, 0, 255), new Point(25, 35));
                m_Notify.TitleRectangle = new Rectangle(5, 8, 76, 89);
                m_Notify.ContentRectangle = new Rectangle(90, 5, 390, 89);
                m_Notify.ContentClick += new EventHandler(ContentClick);
                m_Notify.CloseClick += new EventHandler(CloseClick);

                m_Notify.NormalContentColor = m_Notify.HoverContentColor = m_Notify.NormalTitleColor = m_Notify.HoverTitleColor = Color.FromName($"{Style}");
                //m_Notify.CloseClickable = m_Notify.ContentClickable =
                //m_Notify.EnableSelectionRectangle = m_Notify.KeepVisibleOnMousOver =
                //m_Notify.ReShowOnMouseOver = true;
            });
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
