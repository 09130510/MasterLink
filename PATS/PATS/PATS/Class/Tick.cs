using PATS.Utility;
using PriceLib.PATS;
using SourceCell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PATS.Class
{
    /// <summary>
    /// 換商品 => 重訂，不重建
    /// 改Expend => 重跑價格
    /// 改TickNumber => 重建
    /// </summary>
    public class Tick : IDisposable
    {
        public readonly int ColCount = 5;

        #region Variable
        private ProductInfo m_ProductInfo = null;
        private bool m_isAscending = true;
        private bool m_isExtend = false;
        #endregion

        #region Property
        public AccountInfo Account { get; set; } = null;
        public ProductInfo ProductInfo
        {
            get { return m_ProductInfo; }
            set
            {
                if (value == m_ProductInfo) { return; }
                if (m_ProductInfo != null && !string.IsNullOrEmpty(m_ProductInfo.Key))
                {
                    Util.PATS.Unsubscribe(m_ProductInfo.Key);
                }

                m_ProductInfo = value;
                _ResetCellValue();
                if (m_ProductInfo == null) { return; }

                Util.PATS.Subscribe(m_ProductInfo.Key);
                string[] ticksize = m_ProductInfo.TickSize.ToString().Split('.');
                ChangeRowPrperty(nameof(Row.PriceFormat), "#,##0.".PadRight(6 + (ticksize.Length > 1 ? ticksize[1].Length : 0), '0'));
                PATS_OnPriceUpdate(m_ProductInfo.Key, Util.PATS.Price(m_ProductInfo.Index));
            }
        }

        public int TickNumber { get; }
        public int RowCount { get { return TickNumber * 2 + 4; } }
        private double TheMaxTick
        {
            get
            {
                if (TickNumber == 0)
                {
                    return (double)this[RowGenre.MP, ColGenre.Price];
                }
                return (double)this[RowGenre.AP, ColGenre.Price, TickNumber - 1];
            }
        }
        private double TheMinTick
        {
            get
            {
                if (TickNumber == 0)
                {
                    return (double)this[RowGenre.MP, ColGenre.Price];
                }
                return (double)this[RowGenre.BP, ColGenre.Price, 0];
            }
        }
        public bool isAscending
        {
            get { return m_isAscending; }
            set
            {
                if (value == m_isAscending) { return; }
                m_isAscending = value;
                if (ProductInfo == null || string.IsNullOrEmpty(ProductInfo.Exch)) { return; }
                //PATS_OnPriceUpdate(ProductInfo.Key, (PriceStruct)Util.PATS.Price(ProductInfo.Exch, ProductInfo.Commodity, ProductInfo.Date));
                Util.PATS.RefreshPrice(ProductInfo.Exch, ProductInfo.Commodity, ProductInfo.Date);
            }
        }
        public bool isExtend
        {
            get { return m_isExtend; }
            set
            {
                if (value == m_isExtend) { return; }
                m_isExtend = value;
                if (ProductInfo == null || string.IsNullOrEmpty(ProductInfo.Exch)) { return; }
                //PATS_OnPriceUpdate(ProductInfo.Key, (PriceStruct)Util.PATS.Price(ProductInfo.Exch, ProductInfo.Commodity, ProductInfo.Date));
                Util.PATS.RefreshPrice(ProductInfo.Exch, ProductInfo.Commodity, ProductInfo.Date);
            }
        }

        public Dictionary<RowGenre, List<Row>> CellList { get; private set; }
        public object this[RowGenre row, ColGenre col, int index = -1]
        {
            get
            {
                CellBase cell = null;
                switch (row)
                {
                    case RowGenre.Functional:
                    case RowGenre.Statistics:
                    case RowGenre.MP:
                        cell = CellList[row].First()[col];
                        return cell is TextCell ? ((TextCell)cell).Value : 0;
                    case RowGenre.BP:
                    case RowGenre.AP:
                        if (index == -1) { return null; }
                        cell = CellList[row][index][col];
                        return cell is TextCell ? ((TextCell)cell).Value : 0;
                    default:
                    case RowGenre.Header:
                        return 0;
                }
            }
            set
            {
                switch (row)
                {
                    case RowGenre.Header:
                    case RowGenre.Functional:
                    case RowGenre.Statistics:
                    case RowGenre.MP:
                    default:
                        CellList[row].First().SetValue(col, value);
                        break;
                    case RowGenre.BP:
                    case RowGenre.AP:
                        if (index == -1)
                        {
                            return;
                        }
                        else if (index == 999)
                        {
                            for (int i = 0; i < TickNumber; i++)
                            {
                                CellList[row][i].SetValue(col, value);
                            }
                        }
                        else
                        {
                            CellList[row][index].SetValue(col, value);
                        }
                        break;
                }
            }
        }
        #endregion

        public Tick(int tickNumber, bool extend = false, bool ascending = true, float fontsize = 8)
        {
            TickNumber = tickNumber;
            isExtend = extend;
            isAscending = ascending;
            InitCell();
            ChangeRowPrperty(nameof(Row.FontSize), fontsize);
            Center.Instance.AddObserver(_BeforePATSReset, Observer.PATS_BeforeReset);
            Center.Instance.AddObserver(_AfterPATSReset, Observer.PATS_AfterReset);
            _AfterPATSReset(Observer.None, null);
            //Util.PATS.OnPriceUpdate += PATS_OnPriceUpdate;
            //Util.PATS.Subscribe(Key);
        }

        #region IDisposable Support
        private bool disposedValue = false; // 偵測多餘的呼叫

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 處置 Managed 狀態 (Managed 物件)。
                    _BeforePATSReset(Observer.None, new Msg(null, null));
                    Center.Instance.RemoveObserver(_BeforePATSReset, Observer.PATS_BeforeReset);
                    Center.Instance.RemoveObserver(_AfterPATSReset, Observer.PATS_AfterReset);
                }

                // TODO: 釋放 Unmanaged 資源 (Unmanaged 物件) 並覆寫下方的完成項。
                // TODO: 將大型欄位設為 null。

                disposedValue = true;
            }
        }

        // TODO: 僅當上方的 Dispose(bool disposing) 具有會釋放 Unmanaged 資源的程式碼時，才覆寫完成項。
        // ~Tick() {
        //   // 請勿變更這個程式碼。請將清除程式碼放入上方的 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 加入這個程式碼的目的在正確實作可處置的模式。
        public void Dispose()
        {
            // 請勿變更這個程式碼。請將清除程式碼放入上方的 Dispose(bool disposing) 中。
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        #region Delegate
        private void _BeforePATSReset(Observer MsgName, Msg Msg)
        {
            Util.PATS.OnPriceUpdate -= PATS_OnPriceUpdate;
            if (ProductInfo != null) { Util.PATS.Unsubscribe(ProductInfo.Key); }
        }
        private void _AfterPATSReset(Observer MsgName, Msg Msg)
        {
            Util.PATS.OnPriceUpdate += PATS_OnPriceUpdate;
            if (ProductInfo != null) { Util.PATS.Subscribe(ProductInfo.Key); }
        }
        private void PATS_OnPriceUpdate(string key, PriceLib.PATS.PriceStruct price)
        {
            if (ProductInfo == null || key != ProductInfo.Key) { return; }
            List<PQ> bid, offer;
            if (isExtend)
            {
                ExtendTick(price, out bid, out offer);
            }
            else
            {
                BestTick(price, out bid, out offer);
            }

            //if ((price.ChangeMask & (int)PriceChange.ptChangeLast) == (int)PriceChange.ptChangeLast)
            //{
            this[RowGenre.MP, ColGenre.Price] = price.Last0.Price.ToDouble();
            this[RowGenre.MP, ColGenre.BL] = price.Last0.Volume;
            this[RowGenre.MP, ColGenre.SL] = price.Total.Volume;
            //}

            for (int i = 0; i < TickNumber; i++)
            {
                int ap = isAscending ? i : (TickNumber - 1 - i);
                int bp = isAscending ? (TickNumber - 1 - i) : i;

                this[RowGenre.AP, ColGenre.Price, i] = offer[ap].Price;
                if (offer[ap].Qty == 0)
                {
                    this[RowGenre.AP, ColGenre.SL, i] = "";
                }
                else
                {
                    this[RowGenre.AP, ColGenre.SL, i] = offer[ap].Qty;
                }

                this[RowGenre.BP, ColGenre.Price, i] = bid[bp].Price;
                if (bid[bp].Qty == 0)
                {
                    this[RowGenre.BP, ColGenre.BL, i] = "";
                }
                else
                {
                    this[RowGenre.BP, ColGenre.BL, i] = bid[bp].Qty;
                }
            }
        }
        #endregion

        #region Public
        public void ChangeRowPrperty(string PropertyName, object value)
        {
            if (CellList == null)
            {
                return;
            }
            PropertyInfo pi = typeof(Row).GetProperty(PropertyName);
            foreach (var rowClass in CellList.Values)
            {
                foreach (var row in rowClass)
                {
                    pi.SetValue(row, value);
                }
            }
        }
        public void UpdateAliveOrder()
        {


        }
        #endregion

        #region Private        
        private void InitCell()
        {
            if (CellList != null) { return; }
            CellList = new Dictionary<RowGenre, List<Row>>();
            Array rows = Enum.GetValues(typeof(RowGenre));

            foreach (RowGenre row in rows)
            {
                CellList.Add(row, new List<Row>());
                switch (row)
                {
                    case RowGenre.Functional:
                    case RowGenre.Statistics:
                    case RowGenre.Header:
                    case RowGenre.MP:
                        CellList[row].Add(new Row(row));
                        break;
                    case RowGenre.BP:
                    case RowGenre.AP:
                        for (int i = 0; i < TickNumber; i++)
                        {
                            CellList[row].Add(new Row(row));
                        }
                        break;
                }
            }
        }
        //private void _SetInitValue(string key, PriceLib.PATS.PriceStruct price)
        //{
        //    if (ProductInfo == null || key != ProductInfo.Key) { return; }
        //    List<PQ> bid, offer;
        //    if (isExtend)
        //    {
        //        ExtendTick(price, out bid, out offer);
        //    }
        //    else
        //    {
        //        BestTick(price, out bid, out offer);
        //    }

        //    this[RowGenre.MP, ColGenre.Price] = price.Last0.Price.ToDouble();
        //    this[RowGenre.MP, ColGenre.BL] = price.Last0.Volume;
        //    this[RowGenre.MP, ColGenre.SL] = price.Total.Volume;

        //    for (int i = 0; i < TickNumber; i++)
        //    {
        //        int ap = isAscending ? i : (TickNumber - 1 - i);
        //        int bp = isAscending ? (TickNumber - 1 - i) : i;

        //        //this[RowGenre.AP, ColGenre.Price, i] = price.OfferDOMs[ap].Price.ToDouble();
        //        //this[RowGenre.AP, ColGenre.SL, i] = price.OfferDOMs[ap].Volume;
        //        //this[RowGenre.BP, ColGenre.Price, i] = price.BidDOMs[bp].Price.ToDouble();
        //        //this[RowGenre.BP, ColGenre.BL, i] = price.BidDOMs[bp].Volume;
        //        this[RowGenre.AP, ColGenre.Price, i] = offer[ap].Price;
        //        if (offer[ap].Qty == 0)
        //        {
        //            this[RowGenre.AP, ColGenre.SL, i] = "";
        //        }
        //        else
        //        {
        //            this[RowGenre.AP, ColGenre.SL, i] = offer[ap].Qty;
        //        }
        //        this[RowGenre.BP, ColGenre.Price, i] = bid[bp].Price;
        //        if (bid[bp].Qty == 0)
        //        {
        //            this[RowGenre.BP, ColGenre.BL, i] = "";
        //        }
        //        else
        //        {
        //            this[RowGenre.BP, ColGenre.BL, i] = bid[bp].Qty;
        //        }
        //    }
        //}
        private void _ResetCellValue()
        {
            if (CellList == null) { return; }
            var rows = Enum.GetValues(typeof(RowGenre));
            foreach (RowGenre row in rows)
            {
                foreach (var r in CellList[row])
                {
                    r.ResetValue();
                }
            }
        }

        private void BestTick(PriceStruct price, out List<PQ> bid, out List<PQ> offer)
        {
            double last = price.Last0.Price.ToDouble();
            bid = _Best(-1, last, price.BidDOMs);
            offer = _Best(1, last, price.OfferDOMs);
        }
        private void ExtendTick(PriceStruct price, out List<PQ> bid, out List<PQ> offer)
        {
            double lastPrice = price.Last0.Price.ToDouble();
            double bidInit = price.BidDOM0.Price.ToDouble();
            double offerInit = price.OfferDOM0.Price.ToDouble();
            int bidPlusCnt = 0, offerPlusCnt = 0;
            if (lastPrice != 0)
            {
                if (lastPrice > bidInit | bidInit == 0)
                {
                    bidInit = lastPrice;
                    bidPlusCnt = 1;
                }
                if (lastPrice < offerInit || offerInit == 0)
                {
                    offerInit = lastPrice;
                    offerPlusCnt = 1;
                }
            }
            bid = _Extend(bidInit, bidPlusCnt, -1, price.BidDOMs);
            offer = _Extend(offerInit, offerPlusCnt, 1, price.OfferDOMs);
        }
        private List<PQ> _Extend(double initPrice, int plusCnt, int multiplier, PriceDetailStruct[] DOM)
        {
            List<PQ> re = new List<PQ>();
            for (int i = 0; i < TickNumber; i++)
            {
                decimal p = (decimal)(initPrice + (i + plusCnt) * (multiplier) * ProductInfo.TickSize);
                int qty = 0;
                for (int idx = 0; idx < DOM.Length; idx++)
                {
                    if (DOM[idx].Price == p.ToString())
                    {
                        qty = DOM[idx].Volume;
                        break;
                    }
                }
                re.Add(new PQ() { Price = (double)p, Qty = qty });
            }
            return re;
        }
        private List<PQ> _Best(double multiplier, double lastPrice, PriceDetailStruct[] DOM)
        {
            List<PQ> re = new List<PQ>();
            int baseIdx = 0;

            for (int i = 0; i < TickNumber; i++)
            {
                if (i < DOM.Length && DOM[i].Price.ToDouble() != 0)
                {
                    re.Add(new PQ() { Price = DOM[i].Price.ToDouble(), Qty = DOM[i].Volume });
                    baseIdx = i;
                }
                else
                {
                    double p = DOM[baseIdx].Price.ToDouble();
                    if (p == 0) { p = lastPrice; }
                    re.Add(new PQ() { Price = p == 0 ? 0 : (p + (i - baseIdx) * multiplier * ProductInfo.TickSize), Qty = 0 });
                }
            }
            return re;
        }
        #endregion
    }
}