using Notifier.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Notifier.Class
{
    public class DisplayChannel : IDisposable
    {
        #region Variable                
        private string m_Value;
        private TimeSpan m_UpdateTime;
        private NotifyBox m_Notify;
        #endregion

        #region Property
        public ServerInfo Server { get; private set; }
        public string ChannelName { get; private set; }
        public string Item { get; private set; }
        public string Value
        {
            get { return m_Value; }
            set
            {
                m_UpdateTime = new TimeSpan(DateTime.Now.Ticks);
                m_Value = value;
                _Show();
            }
        }
        public Style Style { get; private set; }
        public bool Alerted { get; private set; } = false;
        #endregion

        private void _Show()
        {
            if (!Alerted)
            {
                Alerted = true;
                Util.Main.InvokeIfRequired(() =>
                {
                    var d = new DateTime() + m_UpdateTime;
                    m_Notify.Show(string.Empty, $"{Server.Name}\r\n{ChannelName}.{Item}\r\n{Value}\r\n{d.ToString("HH:mm:ss.fff")}", 100, 5000, 1000, 100);
                });
            }
            else
            {
                Util.Main.InvokeIfRequired(() =>
                {
                    var d = new DateTime() + m_UpdateTime;
                    m_Notify.Update(string.Empty, $"{Server.Name}\r\n{ChannelName}.{Item}\r\n{Value}\r\n{d.ToString("HH:mm:ss.fff")}");
                });
            }
        }
        public DisplayChannel(ServerInfo server, Style style, string channel, string item, string value)
        {
            Server = server;
            Style = style;
            ChannelName = channel;
            Item = item;
            m_Value = value;
            m_UpdateTime = new TimeSpan(DateTime.Now.Ticks);
            Util.Main.InvokeIfRequired(() =>
            {
                m_Notify = new NotifyBox(Util.Main, Style);
                m_Notify.ContentClick += new EventHandler(ContentClick);
                m_Notify.CloseClick += new EventHandler(CloseClick);
            });
            _Show();
        }

        #region Delegate                
        private void CloseClick(object sender, EventArgs e)
        {
            m_UpdateTime = new TimeSpan(DateTime.Now.Ticks);
            Alerted = false;
        }
        private void ContentClick(object sender, EventArgs e)
        {
            System.Windows.Forms.Clipboard.SetDataObject(((NotifyBox)sender).ContentText);
        }
        #endregion

        #region Public
        public void UnAlert()
        {
            Alerted = false;
        }
        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // 偵測多餘的呼叫

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {

                }

                // TODO: 釋放 Unmanaged 資源 (Unmanaged 物件) 並覆寫下方的完成項。
                // TODO: 將大型欄位設為 null。

                disposedValue = true;
            }
        }

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