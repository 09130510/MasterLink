using Notifier.Class;
using Notifier.Utility;
using PriceLib.Redis;
using SourceCell;
using SourceGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Notifier
{
    public partial class frmSetting : Form
    {
        #region Variable        
        //private object m_CancelObj = null;
        private static Dictionary<string, List<StopNotifyInfo>> m_StopNotifies = new Dictionary<string, List<StopNotifyInfo>>();
        private static Dictionary<string, List<InfoNotifyInfo>> m_InfoNotifies = new Dictionary<string, List<InfoNotifyInfo>>();
        //private Dictionary<string, Monitor> m_Monitors = new Dictionary<string, Monitor>();
        #endregion

        #region Property
        public object CancelObj { get; set; }
        public static Dictionary<string, List<StopNotifyInfo>> StopNotifies
        {
            get
            {
                return m_StopNotifies;
            }
        }
        public static Dictionary<string, List<InfoNotifyInfo>> InfoNotifies
        {
            get
            {
                return m_InfoNotifies;
            }
        }
        #endregion

        public frmSetting()
        {
            InitializeComponent();
            Text = Util.VersionInfo();

            _InitStopGrid();
            _InitInfoGrid();
            _InitStopNotifies();
            _InitInfoNotifies();
            cboServer.Items.AddRange(Util.Servers.Values.Select(e => e.Name).ToArray());

            cboServer1.Items.AddRange(Util.Servers.Values.Select(e => e.Name).ToArray());
            cboServer2.Items.AddRange(Util.Servers.Values.Select(e => e.Name).ToArray());
            cboStyle2.Items.AddRange(Enum.GetNames(typeof(Style)).ToArray());

            //((Control)tpInfoNotify).Enabled = false;
            //((Control)tabControl1.TabPages[1]).Enabled = false;
        }
        private void frmStopNotify_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (CancelObj == null)
            {
                e.Cancel = true;
                Hide();
                CancelObj = null;
            }
        }

        private void tsAdd_CheckedChanged(object sender, EventArgs e)
        {
            //StopNotifyInfo info = StopNotifyInfo.Empty();
            //_AddRow(info);
            if (tsAdd.Checked) { tsModify.Checked = false; }
            splitContainer1.Panel1Collapsed = !tsAdd.Checked;
            btnAdd.Visible = tsAdd.Checked;
            //m_EditingAllowInfo = null;
            cboServer.SelectedItem = null;
            txtChannel.Text = txtItem.Text = txtSeconds.Text = string.Empty;
        }
        private void tsDelete_Click(object sender, EventArgs e)
        {
            if (gdStop.Selection.IsEmpty()) { return; }
            RangeRegion range = gdStop.Selection.GetSelectionRegion();
            foreach (var r in range)
            {
                for (int i = r.End.Row; i >= r.Start.Row; i--)
                {
                    if (gdStop[i, 0].Tag == null) { continue; }
                    StopNotifyInfo info = (StopNotifyInfo)gdStop[i, 0].Tag;
                    StopNotifies[info.Server].Remove(info);
                    Util.Monitors[info.Server].RemoveStopNotify(info);
                    for (int col = 0; col < 4; col++)
                    {
                        gdStop[i, col].UnBindToGrid();
                    }
                    gdStop.Rows.Remove(i);
                }
            }
            tsDelete.Enabled = !gdStop.Selection.IsEmpty();
        }
        private void tsExit1_Click(object sender, EventArgs e)
        {
            CancelObj = sender;
            Util.Main.Close();
        }
        private void grid1_Click(object sender, EventArgs e)
        {
            tsDelete.Enabled = !gdStop.Selection.IsEmpty();
        }
        private void grid1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                tsDelete_Click(tsDelete, EventArgs.Empty);
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            ServerInfo server = Util.Servers.Values.First(entry => entry.Name == cboServer.Text);
            StopNotifyInfo ainfo = StopNotifyInfo.Create(server, txtChannel.Text, txtItem.Text, txtSeconds.Text);
            if (ainfo == null) { return; }
            _AddStopNotify(ainfo);
        }

        #region Private
        private void unbind(Grid grid)
        {
            for (int i = 0; i < grid.RowsCount; i++)
            {
                for (int j = 0; j < grid.ColumnsCount; j++)
                {
                    grid[i, j].UnBindToGrid();
                }
            }
            grid.Rows.Clear();
        }
        private void _InitStopGrid()
        {
            unbind(gdStop);
            for (int i = 0; i < gdStop.RowsCount; i++)
            {
                for (int j = 0; j < gdStop.ColumnsCount; j++)
                {
                    gdStop[i, j].UnBindToGrid();
                }
            }
            gdStop.Rows.Clear();

            gdStop.Redim(1, 5);
            gdStop[0, 0] = new CHeaderCell() { Caption = "Server", ColorStyle = DevAge.Drawing.BackgroundColorStyle.Solid, FontName = CellBase.FontName.Verdana, Sortable = true }.Field;
            gdStop[0, 1] = new CHeaderCell() { Caption = "Channel", ColorStyle = DevAge.Drawing.BackgroundColorStyle.Solid, FontName = CellBase.FontName.Verdana, Sortable = true }.Field;
            gdStop[0, 2] = new CHeaderCell() { Caption = "Item", ColorStyle = DevAge.Drawing.BackgroundColorStyle.Solid, FontName = CellBase.FontName.Verdana, Sortable = true }.Field;
            gdStop[0, 3] = new CHeaderCell() { Caption = "Sec", ColorStyle = DevAge.Drawing.BackgroundColorStyle.Solid, FontName = CellBase.FontName.Verdana, Sortable = true }.Field;            
            gdStop.AutoSizeCells();
        }
        private void _InitInfoGrid()
        {
            unbind(gdInfo);

            gdInfo.Redim(1, 3);
            gdInfo[0, 0] = new CHeaderCell() { Caption = "Server", ColorStyle = DevAge.Drawing.BackgroundColorStyle.Solid, FontName = CellBase.FontName.Verdana, Sortable = true }.Field;
            gdInfo[0, 1] = new CHeaderCell() { Caption = "Channel", ColorStyle = DevAge.Drawing.BackgroundColorStyle.Solid, FontName = CellBase.FontName.Verdana, Sortable = true }.Field;
            gdInfo[0, 2] = new CHeaderCell() { Caption = "Item", ColorStyle = DevAge.Drawing.BackgroundColorStyle.Solid, FontName = CellBase.FontName.Verdana, Sortable = true }.Field;
            gdInfo.AutoSizeCells();
        }
        private void _AddRow(StopNotifyInfo info)
        {
            gdStop.Redim(gdStop.RowsCount + 1, gdStop.ColumnsCount);
            gdStop[gdStop.RowsCount - 1, 0] = info.cServer.Field;
            gdStop[gdStop.RowsCount - 1, 1] = info.cChannel.Field;
            gdStop[gdStop.RowsCount - 1, 2] = info.cItem.Field;
            gdStop[gdStop.RowsCount - 1, 3] = info.cInterval.Field;            
            gdStop.AutoSizeCells();
        }
        private void _AddRow(InfoNotifyInfo info)
        {
            gdInfo.Redim(gdInfo.RowsCount + 1, gdInfo.ColumnsCount);
            gdInfo[gdInfo.RowsCount - 1, 0] = info.cServer.Field;
            gdInfo[gdInfo.RowsCount - 1, 1] = info.cChannel.Field;
            gdInfo[gdInfo.RowsCount - 1, 2] = info.cItem.Field;
            gdInfo.AutoSizeCells();
        }

        private void _InitStopNotifies()
        {
            var stops = Util.INI["SETTING"]["STOPNOTIFY"].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in stops)
            {
                var info = item.Split('|');

                var sn = StopNotifyInfo.Create(info);
                if (sn == null) { continue; }
                _AddStopNotify(sn);
            }
        }
        private void _InitInfoNotifies()
        {
            var infos = Util.INI["SETTING"]["INFONOTIFY"].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in infos)
            {
                var info = item.Split('|');

                var inn = InfoNotifyInfo.Create(info);
                if (inn == null) { continue; }
                _AddInfoNotify(inn);
            }
        }
        private void _AddStopNotify(StopNotifyInfo info)
        {
            if (!m_StopNotifies.ContainsKey(info.Server))
            {
                m_StopNotifies.Add(info.Server, new List<StopNotifyInfo>());
                m_StopNotifies[info.Server].Add(info);
            }
            else
            {
                m_StopNotifies[info.Server].Add(info);
            }
            _AddRow(info);

            if (!Util.Monitors.ContainsKey(info.Server))
            {
                var server = Monitor.Create(info.Server);
                if (server == null) { return; }
                Util.Monitors.Add(info.Server, server);
                Util.Monitors[info.Server].MonitorStart();
            }
        }
        private void _AddInfoNotify(InfoNotifyInfo info)
        {
            if (!m_InfoNotifies.ContainsKey(info.Server))
            {
                m_InfoNotifies.Add(info.Server, new List<InfoNotifyInfo>());
                m_InfoNotifies[info.Server].Add(info);
            }
            else
            {
                m_InfoNotifies[info.Server].Add(info);
            }
            _AddRow(info);

            if (!Util.Monitors.ContainsKey(info.Server))
            {
                var server = Monitor.Create(info.Server);
                if (server == null) { return; }
                Util.Monitors.Add(info.Server, server);
                Util.Monitors[info.Server].MonitorStart();
            }
        }
        #endregion

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            e.Cancel = !e.TabPage.Enabled;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            var server = Util.Servers.Values.First(entry => entry.Name == cboServer1.Text);             
            Monitor monitor = Monitor.Create(server.Key);
            monitor.MonitorStart();
            monitor.Publish($"{txtChannel1.Text}.{txtItem1.Text}", txtValue.Text);
            monitor.MonitorStop();
        }

        private void cboServer1_Validated(object sender, EventArgs e)
        {
            btnSend.Enabled = !string.IsNullOrEmpty(cboServer1.Text) &&
                              !string.IsNullOrEmpty(txtChannel1.Text) &&
                              !string.IsNullOrEmpty(txtItem1.Text);
        }

        private void btnAdd1_Click(object sender, EventArgs e)
        {
            ServerInfo server = Util.Servers.Values.First(entry => entry.Name == cboServer2.Text);
            InfoNotifyInfo info = InfoNotifyInfo.Create(server, txtChannel2.Text, txtItem2.Text, cboStyle2.Text);
            if (info == null) { return; }
            _AddInfoNotify(info);
        }

        private void cboServer2_Validated(object sender, EventArgs e)
        {
            btnAdd2.Enabled = !string.IsNullOrEmpty(cboServer2.Text) &&
                  !string.IsNullOrEmpty(txtChannel2.Text);
        }

        private void gdInfo_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                if (gdInfo.Selection.IsEmpty()) { return; }
                RangeRegion range = gdInfo.Selection.GetSelectionRegion();
                foreach (var r in range)
                {
                    for (int i = r.End.Row; i >= r.Start.Row; i--)
                    {
                        if (gdInfo[i, 0].Tag == null) { continue; }
                        InfoNotifyInfo info = (InfoNotifyInfo)gdInfo[i, 0].Tag;
                        InfoNotifies[info.Server].Remove(info);
                        Util.Monitors[info.Server].RemoveInfoNotify(info);
                        for (int col = 0; col < 3; col++)
                        {
                            gdInfo[i, col].UnBindToGrid();
                        }
                        gdInfo.Rows.Remove(i);
                    }
                }
            }
        }

        private void cboStyle2_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index >= 0)
            {
                ComboBox combo = sender as ComboBox;
                e.Graphics.DrawString(combo.Items[e.Index].ToString(), e.Font, new SolidBrush(Color.FromName(combo.Items[e.Index].ToString())), new Point(e.Bounds.X, e.Bounds.Y));
            }
        }
    }
}