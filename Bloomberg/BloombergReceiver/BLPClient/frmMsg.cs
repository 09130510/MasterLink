using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using Util.Extension.Class;
using BLPClient.Class;
//using EventHandler = Bloomberglp.Blpapi.EventHandler;
//using Bloomberglp.Blpapi;
using BLParser;


namespace BLPClient
{
    public partial class frmMsg : DockContent
    {
        public frmMsg()
        {
            InitializeComponent();

            //NotificationCenter.Instance.AddObserver(BeforeSetSubscriber, "BEFORESETSUBSCRIBER");
            //NotificationCenter.Instance.AddObserver(AfterSetSubscriber, "AFTERSETSUBSCRIBER");
            //if (Utility.Subscriber != null)
            //{
            //    Utility.Subscriber.OnRequestReply += new EventHandler(OnRequestReply);
            //    Utility.Subscriber.OnSubscriptionReply += new EventHandler(OnSubscriptionReply);
            //    Utility.Subscriber.OnStatusReceived += new EventHandler(OnStatusReceived);
            //    Utility.Subscriber.OnOtherReceived += new EventHandler(OnOtherReceived);
            //}
        }


        //private void BeforeSetSubscriber(Notification n)
        //{
        //    if (Utility.Subscriber == null) { return; }
        //    Utility.Subscriber.OnRequestReply -= new EventHandler(OnRequestReply);
        //    Utility.Subscriber.OnSubscriptionReply -= new EventHandler(OnSubscriptionReply);
        //    Utility.Subscriber.OnStatusReceived -= new EventHandler(OnStatusReceived);
        //    Utility.Subscriber.OnOtherReceived -= new EventHandler(OnOtherReceived);
        //}
        //private void AfterSetSubscriber(Notification n)
        //{
        //    if (Utility.Subscriber == null) { return; }
        //    Utility.Subscriber.OnRequestReply += new EventHandler(OnRequestReply);
        //    Utility.Subscriber.OnSubscriptionReply += new EventHandler(OnSubscriptionReply);
        //    Utility.Subscriber.OnStatusReceived += new EventHandler(OnStatusReceived);
        //    Utility.Subscriber.OnOtherReceived += new EventHandler(OnOtherReceived);
        //}

        //private void OnRequestReply(Event eventObject, Session session)
        //{
        //    Response response = new Response(eventObject);            
        //    if (response.HasError)
        //    {
        //        foreach (var item in response.Errors)
        //        {
        //            //txtMsg.AppendText(Response.ErrorElement(this, "ResponseERR", item));
        //            txtMsg.AppendText(item);
        //        }                
        //    }
        //    //foreach (var collection in response.Collections)
        //    //{
        //        //if (collection.HasError)
        //        //{
        //        //    foreach (var err in collection.Errors)
        //        //    {
        //        //        txtMsg.AppendText(Response.ErrorElement(this, "SecurityERR", err));
        //        //    }                    
        //        //}
        //        foreach (var security in response.Collection.Securities.Values)
        //        {
        //            if (security.HasException)
        //            {
        //                foreach (var err in security.FieldEx)
        //                {
        //                    //txtMsg.AppendText(Response.ErrorElement(this, "FieldERR", err));
        //                    txtMsg.AppendText(err);
        //                }
        //            }
        //        }	
        //    //}
        //}
        //private void OnSubscriptionReply(Event eventObject, Session session)
        //{
        //    Subscribe s = Subscribe.Create(eventObject);
        //    if (s.HasError)
        //    {
        //        foreach (var err in s.Errors)
        //        {
        //            //txtMsg.AppendText(Response.ErrorElement(this, "SubscriptionERR", err));
        //            txtMsg.AppendText(err);
        //        }
        //    }
        //}
        //private void OnStatusReceived(Event eventObject, Session session)
        //{
        //    foreach (Bloomberglp.Blpapi.Message msg in eventObject)
        //    {
        //        txtMsg.AppendText("Status   "+msg.ToString());
        //    }
        //}
        //private void OnOtherReceived(Event eventObject, Session session)
        //{
        //    foreach (Bloomberglp.Blpapi.Message msg in eventObject)
        //    {
        //        txtMsg.AppendText("Other    "+msg.ToString());
        //    }
        //}
    }
}
