//#define NET4

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RtNAV.Utility
{
    public abstract class Notify : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

#if NET4
        protected virtual void OnPropertyChanged( string caller )
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }
#else
        protected virtual void OnPropertyChanged([CallerMemberName] string caller = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }
#endif

    }
}