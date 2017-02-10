using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PATSOrder.Utility
{
    public delegate void Invoker(params object[] args);
    public class BeforeInvoker
    {
        public Invoker Invoker { get; private set; }
        public object[] Args { get; private set; }

        public BeforeInvoker(Invoker invoker, params object[] args)
        {
            Invoker = invoker;
            Args = args;
        }        
    }
}
