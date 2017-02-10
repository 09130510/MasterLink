using Notifier.Utility;
using SourceCell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Notifier.Class
{
    public class StopNotifyInfo
    {
        #region Variable        
        private ServerInfo m_Server;
        #endregion

        #region Cell
        private ComboBoxCell c_Server;
        private TextCell c_Channel;
        private TextCell c_Item;
        private TextCell c_Interval;        

        public ComboBoxCell cServer
        {
            get
            {
                if (c_Server == null)
                {
                    c_Server = new ComboBoxCell() { IsExclusive = true, /*Enable= true,*/ SelItem = Util.Servers.Values.Select(E => E.Name).ToArray(), EditMode = SourceGrid.EditableMode.DoubleClick, HasBorder = true, DropDownStyle = ComboBoxStyle.DropDownList, FontName = CellBase.FontName.Verdana, FontSize = 7, Value = ServerName, Tag = this };
                    c_Server.OnValueChanged += (cell, e) =>
                    {
                        var newName = cell.Cell.Value.ToString();
                        if (newName != ServerName)
                        {
                            m_Server = Util.Servers.Values.FirstOrDefault(entry => entry.Name == newName);
                            if (m_Server == null)
                            {
                                cell.Cell.Value = string.Empty;
                            }
                        }
                    };
                }
                return c_Server;
            }
        }
        public TextCell cChannel
        {
            get
            {

                if (c_Channel == null)
                {
                    c_Channel = new TextCell() { CellType = TextCell.TextType.String, EditMode = SourceGrid.EditableMode.DoubleClick,/* Enable = true,*/ FontName = CellBase.FontName.Verdana, FontSize = 7, HasBorder = true, TextAlignment = DevAge.Drawing.ContentAlignment.MiddleLeft, Value = Channel, Tag = this };
                    c_Channel.OnValueChanged += (cell, e) =>
                    {
                        Channel = cell.Cell.Value.ToString();
                    };
                }

                return c_Channel;
            }
        }
        public TextCell cItem
        {
            get
            {

                if (c_Item == null)
                {
                    c_Item = new TextCell() { CellType = TextCell.TextType.String, EditMode = SourceGrid.EditableMode.DoubleClick,/* Enable = true,*/ FontName = CellBase.FontName.Verdana, FontSize = 7, HasBorder = true, TextAlignment = DevAge.Drawing.ContentAlignment.MiddleLeft, Value = Item, Tag = this };
                    c_Item.OnValueChanged += (cell, e) =>
                    {
                        Item = cell.Cell.Value.ToString();
                    };
                }

                return c_Item;
            }
        }
        public TextCell cInterval
        {
            get
            {
                if (c_Interval == null)
                {
                    c_Interval = new TextCell() { CellType = TextCell.TextType.Numeric, Enable = true, FontName = CellBase.FontName.Verdana, FontSize = 7, HasBorder = true, BackColor = System.Drawing.Color.FromArgb(255, 243, 234), Minimum = 1, Increment = 1, Value = Interval, Tag = this };
                    c_Interval.OnValueChanged += (cell, e) =>
                    {
                        Interval = (int)cell.Cell.Value;
                        Util.Monitors[m_Server.Key].ChannelInterval(this, Interval);
                    };
                }
                return c_Interval;
            }
        }
        #endregion

        #region Property
        public string Server
        {
            get
            {
                return m_Server == null ? string.Empty : m_Server.Key;
            }
        }
        public string ServerName
        {
            get
            {
                return m_Server == null ? string.Empty : m_Server.Name;
            }
        }
        public string Channel { get; private set; }
        public string Item { get; private set; }
        public int Interval { get;  private set; }
        public Style Style { get { return Style.Red; } }
        public string Key { get { return $"{Channel}|{Item}"; } }
        #endregion

        private StopNotifyInfo(ServerInfo server, string channel, string item, int interval)
        {
            m_Server = server;
            Channel = channel;
            Item = item;
            Interval = interval;
        }
        public static StopNotifyInfo Create(string[] info)
        {
            int interval;
            if (info.Length < 4 || string.IsNullOrEmpty(info[1]) || !int.TryParse(info[3], out interval))
            {
                return null;
            }
            var server = Util.Servers.ContainsKey(info[0]) ? Util.Servers[info[0]] : null;
            return new StopNotifyInfo(server, info[1], info[2], interval);
        }
        public static StopNotifyInfo Create(ServerInfo server, string channel, string item, string interval)
        {
            int i;
            if (server == null || string.IsNullOrEmpty(channel) || !int.TryParse(interval, out i))
            {
                return null;
            }
            return new StopNotifyInfo(server, channel, item, i);
        }
        public static StopNotifyInfo Empty()
        {
            return new StopNotifyInfo(null, string.Empty, string.Empty, -1);
        }

        #region Public
        public bool isAllow(string channel, string item)
        {
            return channel == Channel && (string.IsNullOrEmpty(Item) || item == Item);
        }
        public override string ToString()
        {
            return $"{Server}|{Channel}|{Item}|{Interval}";
        }
        #endregion
    }
}
