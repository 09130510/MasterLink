using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace FixInitiator.Class.Public
{
    public delegate void SetTextCallback(object sender, string text);
    public delegate void SetIndexTextCallback(object sender, int ndx, string text);
    class SrnItems
    {
        public Button butOrdSvr { get; set; }
        public ToolStripMenuItem exportToolStripMenuItem { get; set; }


        private void SetText(object sender, string text)
        {
            if (sender is Button)
            {
                (sender as Button).Text = text;
            }
            else if (sender is TextBox)
            {
                (sender as TextBox).Text = text;
            }
            else if (sender is Label)
                (sender as Label).Text = text;
            else if (sender is ListBox)
            {
                ListBox listBox = sender as ListBox;
                string[] s = text.Split(';');
                listBox.Items.AddRange(s);
                listBox.SelectedIndex = listBox.Items.Count - 1;
            }
            else if (sender is ListView)
            {
                ListView listView = sender as ListView;
                ListViewItem myItem = new ListViewItem(text);
                myItem.ForeColor = Color.Red;
                listView.Items.Add(myItem);
            }
        }
        private void SetText(object sender, int ndx, string text)
        {
            if (sender is ListView)
            {
                string[] items = text.Split(',');
                ListView listView = sender as ListView;
                listView.Columns[ndx].Text = items[0];
                for (int i = 0; i < items.Length - 1; i++)
                {
                    listView.Items[i].SubItems[ndx].Text = items[i + 1];
                }
            }
        }
        public void ShowSrnData(string data, Control obj)
        {
            if (data != string.Empty)
            {
                if (obj.InvokeRequired)
                {
                    SetTextCallback d = new SetTextCallback(SetText);
                    obj.BeginInvoke(d, new object[] { obj, data });
                }
                else
                    SetText(obj, data);
            }
        }

        public void ShowSrnData(int ndx, string data, Control obj)
        {
            if (data != string.Empty)
            {
                if (obj.InvokeRequired)
                {
                    SetIndexTextCallback d = new SetIndexTextCallback(SetText);
                    obj.BeginInvoke(d, new object[] { obj, ndx, data });
                }
                else
                    SetText(obj, ndx, data);
            }
        }
    }
   
    
}
