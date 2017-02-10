using System;
using System.Collections.Generic;
using System.Text;
using QuickFix;
using System.Threading;
using FixInitiator.Class.Public;
using System.Drawing;
using System.Reflection;

namespace FixInitiator.Class
{
    class MainClass
    {
        public delegate void OnMessgaeDelegate(string data);
        private static MainClass mainClass_ = new MainClass();
        private DataBase dataBase_ = new DataBase();
        private SrnItems srnItems_ = new SrnItems();
        private QuickFix.Application application_ = new ClientInitiator();
        private SessionSettings settings_ = new SessionSettings(Properties.Settings.Default.Path);
        private FileStoreFactory storeFactory_ = null;
        private ScreenLogFactory logFactory_ = null;
        private MessageFactory messageFactory_ = new DefaultMessageFactory();
        private SocketInitiator initiator_;
        private System.Collections.ArrayList list_ = null;  //存放 Session
        private SessionID sessionID_ = null;
        private bool isLogon_ = false;
        private List<string> logs_ = new List<string>();
        private Dictionary<string, Order> orders_ = new Dictionary<string, Order>();
        public event OnMessgaeDelegate OnMessage;
        public bool isResetSeq { get; set; }  //是否重設連線時的序號
        private int seqNo_ = 0;  
          
        private MainClass()
        {
            Initialize();
            isResetSeq = true;
            if (Properties.Settings.Default.TradeDate == DateTime.Today)
                seqNo_ = Properties.Settings.Default.SeqNo;
            else
                seqNo_ = 0;

            dataBase_.SetParam(Properties.Settings.Default.SQL_Server, Properties.Settings.Default.SQL_ID, Properties.Settings.Default.SQL_Password);
            //dataBase_.FillExecutionReport(Properties.Settings.Default.SystemID ,System.DateTime.Today);
        }
        private void Initialize()
        {
            storeFactory_ = new FileStoreFactory(settings_);
            logFactory_ = new ScreenLogFactory(settings_);
        }
        public void  StartInitiator()
        {
            initiator_ = new SocketInitiator(application_, storeFactory_, settings_, logFactory_, messageFactory_);
            initiator_.start();
            Thread.Sleep(3000);
            list_ = initiator_.getSessions();
            if (list_.Count > 0)
            {
                sessionID_ = (SessionID)list_[0];
            }
        }
        public void StopInitiator()
        {
            initiator_.stop();
            initiator_.Dispose();
        }

        //下單
        public void NewOrderSingle(string account,string symbol,char side,char ordType,double price,double orderQty,char timeInForce)
        {
            Account account_ = new Account(account);
            Symbol symbol_ = new Symbol(symbol);
            Side side_ = new Side(side);
            OrdType ordType_ = new OrdType(ordType);
            Price price_ = new Price(price);
            OrderQty orderQty_ = new OrderQty(orderQty);
            TimeInForce timeInForce_ = new TimeInForce (timeInForce);

            ClOrdID clOrdID = new ClOrdID(string.Format("{0,-2}{1,-10}{2:000000}",  Properties.Settings.Default.SystemID,System.DateTime.Today.ToString("yyyy/M/dd"), ++seqNo_));
            QuickFix42.NewOrderSingle order = new QuickFix42.NewOrderSingle(clOrdID, new HandlInst(HandlInst.AUTOMATED_EXECUTION_ORDER_PRIVATE), symbol_, side_,  new TransactTime(DateTime.Now), ordType_);
            order.set(orderQty_);
            order.set(price_);
            order.set(timeInForce_);
            order.set(account_);
           
            order.set(new ExDestination("SEHK"));
            order.set(new SecurityExchange("SEHK"));
            order.set(new IDSource(IDSource.RIC_CODE));
            order.set(new Currency("HKD"));
            order.set(new Rule80A(Rule80A.AGENCY_SINGLE_ORDER));
            order.set(new PegDifference(0d));
            string s = string.Format("{0}\t{1}:{2},ClOrdID:{3},Account:{4},Symbol:{5},Side:{6},OrdType:{7},Price:{8},OrderQty:{9},TimeInForce:{10}", DateTime.Now.ToString("hh:mm:ss.ffff"), MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name,
                 clOrdID, account, symbol,side, ordType, price, orderQty, timeInForce);
            
            Session.sendToTarget(order, sessionID_);
            logs_.Add(s);
            ShowMessage(s);
        }

        public void OrderCancelRequest(string origClOrdID,string account,string symbol,char side)
        {
            OrigClOrdID origClOrdID_ = new OrigClOrdID(origClOrdID);
            ClOrdID clOrdID_ = new ClOrdID(string.Format("{0,-2}{1,-10}{2:000000}",  Properties.Settings.Default.SystemID,System.DateTime.Today.ToString("yyyy/M/dd"), ++seqNo_));
            Account account_ = new Account(account);
            Symbol symbol_ = new Symbol(symbol);
            Side side_ = new Side(side);
            

            QuickFix42.OrderCancelRequest ordCancelRequest = new QuickFix42.OrderCancelRequest(origClOrdID_, clOrdID_, symbol_, side_, new TransactTime(DateTime.Now));
            ordCancelRequest.set(account_);
            

            //ordCancelRequest.set(new ExDestination("SEHK"));
            ordCancelRequest.set(new SecurityExchange("SEHK"));
            ordCancelRequest.set(new IDSource(IDSource.RIC_CODE));
            //ordCancelRequest.set(new Currency("HKD"));
            //ordCancelRequest.set(new Rule80A(Rule80A.AGENCY_SINGLE_ORDER));
            //ordCancelRequest.set(new PegDifference(0d));
            //order2.set(new OrderQty(200));
            //order2.set(new Price(24.65d));
            //order2.set(new TimeInForce(TimeInForce.DAY));

            string s = string.Format("{0}\t{1}:{2},OrigClOrdID:{3},ClOrdID:{4},Account:{5},Symbol:{6},Side:{7}", DateTime.Now.ToString("hh:mm:ss.ffff"), MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name,
              origClOrdID_,clOrdID_, account, symbol, side);

            Session.sendToTarget(ordCancelRequest, sessionID_);

 
            logs_.Add(s);
            ShowMessage(s);
            /*
            QuickFix42.OrderCancelRequest ordCancelRequest = new QuickFix42.OrderCancelRequest(orgiClOrdID, new ClOrdID(string.Format("D_{0}", orgiClOrdID)), symbol, side, new TransactTime(DateTime.Now));
            order2.set(new OrderQty(200));
            order2.set(new Price(24.65d));
            order2.set(new TimeInForce(TimeInForce.DAY));
            order2.set(new Account("71402150XXXX"));
            order2.set(new ExDestination("SEHK"));
            order2.set(new SecurityExchange("SEHK"));
            order2.set(new IDSource(IDSource.RIC_CODE));
            order2.set(new Currency("HKD"));
            order2.set(new Rule80A(Rule80A.AGENCY_SINGLE_ORDER));
            order2.set(new PegDifference(0d));

            order2.setString(SenderSubID.FIELD, "MASTERLINK");
            order2.setString(TargetSubID.FIELD, "SPSA");



            Console.WriteLine("Sending Order2 to Server");
            Session.sendToTarget(order2, sessionID);
             */
 

        }
        public void ShowMessage(string message)
        {
            if (OnMessage != null)
                OnMessage(message);
        }
        public void DoExport()
        {
            Thread thread = new Thread(new ThreadStart(ExportFile));
            thread.Start();
        }
        private void ExportFile()
        {
            string path = DateTime.Today.ToString("yyyyMMdd");
            LogClass.CreatPath(path);

            string fName = string.Format("{0}_{1}", System.Diagnostics.Process.GetCurrentProcess().ProcessName, DateTime.Now.ToString("hhmmss"));
            string[] items;
        
            lock (this)
            {

                items = logs_.ToArray();
            }
            foreach (string s in items)
            {
                LogClass.Writing(s, fName);
            }

        }
        /*
         *  ClientInitiator app = new ClientInitiator();
            //SessionSettings settings = new SessionSettings(@"C:\Users\nek\Code\FIX test App\initiator.cfg");
            //SessionSettings settings = new SessionSettings(@"D:\Program\c#\Taiwan\C#\HongKong\FixInitiator\initiator.cfg");
          
            //SessionSettings settings = new SessionSettings(@"D:\HongKong\FixInitiator\initiator.cfg");

            SessionSettings settings = new SessionSettings(Properties.Settings.Default.Path);
            QuickFix.Application application = new ClientInitiator();
            FileStoreFactory storeFactory = new FileStoreFactory(settings);
            ScreenLogFactory logFactory = new ScreenLogFactory(settings);
            MessageFactory messageFactory = new DefaultMessageFactory();

            SocketInitiator initiator = new SocketInitiator(application, storeFactory, settings, logFactory, messageFactory);
            
            initiator.start();
            Thread.Sleep(3000);
            System.Collections.ArrayList list = initiator.getSessions();

            SessionID sessionID = (SessionID)list[0];

           
            //QuickFix42.NewOrderSingle order = new QuickFix42.NewOrderSingle(new ClOrdID("DLF"), new HandlInst(HandlInst.MANUAL_ORDER), new Symbol("DLF"), new Side(Side.BUY), new TransactTime(DateTime.Now), new OrdType(OrdType.LIMIT));
            QuickFix42.NewOrderSingle order = new QuickFix42.NewOrderSingle(new ClOrdID("2823_3"), new HandlInst(HandlInst.AUTOMATED_EXECUTION_ORDER_PRIVATE), new Symbol("2823"), new Side(Side.BUY), new TransactTime(DateTime.Now), new OrdType(OrdType.LIMIT));


            order.set(new OrderQty(100));
            order.set(new Price(14.58));
            order.set(new TimeInForce(TimeInForce.DAY));
            order.set(new Account("714023400002"));
            order.set(new ExDestination("SEHK"));
            order.set(new SecurityExchange("SEHK"));
            order.set(new IDSource(IDSource.RIC_CODE));
            order.set(new Currency("HKD"));
            order.set(new Rule80A(Rule80A.AGENCY_SINGLE_ORDER));
            order.set(new PegDifference(0d));

            //order.setString(SenderSubID.FIELD, "MASTERLINK");
            //order.setString(TargetSubID.FIELD, "SPSA");
             
            

            Console.WriteLine("Sending Order to Server");
            Session.sendToTarget(order, sessionID);
            
            
            
           
            QuickFix42.NewOrderSingle order2 = new QuickFix42.NewOrderSingle(new ClOrdID("2827"), new HandlInst(HandlInst.AUTOMATED_EXECUTION_ORDER_PRIVATE), new Symbol("2827"), new Side(Side.BUY), new TransactTime(DateTime.Now), new OrdType(OrdType.LIMIT));


            order2.set(new OrderQty(200));
            order2.set(new Price(24.65d));
            order2.set(new TimeInForce(TimeInForce.DAY));
            order2.set(new Account("71402150XXXX"));
            order2.set(new ExDestination("SEHK"));
            order2.set(new SecurityExchange("SEHK"));
            order2.set(new IDSource(IDSource.RIC_CODE));
            order2.set(new Currency("HKD"));
            order2.set(new Rule80A(Rule80A.AGENCY_SINGLE_ORDER));
            order2.set(new PegDifference(0d));

            order2.setString(SenderSubID.FIELD, "MASTERLINK");
            order2.setString(TargetSubID.FIELD, "SPSA");



            Console.WriteLine("Sending Order2 to Server");
            Session.sendToTarget(order2, sessionID);
 
            OrigClOrdID orgiClOrdID = new OrigClOrdID("2827");
            Side side = order.getSide ();
            Symbol symbol = order.getSymbol ();
            QuickFix42.OrderCancelRequest ordCancelRequest = new QuickFix42.OrderCancelRequest(orgiClOrdID, new ClOrdID(string.Format("D_{0}", orgiClOrdID)) ,symbol, side, new TransactTime(DateTime.Now));

            Console.WriteLine("Sending Cancel Order to Server");
            Session.sendToTarget(ordCancelRequest, sessionID);
            
            Console.ReadLine();





            initiator.stop();
         */


        #region 屬性
        public static  MainClass mainClass
        {
            get
            {
                return mainClass_;
            }

        }
        public SrnItems srnItems
        {
            get
            {
                return srnItems_;
            }
        }
        public bool isLogon
        {
            get
            {
                return isLogon_;
            }
            set
            {
                isLogon_ = value;
                if (isLogon_)
                {
                    srnItems_.butOrdSvr.BackColor = Color.Green;
                }
                else
                {
                    srnItems_.butOrdSvr.BackColor = Color.Red;
                }
            }
        }
        public List<string> logs
        {
            get
            {
                return logs_;
            }
        }
        public int seqNo
        {
            get
            {
                return seqNo_;
            }
            set
            {
                seqNo_ = value;
            }
        }
        public DataBase dataBase
        {
            get
            {
                return dataBase_;
            }
        }
        public Dictionary<string, Order> orders
        {
            get
            {
                return orders_;
            }
        }
        #endregion
    }
}
