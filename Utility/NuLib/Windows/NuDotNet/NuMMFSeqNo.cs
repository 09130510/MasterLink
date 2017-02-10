using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.IO.MemoryMappedFiles;

namespace NuDotNet.MMF
{
    public class NuMMFSeqNoItem
    {
        private MemoryMappedViewAccessor m_accesor_writer = null;
        private MemoryMappedViewAccessor m_accesor_reader = null;
        private byte m_used = 0x00;
        private string m_prefix = "";
        private string m_min = "";
        private string m_max = "";
        private UInt32 m_int_min = 0;
        private UInt32 m_int_max = 0;

        private int m_start_pos = 0;
        private int m_prefix_sz = 0;
        private int m_body_sz = 0;
        private int m_block_sz = 0;
        private int m_seqno_sz = 0;
        private readonly MemoryMappedFile m_mmap = null;

        public NuMMFSeqNoItem(ref MemoryMappedFile mmap)
        {
            m_mmap = mmap;
        }


        ~NuMMFSeqNoItem()
        {
            if (m_accesor_reader != null)
                m_accesor_reader.Dispose();
            if (m_accesor_writer != null)
                m_accesor_writer.Dispose();
        }


        // property
        public int SeqNoSz { get { return (m_prefix_sz + m_body_sz); } }
        public int MemorySz { get { return (m_prefix_sz + m_body_sz + 4 + 4); } }
        public string Prefix { get { return m_prefix; } }
        public bool IsUsed { get { return (m_used == 0x00) ? false : true; } }

        //event
        public delegate bool dlgGenerateNext(ref string sNowSeqNo, int iSeqBodySz, 
                                             ref string sMinSeqNo, ref string sMaxSeqNo);
        public event dlgGenerateNext GenerateNextEv;

        private bool _GenerateNext(ref string sSeqNoBody,
                                   ref string sMinSeqNo, ref string sMaxSeqNo)
        {
            try
            {
                UInt32 iNo = UInt32.Parse(sSeqNoBody);
                iNo++;

                if (iNo < m_int_min || iNo > m_int_max)
                    return false;

                sSeqNoBody = iNo.ToString().PadLeft(m_body_sz, '0');
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("_GenerateNext : " + ex.Message.ToString());
            }
        }

        internal void Init(int iStartPos, int iBlockSz, string sPrefix,
            string sSeqNoBodyMin, string sSeqNoBodyMax)
        {
            byte bUsed = 0x00;
            byte bEnd = 0x00;
            int pos = 0;
            byte[] bPrefix = Encoding.Default.GetBytes(sPrefix);
            byte[] bMin = Encoding.Default.GetBytes(sSeqNoBodyMin);
            byte[] bMax = Encoding.Default.GetBytes(sSeqNoBodyMax);

            m_used = bUsed;
            m_start_pos = iStartPos;
            m_block_sz = iBlockSz;
            m_accesor_writer = m_mmap.CreateViewAccessor(iStartPos, m_block_sz);

            m_accesor_writer.Write(pos, bUsed);
            ++pos;
            m_accesor_writer.Write(pos, bPrefix.Length);
            pos += 4;
            m_accesor_writer.Write(pos, bMin.Length);
            pos += 4;

            m_accesor_writer.WriteArray<byte>(pos, bPrefix, 0, bPrefix.Length);
            pos += bPrefix.Length;
            m_accesor_writer.WriteArray<byte>(pos, bMin, 0, bMin.Length);
            pos += bMin.Length;
            m_accesor_writer.WriteArray<byte>(pos, bMin, 0, bMin.Length);
            pos += bMin.Length;
            m_accesor_writer.WriteArray<byte>(pos, bMax, 0, bMax.Length);
            pos += bMin.Length;

            m_accesor_writer.Write(pos, bEnd);
            ++pos;

            m_accesor_writer.Dispose();
        }

        private void _internal_load()
        {
            int pos = 0;
            int current_seqno_body_pos = 0;
            int current_seqno_pos = 0;
            byte[] bPrefix = null;
            byte[] bMin = null;
            byte[] bMax = null;

            m_accesor_reader = m_mmap.CreateViewAccessor(m_start_pos, m_block_sz, MemoryMappedFileAccess.Read);

            m_used = m_accesor_reader.ReadByte(pos);
            ++pos;
            m_prefix_sz = m_accesor_reader.ReadInt32(pos);
            pos += 4;
            m_body_sz = m_accesor_reader.ReadInt32(pos);
            pos += 4;

            bPrefix = new byte[m_prefix_sz];
            bMin = new byte[m_body_sz];
            bMax = new byte[m_body_sz];

            current_seqno_pos = pos + m_start_pos;
            if (m_prefix_sz > 0)
            {
                m_accesor_reader.ReadArray<byte>(pos, bPrefix, 0, m_prefix_sz);
                pos += m_prefix_sz;
                m_prefix = Encoding.Default.GetString(bPrefix);
            }
            else
            {
                m_prefix = "";
            }

            // current seqno
            current_seqno_body_pos = pos + m_start_pos;
            pos += m_body_sz;

            m_accesor_reader.ReadArray<byte>(pos, bMin, 0, m_body_sz);
            pos += m_body_sz;
            m_min = Encoding.Default.GetString(bMin);

            m_accesor_reader.ReadArray<byte>(pos, bMax, 0, m_body_sz);
            pos += m_body_sz;
            m_max = Encoding.Default.GetString(bMax);

            if (!UInt32.TryParse(m_min, out m_int_min))
                m_int_min = 0;

            if (!UInt32.TryParse(m_max, out m_int_max))
                m_int_max = 0;

            m_accesor_reader.Dispose();

            //---------------------------------------------------------------------
            m_seqno_sz = m_body_sz + m_prefix_sz;
            m_accesor_writer = m_mmap.CreateViewAccessor(current_seqno_body_pos, m_body_sz, MemoryMappedFileAccess.ReadWrite);
            m_accesor_reader = m_mmap.CreateViewAccessor(current_seqno_pos, m_seqno_sz, MemoryMappedFileAccess.Read);

        }

        internal void Load(int iStartPos, int iBlockSz)
        {
            m_start_pos = iStartPos;
            m_block_sz = iBlockSz;

            _internal_load();
        }

        public void InitSeqNo(string sPrefix, string sMin, string sMax)
        {
            if (IsUsed)
            {
                m_accesor_reader.Dispose();
                m_accesor_writer.Dispose();
            }

            byte bUsed = 0x01;
            int pos = 0;
            byte[] bPrefix = Encoding.Default.GetBytes(sPrefix);
            byte[] bMin = Encoding.Default.GetBytes(sMin);
            byte[] bMax = Encoding.Default.GetBytes(sMax);

            m_used = bUsed;
            m_accesor_writer = m_mmap.CreateViewAccessor(m_start_pos, m_block_sz);

            m_accesor_writer.Write(pos, bUsed);
            ++pos;
            m_accesor_writer.Write(pos, bPrefix.Length);
            pos += 4;
            m_accesor_writer.Write(pos, bMin.Length);
            pos += 4;

            m_accesor_writer.WriteArray<byte>(pos, bPrefix, 0, bPrefix.Length);
            pos += bPrefix.Length;
            m_accesor_writer.WriteArray<byte>(pos, bMin, 0, bMin.Length);
            pos += bMin.Length;
            m_accesor_writer.WriteArray<byte>(pos, bMin, 0, bMin.Length);
            pos += bMin.Length;
            m_accesor_writer.WriteArray<byte>(pos, bMax, 0, bMax.Length);
            pos += bMax.Length;

            m_accesor_writer.Dispose();

            _internal_load();
        }

        public bool GetNext(out string OutSeqNo)
        {
            bool bRC = false;
            byte[] btmp = new byte[m_body_sz ];
            byte[] bOut = new byte[m_seqno_sz];
            string seqno_body = "";

            OutSeqNo = "";
            lock (m_accesor_writer)
            {
                m_accesor_writer.ReadArray<byte>(0, btmp, 0, m_body_sz);

                seqno_body = Encoding.Default.GetString(btmp);

                if (GenerateNextEv != null)
                {
                    bRC = GenerateNextEv(ref seqno_body, m_body_sz, ref m_min, ref m_max);
                }
                else
                {
                    bRC = _GenerateNext(ref seqno_body, ref m_min, ref m_max);
                }

                if (bRC)
                {
                    m_accesor_writer.WriteArray<byte>(0, Encoding.Default.GetBytes(seqno_body), 0, m_body_sz);

                    m_accesor_reader.ReadArray<byte>(0, bOut, 0, m_seqno_sz);
                    OutSeqNo = Encoding.Default.GetString(bOut);
                }
            }
            return bRC;
        }

        public string GetCurrentSeqNo()
        {
            byte[] btmp = new byte[m_seqno_sz];

            m_accesor_reader.ReadArray<byte>(0, btmp, 0, m_seqno_sz);
            return Encoding.Default.GetString(btmp);
        }

        public bool SetSeqNo(string sSeqNo)
        {
            if (sSeqNo.Length != m_seqno_sz)
                return false;

            lock (m_accesor_writer)
            {
                byte[] tmp = Encoding.Default.GetBytes(sSeqNo);
                m_accesor_writer.WriteArray<byte>(0, 
                    tmp, 
                    m_prefix_sz, m_body_sz);
            }
            return true;
        }

    }

    /* ------------------------------------------
     * File 結構
     * used      byte       0x00 or 0x01
     * prefix_sz int32      4 bytes
     * body_sz   int32      4 bytes
     * seqno
     *  prefix     char(n)
     *  seqno_body char(n)    n bytes
     *  min_body   char(n)
     *  max_body   char(n)
     * end       byte       0x00
     * ------------------------------------------ */
    public class NuMMFSeqNo
    {
        private string m_path = "";
        private string m_mmap_name = "";
        private long m_mmap_size = 0;
        private int m_seqno_size = 0;
        private int m_seqno_count = 0;
        private int m_block_size = 0;   // seqno block
        private MemoryMappedFile m_mmap = null;
        private List<NuMMFSeqNoItem> m_lst_accesor = null;
        FileStream m_FStream = null;
        MemoryMappedFileSecurity m_mmap_security = new MemoryMappedFileSecurity();

        private string m_Max = "";
        private string m_Min = "";

        public NuMMFSeqNo(string sFile, string sMapName, int iSeqNoSz, int SeqNoCount)
        {
            m_path = sFile;
            m_mmap_name = sMapName;

            m_seqno_size = iSeqNoSz;
            m_seqno_count = SeqNoCount;
            m_block_size = 1 + 4 + 4 + (m_seqno_size * 3) + 1; //假設沒有prefix來計算大小
            m_mmap_size = m_block_size * SeqNoCount;
            
            m_lst_accesor = new List<NuMMFSeqNoItem>(SeqNoCount);

            if (iSeqNoSz >= 10)
            {
                m_Max = UInt32.MaxValue.ToString();
                m_Max = m_Max.PadLeft(iSeqNoSz, '0');
            }
            else
                m_Max = m_Max.PadLeft(iSeqNoSz, '9');

            m_Min = m_Min.PadLeft(iSeqNoSz, '0');
        }

        ~NuMMFSeqNo()
        {
            Close();
        }

        public bool Open()
        {
            #region initial mmap file variable
            long fsize = 0;
            if (File.Exists(m_path))
            {
                FileInfo finfo = new FileInfo(m_path);
                fsize = finfo.Length;
            }

            if (fsize < m_mmap_size)
                using (FileStream fstream = new FileStream(m_path, FileMode.OpenOrCreate))
                    fstream.SetLength(m_mmap_size);
            else
                m_mmap_size = fsize;
            #endregion

            #region create mmap file
            try
            {
                m_mmap = MemoryMappedFile.OpenExisting(m_mmap_name, MemoryMappedFileRights.ReadWrite);
            }
            catch (FileNotFoundException)
            {
                m_FStream = new FileStream(m_path, FileMode.OpenOrCreate,FileAccess.ReadWrite, FileShare.ReadWrite);
                //m_mmap = MemoryMappedFile.CreateFromFile(m_path, FileMode.OpenOrCreate, m_mmap_name, m_mmap_size); 
                m_mmap = MemoryMappedFile.CreateFromFile(
                    m_FStream, m_mmap_name, m_mmap_size, 
                    MemoryMappedFileAccess.ReadWrite,
                    m_mmap_security,
                    HandleInheritability.Inheritable, true);
            }

            if (m_mmap == null)
                return false;
            #endregion

            #region initial mmap
            int pos = 0;
            MemoryMappedViewAccessor accesor = null;
            NuMMFSeqNoItem seqno_item = null;
            accesor = m_mmap.CreateViewAccessor(0, m_mmap_size);

            while (pos < m_mmap_size)
            {
                seqno_item = new NuMMFSeqNoItem(ref m_mmap);
                if (accesor.ReadByte(pos) == 0x00) // used
                {
                    seqno_item.Init(pos, m_block_size, "", m_Min, m_Max);
                }
                else
                {
                    seqno_item.Load(pos, m_block_size);
                }
                // add to list
                lock (m_lst_accesor)
                    m_lst_accesor.Add(seqno_item);

                pos += m_block_size;
            }
            #endregion

            return true;
        }

        public void Close()
        {
            if (m_mmap != null)
            {
                m_mmap.Dispose();
                m_FStream.Close();
                m_FStream.Dispose();
                m_FStream = null;
                m_mmap = null;
            }
        }

        public NuMMFSeqNoItem GetSeqNoItem(string sPrefix)
        {
            lock (m_lst_accesor)
            {
                foreach (NuMMFSeqNoItem item in m_lst_accesor)
                {
                    if (item.IsUsed &&
                         item.Prefix == sPrefix)
                    {
                        return item;
                    }
                }
            }
            return null;
        }

        public NuMMFSeqNoItem GetSeqNoItem()
        {
            return GetSeqNoItem("");
        }

        public bool AddSeqNoItem(string sPrefix, string sBodyMin, string sBodyMax)
        {
            lock (m_lst_accesor)
            {
                foreach (NuMMFSeqNoItem item in m_lst_accesor)
                {
                    if (item.IsUsed == false)
                    {
                        item.InitSeqNo(sPrefix, sBodyMin, sBodyMax);
                        return true;
                    }
                    else if (item.IsUsed &&
                             item.Prefix == sPrefix)
                        return false;
                }
            }
            return false;
        }

        public bool AddSeqNoItem()
        {
            return AddSeqNoItem("", m_Min, m_Max);
        }
    }

    /*
    public class NuMMFSeqNo
    {
        private string m_path = "";
        private string m_mmap_name = "";
        private long m_mmap_size = 0;
        private int m_seqno_size = 0;
        private int m_seqno_count = 0;
        private MemoryMappedFile m_mmap = null;
        private List<MemoryMappedViewAccessor> m_lst_accesor = null;

        private readonly string m_Max;
        private readonly string m_Min;

        public NuMMFSeqNo(string sFile, string sMapName, int iSeqNoSz, int SeqNoCount)
        {
            m_path = sFile;
            m_mmap_name = sMapName;
            m_mmap_size = (iSeqNoSz + 1) * SeqNoCount;
            m_seqno_size = iSeqNoSz;
            m_seqno_count = SeqNoCount;
            m_lst_accesor = new List<MemoryMappedViewAccessor>(SeqNoCount);

            m_Max = "";
            m_Min = "";
            m_Max = m_Max.PadLeft(iSeqNoSz, '9');
            m_Min = m_Min.PadLeft(iSeqNoSz, '0');
        }

        ~NuMMFSeqNo()
        {
            Close();
        }

        public bool Open()
        {
            #region initial mmap file variable
            long fsize = 0;
            if (File.Exists(m_path))
            {
                FileInfo finfo = new FileInfo(m_path);
                fsize = finfo.Length;
            }

            if (fsize < m_mmap_size)
                using (FileStream fstream = new FileStream(m_path, FileMode.OpenOrCreate))
                    fstream.SetLength(m_mmap_size);
            else
                m_mmap_size = fsize;
            #endregion

            #region create mmap file
            try
            {
                m_mmap = MemoryMappedFile.OpenExisting(m_mmap_name, MemoryMappedFileRights.ReadWrite);
            }
            catch (FileNotFoundException)
            {
                m_mmap = MemoryMappedFile.CreateFromFile(m_path, FileMode.OpenOrCreate, m_mmap_name, m_mmap_size);
            }

            if (m_mmap == null)
                return false;
            #endregion

            #region create mmap file accessor 
            int pos = 0;
            MemoryMappedViewAccessor accesor = null;
            for (int i = 0; i < m_seqno_count; i++)
            {
                accesor = m_mmap.CreateViewAccessor(pos, m_seqno_size);
                m_lst_accesor.Add(accesor);
                pos += (m_seqno_size + 1);
            }
            #endregion

            return true;
        }

        public void Close()
        {
            foreach (MemoryMappedViewAccessor mmap_accesor in m_lst_accesor)
                mmap_accesor.Dispose();

            if (m_mmap != null)
                m_mmap.Dispose();
        }


        private bool _GenerateNext(ref string sSeqNoBody)
        {
            try
            {
                Int32 iNo = Int32.Parse(sSeqNoBody);
                iNo++;
                sSeqNoBody = iNo.ToString().PadLeft(m_seqno_size, '0');
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("_GenerateNext : " + ex.Message.ToString());
            }
        }

        public bool GetNext(int iIndex, out string OutSeqNo)
        {
            bool bRC = false;
            MemoryMappedViewAccessor mmap_accesor = m_lst_accesor[iIndex];
            byte[] btmp = new byte[m_seqno_size];
            lock (mmap_accesor)
            {
                mmap_accesor.ReadArray<byte>(0, btmp, 0, m_seqno_size);

                OutSeqNo = Encoding.Default.GetString(btmp);

                if (GenerateNextEv != null)
                {
                    bRC = GenerateNextEv(ref OutSeqNo, m_seqno_size);
                }
                else
                {
                    bRC = _GenerateNext(ref OutSeqNo);
                }

                mmap_accesor.WriteArray<byte>(0, Encoding.Default.GetBytes(OutSeqNo), 0, m_seqno_size);
                
                return bRC;
            }
        }

        public bool SetSeqNo(int iIndex, ref string sSeqNo)
        {
            MemoryMappedViewAccessor mmap_accesor = m_lst_accesor[iIndex];

            lock (mmap_accesor)
            {
                mmap_accesor.WriteArray<byte>(0, Encoding.Default.GetBytes(sSeqNo), 0, m_seqno_size);
            }
            return true;
        }

        public string GetCurrentSeqNo(int iIndex)
        {
            MemoryMappedViewAccessor mmap_accesor = m_lst_accesor[iIndex];
            byte[] btmp = new byte[m_seqno_size];

            lock (mmap_accesor)
            {
               mmap_accesor.ReadArray<byte>(0, btmp, 0, m_seqno_size);
               return Encoding.Default.GetString(btmp);
            }
        }

        #region --  public event  --
        public delegate bool dlgGenerateNext(ref string sNowSeqNoObj, int iSeqNoSz);
        public event dlgGenerateNext GenerateNextEv;
        #endregion

    }
     */
}
