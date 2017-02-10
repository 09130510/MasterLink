
namespace PriceProcessor.Capital
{
    public class Product
    {
        private Price m_Price;

        public string Exchange { get; private set; }
        public string StockNo { get; private set; }
        public string TickName { get; private set; }
        public string OrderPID { get { return OrderHead + YM1 + (string.IsNullOrEmpty(YM2) ? "" : "/" + YM2); } }
        public string PIDHead { get; private set; }
        public string OrderHead { get; private set; }
        public string YM1 { get; private set; }
        public string YM2 { get; private set; } = "";
        public string SubStr { get { return Exchange + "," + StockNo; } }
        public Price Price
        {
            get { return m_Price; }
            set { m_Price = value; }
        }

        public Product(string Info, MappingInfo mi)
        {
            string[] info = Info.Split(',');
            Exchange = info[0];
            StockNo = info[2];

            TickName = mi == null ? string.Empty : mi.TickName;
            PIDHead = mi == null ? string.Empty : mi.PIDHead;
            OrderHead = mi == null ? string.Empty : mi.OrderHead;
            if (!StockNo.Contains("SPD"))
            {
                YM1 = mi == null ? string.Empty : mi.OrderYM(StockNo.Substring(StockNo.Length - 4, 4));
            }
            else
            {
                string[] items = info[3].Split('/');

                YM1 = mi == null ? "" : mi.OrderYM(items[0].Substring(items[0].Length - 4, 4));
                YM2 = mi == null ? "" : mi.OrderYM(items[1]);
            }
        }
    }
}
