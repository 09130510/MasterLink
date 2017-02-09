using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Bloomberglp.Blpapi;
using BLParser;
using SourceCell;
using SourceGrid;
using EventHandler = Bloomberglp.Blpapi.EventHandler;

namespace BLPServer.Class
{
    public class Display : DisposableClass
    {

        #region Variable
        private string[] m_Fields;
        private string[] m_Securities;
        private Dictionary<Position, CellBase> m_CellList;
        #endregion

        public Display()
        {
            m_Fields = Utility.Load<string>("SUBSCRIBE", "FIELDS").Split(';');
            m_Securities = Utility.Load<string>("SUBSCRIBE", "SECURITIES").Split(';');
                        
            Utility.Subscriber.OnRequestReply += new EventHandler(Subscriber_OnRequestReply);
            Utility.Subscriber.OnSubscriptionReply += new EventHandler(Subscriber_OnSubscriptionReply);
            _InitCell();
        }

        #region Delegate
        

        private void Subscriber_OnSubscriptionReply(Event eventObject, Session session)
        {
            Subscribe s = Subscribe.Create(eventObject);
            if (s != null)
            {
                foreach (var security in m_Securities)
                {
                    foreach (var field in m_Fields)
                    {
                        if (String.IsNullOrEmpty(security) || String.IsNullOrEmpty(field))
                        {
                            continue;
                        }
                        //Element element  = s[security, field];
                        //string v = element != null ? element.GetValue().ToString() : string.Empty;
                        object value = s[security, field];

                        //Utility.Log(this, "Update Value", field+ ":" +value);
                        string v = value != null ? value.ToString() : string.Empty;
                        //Console.WriteLine("[" + security + "," + field + "] " + v);
                        if (!String.IsNullOrEmpty(v))
                        {
                            CellBase c = m_CellList.Values.FirstOrDefault(e => e.Name == security && e.Tag != null && e.Tag.ToString() == field);
                            if (c != null) { c.SetValue(v); }
                        }
                    }
                }
            }
        }

        private void Subscriber_OnRequestReply(Event eventObject, Session session)
        {
            Response response = new Response(eventObject);
            //Console.WriteLine("Response");
            if (response.HasError)
            {
                //foreach (var item in response.Errors)
                //{
                //    //Response.ErrorElement(this, "", item);
                //    //Console.WriteLine(item);
                //    Utility.Log(this, "RequestError", item);
                //}
                return;
            }
            foreach (var security in m_Securities)
            {
                foreach (var field in m_Fields)
                {
                    if (String.IsNullOrEmpty(security) || String.IsNullOrEmpty(field))
                    {
                        continue;
                    }
                    //Element element  = s[security, field];
                    //string v = element != null ? element.GetValue().ToString() : string.Empty;
                    object value = response[security, field];
                    //Utility.Log(this, "Update Value", field+ ":" +value);
                    string v = value != null ? value.ToString() : string.Empty;
                    //Console.WriteLine("[" + security + "," + field + "] " + v);
                    if (!String.IsNullOrEmpty(v))
                    {
                        CellBase c = m_CellList.Values.FirstOrDefault(e => e.Name == security && e.Tag != null && e.Tag.ToString() == field);
                        if (c != null) { c.SetValue(v); }
                    }
                }
            }
            //Utility.Log(this, "Request", response.ToString());
        }
        
        #endregion

        #region Private
        private void _InitCell()
        {
            if (m_CellList == null) { m_CellList = new Dictionary<Position, CellBase>(); }
            for (int row = 0; row < m_Securities.Length; row++)
            {
                for (int col = 0; col < m_Fields.Length; col++)
                {
                    if (row == 0 && col == 0) { continue; }
                    CellBase c = null;
                    //Field Header
                    if (row == 0 && col != 0)
                    {
                        c = new CHeaderCell() { Caption = m_Fields[col - 1] };

                    }
                    //SecurityHeader
                    else if (row != 0 && col == 0)
                    {
                        c = new RHeaderCell() { Caption = m_Securities[row - 1], BackColor = CHeaderCell.HeaderBackColor, DefaultFontColor = CHeaderCell.HeaderFontColor, TextAlignment = DevAge.Drawing.ContentAlignment.MiddleLeft };
                    }
                    else
                    {
                        //content
                        c = new TextCell() { Name = m_Securities[row - 1], Tag = m_Fields[col - 1], CellType = TextCell.TextType.String, FontColor = Color.Black, TextAlignment = DevAge.Drawing.ContentAlignment.MiddleRight };
                    }
                    if (c != null) { m_CellList.Add(new Position(row, col), c); }

                }
            }
        }
        #endregion

        #region Public
        public void SetCell(Grid grid)
        {
            //for (int i = 0; i < grid.RowsCount; i++)
            //{
            //    for (int j = 0; j < grid.ColumnsCount; j++)
            //    {
            //        grid[i, j].UnBindToGrid();
            //    }
            //}
            grid.Rows.Clear();
            grid.Redim(m_Securities.Length, m_Fields.Length);
            foreach (var item in m_CellList)
            {
                grid.SetCell(item.Key, item.Value.Cell);
            }
            grid.AutoSizeCells();
        }
        #endregion

        protected override void DoDispose()
        {   
            Utility.Subscriber.OnRequestReply -= new EventHandler(Subscriber_OnRequestReply);
            Utility.Subscriber.OnSubscriptionReply -= new EventHandler(Subscriber_OnSubscriptionReply);
        }
    }
}
