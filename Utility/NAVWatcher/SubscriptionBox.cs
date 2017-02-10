using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PriceLib.Redis;

namespace NAVWatcher
{
    public partial class SubscriptionBox : UserControl
    {

        #region Property
        public string Channel { get { return txtChannel.Text; } }
        public string Item { get { return txtItem.Text; } }
        public string Value { get { return txtValue.Text; } }
        #endregion

        public SubscriptionBox()
        {
            InitializeComponent();
            Util.RedisLib.OnChannelUpdate += new RedisLib.OnChannelUpdateDelegate(RedisLib_OnChannelUpdate);
        }

        private void RedisLib_OnChannelUpdate(string channel, string item, string value)
        {
            if ((Channel == channel && Item == item) ||(Channel == channel && Item=="*" ))
            {
                txtValue.InvokeIfRequired(() =>
                {
                    txtValue.Text = value;
                });                
            }
        }

        private void btnSub_Click(object sender, EventArgs e)
        {
            if (btnSub.Text =="Go")
            {
                Util.RedisLib.SubscribeChannel(Channel, Item);
                txtChannel.Enabled = txtItem.Enabled = false;
                btnSub.Text = "x";
            }
            else if (btnSub.Text =="x")
            {
                Util.RedisLib.UnsubscribeChannel(Channel, Item);
                txtValue.Clear();
                txtChannel.Enabled = txtItem.Enabled = true;
                btnSub.Text = "Go";
            }
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Util.RedisLib.UnsubscribeChannel(Channel, Item);
        }
        
    }
}
