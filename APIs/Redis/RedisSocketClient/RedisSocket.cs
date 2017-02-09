using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading;
using System.Net.Sockets;
using APLibrary;

namespace RedisSocket
{
    class RedisSocket
    {
        public static RedisSocket[] InfoList = new RedisSocket[1];
        public static Queue infoQueue = Queue.Synchronized(new Queue(1000));                //寫新單 StockOrder Queue
        public static ManualResetEvent infoQueueEvent = new ManualResetEvent(false);        //Write StockOrder 事件
        public static Queue writeTxtQueue = Queue.Synchronized(new Queue(1000));            //寫新單 StockOrder Queue
        public static ManualResetEvent writeTxtQueueEvent = new ManualResetEvent(false);    //Write StockOrder 事件
        public static bool InfoFlag = true;                                                 //寫DB註記
        public string ThreadID = "";                                                        //執行緒ID
        public static bool parseFlag = true;
        public string lastUpdateTimeA = "";
        public static bool decreaseFlag = false;


        //建構式
        public RedisSocket(string vThreadID) 
        {
            ThreadID = vThreadID;
        }

        public static void InitialInfo()
        {
            //啟動解析封包程序
            Thread threadP = new Thread(new ThreadStart(connectServer));
            threadP.Priority = ThreadPriority.Highest;
            threadP.IsBackground = true;
            threadP.Start();
        }

        #region 連接行情主機A connectServer()
        public static void connectServer()
        {
            Socket Infosocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);     //行情Socket
            string ServerIP;
            int ServerPort;
            string msg = "";

            

            try
            {
                //啟動顯示資料程序
                Thread threadS = new Thread(new ThreadStart(parsePrice));
                threadS.Priority = ThreadPriority.Lowest;
                threadS.IsBackground = true;
                threadS.Start();


                ServerIP = "10.14.105.85";
                ServerPort = 26386;

                //建立socket物件
                Infosocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                
                Infosocket.Connect(ServerIP, ServerPort);

                byte[] registerByte = Encoding.Default.GetBytes("publish ordera CHG|C|3\r\n");
                Infosocket.Send(registerByte);
                

                 while (true)
                 {
                     byte[] bytesBody = new byte[20];
                     int result = 0;
                     result = AsyncSocket.ReadLine(Infosocket, bytesBody, 20, FrmMain.APName, " connectServerA()");
                     if (result < 0)
                     {
                         throw new Exception("封包異常");
                     }
                     if (result > 0)
                     {
                         msg = Encoding.Default.GetString(bytesBody, 0, result);
                         infoQueue.Enqueue(msg);
                         infoQueueEvent.Set();
                     }
                 }
            }
            catch (Exception exc)
            {
                //程式自己發生的錯誤才要記錄
                parseFlag = false; //設定false，解析價格的 Thread才會停止
            }
            finally
            {
                Infosocket.Close(1); //關閉socket
            }
        }
        #endregion

        #region 解析行情主機報價封包 parsePrice()
        public static void parsePrice()
        {
            string msg;
            string[] priceInfo = new string[24];
            StockPrice targetPriceObject = new StockPrice();

            try
            {
                while (true)
                {
                    if (infoQueue.Count > 0)
                    {
                        msg = (string)infoQueue.Dequeue();
                        FrmMain.MainFrm.SetControlText("richTextBox1", msg);
                    }
                    else
                    {
                        infoQueueEvent.Reset(); //重新初始化
                        infoQueueEvent.WaitOne(); //進入睡眠狀態;
                    }
                }
            }
            catch (Exception exc)
            {
                parseFlag = false; //設定false，接收價格封包的 Thread才會停止
            }
        }
        #endregion

        #region ShowText ShowText()
        public static void ShowText()
        {
            string txtData = "";
            while (parseFlag)
            {
                if (writeTxtQueue.Count > 0)
                {
                    txtData = (string)writeTxtQueue.Dequeue();
                    FrmMain.MainFrm.SetControlText("richTextBox1", txtData);
                }
                else
                {
                    writeTxtQueueEvent.Reset(); //重新初始化
                    writeTxtQueueEvent.WaitOne(); //進入睡眠狀態;
                }
            }
        }
        #endregion
    }
}
