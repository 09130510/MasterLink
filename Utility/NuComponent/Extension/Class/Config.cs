using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Util.Extension.Class
{
	/// <summary>
	/// 設定檔物件
	/// </summary>
	public class Config
	{
		#region Variable
		private IniFile m_ini;
		private string Path { get;  set; }
		private string Name { get; set; }
		#endregion

		#region Property
		private  IniFile ini
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
		public  bool ContainSection(string sectionName)
		{
			return ini.ContainSection(sectionName);
		}
		/// <summary>
		/// 找尋設定區塊(用Like方式)
		/// </summary>
		/// <param name="sectionName"></param>
		/// <returns></returns>
		public  bool ContainSectionLike(string sectionName)
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
		public  bool ContainSectionKey(string sectionName, string key)
		{
			return ini.ContainSection(sectionName) && ini[sectionName].ContainsKey(key);
		}
		public  List<string> GetSectionNameLike(string sectionName)
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

		public  T GetSetting<T>(string sectionName, string key)
		{
			return GetSetting(sectionName, key, default(T));
		}
		public  T GetSetting<T>(string sectionName, string key, T defaultValue)
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
		public  void SetSetting(string sectionName, string key, object value)
		{            
            ini.ModifyKeyVal(sectionName, key, value == null ? string.Empty : value.ToString());
            //ini.SaveWithoutBak();
#if Stream
            ini.SaveToStream();
#else
            ini.SaveWithoutBak();

#endif
		}
		public  void DeleteSection(string sectionName)
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
        public   void Reload()
		{
			m_ini = new IniFile();
			if (!File.Exists(Path + Name)) { return; }
			m_ini.Open(string.Format("{0}{1}",Path, Name));
#if Stream
			m_ini.LoadFromStream();
#else
			m_ini.Load();
#endif
		}
		#endregion
	}
}
