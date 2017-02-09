using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BLPServer.Class
{
    /// <summary>
	/// 設定檔物件
	/// </summary>
	public class Config
    {
        #region Variable
        private IniFile m_ini;
        private string Path { get; set; }
        private string Name { get; set; }
        #endregion

        #region Property
        private IniFile ini
        {
            get
            {
                //if (m_ini == null) { _Reload(); }
                if (m_ini == null) { Reload(); }
                return m_ini;
            }
        }
        #endregion
        /// <summary>
        /// 設定檔物件
        /// </summary>
        /// <param name="path">檔案路徑</param>
        /// <param name="name">檔案名稱</param>
        public Config(string path, string name)
        {
            this.Path = path;
            this.Name = name;
        }

        #region Public
        /// <summary>
        /// 找尋設定區塊
        /// </summary>
        /// <param name="sectionName">區塊名稱</param>
        /// <returns></returns>
        public bool ContainSection(string sectionName)
        {
            return ini.ContainSection(sectionName);
        }
        /// <summary>
        /// 找尋設定區塊(用Like方式)
        /// </summary>
        /// <param name="sectionName"></param>
        /// <returns></returns>
        public bool ContainSectionLike(string sectionName)
        {
            Dictionary<string, Dictionary<string, string>>.KeyCollection sections = ini.GetSections();
            foreach (string section in sections)
            {
                if (section.Contains(sectionName))
                {
                    return true;
                }
            }
            return false;
        }
        public bool ContainSectionKey(string sectionName, string key)
        {
            return ini.ContainSection(sectionName) && ini[sectionName].ContainsKey(key);
        }
        public List<string> GetSectionNameLike(string sectionName)
        {
            List<string> re = new List<string>();
            Dictionary<string, Dictionary<string, string>>.KeyCollection sections = ini.GetSections();
            foreach (string section in sections)
            {
                if (section.Contains(sectionName))
                {
                    re.Add(section);
                }
            }
            return re.Count == 0 ? null : re;
        }

        public T GetSetting<T>(string sectionName, string key)
        {
            return GetSetting(sectionName, key, default(T));
        }
        public T GetSetting<T>(string sectionName, string key, T defaultValue)
        {
            //載入有點慢 先拿掉
            //_Reload();
            if (ContainSectionKey(sectionName, key))
            {
                try
                {
                    return (T)Convert.ChangeType(ini[sectionName, key], typeof(T));
                }
                catch (Exception)
                {
                    //Utility.Logger.WrtErr("Default Value Cast Exception: " + ex.Message);
                    return defaultValue;
                }
            }
            return defaultValue;
        }
        public void SetSetting(string sectionName, string key, object value)
        {
            ini.ModifyKeyVal(sectionName, key, value == null ? string.Empty : value.ToString());
            //ini.SaveWithoutBak();
#if Stream
            ini.SaveToStream();
#else
            ini.SaveWithoutBak();

#endif
        }
        public void DeleteSection(string sectionName)
        {
            //_Reload();
            Reload();
            m_ini.DeleteSection(sectionName);
#if Stream
			m_ini.SaveToStream();
#else
            m_ini.SaveWithoutBak();
#endif
        }

        #endregion

        #region Private
        //private  void _Reload()
        public void Reload()
        {
            m_ini = new IniFile();
            if (!File.Exists(Path + Name)) { return; }
            m_ini.Open(string.Format("{0}{1}", Path, Name));
#if Stream
			m_ini.LoadFromStream();
#else
            m_ini.Load();
#endif
        }
        #endregion
    }

    internal class IniFile
    {
        #region --  private items  --
        Dictionary<string, Dictionary<string, string>> m_ini;
        Dictionary<string, string> m_section;
        private string m_iniPath = "";

        private const char m_mark_sign = ';';
        private const char m_open_bracket = '[';
        private const char m_close_bracket = ']';
        private const char m_equal_sign = '=';
        private Encoding m_encoding;
        #endregion

        #region --  construct / destruct  --
        public IniFile()
        {
            m_ini = new Dictionary<string, Dictionary<string, string>>();
            m_encoding = Encoding.GetEncoding("Big5");
        }

        public IniFile(Encoding encoding)
        {
            m_ini = new Dictionary<string, Dictionary<string, string>>();
            m_encoding = encoding;
        }

        ~IniFile() { }
        #endregion

        #region --  property  --
        public Dictionary<string, string> this[string Key]
        {
            get { return m_ini.ContainsKey(Key) ? m_ini[Key] : null; }
        }
        public string this[string Key, string subKey]
        {
            get
            {
                if (m_ini.ContainsKey(Key))
                {
                    if (m_ini[Key].ContainsKey(subKey))
                        return m_ini[Key][subKey];
                }
                return "";
            }
        }
        public int SectionCount { get { return m_ini.Count; } }
        #endregion

        #region --  private function  --
        #endregion

        #region --  public method  --
        #region open ini file
        public bool Open(string sPath)
        {
            this.Close();

            if (!File.Exists(sPath))
            {
                FileStream fs = new FileStream(sPath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                fs.Close();
            }

            m_iniPath = sPath;

            return true;
        }
        #endregion

        #region close ini file
        public void Close()
        {
            m_iniPath = "";
        }
        #endregion

        #region load ini file
        public bool Load()
        {
            string sData = "";
            StreamReader sr_fd;
            string section = "";
            string sKey = "";
            string sVal = "";
            int idx = 0;

            if (m_iniPath == "")
                return false;

            sr_fd = new StreamReader(m_iniPath, m_encoding);
            while (sr_fd.Peek() != -1)
            {
                sData = sr_fd.ReadLine();
                if (sData.Length == 0)
                    continue;

                sData.Trim();

                switch (sData[0])
                {
                    case m_mark_sign:
                        continue;
                    case m_open_bracket:
                        idx = sData.IndexOf(m_close_bracket);
                        if (idx > 0) // change section
                        {
                            section = sData.Substring(1, idx - 1);
                            if (m_ini.ContainsKey(section))
                                continue;
                            m_section = new Dictionary<string, string>();
                            m_ini.Add(section, m_section);
                        }
                        continue;
                    default:
                        if (sData.Length == 0)
                            continue;

                        if (m_section == null)
                            continue;

                        idx = sData.IndexOf(m_equal_sign);

                        sKey = sData.Substring(0, idx).Trim();
                        sVal = sData.Substring(idx + 1).Trim();

                        if (!m_section.ContainsKey(sKey))
                            m_section.Add(sKey, sVal);

                        continue;
                }
            }

            sr_fd.Close();
            return true;
        }
        #endregion

        #region Clear ini file
        public void Clear()
        {
            lock (m_ini)
            {
                foreach (string section in m_ini.Keys)
                    m_ini[section].Clear();
            }
            m_ini.Clear();
        }
        #endregion

        #region save ini file
        /// <summary>
        /// 將 ini 存檔成純文字檔
        /// </summary>
        /// <param name="sPath">檔案完整路徑與名稱</param>
        /// <returns></returns>
        public bool SaveToTextFile(string sPath)
        {
            StreamWriter sw_fd;
            if (sPath == "") return false;

            sw_fd = new StreamWriter(sPath, false, m_encoding);
            foreach (string section in m_ini.Keys)
            {
                sw_fd.WriteLine(String.Format("[{0}]", section));
                foreach (string key in m_ini[section].Keys)
                {
                    sw_fd.WriteLine(String.Format("{0}={1}", key, m_ini[section][key]));
                }

                sw_fd.WriteLine("");
            }

            sw_fd.Close();
            return true;
        }

        /// <summary>
        /// 存檔, 並備份 bak 檔案
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            string sFileBak = "";
            if (m_iniPath == "") return false;

            // backup file 
            sFileBak = String.Format("{0}.bak", m_iniPath);
            File.Copy(m_iniPath, sFileBak, true);

            return SaveWithoutBak();
        }
        /// <summary>
        /// 存檔而不備份
        /// </summary>
        /// <returns></returns>
        public bool SaveWithoutBak()
        {
            StreamWriter sw_fd;
            if (m_iniPath == "") return false;

            sw_fd = new StreamWriter(m_iniPath, false, m_encoding);
            foreach (string section in m_ini.Keys)
            {
                sw_fd.WriteLine(String.Format("[{0}]", section));
                foreach (string key in m_ini[section].Keys)
                {
                    sw_fd.WriteLine(String.Format("{0}={1}", key, m_ini[section][key]));
                }

                sw_fd.WriteLine("");
            }

            sw_fd.Close();
            return true;
        }
        #endregion

        #region add section, key, value
        /// <summary>
        /// add section , key, value.  if the key exists , will return false
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public bool AddKeyVal(string section, string key, string val)
        {
            if (m_ini.ContainsKey(section))
            {
                if (m_ini[section].ContainsKey(key))   // key already exists
                    return false;
                //m_ini[section][key] = val;
                else
                    m_ini[section].Add(key, val);
            }
            else
            {
                m_ini.Add(section, new Dictionary<string, string>());
                m_ini[section].Add(key, val);
            }
            return true;
        }
        #endregion

        #region modify section, key, value
        /// <summary>
        /// modify key and value, if the key not exists , will be add it.
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public bool ModifyKeyVal(string section, string key, string val)
        {
            if (m_ini.ContainsKey(section))
            {
                if (m_ini[section].ContainsKey(key))   // key already exists
                    m_ini[section][key] = val;
                else
                    m_ini[section].Add(key, val);
            }
            else
            {
                m_ini.Add(section, new Dictionary<string, string>());
                m_ini[section].Add(key, val);
            }
            return true;
        }
        #endregion

        #region modify section
        public bool ModifySection(string origSection, string newSection)
        {
            Dictionary<string, string> section;
            if (!m_ini.ContainsKey(origSection))
                return false;
            section = m_ini[origSection];
            m_ini.Add(newSection, section);
            m_ini.Remove(origSection);
            return true;
        }
        #endregion

        #region delete section
        public bool DeleteSection(string section)
        {
            if (!m_ini.ContainsKey(section))
                return false;
            m_ini.Remove(section);
            return true;
        }
        #endregion

        #region GetSections
        public Dictionary<string, Dictionary<string, string>>.KeyCollection GetSections()
        {
            return m_ini.Keys;
        }
        public bool ContainSection(string szSection)
        {
            return m_ini.ContainsKey(szSection);
        }
        #endregion

        public Dictionary<string, string>.KeyCollection GetKeysBySection(string szSection)
        {
            if (m_ini.ContainsKey(szSection))
            {
                return m_ini[szSection].Keys;
            }
            else
            {
                return default(Dictionary<string, string>.KeyCollection);
            }
        }
        #endregion
    }
}
