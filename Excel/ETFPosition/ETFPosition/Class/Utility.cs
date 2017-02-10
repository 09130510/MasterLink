using IniParser;
using IniParser.Model;
using OfficeOpenXml;
using PriceLib.Redis;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ETFPosition.Class
{
    public static class Util
    {
        private static string m_ReportPath = "./Report/";
        private static FileInfo m_BaseExlFile;

        public static IniData INI { get; set; }
        public static ExcelPackage PKG { get; set; }
        public static ExcelWorkbook Book { get { return PKG.Workbook; } }
        public static ExcelWorksheet Position { get { return Book.Worksheets["Position"]; } }
        public static ExcelWorksheet FxRate { get { return Book.Worksheets["FxRate"]; } }
        public static ExcelWorksheet ContractValue { get { return Book.Worksheets["ContractValue"]; } }
        public static SQLTool SQL { get; set; }
        public static RedisPublishLib FX { get; set; }

        public static void Init()
        {
            if (INI == null)
            {
                var parser = new FileIniDataParser();
                INI = parser.ReadFile("Config.ini");
            }
            if (!Directory.Exists(m_ReportPath))
            {
                Directory.CreateDirectory(m_ReportPath);
            }
            m_BaseExlFile = new FileInfo(@"Position.xlsx");
            PKG = new ExcelPackage(m_BaseExlFile);
            SQL = new SQLTool(INI["SYSTEM"]["SQL"], "ETFForBrian");
            string[] fxip = INI["SYSTEM"]["FXRATEIP"].Split(':');
            FX = new RedisPublishLib(fxip[0], int.Parse(fxip[1]));
        }
        public static string VersionInfo(Form form)
        {
            object[] attribute = form.GetType().Assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
            AssemblyDescriptionAttribute desc = (AssemblyDescriptionAttribute)(attribute[0]);
            attribute = form.GetType().Assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
            AssemblyTitleAttribute title = (AssemblyTitleAttribute)(attribute[0]);
            string ostype = Environment.Is64BitProcess ? "x64" : "x86";
#if DEBUG
            return $"[{title.Title} - {Process.GetCurrentProcess().Id}] { desc.Description} ({ostype}-D)  V{Application.ProductVersion}";
#else
            return $"[{title.Title} - {Process.GetCurrentProcess().Id}] {desc.Description} ({ostype}-R)  V{Application.ProductVersion}";
#endif
        }
        public static void SaveAs(string filename)
        {
            FileInfo fi = new FileInfo(filename);
            PKG.SaveAs(fi);
        }
        public static void WriteConfig()
        {
            var parser = new FileIniDataParser();
            parser.WriteFile("Config.ini", INI);
        }
        public static void InvokeIfRequired(this Control control, MethodInvoker action)
        {
            if (control.InvokeRequired)//在非當前執行緒內 使用委派
            {
                control.Invoke(action);
            }
            else
            {
                action();
            }
        }
        public static void InvokeIfRequired(this ToolStripItem control, MethodInvoker action)
        {
            if (control == null) { return; }
            if (control.Owner != null && control.Owner.InvokeRequired)//在非當前執行緒內 使用委派
            {
                try
                {
                    control.Owner.Invoke(action);
                }
                catch (Exception) { }
            }
            else
            {
                action();
            }
        }
    }
}