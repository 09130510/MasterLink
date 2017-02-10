using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace NuDotNet
{
    public static class NuDebug
    {
        public static void WriteLine(string sMsg)
        {
            Debug.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff") + " " + sMsg);
        }
    }
#if true
    public static class NuMStreamFn
    {
        public static void MStreamNCpy(ref MemoryStream dest, ref MemoryStream src, int nByte, ref byte[] bBuf, int BufSz)
        {
            int iNeedn = nByte;
            int iReadn = 0;

            while (iNeedn > 0)
            {
                iReadn = Math.Min(BufSz, iNeedn);
                src.Read(bBuf, 0, iReadn);
                dest.Write(bBuf, 0, iReadn);
                iNeedn -= iReadn;
            }
        }

        public static void MStreamNCpy(ref MemoryStream dest, ref MemoryStream src, int nByte)
        {
            int iBufSz = 256;
            byte[] bBuf = new byte[iBufSz];
            MStreamNCpy(ref dest, ref src, nByte, ref bBuf, iBufSz); 
        }

    }
#endif
    public class NuCommon
    {
    }

    public static class NuAsciiCode
    {
        public static readonly char Ascii_1 = Convert.ToChar(1);
        public static readonly char Ascii_2 = Convert.ToChar(2);
        public static readonly char Ascii_3 = Convert.ToChar(3);
    }

}

namespace NuDotNet.Net
{
    /// <summary>
    /// Socket connection current status
    /// </summary>
    public enum enSockStatus
    {
        NotAvailable = 0,
        Available,
        Connect,
        Disconnect, 
        Login,
        Logout
    }

    public static class NuVariable
    {
        public static int OneSec_us = 1000000;
        public static int OneSec_ms = 1000;
        public static int OneMs_us = 1000;
    }
}