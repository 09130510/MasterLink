using ChannelMonitor.Properties;
using IniParser;
using IniParser.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace ChannelMonitor
{
    public partial class frmMain : Form
    {
        public static string RIGHT;
        public static string LEFT;
        public static string LAYOUT = @"./Layout.xml";

        #region Variable
        private int m_ContentCount = 0;
        private List<string> m_LoadedMonitor = new List<string>();
        #endregion

        #region Property
        public static IniData INI { get; private set; }
        #endregion

        public frmMain()
        {
            InitializeComponent();

            if (INI == null)
            {
                var parser = new FileIniDataParser();
                INI = parser.ReadFile("Config.ini");
            }
            Text = VersionInfo(this);
            tsInterval.Text = frmMonitor.DEFAULTINTERVAL.ToString();                         

            VS2013BlueTheme theme = new VS2013BlueTheme();
            theme.Skin.DockPaneStripSkin.TextFont = new Font("Verdana", 7);
            theme.Skin.DockPaneStripSkin.DocumentGradient.ActiveTabGradient.StartColor = Color.SkyBlue;
            theme.Skin.DockPaneStripSkin.DocumentGradient.ActiveTabGradient.EndColor = Color.DodgerBlue;
            theme.Skin.DockPaneStripSkin.DocumentGradient.ActiveTabGradient.TextColor = Color.White;
            theme.Skin.DockPaneStripSkin.DocumentGradient.ActiveTabGradient.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
            dockPanel1.Theme = theme;

            LoadLayout();            
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            if (!INI.Sections.ContainsSection("SYSTEM"))
            {
                INI["SYSTEM"]["LOCATION"] = $"{Location.X},{Location.Y},{Width},{Height}";
                var parser = new FileIniDataParser();
                parser.WriteFile("Config.ini", INI);
            }
            string[] location = INI["SYSTEM"]["LOCATION"].Split(',');
            int[] re = Array.ConvertAll(location, (s) =>
            {
                int i = 0;
                int.TryParse(s, out i);
                return i;
            });
            SetBounds(re[0], re[1], re[2], re[3]);
        }
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            dockPanel1.SaveAsXml(LAYOUT);
            for (int i = dockPanel1.Contents.Count - 1; i >= 0; i--)
            {
                ((Form)dockPanel1.Contents[i]).Close();
            }
            INI["SYSTEM"]["LOCATION"] = $"{Location.X},{Location.Y},{Width},{Height}";
            var parser = new FileIniDataParser();
            parser.WriteFile("Config.ini", INI);
        }
        private void tsAdd_Click(object sender, EventArgs e)
        {
            tsSeparator1.Visible = tsSeparator2.Visible = tsIPLabel.Visible = tsIP.Visible = tsPortLabel.Visible = tsPort.Visible = tsIntervalLabel.Visible = tsInterval.Visible = tsAdd.Visible = false;
            tsDisplayAdd.Text = nameof(RIGHT);

            int port, interval;
            int.TryParse(tsPort.Text, out port);
            int.TryParse(tsInterval.Text, out interval);
            frmMonitor mon = new frmMonitor(tsIP.Text, port, interval);
            mon.Show(dockPanel1);
            mon.Start();

            dockPanel1.SaveAsXml(LAYOUT);            
        }
        private void tsDisplayAdd_Click(object sender, EventArgs e)
        {
            if (tsDisplayAdd.Text == nameof(RIGHT))
            {
                tsSeparator1.Visible = tsSeparator2.Visible = tsIPLabel.Visible = tsIP.Visible = tsPortLabel.Visible = tsPort.Visible = tsIntervalLabel.Visible = tsInterval.Visible = tsAdd.Visible = true;
                tsDisplayAdd.Image = Resources._1477983381_BT_arrow_left;
                tsDisplayAdd.Text = nameof(LEFT);
            }
            else if (tsDisplayAdd.Text == nameof(LEFT))
            {
                tsSeparator1.Visible = tsSeparator2.Visible = tsIPLabel.Visible = tsIP.Visible = tsPortLabel.Visible = tsPort.Visible = tsIntervalLabel.Visible = tsInterval.Visible = tsAdd.Visible = false;
                tsDisplayAdd.Image = Resources._1477983371_BT_arrow_right;
                tsDisplayAdd.Text = nameof(RIGHT);
            }
        }

        private string VersionInfo(Form form)
        {
            #region Version Info
            object[] attribute = form.GetType().Assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
            AssemblyDescriptionAttribute desc = (AssemblyDescriptionAttribute)(attribute[0]);
            attribute = form.GetType().Assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
            AssemblyTitleAttribute title = (AssemblyTitleAttribute)(attribute[0]);
            return string.Format("[{0} - {3}]  {1}  V{2}", title.Title, desc.Description, Application.ProductVersion, Process.GetCurrentProcess().Id);
            #endregion
        }
        private void LoadLayout()
        {
            if (!File.Exists(LAYOUT))
            {
                foreach (var sec in INI.Sections)
                {
                    if (sec.SectionName == "SYSTEM") { continue; }
                    frmMonitor mon = new frmMonitor(sec);
                    mon.Show(dockPanel1);
                    mon.Start();
                }
            }
            else
            {
                DeserializeDockContent ddContent = new DeserializeDockContent(GetContentFromPersistString);
                dockPanel1.LoadFromXml(LAYOUT, ddContent);

                for (int i = INI.Sections.Count-1; i >=0; i--)
                {
                    string section = INI.Sections.ElementAt(i).SectionName;
                    if (section == "SYSTEM") { continue; }
                    //if (!m_LoadedMonitor.Contains(section))
                    //{
                    //    INI.Sections.RemoveSection(section);                        
                    //}
                }
                var parser = new FileIniDataParser();
                parser.WriteFile("Config.ini", INI);
            }
        }
        private IDockContent GetContentFromPersistString(string persistString)
        {
            if (INI.Sections.Count > 1 &&persistString == typeof(frmMonitor).ToString() )
            {
                var section = INI.Sections.ElementAt(m_ContentCount+1);
                frmMonitor monitor = new frmMonitor(section);                
                monitor.Start();
                m_ContentCount++;
                m_LoadedMonitor.Add(section.SectionName);
                return monitor;
            }
            return null;
        }
    }
}
