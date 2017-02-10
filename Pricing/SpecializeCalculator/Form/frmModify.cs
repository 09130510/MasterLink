using PriceLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace PriceCalculator
{
    public partial class frmModify : DockContent
    {
        #region Variable
        private string m_FieldName;
        private FutureBase m_FutureBase;
        private FUT m_Fut;
        private SubscribeType m_PreType = SubscribeType.NONE;
        #endregion

        #region Property
        public FutureBase FutureBase { get { return m_FutureBase; } }
        public decimal ChgYstNAV { get; private set; }
        #endregion

        public frmModify(string caption, string field, FutureBase fut)
        {
            InitializeComponent();
            Text = caption;
            m_FieldName = field;
            m_FutureBase = fut;

            Util.REDIS.OnMktPriceUpdate += new OnMktPriceUpdateDelegate(REDIS_OnMktPriceUpdate);
            Util.OSCAPITAL.OnMktPriceUpdate += new OnMktPriceUpdateDelegate(OSCAPITAL_OnMktPriceUpdate);
            Util.IPUSH.OnMktPriceUpdate += new OnMktPriceUpdateDelegate(IPUSH_OnMktPriceUpdate);
            Util.PATS.OnMktPriceUpdate += new OnMktPriceUpdateDelegate(PATS_OnMktPriceUpdate);

            _SetFieldValue();
        }
        private void frmModify_FormClosing(object sender, FormClosingEventArgs e)
        {
            Util.REDIS.OnMktPriceUpdate -= new OnMktPriceUpdateDelegate(REDIS_OnMktPriceUpdate);
            Util.OSCAPITAL.OnMktPriceUpdate -= new OnMktPriceUpdateDelegate(OSCAPITAL_OnMktPriceUpdate);
            Util.IPUSH.OnMktPriceUpdate -= new OnMktPriceUpdateDelegate(IPUSH_OnMktPriceUpdate);
            Util.PATS.OnMktPriceUpdate -= new OnMktPriceUpdateDelegate(PATS_OnMktPriceUpdate);
        }
        private void frmModify_Activated(object sender, EventArgs e)
        {
            foreach (Control panel in flowLayoutPanel1.Controls)
            {
                foreach (Control control in panel.Controls)
                {
                    if (control.Name == m_FieldName)
                    {
                        bool s = control.Focus();
                        return;
                    }
                }
            }
        }
        private void _ApplyChange()
        {
            FutBaseCalcType ctype;
            SubscribeType stype;
            Month m;
            decimal ratio, yp, ynav;

            m_FutureBase.ETFCode = cETFCode.Text;
            Enum.TryParse(cCalculateType.Text, out ctype);
            m_FutureBase.CalculateType = ctype;
            Enum.TryParse(cSubscribeType.Text, out stype);
            m_FutureBase.SubscribeType = stype;
            //m_FutureBase.Head = cHead.Text;
            //m_FutureBase.Y = cY.Text;
            //Month m;
            //Enum.TryParse( cM.Text, out m);
            //m_FutureBase.M = m;            
            Enum.TryParse(cM.Text, out m);
            m_FutureBase.SetFuture(cHead.Text, cY.Text, m);
            m_FutureBase.LockDown = cLock.Checked;
            decimal.TryParse(cMPRatio.Text, out ratio);
            m_FutureBase.MPRatio = ratio / 100M;
            decimal.TryParse(cYP.Text, out yp);
            if (m_FutureBase.LockDown)
            {
                m_FutureBase.LockYP = yp;
            }
            else
            {
                m_FutureBase.SetYP(yp);
            }
            decimal.TryParse(cYstNAV.Text, out ynav);
            ChgYstNAV = ynav;
            m_FutureBase.Channel = cChannel.Text;
            //if (m_FutureBase.ETF != null)
            //{
            //    if (m_FutureBase.LockDown)
            //    {
            //        m_FutureBase.LockYstNAV = ynav;
            //        m_FutureBase.ETF.LockYstNAV(ynav);
            //    }
            //    else
            //    {
            //        m_FutureBase.ETF.UnlockYstNAV();
            //        if (ynav != m_FutureBase.ETF.YstNAV) { m_FutureBase.ETF.YstNAV = ynav; }
            //    }
            //}
            //m_FutureBase.Channel = cChannel.Text;
        }
        private void cETFCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
              if (cETFCode.Text != m_FutureBase.ETFCode)
              {
            m_FutureBase.ETFCode = cETFCode.Text;
            if (m_FutureBase.ETF != null)
            {
                lblYstNAV.InvokeIfRequired(() =>
                {
                    lblYstNAV.Text = m_FutureBase.ETF.YstNAV.ToString();
                });
                cYstNAV.InvokeIfRequired(() =>
                {
                    _YstNAVValid();
                    cYstNAV_TextChanged(cYstNAV, EventArgs.Empty);
                });
            }
            }
            */
            var etf = Util.QueryFirst<ETF>(ETF.SELECTONE, new { ETFCode = cETFCode.Text });
            if (etf != null)
            {
                lblYstNAV.InvokeIfRequired(() =>
                {
                    lblYstNAV.Text = etf.YstNAV.ToString();
                });
                cYstNAV.InvokeIfRequired(() =>
                {
                    cYstNAV_TextChanged(cYstNAV, EventArgs.Empty);
                });
            }
        }
        private void cCalculateType_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
            FutBaseCalcType ctype = (FutBaseCalcType)Enum.Parse(typeof(FutBaseCalcType), cCalculateType.Text);
            if (ctype != m_FutureBase.CalculateType)
            {
                m_FutureBase.CalculateType = ctype;
            }
            */
        }
        private void cSubscribeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
            SubscribeType stype = (SubscribeType)Enum.Parse(typeof(SubscribeType), cSubscribeType.Text);
            if (stype != m_FutureBase.SubscribeType)
            {
                m_FutureBase.SubscribeType = stype;
                cYP.Text = m_FutureBase.YP.ToString();
            }
            */

            _DoUnsubscribe(m_PreType);
            //switch (m_PreType)
            //{
            //    case SubscribeType.PATS:
            //        Util.PATS.Unsubscribe(m_Fut.PATS);
            //        break;
            //    case SubscribeType.CAPITAL:
            //        Util.OSCAPITAL.Unsubscribe(m_Fut.Capital);
            //        break;
            //    case SubscribeType.REDIS:
            //        Console.WriteLine("cSubscribeType_SelectedIndexChanged");
            //        Util.REDIS.Unsubscribe(PriceLib.Redis.SubscribeType.Future, m_Fut.Redis);
            //        break;
            //    case SubscribeType.IPUSH:
            //        Util.IPUSH.Unsubscribe(m_Fut.iPush);
            //        break;
            //    case SubscribeType.NONE:
            //    default:
            //        break;
            //}


            lblYP.Text = string.Empty;
            _DoSubscribe();
            //if (m_Fut == null) { return; }
            //SubscribeType stype;
            //Enum.TryParse(cSubscribeType.Text, out stype);
            //switch (stype)
            //{
            //    case SubscribeType.PATS:
            //        Util.PATS.Subscribe(m_Fut.PATS);
            //        break;
            //    case SubscribeType.CAPITAL:
            //        Util.OSCAPITAL.Subscribe(m_Fut.Capital);
            //        break;
            //    case SubscribeType.REDIS:
            //        Util.REDIS.Subscribe(PriceLib.Redis.SubscribeType.Future, m_Fut.Redis);
            //        break;
            //    case SubscribeType.IPUSH:
            //        Util.IPUSH.Subscribe(m_Fut.iPush);
            //        break;
            //}
            //m_PreType = stype;
        }
        private void cHead_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
            if (sender == cHead && cHead.Text != m_FutureBase.Head)
            {
                m_FutureBase.Head = cHead.Text;
            }
            if (sender == cY && cY.Text != m_FutureBase.Y) { m_FutureBase.Y = cY.Text; }
            if (sender == cM)
            {
                Month m = (Month)Enum.Parse(typeof(Month), cM.Text);
                if (m != m_FutureBase.M) { m_FutureBase.M = m; }
            }
            */
            if (string.IsNullOrEmpty(cY.Text) || string.IsNullOrEmpty(cM.Text) || string.IsNullOrEmpty(cHead.Text))
            {
                m_Fut = null;
            }
            else
            {
                if (m_Fut != null)
                {
                    SubscribeType stype;
                    Enum.TryParse(cSubscribeType.Text, out stype);
                    _DoUnsubscribe(stype);
                    //switch (stype)
                    //{
                    //    case SubscribeType.PATS:
                    //        Util.PATS.Unsubscribe(m_Fut.PATS);
                    //        break;
                    //    case SubscribeType.CAPITAL:
                    //        Util.OSCAPITAL.Unsubscribe(m_Fut.Capital);
                    //        break;
                    //    case SubscribeType.REDIS:
                    //        Console.WriteLine("cHead_SelectedIndexChanged");
                    //        Util.REDIS.Unsubscribe(PriceLib.Redis.SubscribeType.Future, m_Fut.Redis);
                    //        break;
                    //    case SubscribeType.IPUSH:
                    //        Util.IPUSH.Unsubscribe(m_Fut.iPush);
                    //        break;
                    //}
                }

                m_Fut = Util.QueryFirst<FUT>(FUT.SelSQL, new
                {
                    Y = cY.Text,
                    M = cM.Text,
                    HEAD = cHead.Text
                });
            }

            //cSubscribeType_SelectedIndexChanged(cSubscribeType, EventArgs.Empty);

            _DoSubscribe();
            lblYP.Text = string.Empty;
            //if (!string.IsNullOrEmpty(cYP.Text) && cYP.Text.Trim() != "-1" && cYP.Text.Trim() != "0")
            //{
            cYP_TextChanged(cYP, EventArgs.Empty);
            //}                        
        }
        private void cLock_CheckedChanged(object sender, EventArgs e)
        {
            /*
            if (cLock.Checked != m_FutureBase.LockDown)
            {
                m_FutureBase.LockDown = cLock.Checked;
                cYstNAV_Validated(cYstNAV, EventArgs.Empty);
                cYP_Validated(cYP, EventArgs.Empty);
                cYP.BackColor = cLock.Checked ? Color.LightBlue : Color.White;
                cYstNAV.BackColor = cLock.Checked ? Color.LightBlue : Color.White;
            } 
            */
            cYP.BackColor = cYstNAV.BackColor = cLock.Checked ? Color.LightBlue : Color.White;
        }
        private void cMPRatio_Validated(object sender, EventArgs e)
        {
            /*
            decimal d;
            decimal.TryParse(cMPRatio.Text, out d);
            d /= 100M;
            if (d != m_FutureBase.MPRatio)
            {
                m_FutureBase.MPRatio = d;
            }
            */
        }
        private void cYP_Validated(object sender, EventArgs e)
        {
            /*
            decimal yp;
            decimal.TryParse(cYP.Text, out yp);
            if (m_FutureBase.LockDown)
            {
                if (yp != m_FutureBase.LockYP)
                {
                    m_FutureBase.LockYP = yp;
                }
            }
            else
            {
                if (yp != m_FutureBase.YP)
                {
                    //m_FutureBase.YP = yp;
                    m_FutureBase.SetYP(yp);
                }
            }
            */
        }
        private void cYP_TextChanged(object sender, EventArgs e)
        {
            cYP.InvokeIfRequired(() =>
            {
                double yp, realyp;
                double.TryParse(cYP.Text, out yp);
                double.TryParse(lblYP.Text, out realyp);

                cYP.ForeColor = yp != realyp && yp != 0D && yp != -1D ? Color.Crimson : Color.Black;
            });
        }
        private void cYstNAV_Validated(object sender, EventArgs e)
        {
            /*
            _YstNAVValid();
            */
        }
        private void cYstNAV_TextChanged(object sender, EventArgs e)
        {
            double ystnav, realystnav;
            double.TryParse(cYstNAV.Text, out ystnav);
            double.TryParse(lblYstNAV.Text, out realystnav);

            cYstNAV.ForeColor = ystnav != realystnav && ystnav != 0D && ystnav != -1D ? Color.Crimson : Color.Black;
        }
        private void cChannel_Validated(object sender, EventArgs e)
        {
            /*
            if (cChannel.Text != m_FutureBase.Channel)
            {
                m_FutureBase.Channel = cChannel.Text;
            }
            */
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            SubscribeType stype;
            Enum.TryParse(cSubscribeType.Text, out stype);
            _DoUnsubscribe(stype);
            //switch (stype)
            //{
            //    case SubscribeType.PATS:
            //        Util.PATS.Unsubscribe(m_Fut.PATS);
            //        break;
            //    case SubscribeType.CAPITAL:
            //        Util.OSCAPITAL.Unsubscribe(m_Fut.Capital);
            //        break;
            //    case SubscribeType.REDIS:
            //        Console.WriteLine("btnOK_Click");
            //        Util.REDIS.Unsubscribe(PriceLib.Redis.SubscribeType.Future, m_Fut.Redis);
            //        break;
            //    case SubscribeType.IPUSH:
            //        Util.IPUSH.Unsubscribe(m_Fut.iPush);
            //        break;
            //}

            _ApplyChange();
            DialogResult = DialogResult.OK;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            SubscribeType stype;
            Enum.TryParse(cSubscribeType.Text, out stype);
            _DoUnsubscribe(stype);
            DialogResult = DialogResult.Cancel;
        }

        #region Delegate
        private void OSCAPITAL_OnMktPriceUpdate(MktPrice mkt)
        {
            this.InvokeIfRequired(() =>
            {
                if (m_Fut == null) { return; }
                if (cSubscribeType.Text != SubscribeType.CAPITAL.ToString()) { return; }
                if (mkt.ID != m_Fut.Capital || mkt.YP == -1M || mkt.YP.ToString() == lblYP.Text) { return; }

                lblYP.Text = mkt.YP.ToString();
            });
        }
        private void REDIS_OnMktPriceUpdate(MktPrice mkt)
        {
            try
            {
                this.InvokeIfRequired(() =>
                {
                    if (m_Fut == null) { return; }
                    if (cSubscribeType.Text != SubscribeType.REDIS.ToString()) { return; }
                    if (mkt.ID != m_Fut.Redis || mkt.YP == -1M || mkt.YP.ToString() == lblYP.Text) { return; }

                    lblYP.Text = mkt.YP.ToString();
                });
            }
            catch (Exception)
            { }
        }
        private void IPUSH_OnMktPriceUpdate(MktPrice mkt)
        {
            this.InvokeIfRequired(() =>
            {
                if (m_Fut == null) { return; }
                if (cSubscribeType.Text != SubscribeType.IPUSH.ToString()) { return; }
                if (mkt.ID != m_Fut.iPush || mkt.YP == -1M || mkt.YP.ToString() == lblYP.Text) { return; }

                lblYP.Text = mkt.YP.ToString();
            });
        }
        private void PATS_OnMktPriceUpdate(MktPrice mkt)
        {
            this.InvokeIfRequired(() =>
            {
                if (m_Fut == null) { return; }
                if (cSubscribeType.Text != SubscribeType.PATS.ToString()) { return; }
                if (mkt.ID != m_Fut.PATS || mkt.YP == -1M || mkt.YP.ToString() == lblYP.Text) { return; }

                lblYP.Text = mkt.YP.ToString();
            });
        }
        #endregion

        private void _SetFieldValue()
        {
            cETFCode.Items.AddRange(m_FutureBase.cETFCode.SelItem);
            cETFCode.SelectedItem = m_FutureBase.ETFCode;
            cCalculateType.Items.AddRange(m_FutureBase.cCalculateType.SelItem);
            cCalculateType.SelectedItem = m_FutureBase.CalculateType.ToString();
            cHead.Items.AddRange(m_FutureBase.cHead.SelItem);
            cHead.SelectedItem = m_FutureBase.Head;
            cY.Items.AddRange(m_FutureBase.cY.SelItem);
            cY.SelectedItem = m_FutureBase.Y;
            cM.Items.AddRange(m_FutureBase.cM.SelItem);
            cM.SelectedItem = m_FutureBase.M.ToString();
            cSubscribeType.Items.AddRange(m_FutureBase.cSubscribeType.SelItem);
            cSubscribeType.SelectedItem = m_FutureBase.SubscribeType.ToString();

            cYP.Text = m_FutureBase.YP.ToString();
            cMPRatio.Text = (m_FutureBase.MPRatio * 100M).ToString();
            if (m_FutureBase.ETF != null)
            {
                cYstNAV.Text = m_FutureBase.ETF.YstNAV.ToString();
                lblYstNAV.Text = (m_FutureBase.ETF.FundAssetValue / m_FutureBase.ETF.PublicShares).ToString();
            }
            cChannel.Text = m_FutureBase.Channel;
            cLock.Checked = m_FutureBase.LockDown;
        }
        private void _DoSubscribe()
        {
            if (m_Fut == null) { return; }
            SubscribeType subtype;
            Enum.TryParse(cSubscribeType.Text, out subtype);
            switch (subtype)
            {
                case SubscribeType.PATS:
                    Util.PATS.Subscribe(m_Fut.PATS);
                    break;
                case SubscribeType.CAPITAL:
                    Util.OSCAPITAL.Subscribe(m_Fut.Capital);
                    break;
                case SubscribeType.REDIS:
                    Util.REDIS.Subscribe(PriceLib.Redis.SubscribeType.Future, m_Fut.Redis);
                    break;
                case SubscribeType.IPUSH:
                    Util.IPUSH.Subscribe(m_Fut.iPush);
                    break;
            }
            m_PreType = subtype;
        }
        private void _DoUnsubscribe(SubscribeType preType)
        {
            if (m_Fut == null) { return; }
            switch (preType)
            {
                case SubscribeType.PATS:
                    Util.PATS.Unsubscribe(m_Fut.PATS);
                    break;
                case SubscribeType.CAPITAL:
                    Util.OSCAPITAL.Unsubscribe(m_Fut.Capital);
                    break;
                case SubscribeType.REDIS:
                    Util.REDIS.Unsubscribe(PriceLib.Redis.SubscribeType.Future, m_Fut.Redis);
                    break;
                case SubscribeType.IPUSH:
                    Util.IPUSH.Unsubscribe(m_Fut.iPush);
                    break;
                case SubscribeType.NONE:
                default:
                    break;
            }
        }
        /*
        private void _YstNAVValid()
        {
            decimal ynav;
            decimal.TryParse(cYstNAV.Text, out ynav);

            if (m_FutureBase.ETF != null)
            {
                if (m_FutureBase.LockDown)
                {
                    m_FutureBase.LockYstNAV = ynav;
                    m_FutureBase.ETF.LockYstNAV(ynav);
                }
                else
                {
                    m_FutureBase.ETF.UnlockYstNAV();
                    if (ynav != m_FutureBase.ETF.YstNAV) { m_FutureBase.ETF.YstNAV = ynav; }
                }
            }
        }
        */
    }
}