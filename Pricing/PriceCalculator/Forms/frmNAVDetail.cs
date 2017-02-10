using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using Newtonsoft.Json.Linq;
using PriceCalculator.Component;
using PriceCalculator.Utility;

namespace PriceCalculator
{
    public partial class frmNAVDetail : DockContent
    {
        private ETF m_ETF;

        public frmNAVDetail(ETF etf)
        {
            InitializeComponent();
            if (etf == null) { return; }

            m_ETF = etf;
            string str = etf.ToString();
            txtJson.Text = str;
            Parse(etf.ETFCode, str);
        }
        private void tsMktPrice_TextChanged(object sender, EventArgs e)
        {
            decimal mktPrice;
            TreeNode node = (TreeNode)tsMktPrice.Tag;
            if (decimal.TryParse(tsMktPrice.Text, out mktPrice))
            {
                CollectionType type = CollectionType.Stock;
                if (node.Parent.Parent.Text == "FUND")
                {
                    type = CollectionType.Fund;
                }
                else if (node.Parent.Parent.Text == "FUT")
                {
                    type = CollectionType.Future;
                }
                m_ETF.AssignMktPrice(type, node.Parent.Tag.ToString(), mktPrice);
                node.Text = $"* [{node.Name}]   {mktPrice}";
                node.Tag = mktPrice;                
            }
        }
        private void tsPublicShares_TextChanged(object sender, EventArgs e)
        {
            double shares;
            TreeNode node = (TreeNode)tsFundAssetValue.Tag;
            if (double.TryParse(tsFundAssetValue.Text, out shares))
            {
                m_ETF.AssignFundAssetValue(shares);
                node.Text = $"* [{node.Name}]   {shares}";
                node.Tag = shares;
            }
        }
        private void tsYstPrice_TextChanged(object sender, EventArgs e)
        {
            decimal yp;
            TreeNode node = (TreeNode)tsYstPrice.Tag;
            if (decimal.TryParse(tsYstPrice.Text, out yp))
            {
                CollectionType type = CollectionType.Stock;
                if (node.Parent.Parent.Text == "FUND")
                {
                    type = CollectionType.Fund;
                }
                else if (node.Parent.Parent.Text == "FUT")
                {
                    type = CollectionType.Future;
                }
                m_ETF.AssignYstPrice(type, node.Parent.Tag.ToString(), yp);
                node.Text = $"* [{node.Name}]   {yp}";
                node.Tag = yp;
            }
        }
        private void tvJson_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Parent == null || e.Node.Parent.Parent == null) { return; }

            switch (e.Node.Name)
            {
                case "AssignedYP":
                    tsYstPrice.Tag = e.Node;
                    tsYstPrice.Text = e.Node.Tag.ToString();
                    break;
                case "AssignedMP":
                    tsMktPrice.Tag = e.Node;
                    tsMktPrice.Text = e.Node.Tag.ToString();
                    break;
                case "ETF":
                case "FundAssetValue":
                    tsFundAssetValue.Tag = e.Node;
                    tsFundAssetValue.Text = e.Node.Tag.ToString();
                    break;
                default:
                    tsYstPrice.Tag = null;
                    tsFundAssetValue.Tag = null;
                    tsMktPrice.Tag = null;
                    break;
            }
        }



        private TreeNode Json2Tree(TreeNode parent, JObject obj)
        {
            //create the parent node
            //TreeNode parent = new TreeNode();
            //loop through the obj. all token should be pair<key, value>
            foreach (var token in obj)
            {
                //change the display Content of the parent
                //parent.Text = token.Key.ToString();
                //create the child node
                TreeNode child = new TreeNode(token.Key.ToString());

                //check if the value is of type obj recall the method
                if (token.Value.Type.ToString() == "Object")
                {
                    // child.Text = token.Key.ToString();
                    //create a new JObject using the the Token.value
                    JObject o = (JObject)token.Value;
                    //recall the method
                    child = Json2Tree(child, o);
                    //add the child to the parentNode
                    parent.Nodes.Add(child);
                }
                //if type is of array
                else if (token.Value.Type.ToString() == "Array")
                {
                    int ix = -1;
                    //  child.Text = token.Key.ToString();
                    //loop though the array
                    foreach (var itm in token.Value)
                    {
                        TreeNode arrNode = new TreeNode("");
                        //check if value is an Array of objects
                        if (itm.Type.ToString() == "Object")
                        {
                            TreeNode objTN = new TreeNode();
                            //child.Text = token.Key.ToString();
                            //call back the method
                            ix++;

                            JObject o = (JObject)itm;
                            objTN = Json2Tree(child, o);
                            objTN.Text = $"{token.Key}[{ix}]";
                            child.Nodes.Add(objTN);
                            //parent.Nodes.Add(child);
                        }
                        //regular array string, int, etc
                        else if (itm.Type.ToString() == "Array")
                        {
                            ix++;
                            TreeNode dataArray = new TreeNode();
                            foreach (var data in itm)
                            {
                                dataArray.Text = $"{token.Key}[{ix}]";
                                dataArray.Nodes.Add(data.ToString());
                            }
                            child.Nodes.Add(dataArray);
                        }

                        else
                        {
                            Node(arrNode, itm.ToString());
                            child.Nodes.Add(arrNode);
                            //child.Nodes.Add(itm.ToString());
                        }
                    }
                    parent.Nodes.Add(child);
                }
                else
                {
                    //if token.Value is not nested
                    // child.Text = token.Key.ToString();
                    //change the value into N/A if value == null or an empty string 
                    if (token.Value.ToString() == "")
                        child.Nodes.Add("N/A");
                    else
                    {
                        //child.Nodes.Add(token.Value.ToString());
                        Node(child, token.Value.ToString());
                    }
                    parent.Nodes.Add(child);
                }
            }
            return parent;
        }
        private void Parse(string etfcode, string json)
        {
            if (string.IsNullOrEmpty(json)) { return; }
            JObject obj = JObject.Parse(json);
            tvJson.Nodes.Clear();
            TreeNode root = new TreeNode(etfcode);
            TreeNode keyNode = Json2Tree(root, obj);            
            tvJson.Nodes.Add(root);
        }
        private void Node(TreeNode parent, string value)
        {
            JObject objs = JObject.Parse(value);
            foreach (var obj in objs)
            {
                if (string.IsNullOrEmpty(parent.Text) &&
                    (((obj.Key == "PID") || (obj.Key == "Quoted")) || (obj.Key == "BaseCrncy")))
                {
                    parent.Text = obj.Value.ToString();
                    parent.Tag = obj.Value.ToString();
                }
                TreeNode node = new TreeNode($"[{obj.Key}]   {obj.Value}")
                {
                    Name = obj.Key,
                    Tag = obj.Value
                };
                parent.Nodes.Add(node);
                if (node.Parent != null)
                {
                    if (node.Name == "AssignedYP")
                    {
                        node.ContextMenuStrip = cmYstPrice;
                        node.Text = $"* {node.Text}";
                    }
                    else if (node.Name == "AssignedMP")
                    {
                        node.ContextMenuStrip = cmMktPrice;
                        node.Text = $"* {node.Text}";
                    }
                    else if (node.Name == "FundAssetValue")
                    {
                        node.ContextMenuStrip = cmFundAssetValue;
                        node.Text = $"* {node.Text}";
                    }
                }
            }
        }
    }
}
