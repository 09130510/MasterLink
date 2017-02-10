using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace RtNAV.Utility
{
    public class Ticker: Notify
    {
        private Timer m_Timer;

        public DateTime Current
        {
            get { return DateTime.Now; }
        }

        public Ticker(double interval = 1000)
        {
            m_Timer = new Timer(interval);
            m_Timer.Elapsed += Timer_Elapsed;
            m_Timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            OnPropertyChanged(nameof(Current));
        }
    }
}
