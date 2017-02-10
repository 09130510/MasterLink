
namespace PriceProcessor.Capital
{
    public class MappingInfo
    {
        public string Key { get { return Exchange + "," + PIDHead; } }
        public string OrderKey { get { return $"{Exchange},{OrderHead}"; } }

        public string Exchange { get; private set; }
        public string TickName { get; private set; }
        public string PIDHead { get; private set; }
        public string OrderHead { get; private set; }

        public MappingInfo(object exchange, object tickname, object pidhead, object orderhead)
        {
            Exchange = exchange.ToString();
            TickName = tickname.ToString();
            PIDHead = pidhead.ToString();
            OrderHead = orderhead.ToString();
        }
        public string OrderYM(string QuoteYM)
        {
            return (int.Parse(QuoteYM) + 200000).ToString();
        }
    }
}
