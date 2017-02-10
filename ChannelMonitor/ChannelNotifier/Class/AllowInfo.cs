using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notifier.Class
{
    public class AllowInfo
    {
        private ListViewItem m_ListViewItem;

        public ServerInfo Server { get; private set; }
        public string Channel { get;  private set; }
        public string Item { get;  private set; }
        public int Interval { get;  private set; }
        public ChannelStyle Style { get; private  set; }
        public string Key { get { return $"{Channel}|{Item}"; } }
        

        private AllowInfo(string channel, string item, int interval, ChannelStyle style)
        {
            Channel = channel;
            Item = item;
            Interval = interval;
            Style = style;
        }
        public static AllowInfo Create(string[] info)
        {
            int interval;
            ChannelStyle style;            
            if (info.Length < 5 || string.IsNullOrEmpty(info[2]) || !int.TryParse(info[4], out interval))
            {
                return null;
            }
            Enum.TryParse(info[1], out style);
            return new AllowInfo(info[2], info[3], interval, style);
        }
        public static AllowInfo Create(string channel, string item, string interval, string style)
        {
            int i;
            ChannelStyle s;
            if (string.IsNullOrEmpty(channel) || !int.TryParse(interval, out i))
            {
                return null;
            }
            Enum.TryParse(style, out s);
            return new AllowInfo(channel, item, i, s);
        }

        public bool isAllow(string channel, string item)
        {
            return channel == Channel && (string.IsNullOrEmpty(Item) || item == Item);
        }
        public ListViewItem ListViewItem(ServerInfo server)
        {
            if (m_ListViewItem == null)
            {
                m_ListViewItem = new ListViewItem(new string[] { server.Name, Channel, Item, Interval.ToString(),Style.ToString() }) { Tag = server, Name = new Guid().ToString() };
            }
            else
            {
                
            }
            
            return m_ListViewItem;
        }
        public override string ToString()
        {
            return $"";

        }
    }
}
