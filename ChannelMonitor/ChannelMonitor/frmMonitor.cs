using ChannelMonitor.Class;
using IniParser;
using IniParser.Model;
using PriceLib.Redis;
using SourceCell;
using SourceGrid;
using SourceGrid.Selection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;


namespace ChannelMonitor
{
    public partial class frmMonitor : DockContent
    {
        public const int DEFAULTINTERVAL = 120;

        #region Private
        private string m_Identity;
        private frmDeny m_DenyForm;
        private frmAllow m_AllowForm;
        private RedisPublishLib m_Publish;
        private ConcurrentDictionary<string, ChannelInfo> m_Channels = new ConcurrentDictionary<string, ChannelInfo>();
        //private Dictionary<string, List<string>> m_Deny;
        //private Dictionary<string, List<string>> m_Allow;
        #endregion

        #region Property
        public string IP { get; private set; }
        public string Port { get; private set; }
        public string Interval { get; private set; }
        public Dictionary<string, List<string>> Deny { get; private set; } = new Dictionary<string, List<string>>();
        public Dictionary<string, List<string>> Allow { get; private set; } = new Dictionary<string, List<string>>();
        #endregion

        #region Cell
        private TextCell c_Summary = new TextCell() { CellType = TextCell.TextType.Int, Value = 0, TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter, ColumnSpan = 3, DefaultBackColor = Color.LightGray, HasBorder = true };
        private TextCell c_ExceedSummary = new TextCell() { CellType = TextCell.TextType.Int, Value = 0, TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter, DefaultBackColor = Color.LightGray, HasBorder = true };
        private CHeaderCell c_SummaryHead = new CHeaderCell() { Caption = "Summary" };
        private CHeaderCell c_SECsHead = new CHeaderCell() { Caption = "SECs", Sortable = true };
        private CHeaderCell c_NoHead = new CHeaderCell() { Caption = "", Sortable = true };
        private CHeaderCell c_ChannelHead = new CHeaderCell() { Caption = "Channel", Sortable = true };
        private CHeaderCell c_ItemHead = new CHeaderCell() { Caption = "Item", Sortable = true };
        private CHeaderCell c_ValueHead = new CHeaderCell() { Caption = "Value" };
        #endregion

        private frmMonitor()//(string deny)
        {
            InitializeComponent();
            //m_Deny = new Dictionary<string, List<string>>();
            //m_Allow = new Dictionary<string, List<string>>();
            //if (!string.IsNullOrEmpty(deny))
            //{
            //    string[] channels = deny.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            //    foreach (var channel in channels)
            //    {
            //        var temp = channel.Split('|');
            //        string channelName = temp[0];
            //        string[] items = temp[1].Split(',');

            //        if (!m_Deny.ContainsKey(channelName))
            //        {
            //            m_Deny.Add(channelName, new List<string>());
            //        }
            //        m_Deny[channelName] = items.ToList();
            //    }
            //    var sum = m_Deny.Sum(e => e.Value.Count);
            //    tsDeny.Text = sum == 0 ? string.Empty : $"[{sum}]";
            //}            
            _HeaderCell();
        }
        public frmMonitor(SectionData sec) : this()
        {
            m_Identity = sec.SectionName;
            if (sec.Keys.ContainsKey("IP")) { IP = sec.Keys["IP"]; }
            if (sec.Keys.ContainsKey("PORT")) { Port = sec.Keys["PORT"]; }
            if (sec.Keys.ContainsKey("INTERVAL")) { Interval = sec.Keys["INTERVAL"]; }
            if (sec.Keys.ContainsKey("DENY")) { Deny = sec.Keys["DENY"].ToDictionary(); }
            if (sec.Keys.ContainsKey("ALLOW")) { Allow = sec.Keys["ALLOW"].ToDictionary(); }
            Text = $"[{IP} : {Port}] - {Interval} 秒";
            tsIP.Text = IP;
            tsPort.Text = Port;
            tsInterval.Text = Interval;
            _CreateDenyAllowForm();
        }
        public frmMonitor(string ip, int port, int interval) : this()
        {
            m_Identity = Guid.NewGuid().ToString();
            Text = $"[{ip} : {port}] - {interval} 秒";
            tsIP.Text = ip;
            tsPort.Text = port.ToString();
            tsInterval.Text = interval.ToString();
            _CreateDenyAllowForm();
        }
        private void _CreateDenyAllowForm()
        {
            m_DenyForm = new frmDeny(m_Identity, Deny);
            m_AllowForm = new frmAllow(m_Identity, Allow);
            var dsum = Deny.Sum(entry => entry.Value.Count);
            tsDeny.Text = dsum == 0 ? string.Empty : $"[{dsum}]";
            var asum = Allow.Sum(entry => entry.Value.Count) + Allow.Count(entry => entry.Value == null || entry.Value.Count <= 0);
            tsAllow.Text = asum == 0 ? string.Empty : $"[{asum}]";
        }

        private void frmMonitor_FormClosing(object sender, FormClosingEventArgs e)
        {            
            m_Publish.OnValueUpdated -= OnValueUpdated;
            m_Publish.UnsubscribeAll();
            m_Publish.Close();

            for (int i = m_Channels.Count - 1; i >= 0; i--)
            {
                ChannelInfo ci;
                m_Channels.TryRemove(m_Channels.Keys.ElementAt(i), out ci);
                ci.Dispose();
            }
        }
        private void grid1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                tsSetDeny_Click(tsSetDeny, EventArgs.Empty);
                m_DenyForm.ResetItem();
            }
            if (e.KeyCode == Keys.Add)
            {
                tsSetAllow_Click(tsSetAllow, EventArgs.Empty);
                m_AllowForm.ResetItem();
            }
        }
        private void tsSetAllow_Click(object sender, EventArgs e)
        {
            SelectionBase selection = (SelectionBase)grid1.Selection;
            var region = selection.GetSelectionRegion();
            foreach (var range in region)
            {
                for (int i = range.Start.Row; i <= range.End.Row; i++)
                {
                    string channel = grid1[i, 2].Value.ToString();
                    string item = grid1[i, 3].Value.ToString();
                    if (!Allow.ContainsKey(channel))
                    {
                        Allow.Add(channel, new List<string>());
                    }
                    Allow[channel].Add(item);
                }
            }
            _Save();
            _ApplyAllowDeny();
            c_Summary.SetValue(m_Channels.Count);
            m_AllowForm.ResetItem();

            var sum = Allow.Sum(entry => entry.Value.Count) + Allow.Count(entry => entry.Value == null || entry.Value.Count <= 0);
            tsAllow.Text = sum == 0 ? string.Empty : $"[{sum}]";
        }
        private void tsSetDeny_Click(object sender, EventArgs e)
        {
            SelectionBase selection = (SelectionBase)grid1.Selection;
            var region = selection.GetSelectionRegion();
            foreach (var range in region)
            {
                for (int i = range.Start.Row; i <= range.End.Row; i++)
                {
                    string channel = grid1[i, 2].Value.ToString();
                    string item = grid1[i, 3].Value.ToString();
                    if (!Deny.ContainsKey(channel))
                    {
                        Deny.Add(channel, new List<string>());
                    }
                    Deny[channel].Add(item);
                }
            }
            _Save();
            _ApplyAllowDeny();
            c_Summary.SetValue(m_Channels.Count);
            m_DenyForm.ResetItem();

            var sum = Deny.Sum(entry => entry.Value.Count);
            tsDeny.Text = sum == 0 ? string.Empty : $"[{sum}]";
        }
        private void tsSetStop_Click(object sender, EventArgs e)
        {
            SelectionBase selection = (SelectionBase)grid1.Selection;
            var region = selection.GetSelectionRegion();
            foreach (var range in region)
            {
                for (int i = range.Start.Row; i <= range.End.Row; i++)
                {
                    string key = $"{grid1[i, 2].Value.ToString()}.{grid1[i, 3].Value.ToString()}";
                    m_Channels[key].Stop();
                }
            }
            CountExceed();
        }

        private void tsRestart_Click(object sender, EventArgs e)
        {
            SelectionBase selection = (SelectionBase)grid1.Selection;
            var region = selection.GetSelectionRegion();
            foreach (var range in region)
            {
                for (int i = range.Start.Row; i <= range.End.Row; i++)
                {
                    string key = $"{grid1[i, 2].Value.ToString()}.{grid1[i, 3].Value.ToString()}";
                    m_Channels[key].Start();
                }
            }
        }
        private void tsDeny_Click(object sender, EventArgs e)
        {
            m_DenyForm.ShowDialog(this);
            _ApplyAllowDeny();
            var sum = Deny.Sum(entry => entry.Value.Count);
            tsDeny.Text = sum == 0 ? string.Empty : $"[{sum}]";
        }
        private void tsAllow_Click(object sender, EventArgs e)
        {
            m_AllowForm.ShowDialog(this);
            _ApplyAllowDeny();
            var sum = Allow.Sum(entry => entry.Value.Count) + Allow.Count(entry => entry.Value == null || entry.Value.Count <= 0);
            tsAllow.Text = sum == 0 ? string.Empty : $"[{sum}]";
        }
        private void tsConnect_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tsIP.Text) || string.IsNullOrEmpty(tsPort.Text) || string.IsNullOrEmpty(tsInterval.Text)) { return; }
            m_Publish = new RedisPublishLib(tsIP.Text, int.Parse(tsPort.Text));
            m_Publish.OnValueUpdated += OnValueUpdated;
            m_Publish.SubscribeAllChannels();
            tsDisconnect.Enabled = true;
            tsConnect.Enabled = tsIP.Enabled = tsPort.Enabled = tsInterval.Enabled = false;
        }
        private void tsDisconnect_Click(object sender, EventArgs e)
        {
            m_Publish.OnValueUpdated -= OnValueUpdated;
            m_Publish.UnsubscribeAll();
            m_Publish.Close();
            tsConnect.Enabled = tsIP.Enabled = tsPort.Enabled = tsInterval.Enabled = true;
            tsDisconnect.Enabled = false;
            _HeaderCell();
            m_Channels.Clear();
        }
        //private void txtInterval_Validated(object sender, EventArgs e)
        //{
        //    int interval;
        //    int.TryParse(tsInterval.Text, out interval);
        //    lock (m_Channels)
        //    {
        //        foreach (var channel in m_Channels.Values)
        //        {
        //            channel.Interval = TimeSpan.FromSeconds(interval);
        //        }
        //    }
        //}

        private void tsIP_TextChanged(object sender, EventArgs e)
        {
            if (sender == tsIP) { IP = tsIP.Text; }
            if (sender == tsPort) { Port = tsPort.Text; }
            if (sender == tsInterval) { Interval = tsInterval.Text; }

            _Save();
        }

        private void OnValueUpdated(string channel, string item, string value)
        {
            string key = $"{channel}.{item}";
            if (Allow.Count > 0)
            {
                //if (!Allow.ContainsKey(channel) || !Allow[channel].Contains(item))
                if (!Allow.ContainsKey(channel) || (Allow[channel].Count > 0 && !Allow[channel].Contains(item)))
                {
                    return;
                }
            }
            if (Deny.ContainsKey(channel))
            {
                if (Deny[channel].Contains(item)) { return; }
            }
            ThreadPool.QueueUserWorkItem((state) =>
            {
                string k = state.ToString();
                try
                {
                    if (!m_Channels.ContainsKey(k))
                    {
                        ChannelInfo info = new ChannelInfo(this, int.Parse(tsInterval.Text), channel, item, value);
                        if (m_Channels.TryAdd(k, info))
                        {
                            _ContentCell(k);
                            c_Summary.SetValue(m_Channels.Count);
                        }
                    }
                    else
                    {
                        m_Channels[key].Value.SetValue(value);
                    }
                }
                catch (Exception) { }
            }, key);
        }

        #region Public
        public void Start()
        {
            tsConnect_Click(tsConnect, EventArgs.Empty);
        }
        public void CountExceed()
        {
            ThreadPool.QueueUserWorkItem((state) =>
            {
                c_ExceedSummary.SetValue(m_Channels.Values.Count(e => e.Exceed));
            });
        }
        #endregion

        #region Private
        private void _ClearCell()
        {
            c_Summary.Field.UnBindToGrid();
            c_ExceedSummary.Field.UnBindToGrid();
            c_SummaryHead.Field.UnBindToGrid();
            c_SECsHead.Field.UnBindToGrid();
            c_NoHead.Field.UnBindToGrid();
            c_ChannelHead.Field.UnBindToGrid();
            c_ItemHead.Field.UnBindToGrid();
            c_ValueHead.Field.UnBindToGrid();
            for (int row = 2; row < grid1.RowsCount; row++)
            {
                for (int col = 0; col < grid1.ColumnsCount; col++)
                {
                    if (grid1[row, col] != null) { grid1[row, col].UnBindToGrid(); }
                }
            }
            grid1.Rows.Clear();
        }
        private void _HeaderCell()
        {
            _ClearCell();
            grid1.Redim(2, 5);
            grid1[0, 0] = c_ExceedSummary.Field;
            grid1[0, 1] = c_SummaryHead.Field;
            grid1[0, 2] = c_Summary.Field;
            grid1[1, 0] = c_SECsHead.Field;
            grid1[1, 1] = c_NoHead.Field;
            grid1[1, 2] = c_ChannelHead.Field;
            grid1[1, 3] = c_ItemHead.Field;
            grid1[1, 4] = c_ValueHead.Field;
            //grid1[0, 1] = new CHeaderCell() { Caption = "Summary" }.Field;
            //grid1[0, 2] = c_Summary.Field;
            //grid1[1, 0] = new CHeaderCell() { Caption = "SECs", Sortable = true }.Field;
            //grid1[1, 1] = new CHeaderCell() { Caption = "", Sortable = true }.Field;
            //grid1[1, 2] = new CHeaderCell() { Caption = "Channel", Sortable = true }.Field;
            //grid1[1, 3] = new CHeaderCell() { Caption = "Item", Sortable = true }.Field;
            //grid1[1, 4] = new CHeaderCell() { Caption = "Value" }.Field;
            ((SourceGrid.Cells.ColumnHeader)grid1[1, 2]).SortComparer = new MultiColumnsComparer(3);
            ((SourceGrid.Cells.ColumnHeader)grid1[1, 3]).SortComparer = new MultiColumnsComparer(2);

        }
        private void _ContentCell(string key)
        {
            grid1.InvokeIfRequired(() =>
            {
                //int cnt = m_Channels.Count;
                //int cnt = m_Channels.Count + 2-1;
                int cnt = grid1.RowsCount;
                grid1.Redim(cnt + 1, 5);
                grid1[cnt, 1] = new RHeaderCell() { BackColor = Color.DimGray, Caption = (cnt - 1).ToString() }.Field;
                grid1[cnt, 2] = m_Channels[key].Channel.Field;
                grid1[cnt, 3] = m_Channels[key].Item.Field;
                grid1[cnt, 4] = m_Channels[key].Value.Field;
                grid1[cnt, 0] = m_Channels[key].Delay.Field;
                grid1.AutoSizeCells();

                _Sort();
            });
        }
        private void _ApplyAllowDeny()
        {
            _ClearCell();
            lock (m_Channels)
            {
                for (int i = m_Channels.Count - 1; i >= 0; i--)
                {
                    string[] items = m_Channels.ElementAt(i).Key.Split('.');
                    if (Allow.Count > 0)
                    {
                        if (!Allow.ContainsKey(items[0]) || (Allow[items[0]].Count > 0 && !Allow[items[0]].Contains(items[1])))
                        {
                            ChannelInfo ci;
                            m_Channels.TryRemove(m_Channels.ElementAt(i).Key, out ci);
                            ci.Dispose();
                            continue;
                        }
                    }

                    if (Deny.ContainsKey(items[0]))
                    {
                        if (Deny[items[0]].Contains(items[1]))
                        {
                            ChannelInfo ci;
                            m_Channels.TryRemove(m_Channels.ElementAt(i).Key, out ci); ci.Dispose();
                        }
                    }
                }
            }
            _HeaderCell();
            foreach (var channel in m_Channels.Keys)
            {
                _ContentCell(channel);
            }
            grid1.AutoSizeCells();
            _Sort();
        }
        private void _Sort()
        {
            for (int i = 0; i < 5; i++)
            {
                if (((SourceGrid.Cells.ColumnHeader)grid1[1, i]).SortStyle != DevAge.Drawing.HeaderSortStyle.None)
                {
                    ((SourceGrid.Cells.ColumnHeader)grid1[1, i]).Sort(((SourceGrid.Cells.ColumnHeader)grid1[1, i]).SortStyle == DevAge.Drawing.HeaderSortStyle.Ascending);
                    break;
                }
            }
        }
        private void _Save()
        {
            if (!frmMain.INI.Sections.ContainsSection(m_Identity))
            {
                frmMain.INI.Sections.AddSection(m_Identity);
            }
            var sec = frmMain.INI.Sections[m_Identity];
            if (!sec.ContainsKey("IP")) { sec.AddKey("IP"); }
            if (!sec.ContainsKey("PORT")) { sec.AddKey("PORT"); }
            if (!sec.ContainsKey("INTERVAL")) { sec.AddKey("INTERVAL"); }
            if (!sec.ContainsKey("DENY")) { sec.AddKey("DENY"); }
            if (!sec.ContainsKey("ALLOW")) { sec.AddKey("ALLOW"); }
            sec["IP"] = IP;
            sec["PORT"] = Port;
            sec["INTERVAL"] = Interval;
            sec["DENY"] = Deny.SectionString();
            sec["ALLOW"] = Allow.SectionString();

            var parser = new FileIniDataParser();
            parser.WriteFile("Config.ini", frmMain.INI);
        }

        #endregion
    }
}