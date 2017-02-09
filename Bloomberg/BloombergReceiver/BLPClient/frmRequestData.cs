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
//using Bloomberglp.Blpapi;
//using EventHandler = Bloomberglp.Blpapi.EventHandler;
using BLParser;

namespace BLPClient
{
    public partial class frmRequestData : DockContent
    {

        public frmRequestData()
        {
            InitializeComponent();

            //NotificationCenter.Instance.AddObserver(BeforeSetSubscriber, "BEFORESETSUBSCRIBER");
            //NotificationCenter.Instance.AddObserver(AfterSetSubscriber, "AFTERSETSUBSCRIBER");
            //if (Utility.Subscriber!= null)
            //{
            //    Utility.Subscriber.OnRequestReply += new EventHandler(OnRequestReply);
            //}
        }

        //private void BeforeSetSubscriber(Notification n)
        //{
        //    if (Utility.Subscriber == null) { return; }
        //    Utility.Subscriber.OnRequestReply -= new EventHandler(OnRequestReply);
        //}
        //private void AfterSetSubscriber(Notification n)
        //{
        //    if (Utility.Subscriber == null) { return; }
        //    Utility.Subscriber.OnRequestReply += new EventHandler(OnRequestReply);
        //}

        //private void OnRequestReply(Event eventObject, Session session)
        //{
        //    //Response response = new Response(eventObject);
        //    //Console.WriteLine("Response");
        //    //if (response.HasError)
        //    //{
        //    //    foreach (var item in response.Errors)
        //    //    {
        //    //        Response.ErrorElement(this, "", item);
        //    //    }
        //    //    return;
        //    //}
        //    foreach (Bloomberglp.Blpapi.Message msg in eventObject)
        //    {
        //        txtResponse.AppendText(msg.ToString());
        //    }
        //}

    }
}
