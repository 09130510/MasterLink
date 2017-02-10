using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net.Config;
using log4net;


namespace PriceLib
{
    public delegate void OnMktPriceUpdateDelegate(MktPrice mkt);
    public class PriceLib
    {
        public event OnMktPriceUpdateDelegate OnMktPriceUpdate;

        protected Dictionary<string, MktPrice> m_Newest = new Dictionary<string, MktPrice>();
        protected ILog m_Log;
        
        public PriceLib()
        {
            XmlConfigurator.Configure(new System.IO.FileInfo("./LogConfig.xml"));
        }

        protected void RaiseMktPrice(MktPrice mkt)
        {
            lock (m_Newest)
            {
                if (!m_Newest.ContainsKey(mkt.ID))
                {
                    m_Newest.Add(mkt.ID, mkt);
                }
                else
                {
                    m_Newest[mkt.ID].Join(mkt);
                }
            }
            if (OnMktPriceUpdate != null && mkt != null && !(mkt.YP == MktPrice.NULLVALUE && mkt.MP == MktPrice.NULLVALUE))
            {
                OnMktPriceUpdate(mkt);
            }

        }
        protected void RaiseMktPrice(string id, decimal yp = -1M, decimal mp = -1M, decimal ap = -1M, decimal bp = -1M, int aq = -1, int bq = -1)
        {
            RaiseMktPrice(new MktPrice(id, yp, mp, ap, bp, aq, bq));
        }
        protected void RaiseMktPrice(string id, double yp = -1, double mp = -1, double ap = -1, double bp = -1, int aq = -1, int bq = -1)
        {
            RaiseMktPrice(new MktPrice(id, (decimal)yp, (decimal)mp, (decimal)ap, (decimal)bp, aq, bq));
        }
        protected void RaiseMktPrice(string id, string yp = "", string mp = "", string ap = "", string bp = "", string aq = "", string bq = "")
        {
            RaiseMktPrice(new MktPrice(id,
                string.IsNullOrEmpty(yp) ? -1M : decimal.Parse(yp),
                string.IsNullOrEmpty(mp) ? -1M : decimal.Parse(mp),
                string.IsNullOrEmpty(ap) ? -1M : decimal.Parse(ap),
                string.IsNullOrEmpty(bp) ? -1M : decimal.Parse(bp),
                string.IsNullOrEmpty(aq) ? -1 : int.Parse(aq),
                string.IsNullOrEmpty(bq) ? -1 : int.Parse(bq)));
        }
        protected void RaiseMktPrice(string id, int yp = -1, int mp = -1, int ap = -1, int bp = -1, int aq = -1, int bq = -1, double multiplier = 1)
        {
            RaiseMktPrice(new MktPrice(id,
                yp == -1 ? -1M : (decimal)(yp * multiplier),
                mp == -1 ? -1M : (decimal)(mp * multiplier),
                ap == -1 ? -1M : (decimal)(ap * multiplier),
                bp == -1 ? -1M : (decimal)(bp * multiplier),
                aq, bq));
        }
        protected void RaiseMktPrice(string id, int yp = -1, int mp = -1, int ap = -1, int bp = -1, int aq = -1, int bq = -1, int divisor = 1)
        {
            RaiseMktPrice(new MktPrice(id,
                yp == -1 ? -1M : yp / (decimal)divisor,
                mp == -1 ? -1M : mp / (decimal)divisor,
                ap == -1 ? -1M : ap / (decimal)divisor,
                bp == -1 ? -1M : bp / (decimal)divisor,
                aq, bq));
        }
        
    }

    public class MktPrice
    {
        public const decimal NULLVALUE = -1M;
        public const int NULLINT = -1;

        #region Property
        public string ID { get; private set; }
        public TimeSpan TriggerTime { get; private set; }
        public decimal YP { get; private set; }
        public decimal MP { get; private set; }
        public decimal AP { get; private set; }
        public decimal BP { get; private set; }
        public int AQ { get; private set; }
        public int BQ { get; private set; }
        #endregion



        public MktPrice(string id, decimal yp = NULLVALUE, decimal mp = NULLVALUE, decimal ap = NULLVALUE, decimal bp = NULLVALUE, int aq = NULLINT, int bq = NULLINT)
        {
            TriggerTime = new TimeSpan(DateTime.Now.Ticks);
            ID = id;
            YP = yp;
            MP = mp;
            AP = ap;
            BP = bp;
            AQ = aq;
            BQ = bq;

            if ((MP <= 0M || MP == NULLVALUE) && (YP > 0M))
            {
                MP = YP;
            }
        }
        public MktPrice(string id, int yp = NULLINT, int mp = NULLINT, int ap = NULLINT, int bp = NULLINT, int aq = NULLINT, int bq = NULLINT, double multiplier = 1)
            : this(id,
            yp == -1 ? NULLVALUE : (decimal)(yp * multiplier),
            mp == -1 ? NULLVALUE : (decimal)(mp * multiplier),
            ap == -1 ? NULLVALUE : (decimal)(ap * multiplier),
            bp == -1 ? NULLVALUE : (decimal)(bp * multiplier),
            aq, bq) { }

        public MktPrice(string id, int yp = NULLINT, int mp = NULLINT, int ap = NULLINT, int bp = NULLINT, int aq = NULLINT, int bq = NULLINT, int divisor = 1)
            : this(id,
            yp == -1 ? NULLVALUE : ((decimal)yp / (decimal)divisor),
            mp == -1 ? NULLVALUE : ((decimal)mp / (decimal)divisor),
            ap == -1 ? NULLVALUE : ((decimal)ap / (decimal)divisor),
            bp == -1 ? NULLVALUE : ((decimal)bp / (decimal)divisor),
            aq, bq) { }

        public void Join(MktPrice mkt)
        {
            if (mkt.ID != this.ID) { return; }
            //2016/06/08 為了Capital清盤時間不 固定試著改一下
            //if (this.YP == 0 || this.YP == NULLVALUE) { this.YP = mkt.YP; }
            if (mkt.YP != 0M && mkt.YP != NULLVALUE) { YP = mkt.YP; }
            if (TriggerTime < mkt.TriggerTime)
            {
                if (mkt.MP != 0M && mkt.MP != NULLVALUE) { MP = mkt.MP; }
                if (mkt.AP != 0M && mkt.AP != NULLVALUE) { AP = mkt.AP; }
                if (mkt.BP != 0M && mkt.BP != NULLVALUE) { BP = mkt.BP; }
                if (mkt.AQ != 0M && mkt.AQ != NULLINT) { AQ = mkt.AQ; }
                if (mkt.BQ != 0M && mkt.BQ != NULLINT) { BQ = mkt.BQ; }
            }
        }
    }
}