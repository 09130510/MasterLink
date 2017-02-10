using Notifier.Utility;
using SourceCell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Notifier.Class
{
    public class InfoNotifyInfo
    {
        #region Variable        
        private ServerInfo m_Server;
        private Style m_Style;
        #endregion

        #region Cell
        private ComboBoxCell c_Server;
        private TextCell c_Channel;
        private TextCell c_Item;


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
        //public Style Style { get { return Style.Blue; } }
        public Style Style
        {
            get { return m_Style; }
            private set
            {
                m_Style = value;
                switch (m_Style)
                {
                    case Style.Red:
                        cServer.BackColor = System.Drawing.Color.MistyRose;
                        break;
                    case Style.Orange:
                        cServer.BackColor = System.Drawing.Color.LemonChiffon;
                        break;
                    case Style.Blue:
                        cServer.BackColor = System.Drawing.Color.LightCyan;
                        break;
                    case Style.Green:
                        cServer.BackColor = System.Drawing.Color.FromArgb(232, 253, 208);
                        break;
                    case Style.Gray:
                        cServer.BackColor = System.Drawing.Color.Gainsboro;
                        break;
                }
            }
        }
        public string Key { get { return $"{Channel}|{Item}"; } }
        #endregion

        private InfoNotifyInfo(ServerInfo server, string channel, string item, Style style)
        {
            m_Server = server;
            Channel = channel;
            Item = item;
            Style = style;
        }
        public static InfoNotifyInfo Create(string[] info)
        {
            Style s;
            if (info.Length < 4 || string.IsNullOrEmpty(info[1]) || !Enum.TryParse(info[3], out s))
            {
                return null;
            }
            var server = Util.Servers.ContainsKey(info[0]) ? Util.Servers[info[0]] : null;
            return new InfoNotifyInfo(server, info[1], info[2], s);
        }
        public static InfoNotifyInfo Create(ServerInfo server, string channel, string item, string style)
        {
            Style s;
            if (server == null || string.IsNullOrEmpty(channel) || !Enum.TryParse(style, out s)) { return null; }
            return new InfoNotifyInfo(server, channel, item, s);
        }

        #region Public
        public bool isAllow(string channel, string item)
        {
            return channel == Channel && (string.IsNullOrEmpty(Item) || item == Item);
        }
        public override string ToString()
        {
            return $"{Server}|{Channel}|{Item}|{Style}";
        }
        #endregion
    }
}