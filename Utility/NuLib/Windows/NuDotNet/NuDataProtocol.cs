using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NuDotNet.Net
{
    #region -- public data --
    #region event argument class
    public class RcvDataArgs : EventArgs
    {
        private string m_hdr;
        private string m_body;

        public RcvDataArgs(string hdr, string body)
        {
            this.m_hdr = hdr;
            this.m_body = body;
        }

        public string GetHdr
        {
            get { return m_hdr; }
        }
        public string GetBody
        {
            get { return m_body; }
        }

        public string GetID
        {
            get
            {
                int idx = m_hdr.IndexOf(NuDataProtocol.protocol_end);
                return (idx > 0) ? m_hdr.Substring(idx + 1) : "";
            }
        }
    }
    #endregion
    #endregion

    #region -- private data --
    

    public static class NuMsgType
    {
        public const char Login = 'A';
        public const char Logout = '5';
        public const char HBT = '0';
        public const char TestReq = '1';
        public const char Msg = 'M';
        public const char Error = 'E';

        public const char Broadcast = 'B';
    }

    public static class NuTukanBusMsgType
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

    public static class NuDataProtocol
    {
        public static char protocol_end = Convert.ToChar(1);

        public static byte HdrSz = 11;

        public static byte BusHdrSz = 10;

        public static string genHdr(string HdrID, char MsgType, int data_len)
        {
            if (HdrID == "")
                return "";
            return String.Format("{0:0000}{1}{2:00000}{3}", data_len, MsgType, Convert.ToInt32(HdrID), protocol_end);
        }

        /// <summary>
        /// 產生TukanBus Message
        /// </summary>
        /// <param name="HdrID"></param>
        /// <param name="MsgType"></param>
        /// <param name="id_len"></param>
        /// <param name="data_len"></param>
        /// <returns></returns>
        public static string genTukanBusHdr(string HdrID, char MsgType, int id_len, int data_len)
        {
            if (HdrID == "")
                return "";
            return String.Format("{0}{1:0000}{2:0000}{3}{4}", MsgType, id_len, data_len, protocol_end, HdrID);
        }

        /// <summary>
        /// 產生TukanBus格式的memory stream
        /// </summary>
        /// <param name="sTargetID"></param>
        /// <param name="BusMsgType"></param>
        /// <param name="sMsg"></param>
        /// <param name="mStream"></param>
        /// <returns></returns>
        public static bool genTukanBusStream(string sTargetID, char BusMsgType, string sMsg, ref MemoryStream mStream)
        {
            try
            {
                int iTargetIDLen = ASCIIEncoding.Default.GetByteCount(sTargetID);
                byte[] bMsg = null;
                byte[] bHdr = null;

                bMsg = Encoding.Default.GetBytes(sMsg);
                bHdr = Encoding.Default.GetBytes(genTukanBusHdr(sTargetID, BusMsgType, iTargetIDLen, bMsg.Length));

                mStream.SetLength(0);
                mStream.Write(bHdr, 0, bHdr.Length);
                mStream.Write(bMsg, 0, bMsg.Length);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
    #endregion
}
