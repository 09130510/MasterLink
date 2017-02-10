using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XTLib
{
    using TMsgCode = UInt32;
    using TPkPtr = IntPtr;
    using TPkSz = UInt32;
    using TIndex = UInt32;
    using SorApi;
    using System.Diagnostics;
    using System.Threading;

    public class XTLib
    {
        public delegate void OnSorDealReportEvent(SorObj Ord, SorObj Rpt, List<SorObj> Details);
        public delegate void OnSorOrdReportEvent(SorObj Ord, SorObj Rpt);


        public const char TableNameSplit = ':';
        public const char RecordSplit = '\n';
        public const char FieldSplit = '\x01';
        public const string RequestHeader = "-----";

        #region Variable
        private SorClient m_SorClient = new SorClient(true);
        private SorFlowCtrlSender m_SorFlowCtrl;
        private SorAccs m_SorAccs = new SorAccs();
        private Dictionary<string, SorTable> m_Tables = new Dictionary<string, SorTable>();
        private Dictionary<string, SorTable> m_REQTables = new Dictionary<string, SorTable>();
        private Dictionary<string, SorTable> m_ORDTables = new Dictionary<string, SorTable>();
        private Dictionary<string, SorTable> m_RPTTables = new Dictionary<string, SorTable>();
        private Dictionary<string, SorTable> m_DDSTables = new Dictionary<string, SorTable>();
        private Dictionary<string, SorObj> m_Orders = new Dictionary<string, SorObj>();
        #endregion

        #region Property
        public SorClientState State { get { return m_SorClient.State; } }
        public string[] Accs
        {
            get
            {
                if (m_SorAccs == null) { return null; }
                return m_SorAccs.Values.Select(e => e.SubacNo).ToArray();
            }
        }

        public OnSorApReadyEvent OnSorApReady { private get; set; }
        public OnSorChgPassResultEvent OnSorChgPassResult { set { m_SorClient.OnSorChgPassResultEvent = value; } }
        public OnSorClientDeleteEvent OnSorClientDelete { set { m_SorClient.OnSorClientDeleteEvent = value; } }
        public OnSorConnectEvent OnSorConnect { set { m_SorClient.OnSorConnectEvent = value; } }
        //public OnSorReportEvent OnSorReport{ set { m_SorClient.OnSorReportEvent = value; } }
        public OnSorOrdReportEvent OnSorOrdReport { private get; set; }
        public OnSorDealReportEvent OnSorDealReport { private get; set; }
        public OnSorRequestAckEvent OnSorRequestAck { set { m_SorClient.OnSorRequestAckEvent = value; } }
        public OnSorTaskResultEvent OnSorTaskResult { set { m_SorClient.OnSorTaskResultEvent = value; } }
        public OnSorUnknownMsgCodeEvent OnSorUnknownMsgCode { set { m_SorClient.OnSorUnknownMsgCodeEvent = value; } }
        #endregion

        public XTLib()
        {
            m_SorClient.OnSorApReadyEvent = _OnSorApReady;
            m_SorClient.OnSorReportEvent = _OnSorReport;
            m_SorFlowCtrl = new SorFlowCtrlSender(m_SorClient);
        }

        #region Delegate
        private void _OnSorApReady(SorClient client)
        {
            SorTable accTable = client.SgnResult.NameTable("Accs");
            m_SorAccs.SorTableParser(accTable);

            TIndex tcount = client.SgnResult.TablesCount;
            for (TIndex L = 0; L < tcount; L++)
            {
                SorTable table = client.SgnResult.IndexTable(L);
                SorProperties properties = table.Properties;
                string[] tablename = properties.Name.Split(TableNameSplit);
                switch (tablename[0].ToEnum<TableType>())
                {
                    case TableType.REQ:
                        if (!m_REQTables.ContainsKey(tablename[1]))
                        {
                            m_REQTables.Add(tablename[1], table);
                        }
                        break;
                    case TableType.ORD:
                        if (!m_ORDTables.ContainsKey(tablename[1]))
                        {
                            m_ORDTables.Add(tablename[1], table);
                        }
                        break;
                    case TableType.RPT:
                        if (!m_RPTTables.ContainsKey(tablename[1]))
                        {
                            m_RPTTables.Add(tablename[1], table);
                        }
                        break;
                    case TableType.DDS:
                        if (!m_DDSTables.ContainsKey(tablename[1]))
                        {
                            m_DDSTables.Add(tablename[1], table);
                        }
                        break;
                    default:
                        if (!m_Tables.ContainsKey(tablename[0])) { m_Tables.Add(tablename[0], table); }
                        break;
                }
            }
            if (OnSorApReady != null)
            {
                OnSorApReady(client);
            }
        }
        private void _OnSorReport(SorClient client, string result)
        {
            string[] records = result.Split(RecordSplit);
            int recordCnt = records.Length;


            for (int i = 0; i < recordCnt; )
            {
                SorObj rpt = null;
                SorObj ord = null;
                List<SorObj> details = new List<SorObj>();

                string[] itemNames = records[i++].Split(FieldSplit);
                string[] fields = records[i++].Split(FieldSplit);
                string sorRID = fields[0];

                lock (m_Orders)
                {
                    if (itemNames.Length > 1)
                    {
                        //委回非回補
                        rpt = new SorObj(m_RPTTables[itemNames[1]], fields);
                        if (!m_Orders.ContainsKey(sorRID))
                        {
                            m_Orders.Add(sorRID, new SorObj(m_ORDTables[itemNames[0]], rpt));
                        }
                        //ord = new SorObj(m_ORDTables[itemNames[0]], rpt);
                    }
                    else
                    {
                        //if (!m_Orders.ContainsKey(sorRID))
                        //{
                        //    m_Orders.Add(sorRID, new SorObj(m_ORDTables[itemNames[0]], fields));
                        //}
                        //else if (m_Orders[sorRID].TableName != m_ORDTables[itemNames[0]].Properties.Name)
                        //{
                        m_Orders[sorRID] = new SorObj(m_ORDTables[itemNames[0]], fields);
                        //}
                    }
                    ord = m_Orders[sorRID];

                }

                while (i < recordCnt && String.IsNullOrEmpty(records[i]))
                {
                    string dds = records[i++];
                    string[] ds = records[i++].Split(FieldSplit);
                    SorObj d = new SorObj(m_DDSTables["TwsOrd"], ds);
                    details.Add(d);
                }

                //GOrd不處理; 還是會收單拆單後的TwsOrd
                if (ord.TableName.Contains("GOrd")) { continue; }
                //委回, 成回不送; 
                if (OnSorOrdReport != null)
                {
                    if (rpt != null && rpt.TableName != "RPT:TwsDeal" /*&& !rpt.TableName.Contains("GOrd")*/)
                    {
                        OnSorOrdReport(ord, rpt);

                    }
                }
                //只送成回
                if (OnSorDealReport != null)
                {
                    if (rpt == null && details.Count <= 0) { continue; }
                    if (rpt != null && rpt.TableName != "RPT:TwsDeal" && details.Count <= 0) { continue; }
                    //回補 或 回報
                    //if (((rpt == null  && details.Count>0)|| (rpt != null && rpt.TableName == "RPT:TwsDeal")))//&&
                    ////(ord["OrderSt"] == ((int)OrderSt.PartFilled).ToString() || 
                    //// ord["OrderSt"] == ((int)OrderSt.FullFilled).ToString()))
                    //{

                    OnSorDealReport(ord, rpt, details);
                    //}
                }
            }
        }
        #endregion

        #region Public
        public void Connect(string ipport, string user, string pwd, string ver)
        {
            m_SorClient.Connect(ipport, "XMOrd", user, pwd, ver);
        }
        public void Disconnect()
        {
            m_SorClient.Disconnect();
            //m_SorAccs.Clear();
            //m_Tables.Clear();
            //m_REQTables.Clear();
            //m_ORDTables.Clear();
            //m_RPTTables.Clear();
            //m_DDSTables.Clear();
        }
        public bool Recover(RecoverType rtype)
        {
            if (State != SorClientState.SorClientState_ApReady) { return false; }
            string desc = RequestHeader;
            switch (rtype)
            {
                case RecoverType.AllIncludeDealDetail:
                case RecoverType.AllNoDealDetail:
                case RecoverType.RemainIncludeDealDetail:
                case RecoverType.DealWithIDIncludeDealDetail:
                case RecoverType.DealWithIDNoDealDetail:
                case RecoverType.AllDealIncludeDealDetail:
                case RecoverType.AllDealNoDealDetail:
                case RecoverType.Remain:
                    desc = string.Format("{0}1{1}{2}", desc, FieldSplit, rtype.Description());
                    break;
                case RecoverType.NotReceive:
                case RecoverType.OnlyDeal:
                    desc = string.Format("{0}0{1}", desc, rtype.Description());
                    break;
            }
            return m_SorClient.SendSorRequest(0x83, desc);
        }
        #endregion
    }
}
