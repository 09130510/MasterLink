using Bloomberglp.Blpapi;
using BLPServer.Class;
using WeifenLuo.WinFormsUI.Docking;
using EventHandler = Bloomberglp.Blpapi.EventHandler;

namespace BLPServer
{
    public partial class frmRequestData : DockContent
    {
        public frmRequestData()
        {
            InitializeComponent();

            NotificationCenter.Instance.AddObserver(BeforeSetSubscriber, "BEFORESETSUBSCRIBER");
            NotificationCenter.Instance.AddObserver(AfterSetSubscriber, "AFTERSETSUBSCRIBER");
            if (Utility.Subscriber!= null)
            {
                Utility.Subscriber.OnRequestReply += new EventHandler(OnRequestReply);
            }
        }

        private void BeforeSetSubscriber(Notification n)
        {
            if (Utility.Subscriber == null) { return; }
            Utility.Subscriber.OnRequestReply -= new EventHandler(OnRequestReply);
        }
        private void AfterSetSubscriber(Notification n)
        {
            if (Utility.Subscriber == null) { return; }
            Utility.Subscriber.OnRequestReply += new EventHandler(OnRequestReply);
        }
        private void OnRequestReply(Event eventObject, Session session)
        {
            //Response response = new Response(eventObject);
            //Console.WriteLine("Response");
            //if (response.HasError)
            //{
            //    foreach (var item in response.Errors)
            //    {
            //        Response.ErrorElement(this, "", item);
            //    }
            //    return;
            //}
            foreach (Bloomberglp.Blpapi.Message msg in eventObject)
            {
                txtResponse.AppendText(msg.ToString());
            }
        }
    }
}
