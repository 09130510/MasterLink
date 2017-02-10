using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExcelDna.Integration;
using System.ServiceModel;
using System.Reactive.Disposables;
using ExcelDna.Integration.RxExcel;
using System.Reactive.Linq;

namespace RTDSvr
{
    public class ExcelAddIn: IExcelAddIn, IRTDClnt
    {
        private static IRTDSvr m_Server;
        #region IExcelAddIn 成員

        

        public void AutoOpen()
        {
            ExcelIntegration.RegisterUnhandledExceptionHandler(ex => "Exception: " + ex.ToString());
            EndpointAddress addr = new EndpointAddress("net.tcp://localhost:8080/rtd");
            NetTcpBinding binding = new NetTcpBinding();
            DuplexChannelFactory<IRTDSvr> factory = new DuplexChannelFactory<IRTDSvr>(this, binding, addr);
            m_Server = factory.CreateChannel();
            m_Server.Register();
        }
        public void AutoClose()
        {
            m_Server.Unregister();
        }
        #endregion

        #region IRTDClnt 成員
        public void HeartBeat() { }
        public void ValueToClnt(string Account, string ComID, SummaryItem Item,  object Value)
        {
            if (OnValueSent != null)
                OnValueSent(new ValueSentEventArgs(Account, ComID, Item, Value));
        }

        #endregion

        
        // event boilerplate stuff
        public delegate void ValueSentHandler(ValueSentEventArgs args);
        public static event ValueSentHandler OnValueSent;
        public class ValueSentEventArgs : EventArgs
        {
            public string Account { get; private set; }
            public string ComID { get; private set; }
            public SummaryItem Item { get; private set; }
            public object Value { get; private set; }
            public ValueSentEventArgs(string Account,string ComID, SummaryItem Item, object Value)
            {
                this.Account = Account;
                this.ComID = ComID;
                this.Item = Item;
                this.Value = Value;
            }
        }
        
        [ExcelFunction("Gets realtime values from server")]
        public static object GetValues()
        {

            //m_Server.sub
            // a delegate that creates an observable over Event2Observable
            Func<IObservable<object>> f2 =
                () => Observable.Create<object>(Event2Observable);

            //  pass that to Excel wrapper   
            return RxExcel.Observe("GetValues", null, f2);
        }
        /// <summary>
        /// Summary
        /// </summary>
        /// <param name="Account"></param>
        /// <param name="ComID"></param>
        /// <param name="Item"></param>
        /// <returns></returns>
        [ExcelFunction(Name="GetSummary", Description="彙總")]        
        public static object GetSummary(
            [ExcelArgument(Name="Account",Description="帳號")]
            string Account, 
            string ComID, 
            string Item)
        {

            //ObserveEventArgs e = new ObserveEventArgs(Account, ComID, Item);
            Func<IObservable<object>> f2 =
                () => Observable.Create<object>(
                    observer =>
                    {
                        OnValueSent += d =>
                            {
                                if (Account == d.Account)
                                {
                                    observer.OnNext(d.Value);
                                }
                            };
                        return Disposable.Empty;
                    }
                    );
            
            //  pass that to Excel wrapper   
            return RxExcel.Observe("GetSummary", new[] { Account, ComID,Item }, f2);
        }

        static Func<IObserver<object>, IDisposable> Event2Observable = observer =>
        {
            OnValueSent += d => observer.OnNext(d.Value);  
            return Disposable.Empty;                       
        };
        //static Func<string, IObserver<object>, IDisposable> Summary2Observable = (Account, observer )=>
        //{
        //    OnValueSent += d =>
        //        {
        //            if (d.Account == Account)
        //            {
        //                observer.OnNext(d.Value);
        //            }                    
        //        };
        //    return Disposable.Empty;
        //};
    }
}
