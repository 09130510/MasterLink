using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.IO;
using NuDotNet;
using NuDotNet.SocketUtil;

namespace NuTukanBus
{
    public delegate bool dlgTukanCB(NuSocketParam oParam, TukanBusMsg oMsg);
    public delegate void dlgTukanExceptionAlert(string sMsg);

    public static class TukanBusExtension
    {
        public static void LoadIDBody(this TukanBusMsg oMsg, ref MemoryStream src, int iIDLen, int iBodyLen)
        {
            int iBufSz = 256;
            byte[] bBuf = new byte[iBufSz];

            LoadIDBody(oMsg, ref src, iIDLen, iBodyLen, ref bBuf, iBufSz);
        }
        public static void LoadIDBody(this TukanBusMsg oMsg, ref MemoryStream src, int iIDLen, int iBodyLen, ref byte[] bBuf, int BufSz)
        {
            int iNeedn = iIDLen;
            int iReadn = 0;

            while (iNeedn > 0)
            {
                iReadn = Math.Min(BufSz, iNeedn);
                src.Read(bBuf, 0, iReadn);
                oMsg.Body.Write(bBuf, 0, iReadn);
                iNeedn -= iReadn;
            }
            oMsg.ID = Encoding.Default.GetString(oMsg.Body.ToArray());
            oMsg.Body.SetLength(0);

            iNeedn = iBodyLen;
            while (iNeedn > 0)
            {
                iReadn = Math.Min(BufSz, iNeedn);
                src.Read(bBuf, 0, iReadn);
                oMsg.Body.Write(bBuf, 0, iReadn);
                iNeedn -= iReadn;
            }
        }
    }

    public class TukanBusMsgType
    {
        public const char Login = 'A';
        public const char Logout = '5';
        public const char TestReq = '1';
        public const char HBT = '0';
        public const char Msg = 'm';
        public const char SubscribeReq = 'S';
        public const char SubscribeCfm = 's';
        public const char Reject = '3';
    }

    public static class TukanBusProtol
    {
        public static int HdrSz = 10;
        public static char TopicGroupLeader = '@';
        public static char[] TopicDelimit = { ',' };

        public static void Pack(ref TukanBusMsg oMsg, ref MemoryStream mStream)
        {
            byte[] bMsg = null;
            byte[] bBody = oMsg.Body.ToArray(); 
            mStream.SetLength(0);

            bMsg = Encoding.Default.GetBytes(
                    String.Format("{0}{1:0000}{2:0000}{3}{4}",
                                  oMsg.MsgType, oMsg.ID.Length, bBody.Length,
                                  NuAsciiCode.Ascii_1, 
                                  oMsg.ID));
            mStream.Write(bMsg, 0, bMsg.Length);
            if (bBody.Length > 0)
                mStream.Write(bBody, 0, bBody.Length);
        }

        public static bool UnPack(ref TukanBusMsg oMsg, ref MemoryStream mStream)
        {
            int iIDLen = 0;
            int iBodyLen = 0;
            int iBufSz = 512;
            string sTmp;

            byte[] bBuf = new byte[iBufSz];

            if (mStream.Length < HdrSz)
                return false;

            mStream.Seek(0, SeekOrigin.Begin);
            sTmp = Encoding.Default.GetString(mStream.ToArray(), 0, HdrSz);

            if (sTmp[HdrSz - 1] != NuAsciiCode.Ascii_1)
                return false;

            oMsg.MsgType = sTmp[0];

            if (!int.TryParse(sTmp.Substring(1, 4), out iIDLen))
                return false;

            if (!int.TryParse(sTmp.Substring(5, 4), out iBodyLen))
                return false;

            // get id
            NuMStreamFn.MStreamNCpy(ref oMsg.Body, ref mStream, iIDLen, ref bBuf, iBufSz);
            oMsg.ID = Encoding.Default.GetString(oMsg.Body.ToArray(), 0, iIDLen);

            // get body
            oMsg.Body.SetLength(0);
            NuMStreamFn.MStreamNCpy(ref oMsg.Body, ref mStream, iBodyLen, ref bBuf, iBufSz);

            return true;
        }

        public static void GenLoginReq(ref MemoryStream mStream, string LoginID)
        {
            TukanBusMsg oMsg = new TukanBusMsg();
            oMsg.Clear();
            oMsg.MsgType = TukanBusMsgType.Login;
            oMsg.ID = LoginID;
            Pack(ref oMsg, ref mStream);
        }
 
        public static void GenSubscribeReq(ref MemoryStream mStream, List<string> Topic)
        {
            TukanBusMsg oMsg = new TukanBusMsg();
            oMsg.Clear();
            oMsg.MsgType = TukanBusMsgType.SubscribeReq;
            foreach (string str in Topic)
            {
                oMsg.ID += String.Format("{0},", str);
            }
            oMsg.ID = oMsg.ID.Trim(new char[] { ',' });
            Pack(ref oMsg, ref mStream);
        }

        public static void GenMsgReq(ref MemoryStream mStream, ref TukanBusMsg oMsg, string TargetID, string Msg)
        {
            byte[] bMsg = null;
            oMsg.MsgType = TukanBusMsgType.Msg;
            oMsg.ID = TargetID;
            bMsg = Encoding.Default.GetBytes(Msg);
            oMsg.Body.Write(bMsg, 0, bMsg.Length);
            Pack(ref oMsg, ref mStream);
        }

        public static void GenMsgReq(ref MemoryStream mStream, string TargetID, string Msg)
        {
            TukanBusMsg oMsg = new TukanBusMsg();
            oMsg.Clear();
            GenMsgReq(ref mStream, ref oMsg, TargetID, Msg);
        }

    }

    public class TukanBusMsg : IDisposable
    {
        public String LoginID;
        public char MsgType;
        public String ID;
//        public String Body;
        public MemoryStream Body = null;
        public String ErrorMsg;

        public TukanBusMsg()
        {
            Body = new MemoryStream();
        }

        public void Dispose()
        {
            if (Body != null)
            {
                Body.Dispose();
                Body = null;
            }
        }

        public void SetBody(byte[] bBody)
        {
            if (bBody.Length > 0)
                Body.Write(bBody, 0, bBody.Length);
        }

        public void SetBody(String sBody)
        {
            byte[] bMsg = null;
            bMsg = Encoding.Default.GetBytes(sBody);
            SetBody(bMsg);
        }

        public void Clear()
        {
            LoginID = "";
            MsgType = ' ';
            ID = "";
            Body.SetLength(0);
            ErrorMsg = "";
        }

        public string BodyAsciiString { get { return Encoding.Default.GetString(Body.ToArray()); } }
    }

    internal static class TukanFn
    {
        public static bool RcvTukanMsg(ref NuSocketParam oParam, ref TukanBusMsg oMsg)
        {
            MemoryStream mStream = new MemoryStream();
            int iNeedRcv = 0;
            int iIDLen = 0;
            int iBodyLen = 0;
            int iRcvCnt = 0;
            string sTmp = "";

            iRcvCnt = oParam.Recv(ref mStream, TukanBusProtol.HdrSz, 1);
            if (iRcvCnt != TukanBusProtol.HdrSz)
                return false;

            sTmp = Encoding.Default.GetString(mStream.ToArray());

            oMsg.MsgType = sTmp[0];

            if (!int.TryParse(sTmp.Substring(1, 4), out iIDLen))
                return false;
            if (!int.TryParse(sTmp.Substring(5, 4), out iBodyLen))
                return false;

            iNeedRcv = iIDLen + iBodyLen;

            mStream.SetLength(0);
            iRcvCnt = oParam.Recv(ref mStream, iNeedRcv, 1);
            if (iRcvCnt != iNeedRcv)
                return false;
            mStream.Seek(0, SeekOrigin.Begin);
            oMsg.LoadIDBody(ref mStream, iIDLen, iBodyLen);

            return true;
        }

    }

}
