using System.Reflection;
using QuickFix;
using Util.Extension.Class;

namespace OrderProcessor.SinoPac
{
    public partial class SinoPacProcessor
    {
        class FixClient : QuickFix.Application
        {
            private SinoPacProcessor m_Processor;

            private string Name { get { return m_Processor.GetType().Name; } }

            public FixClient(SinoPacProcessor processor)
            {
                m_Processor = processor;
            }

            #region Application 成員
            public void onCreate(QuickFix.SessionID value)
            {
                //Console.WriteLine(MethodBase.GetCurrentMethod().ReflectedType.Name);
                //m_Processor.MsgLog(string.Format("[onCreate]    {0}",value.toString()));
                m_Processor.MsgLog(string.Format("{0}", value.toString()));
                NotificationCenter.Instance.Post(Name, new Notification(MethodBase.GetCurrentMethod().Name, value));
            }
            public void onLogon(QuickFix.SessionID value)
            {
                //m_Processor.MsgLog(string.Format("[onLogon]    {0}", value.toString()));
                m_Processor.MsgLog(string.Format("{0}", value.toString()));
                NotificationCenter.Instance.Post(Name, new Notification(MethodBase.GetCurrentMethod().Name, value));

                m_Processor.isConnect = true;
                m_Processor.isResetSeq = false;
            }
            public void onLogout(QuickFix.SessionID value)
            {
                //m_Processor.MsgLog(string.Format("[onLogout]    {0}", value.toString()));
                m_Processor.MsgLog(string.Format("{0}", value.toString()));
                NotificationCenter.Instance.Post(Name, new Notification(MethodBase.GetCurrentMethod().Name, value));

                m_Processor.isConnect = false;
            }
            public void toAdmin(QuickFix.Message msg, QuickFix.SessionID session)
            {
                //m_Processor.MsgLog(string.Format("[toAdmin]    {0}", msg.ToString()));
                m_Processor.MsgLog(string.Format("{0}", msg.ToString()));
                if (m_Processor.isResetSeq)
                {
                    msg.setString(ResetSeqNumFlag.FIELD, "Y");
                }
                NotificationCenter.Instance.Post(Name, new Notification(MethodBase.GetCurrentMethod().Name, msg));
            }
            public void toApp(QuickFix.Message msg, QuickFix.SessionID session)
            {
                //m_Processor.MsgLog(string.Format("[toApp]    {0}", msg.ToString()));
                m_Processor.MsgLog(string.Format("{0}", msg.ToString()));
                NotificationCenter.Instance.Post(Name, new Notification(MethodBase.GetCurrentMethod().Name, msg));
            }
            public void fromAdmin(QuickFix.Message msg, QuickFix.SessionID session)
            {
                //m_Processor.MsgLog(string.Format("[fromAdmin]    {0}", msg.ToString()));
                m_Processor.MsgLog(string.Format("{0}", msg.ToString()));
                NotificationCenter.Instance.Post(Name, new Notification(MethodBase.GetCurrentMethod().Name, msg));
            }
            public void fromApp(QuickFix.Message msg, QuickFix.SessionID session)
            {

                //m_Processor.MsgLog(string.Format("[fromApp]    {0}", msg.ToString()));
                m_Processor.MsgLog(string.Format("{0}", msg.ToString()));

                NotificationCenter.Instance.Post(Name, new Notification(MethodBase.GetCurrentMethod().Name, msg));
                if (msg is QuickFix42.ExecutionReport)
                {
                    SinoPacRPT rpt = new SinoPacRPT(msg as QuickFix42.ExecutionReport);
                    if (rpt.ExecType == ExecType.Fill || rpt.ExecType == ExecType.PartialFill)
                    {
                        m_Processor.MatchReply(rpt);
                    }
                    else
                    {
                        m_Processor.OrderReply(rpt);
                    }
                }
                if (msg is QuickFix42.OrderCancelReject)
                {
                    m_Processor.OrderReply(new SinoPacRPT(msg as QuickFix42.OrderCancelReject));
                }

            }

            #endregion
        }
    }
}