#define SUDPIDMODIFY

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PriceLib.Redis;
using PriceLib.Capital;
using PriceLib.PATS;
using System.Threading;
using PriceLib;
using System.Diagnostics;
using SourceCell;
using SourceGrid;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.ComponentModel;
using static PriceLib.Capital.OSCapitalLib;

namespace PriceCalculator
{
    public class FutureBase : NotifyableClass, IDisposable
    {
        public const char MAINSPLIT = '|';
        public const char SUBSPLIT = ';';


        #region Variable
        private Guid Identity = Guid.NewGuid();
        private bool m_disposed = false;
        private FutBaseCalcType m_CalculateType;
        private SubscribeType m_SubscribeType;
        private decimal m_MPRatio = 0;
        private string m_Channel;
        private decimal m_NAV = 0M;
        private string m_Head = string.Empty;
        private string m_Y = DateTime.Now.Year.ToString();
        private Month m_M = (Month)Enum.Parse(typeof(Month), DateTime.Now.Month.ToString());
        private FUT m_Fut;
        private decimal m_YP = MktPrice.NULLVALUE;
        private decimal m_MP = MktPrice.NULLVALUE;
        private decimal m_AP = MktPrice.NULLVALUE;
        private decimal m_BP = MktPrice.NULLVALUE;
        private int m_AQ = 0;
        private int m_BQ = 0;
        private bool m_LockDown = false;
        private decimal m_LockYP = -1;
        private decimal m_LockYstNAV = -1;
        #endregion

        #region Property
        public ETF ETF { get; private set; }
        public FUT FUT { get { return m_Fut; } }
        public string ETFCode
        {
            get { return ETF == null ? string.Empty : ETF.ETFCode; }
            set
            {
                if (ETF != null)
                {
                    if (value== ETF.ETFCode)
                    {
                        return;
                    }
                    if (c_YstNAV != null) { c_YstNAV.RemoveDataBinding(); }
                    _ChannelRemove(ETF.ETFCode, Channel);
                }
                ETF = Util.QueryFirst<ETF>(ETF.SELECTONE, new { ETFCode = value });

                //if (ETF != null) { cYstNAV.SetValue(ETF.YstNAV); }
                if (ETF != null && c_YstNAV != null)
                {
                    c_YstNAV.SetDataBinding(typeof(ETF), "YstNAV", ETF);
                    c_YstNAV.SetValue(ETF.YstNAV);

                }
#if SUDPIDMODIFY
                cETFCode.SetValue(ETFCode);
#endif
                if (!_ChannelConfirm(ETFCode, Channel)) { cETFCode.SetValue(string.Empty); }
                _Calc();
            }
        }
        public FutBaseCalcType CalculateType
        {
            get { return m_CalculateType; }
            set
            {
                if (m_CalculateType == value) { return; }
                m_CalculateType = value;
#if SUDPIDMODIFY
                cCalculateType.SetValue(m_CalculateType);
                _Calc();
#endif
            }
        }
        public SubscribeType SubscribeType
        {
            get { return m_SubscribeType; }
            set
            {
                if (value == m_SubscribeType) { return; }
                _Unsubscribe();
                _Clear(m_SubscribeType);
                m_SubscribeType = value;
                _Subsecibe();
#if SUDPIDMODIFY
                cSubscribeType.SetValue(m_SubscribeType);
#endif
            }
        }

        public decimal MPRatio
        {
            get { return m_MPRatio; }
            set
            {
                if (value > 1M || value < 0M) { return; }
                m_MPRatio = value;
                _Calc();
#if SUDPIDMODIFY
                cMPRatio.SetValue((double)m_MPRatio);
#endif
            }
        }
        public string Channel
        {
            get { return m_Channel; }
            set
            {
                if (value == m_Channel) { return; }
                _ChannelRemove(ETFCode, m_Channel);

                m_Channel = value;
                if (!_ChannelConfirm(ETFCode, m_Channel)) { Channel = string.Empty; }
#if SUDPIDMODIFY
                cChannel.SetValue(m_Channel);
#else
                OnPropertyChanged("Channel");
#endif                
            }
        }
        public decimal NAV
        {
            get { return m_NAV; }
            private set
            {
                if (value == m_NAV) { return; }
                m_NAV = value;
                OnPropertyChanged(nameof(NAV));
            }
        }
        public string Head
        {
            get { return m_Head; }
#if SUDPIDMODIFY
            //cHead.SetValue(m_Head);
#else
            set
            {
                if (value == m_Head) { return; }
                m_Head = value;
                _CreateFuture();                
                OnPropertyChanged("Head");
        }
#endif
        }
        public string Y
        {
            get { return m_Y; }
#if SUDPIDMODIFY
            //cY.SetValue(m_Y);
#else
            set
            {
                if (value == m_Y) { return; }
                m_Y = value;
                _CreateFuture();
                OnPropertyChanged("Y");
        }
#endif
        }
        public Month M
        {
            get { return m_M; }
#if SUDPIDMODIFY
            //cM.SetValue(m_M);
#else
            set
            {
                if (value == m_M) { return; }
                m_M = value;
                _CreateFuture();
                OnPropertyChanged("M");
        }
#endif
        }
        public void SetFuture(string head, string y, Month m)
        {
            m_Head = head;
            cHead.SetValue(m_Head);
            m_Y = y;
            cY.SetValue(m_Y);
            m_M = m;
            cM.SetValue(m_M);

            _CreateFuture();
        }
        public decimal YP
        {
            get { return LockDown ? LockYP : m_YP; }
            set
            {
                if (m_YP == MktPrice.NULLVALUE || m_YP == 0M)
                {
                    m_YP = value;
                    cYP.SetValue(YP);
                }
                if (LockDown && (LockYP == MktPrice.NULLVALUE || LockYP == 0M))
                {
                    LockYP = value;
                    cYP.SetValue(YP);
                }

            }
        }
        public void SetYP(decimal yp)
        {
            m_YP = yp;
            cYP.SetValue(YP);
        }
        public decimal MP
        {
            get { return m_MP; }
            set
            {
                if (value == m_MP) { return; }
                m_MP = value;
                OnPropertyChanged(nameof(MP));
            }
        }
        public decimal AP
        {
            get { return m_AP; }
            set
            {
                if (value == m_AP) { return; }
                m_AP = value;
                OnPropertyChanged(nameof(AP));
            }
        }
        public decimal BP
        {
            get { return m_BP; }
            set
            {
                if (value == m_BP) { return; }
                m_BP = value;
                OnPropertyChanged(nameof(BP));
            }
        }
        public int AQ
        {
            get { return m_AQ; }
            set
            {
                if (value == m_AQ) { return; }
                m_AQ = value;
                OnPropertyChanged(nameof(AQ));
            }
        }
        public int BQ
        {
            get { return m_BQ; }
            set
            {
                if (value == m_BQ) { return; }
                m_BQ = value;
                OnPropertyChanged(nameof(BQ));
            }
        }
        public decimal AvgP { get; private set; }
        public decimal S { get; private set; }

        public bool LockDown
        {
            get { return m_LockDown; }
            set
            {
                if (value == m_LockDown) { return; }
                m_LockDown = value;
                cYP.SetBackColor(m_LockDown ? Color.LightBlue : cYP.DefaultBackColor);
                cYstNAV.SetBackColor(m_LockDown ? Color.LightBlue : cYstNAV.DefaultBackColor);
                //cYP.BackColor = m_LockDown ? Color.LightBlue : cYP.DefaultBackColor;
                //cYstNAV.BackColor = m_LockDown ? Color.LightBlue : cYstNAV.DefaultBackColor;
                if (m_LockDown)
                {
                    LockYP = m_YP;
                    LockYstNAV = (decimal)cYstNAV.Value;
                    if (ETF != null) { ETF.LockYstNAV(LockYstNAV); }
                }
                else
                {
                    if (ETF != null) { ETF.UnlockYstNAV(); }
                }
#if !SUDPIDMODIFY
                OnPropertyChanged("LockDown");
#else
                cLock.SetChecked(m_LockDown);
#endif


            }
        }
        //public decimal LockYP { get; private set; }
        //public decimal LockYstNAV { get; private set; }
        public decimal LockYP
        {
            get { return m_LockYP; }
            set
            {
                if (value == m_LockYP) { return; }
                m_LockYP = value;
#if !SUDPIDMODIFY                
#else
                cYP.SetValue(YP);
#endif
            }
        }
        public decimal LockYstNAV
        {
            get { return m_LockYstNAV; }
            set
            {
                if (value == m_LockYstNAV) { return; }
                m_LockYstNAV = value;
#if !SUDPIDMODIFY                
#else
                if (ETF != null)
                {
                    cYstNAV.SetValue(ETF.YstNAV);
                }
#endif
            }
        }
        #endregion

        #region Cell
        private List<CellBase> m_CellList;
        private ComboBoxCell c_ETFCode;
        private ComboBoxCell c_CalculateType;
        private ComboBoxCell c_SubscribeType;
        private ComboBoxCell c_Head;
        private ComboBoxCell c_Y;
        private ComboBoxCell c_M;
        private TextCell c_YP;
        private TextCell c_MP;
        private TextCell c_AP;
        private TextCell c_BP;
        private TextCell c_AQ;
        private TextCell c_BQ;
        private TextCell c_MPRatio;
        private TextCell c_Channel;
        private TextCell c_NAV;
        private TextCell c_YstNAV;
        private CheckBoxCell c_Lock;

        public List<CellBase> CellList
        {
            get
            {
                if (m_CellList == null)
                {
                    m_CellList = typeof(FutureBase).GetProperties().Where(e => e.isCellProperty()).OrderBy(e => e.Seqno()).Select(e => (CellBase)e.GetValue(this, null)).ToList();
                }
                return m_CellList;
            }
        }
        [DisplayCell("ETF", 0)]
        public ComboBoxCell cETFCode
        {
            get
            {
                if (c_ETFCode == null)
                {
                    c_ETFCode = new ComboBoxCell()
                    {
#if !SUDPIDMODIFY
                        Enable = true,
                        EditMode = EditableMode.SingleClick | EditableMode.AnyKey,
                        //EditMode = EditableMode.DoubleClick| EditableMode.F2Key,
#endif
                        IsExclusive = true,
                        SelItem = Util.Query<ETF>(ETF.SELECTALL).Select(e => e.ETFCode).ToArray(),
                        TextAlignment = DevAge.Drawing.ContentAlignment.MiddleLeft,
                        FontName = CellBase.FontName.Verdana,
                        Name = nameof(cETFCode),
                        Value = ETFCode,
                        Tag = this
                    };

#if !SUDPIDMODIFY
                    c_ETFCode.OnValueChanged += (cell, e) =>
                    {
                        ETFCode = cell.Cell.Value.ToString();
                        //if (ETF != null) { cYstNAV.SetValue(ETF.YstNAV); }
                    };
#else
                    c_ETFCode.OnDoubleClick += OnModity;
#endif
                }
                return c_ETFCode;
            }
        }
        [DisplayCell("方式", 1)]
        public ComboBoxCell cCalculateType
        {
            get
            {
                if (c_CalculateType == null)
                {
                    c_CalculateType = new ComboBoxCell()
                    {
#if !SUDPIDMODIFY
                        Enable = true,
                        EditMode = EditableMode.SingleClick | EditableMode.AnyKey,
                        //EditMode = EditableMode.DoubleClick | EditableMode.F2Key,
#endif
                        IsExclusive = true,
                        SelItem = Enum.GetNames(typeof(FutBaseCalcType)),
                        DropDownStyle = ComboBoxStyle.DropDownList,
                        TextAlignment = DevAge.Drawing.ContentAlignment.MiddleLeft,
                        FontName = CellBase.FontName.Verdana,
                        Name = nameof(cCalculateType),
                        Value = CalculateType.ToString(),
                        Tag = this
                    };
#if !SUDPIDMODIFY
                    c_CalculateType.OnValueChanged += (cell, e) =>
                    {
                        CalculateType = (FutBaseCalcType)Enum.Parse(typeof(FutBaseCalcType), cell.Cell.Value.ToString());
                        _Calc();
                    };
#else
                    c_CalculateType.OnDoubleClick += OnModity;
#endif

                }
                return c_CalculateType;
            }
        }
        [DisplayCell("來源", 2)]
        public ComboBoxCell cSubscribeType
        {
            get
            {
                if (c_SubscribeType == null)
                {
                    c_SubscribeType = new ComboBoxCell()
                    {
#if !SUDPIDMODIFY
                        Enable = true,
                        EditMode = EditableMode.SingleClick | EditableMode.AnyKey,
                        //EditMode = EditableMode.DoubleClick | EditableMode.F2Key,
#endif
                        IsExclusive = true,
                        SelItem = Enum.GetNames(typeof(SubscribeType)),
                        DropDownStyle = ComboBoxStyle.DropDownList,
                        TextAlignment = DevAge.Drawing.ContentAlignment.MiddleLeft,
                        FontName = CellBase.FontName.Verdana,
                        Name = nameof(cSubscribeType),
                        Value = SubscribeType.ToString(),
                        Tag = this
                    };

#if !SUDPIDMODIFY
                    c_SubscribeType.OnValueChanged += (cell, e) =>
                    {
                        SubscribeType = (SubscribeType)Enum.Parse(typeof(SubscribeType), cell.Cell.Value.ToString());
                    };
#else
                    c_SubscribeType.OnDoubleClick += OnModity;
#endif
                }
                return c_SubscribeType;
            }
        }
        [DisplayCell("期貨", 3)]
        public ComboBoxCell cHead
        {
            get
            {
                if (c_Head == null)
                {
                    c_Head = new ComboBoxCell()
                    {
#if !SUDPIDMODIFY
                        Enable = true,
                        EditMode = EditableMode.SingleClick | EditableMode.AnyKey,
                        //EditMode = EditableMode.DoubleClick | EditableMode.F2Key,
#endif
                        IsExclusive = true,
                        SelItem = Util.Query<string>(FUT.ListSQL).ToArray(),
                        TextAlignment = DevAge.Drawing.ContentAlignment.MiddleLeft,
                        FontName = CellBase.FontName.Verdana,
                        //DropDownStyle = ComboBoxStyle.DropDownList,
                        Name = nameof(cHead),
                        Value = Head,
                        Tag = this
                    };
#if !SUDPIDMODIFY
                    c_Head.SetDataBinding(typeof(FutureBase), "Head", this);
#else
                    c_Head.OnDoubleClick += OnModity;
#endif
                }
                return c_Head;
            }
        }
        [DisplayCell("年", 4)]
        public ComboBoxCell cY
        {
            get
            {
                if (c_Y == null)
                {
                    int year = DateTime.Now.Year;
                    c_Y = new ComboBoxCell()
                    {
#if !SUDPIDMODIFY
                        Enable = true,
                        EditMode = EditableMode.SingleClick | EditableMode.AnyKey,
                        //EditMode = EditableMode.DoubleClick | EditableMode.F2Key,
#endif
                        IsExclusive = true,
                        //SelItem = new string[]{DateTime.Now.Year.ToString(), 
                        //                       DateTime.Now.AddYears(1).Year.ToString()},
                        SelItem = new[] { (year++).ToString(), (year++).ToString(), (year++).ToString(), (year++).ToString(), (year++).ToString() },
                        //DropDownStyle = ComboBoxStyle.DropDownList,
                        FontName = CellBase.FontName.Verdana,
                        Name = nameof(cY),
                        Value = Y,
                        Tag = this
                    };
#if !SUDPIDMODIFY
                    c_Y.SetDataBinding(typeof(FutureBase), "Y", this);
#else
                    c_Y.OnDoubleClick += OnModity;
#endif
                }
                return c_Y;
            }
        }
        [DisplayCell("月", 5)]
        public ComboBoxCell cM
        {
            get
            {
                if (c_M == null)
                {
                    c_M = new ComboBoxCell()
                    {
#if !SUDPIDMODIFY
                        Enable = true,
                        EditMode = EditableMode.SingleClick | EditableMode.AnyKey,
                        //EditMode = EditableMode.DoubleClick | EditableMode.F2Key,
#endif
                        IsExclusive = true,
                        SelItem = Enum.GetNames(typeof(Month)),
                        //DropDownStyle = ComboBoxStyle.DropDownList,
                        //SelItem  = ((IEnumerable<Month>)Enum.GetValues(typeof(Month))).Select(e=>((int)e).ToString().PadLeft(2,'0')).ToArray(),
                        FontName = CellBase.FontName.Verdana,
                        Name = nameof(cM),
                        Value = M.ToString(),
                        Tag = this
                    };

#if !SUDPIDMODIFY
                    c_M.OnValueChanged += (cell, e) =>
                    {
                        M = (Month)Enum.Parse(typeof(Month), cell.Cell.Value.ToString());
                    };
#else
                    c_M.OnDoubleClick += OnModity;
#endif
                }
                return c_M;
            }
        }
        [DisplayCell("昨收", 6)]
        public TextCell cYP
        {
            get
            {
                if (c_YP == null)
                {
                    c_YP = new TextCell()
                    {
#if !SUDPIDMODIFY
                        Enable = true,
                        EditMode = EditableMode.SingleClick| EditableMode.AnyKey,
                        //EditMode = EditableMode.DoubleClick | EditableMode.F2Key,
#endif
                        CellType = TextCell.TextType.Decimal,
                        Format = "#,###.####",
                        Name = nameof(cYP),
                        Value = YP,
                        Tag = this
                    };
#if !SUDPIDMODIFY
                    c_YP.OnEditEnded += (cell, e) =>
                    {
                        m_YP = Convert.ToDecimal(cell.Cell.Value);
                        if (LockDown) { LockYP = m_YP; }
                        _Calc();
                    };
#else
                    c_YP.OnDoubleClick += OnModity;
#endif
                }
                return c_YP;
            }
        }
        [DisplayCell("成交", 7)]
        public TextCell cMP
        {
            get
            {
                if (c_MP == null)
                {
                    c_MP = new TextCell()
                    {
                        CellType = TextCell.TextType.Decimal,
                        Format = "#,###.####",
                        BackColor = Color.Silver,
                        Value = MP,
                        Tag = this
                    };
                    c_MP.SetDataBinding(typeof(FutureBase), nameof(MP), this);
                }
                return c_MP;
            }
        }
#if DEBUG
        [DisplayCell("賣", 8.2)]
#endif
        public TextCell cAP
        {
            get
            {
                if (c_AP == null)
                {
                    c_AP = new TextCell()
                    {
                        CellType = TextCell.TextType.Decimal,
                        Format = "#,###.####",
                        BackColor = Color.Silver,
                        FontColor = Color.Green,
                        Value = AP,
                        Tag = this
                    };
                    c_AP.SetDataBinding(typeof(FutureBase), nameof(AP), this);
                }
                return c_AP;
            }
        }
#if DEBUG
        [DisplayCell("量", 8.2)]
#endif
        public TextCell cAQ
        {
            get
            {
                if (c_AQ == null)
                {
                    c_AQ = new TextCell()
                    {
                        CellType = TextCell.TextType.Int,
                        BackColor = Color.Silver,
                        FontColor = Color.Green,
                        Value = AQ,
                        Tag = this
                    };
                    c_AQ.SetDataBinding(typeof(FutureBase), nameof(AQ), this);
                }
                return c_AQ;
            }
        }
#if DEBUG
        [DisplayCell("買", 8)]
#endif
        public TextCell cBP
        {
            get
            {
                if (c_BP == null)
                {
                    c_BP = new TextCell()
                    {
                        CellType = TextCell.TextType.Decimal,
                        Format = "#,###.####",
                        BackColor = Color.Silver,
                        FontColor = Color.Crimson,
                        Value = BP,
                        Tag = this
                    };
                    c_BP.SetDataBinding(typeof(FutureBase), nameof(BP), this);
                }
                return c_BP;
            }
        }
#if DEBUG
        [DisplayCell("量", 8)]
#endif
        public TextCell cBQ
        {
            get
            {
                if (c_BQ == null)
                {
                    c_BQ = new TextCell()
                    {
                        CellType = TextCell.TextType.Int,
                        BackColor = Color.Silver,
                        FontColor = Color.Crimson,
                        Value = BQ,
                        Tag = this
                    };
                    c_BQ.SetDataBinding(typeof(FutureBase), nameof(BQ), this);
                }
                return c_BQ;
            }
        }
        [DisplayCell("比率", 9)]
        public TextCell cMPRatio
        {
            get
            {
                if (c_MPRatio == null)
                {
                    c_MPRatio = new TextCell()
                    {
#if !SUDPIDMODIFY
                        Enable = true,
                        EditMode = EditableMode.SingleClick | EditableMode.AnyKey,
                        //EditMode = EditableMode.DoubleClick | EditableMode.F2Key,
#endif
                        CellType = TextCell.TextType.Percent,
                        Name = nameof(cMPRatio),
                        Value = (double)MPRatio,
                        Tag = this
                    };

#if !SUDPIDMODIFY
                    c_MPRatio.OnValueChanging += (cell, e) =>
                    {
                        double n = Convert.ToDouble(e.NewValue);
                        if (n > 1 || n < 0)
                        {
                            double o = Convert.ToDouble(e.OldValue);
                            e.NewValue = (o > 1 || o < 0) ? 0 : o;
                        }
                    };
                    c_MPRatio.OnValueChanged += (cell, e) =>
                    {
                        MPRatio = Convert.ToDecimal(cell.Cell.Value);
                    };
#else
                    c_MPRatio.OnDoubleClick += OnModity;
#endif
                }
                return c_MPRatio;
            }
        }
        [DisplayCell("頻道", 11)]
        public TextCell cChannel
        {
            get
            {
                if (c_Channel == null)
                {
                    c_Channel = new TextCell()
                    {
#if !SUDPIDMODIFY
                        Enable = true,
                        EditMode = EditableMode.SingleClick | EditableMode.AnyKey,
                        //EditMode = EditableMode.DoubleClick | EditableMode.F2Key,
#endif
                        CellType = TextCell.TextType.String,
                        FontName = CellBase.FontName.Verdana,
                        Name = nameof(cChannel),
                        Value = Channel,
                        Tag = this
                    };

#if !SUDPIDMODIFY
                    c_Channel.SetDataBinding(typeof(FutureBase), "Channel", this);
#else
                    c_Channel.OnDoubleClick += OnModity;
#endif
                }
                return c_Channel;
            }
        }

        [DisplayCell("NAV", 10)]
        public TextCell cNAV
        {
            get
            {
                if (c_NAV == null)
                {
                    c_NAV = new TextCell()
                    {
                        CellType = TextCell.TextType.Decimal,
                        Format = "#,###.####",
                        BackColor = Color.Silver,
                        Value = NAV,
                        Tag = this
                    };
                    c_NAV.SetDataBinding(typeof(FutureBase), nameof(NAV), this);
                }
                return c_NAV;
            }
        }
        [DisplayCell("昨日NAV", 9)]
        public TextCell cYstNAV
        {
            get
            {
                if (c_YstNAV == null)
                {
                    c_YstNAV = new TextCell()
                    {
                        CellType = TextCell.TextType.Decimal,
                        Format = "#,###.####",
#if !SUDPIDMODIFY
                        Enable = true,
                        EditMode = EditableMode.SingleClick| EditableMode.AnyKey,
#endif
                        Name = nameof(cYstNAV),
                        Value = ETF == null ? 0M : ETF.YstNAV,
                        Tag = this
                    };

                    if (ETF != null)
                    {
                        if (LockDown) { ETF.LockYstNAV(LockYstNAV); }
                        else
                        {
                            ETF.UnlockYstNAV();
                        }
                        c_YstNAV.SetDataBinding(typeof(ETF), "YstNAV", ETF);
                    }

#if !SUDPIDMODIFY
                    c_YstNAV.OnValueChanged += (cell, e) =>
                    {
                        if (ETF != null)
                        {
                            if (LockDown)
                            {
                                LockYstNAV = Convert.ToDecimal(cell.Cell.Value.ToString());
                                ETF.LockYstNAV(LockYstNAV);
                            }
                            else
                            {
                                ETF.YstNAV = Convert.ToDecimal(cell.Cell.Value.ToString());
                            }
                            _Calc();
                        }
                    };
#else
                    c_YstNAV.OnDoubleClick += OnModity;
#endif
                }
                return c_YstNAV;
            }
        }
        [DisplayCell("", 12)]
        public CheckBoxCell cLock
        {
            get
            {
                if (c_Lock == null)
                {
                    c_Lock = new CheckBoxCell()
                    {
#if !SUDPIDMODIFY
                        Enable = true,
#endif
                        Checked = LockDown,
                        CheckBoxAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter,
                        Name = nameof(cLock),
                        Tag = this
                    };

#if !SUDPIDMODIFY
                    c_Lock.SetDataBinding(typeof(FutureBase), "LockDown", this);
#else
                    c_Lock.OnDoubleClick += OnModity;
#endif
                }
                return c_Lock;
            }
        }
        #endregion

        public FutureBase(string etfcode)
        {
            this.ETFCode = etfcode;

            Util.REDIS.OnMktPriceUpdate += new OnMktPriceUpdateDelegate(REDIS_OnMktPriceUpdate);
            Util.OSCAPITAL.OnMktPriceUpdate += new OnMktPriceUpdateDelegate(OSCAPITAL_OnMktPriceUpdate);
            Util.IPUSH.OnMktPriceUpdate += new OnMktPriceUpdateDelegate(IPUSH_OnMktPriceUpdate);
            Util.PATS.OnMktPriceUpdate += new OnMktPriceUpdateDelegate(PATS_OnMktPriceUpdate);
        }
        public FutureBase(string[] items)
            : this(items[0])
        {
            string head = string.Empty, y = string.Empty;
            Month m = Month.JAN;
            for (int i = 1; i < items.Length; i++)
            {
                if (i == 1) { CalculateType = (FutBaseCalcType)Enum.Parse(typeof(FutBaseCalcType), items[i]); }
                if (i == 2) { SubscribeType = (SubscribeType)Enum.Parse(typeof(SubscribeType), items[i]); }
#if SUDPIDMODIFY
                
                if (i == 3) { head = items[i]; }
                if (i == 4) { y = items[i]; }
                if (i == 5) { m = (Month)Enum.Parse(typeof(Month), items[i]); }
                SetFuture(head, y, m);
#else
                if (i == 3) { Head = items[i]; }
                if (i == 4) { Y = items[i]; }
                if (i == 5) { M = (Month)Enum.Parse(typeof(Month), items[i]); }
#endif
                if (i == 6) { MPRatio = items[i].Convert<decimal>(); }
                if (i == 7) { Channel = items[i]; }
                if (i == 8) { LockDown = items[i].Convert<bool>(); }
                if (i == 9)
                {
                    LockYP = items[i].Convert<decimal>();
                    if (m_LockDown) { YP = LockYP; }
                }
                if (i == 10)
                {
                    LockYstNAV = items[i].Convert<decimal>();
                    if (ETF != null)
                    {
                        ETF.LockYstNAV(LockYstNAV);
                        ETF.YstNAV = LockYstNAV;
                    }
                }
            }
        }

#region Delegate
        private void OSCAPITAL_OnMktPriceUpdate(MktPrice mkt)
        {
            if (m_Fut == null) { return; }
            if (SubscribeType != SubscribeType.CAPITAL) { return; }
            if (mkt.ID != m_Fut.Capital) { return; }
            _PriceSetting(mkt);
            _Calc();
        }
        private void REDIS_OnMktPriceUpdate(MktPrice mkt)
        {
            if (m_Fut == null) { return; }
            if (SubscribeType != SubscribeType.REDIS) { return; }
            if (mkt.ID != m_Fut.Redis) { return; }
            _PriceSetting(mkt);
            _Calc();
        }
        private void IPUSH_OnMktPriceUpdate(MktPrice mkt)
        {
            if (m_Fut == null) { return; }
            if (SubscribeType != SubscribeType.IPUSH) { return; }
            if (mkt.ID != m_Fut.iPush) { return; }
            _PriceSetting(mkt);
            _Calc();
        }
        private void PATS_OnMktPriceUpdate(MktPrice mkt)
        {
            if (m_Fut == null) { return; }
            if (SubscribeType != SubscribeType.PATS) { return; }
            if (mkt.ID != m_Fut.PATS) { return; }
            _PriceSetting(mkt);
            _Calc();
        }
        private void OnModity(CellBase cell, EventArgs e)
        {
            frmModify modify = new frmModify($"修改 - [{Channel}.{ETFCode}]", cell.Name, this);
            Util.FUTBaseForm.InvokeIfRequired(() =>
            {
                if (modify.ShowDialog(Util.FUTBaseForm)== DialogResult.OK)
                {
                    ChangeYstNAV(modify.ChgYstNAV);
                } 
                //modify.Show(Util.FUTBaseForm);
            });
        }
#endregion

#region Private
        private void _CreateFuture()
        {
            _Unsubscribe();
            if (string.IsNullOrEmpty(Y) || string.IsNullOrEmpty(Head)) { return; }
            m_Fut = Util.QueryFirst<FUT>(FUT.SelSQL, new { Y = Y, M = M, HEAD = Head });
            //m_YP = MktPrice.NULLVALUE;
            //NAV = 0;
            //_PriceSetting(new MktPrice(m_Fut.Capital, -1M));
            _Clear(SubscribeType);
            _Calc();
            _Subsecibe();
        }
        private void _Subsecibe()
        {
            if (m_Fut == null) { return; }
            switch (SubscribeType)
            {       
                case SubscribeType.CAPITAL:
                    try
                    {
                        Util.OSCAPITAL.Subscribe(m_Fut.Capital);
                    }
                    catch (CapitalException ex)
                    {
                        AlertBox.AlertWithoutReply(Util.MainForm, AlertBoxButton.Error_OK, "Capital", ex.Message);
                    }
                    break;
                case SubscribeType.PATS:
                    Util.PATS.Subscribe(m_Fut.PATS);
                    break;
                case SubscribeType.REDIS:
                    Util.REDIS.Subscribe(PriceLib.Redis.SubscribeType.Future, m_Fut.Redis);
                    break;
                case SubscribeType.IPUSH:
                    Util.IPUSH.Subscribe(m_Fut.iPush);
                    break;
                case SubscribeType.NONE:
                default:
                    break;
            }
        }
        private void _Unsubscribe()
        {
            try
            {
                if (m_Fut == null) { return; }
                switch (m_SubscribeType)
                {
                    case SubscribeType.CAPITAL:
                        Util.OSCAPITAL.Unsubscribe(m_Fut.Capital);
                        break;
                    case SubscribeType.PATS:
                        Util.PATS.Unsubscribe(m_Fut.PATS);
                        break;
                    case SubscribeType.REDIS:                        
                        Util.REDIS.Unsubscribe(PriceLib.Redis.SubscribeType.Future, m_Fut.Redis);
                        break;
                    case SubscribeType.IPUSH:
                        Util.IPUSH.Unsubscribe(m_Fut.iPush);
                        break;
                }
            }
            catch (CapitalException ex)
            {
                AlertBox.AlertWithoutReply(Util.MainForm, AlertBoxButton.Error_OK, m_SubscribeType.ToString(), ex.Message);
            }
        }
        private void _PriceSetting(MktPrice mkt)
        {
            if (mkt.YP != MktPrice.NULLVALUE)
            {
                YP = mkt.YP;
            }
            if (mkt.MP != MktPrice.NULLVALUE) { MP = mkt.MP; }
            if (mkt.AP != MktPrice.NULLVALUE) { AP = mkt.AP; }
            if (mkt.BP != MktPrice.NULLVALUE) { BP = mkt.BP; }
            if (mkt.AQ != MktPrice.NULLVALUE) { AQ = mkt.AQ; }
            if (mkt.BQ != MktPrice.NULLVALUE) { BQ = mkt.BQ; }
        }
        private void _Calc()
        {
            if (ETF == null) { return; }
            if (YP <= 0M) { return; }
            if (MPRatio >= 1M)
            {
                //只用市價算
                S = MP;
            }
            else
            {
                int TQ = AQ + BQ;
                if (TQ <= 0) { return; }
                AvgP = BP * AQ / TQ + AP * BQ / TQ;
                if (MP <= 0M)
                {
                    //沒收到市價, 都用均價算
                    S = AvgP;
                }
                else
                {
                    S = MP * MPRatio + AvgP * (1M - MPRatio);
                }
            }
            decimal RISE = (S / YP - 1M);
            switch (CalculateType)
            {
                case FutBaseCalcType.TYPE0:
                    NAV = ETF.YstNAV * (1M + RISE);
                    break;
                case FutBaseCalcType.TYPE1:
                    NAV = ETF.YstNAV * (1M - RISE);
                    break;
                case FutBaseCalcType.TYPE2:
                    NAV = ETF.YstNAV * (1M + 2 * RISE);
                    break;
            }
            Util.Publish(Channel, ETFCode, NAV.ToString());
        }
        private void _Clear(SubscribeType stype)
        {

            m_YP = MktPrice.NULLVALUE;
            NAV = 0M;
            if (m_Fut == null) { return; }
            switch (stype)
            {
                case SubscribeType.CAPITAL:
                    _PriceSetting(new MktPrice(m_Fut.Capital, 0M, 0M, 0M, 0M, 0, 0));
                    break;
                case SubscribeType.PATS:
                    _PriceSetting(new MktPrice(m_Fut.PATS, 0M, 0M, 0M, 0M, 0, 0));
                    break;
                case SubscribeType.REDIS:
                    _PriceSetting(new MktPrice(m_Fut.Redis, 0M, 0M, 0M, 0M, 0, 0));
                    break;
                case SubscribeType.IPUSH:
                    _PriceSetting(new MktPrice(m_Fut.iPush, 0M, 0M, 0M, 0M, 0, 0));
                    break;
            }
        }

        private void _ChannelRemove(string etfcode, string channel)
        {
            //if (Util.CHANNEL.Contains($"{ETFCode}{Channel}"))
            //{
            // if ((Util.CHANNEL?[$"{ETFCode}{m_Channel}"]??Guid.Empty)==Identity)
            if (Util.CHANNEL.ContainsKey($"{etfcode}{channel}") && Util.CHANNEL[$"{etfcode}{channel}"] == Identity)

            {
                Util.CHANNEL.Remove($"{etfcode}{channel}");
            }
            //}
        }
        private bool _ChannelConfirm(string etfcode, string channel)
        {
            if (string.IsNullOrEmpty(etfcode) || string.IsNullOrEmpty(channel))
            {
                return true;
            }
            if (!Util.CHANNEL.ContainsKey($"{etfcode}{channel}"))
            {
                Util.CHANNEL.Add($"{etfcode}{channel}", Identity);
            }
            else if (Util.CHANNEL?[$"{etfcode}{channel}"] != Identity)
            {
                return false;
            }
            return true;
        }
#endregion

#region Public        
        public new string ToString()
        {
            return $"{ETFCode}{SUBSPLIT}{CalculateType}{SUBSPLIT}{SubscribeType}{SUBSPLIT}{Head}{SUBSPLIT}{Y}{SUBSPLIT}{M}{SUBSPLIT}{MPRatio}{SUBSPLIT}{Channel}{SUBSPLIT}{LockDown}{SUBSPLIT}{(LockDown ? LockYP : 0)}{SUBSPLIT}{(LockDown ? LockYstNAV : 0)}{SUBSPLIT}";
        }
        public static string[] Headers()
        {
            return typeof(FutureBase).GetProperties().Where(e => e.isCellProperty()).OrderBy(e => e.Seqno()).Select(e => e.Caption()).ToArray();
        }
        public void ChangeYstNAV(decimal ystnav)
        {
            if (ETF == null) { return; }
            if (LockDown)
            {
                LockYstNAV = ystnav;
                ETF.LockYstNAV(ystnav);
            }
            else
            {
                ETF.UnlockYstNAV();
                ETF.YstNAV = ystnav;
            }
        }
        
#endregion

#region IDisposable 成員
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this); //要求系統不要呼叫指定物件的完成項。
        }
        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="IsDisposing"></param>
        protected void Dispose(bool IsDisposing)
        {
            if (m_disposed) return;

            if (IsDisposing) { DoDispose(); }
            m_disposed = true;
        }
        /// <summary>
        /// Do something when disposing
        /// </summary>
        protected virtual void DoDispose()
        {
            if (m_Fut != null)
            {
                //Util.OSCAPITAL.Subscribe(m_Fut.Capital);
                _Unsubscribe();
            }
        }
#endregion
    }
}