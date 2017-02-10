using IniParser;
using IniParser.Model;
using Notifier.Class;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Notifier.Utility
{
    public static class Util
    {
        public static frmMain Main { get; private set; }
        public static frmSetting StopNotify { get; private set; }
        public static IniData INI { get; private set; }
        public static Dictionary<string, ServerInfo> Servers { get; private set; }      
        public static Dictionary<string, Monitor> Monitors { get; private set; }  

        public static void Init(frmMain  main)
        {
            if (INI == null)
            {
                var parser = new FileIniDataParser();
                INI = parser.ReadFile("Config.ini");
            }

            Servers = new Dictionary<string, ServerInfo>();
            var items = INI["SETTING"]["SERVER"].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in items)
            {
                var server = ServerInfo.Create(item.Split('|'));
                if (server == null || Servers.ContainsKey(server.Key)) { continue; }
                Servers.Add(server.Key, server);
            }
            Monitors = new Dictionary<string, Monitor>();

            Main = main;
            StopNotify = new frmSetting();
        }

        public static string VersionInfo()
        {
            Assembly assembly = typeof(frmMain).Assembly;
            object[] attribute = assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
            AssemblyDescriptionAttribute desc = (AssemblyDescriptionAttribute)(attribute[0]);
            attribute = assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
            AssemblyTitleAttribute title = (AssemblyTitleAttribute)(attribute[0]);
            return string.Format("[{0} - {3}]  {1}  V{2}", title.Title, desc.Description, Application.ProductVersion, Process.GetCurrentProcess().Id);
        }
        public static void WriteConfig()
        {
            var parser = new FileIniDataParser();
            parser.WriteFile("Config.ini", INI);
        }
    }
}
