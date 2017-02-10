using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PriceLib.Capital
{
    public class IndexInfo
    {
        private const string NATIONALINDEXEXCH = "17";
        private const char SPLIT = ',';

        #region Property
        public string Exch { get; private set; }
        public string ExchName { get; private set; }
        public string Head { get; private set; }
        public string Name { get; private set; }
        #endregion

        private IndexInfo(string[] infos)
        {
            Exch = infos[0];
            ExchName = infos[1];
            Head = infos[2];
            Name = infos[3];

        }
        public static IndexInfo Create(string info)
        {
            string[] infos = info.Split(SPLIT);
            if (infos.Length < 4 || infos[0] != NATIONALINDEXEXCH) { return null; }
            return new IndexInfo(infos);
        }
    }
}
