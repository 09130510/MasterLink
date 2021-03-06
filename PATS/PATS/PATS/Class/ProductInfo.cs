﻿using PATS.Utility;
using PriceLib.PATS;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PATS.Class
{
    /// <summary>
    /// 商品資料
    /// </summary>
    public class ProductInfo
    {
        #region Property
        public int Index { get; private set; }
        public string Key { get; private set; }
        public string Exch { get; set; }
        public string Commodity { get; set; }
        public string Date { get; set; }
        public double TickSize { get; set; }
        public double LimitUP { get; set; }
        public double LimitDOWN { get; set; }
        //public List<string> OrderTypes { get; set; }
        #endregion

        public ProductInfo(int index, ContractStruct contract)
        {
            Index = index;
            Exch = contract.ExchangeName;
            Commodity = contract.ContractName;
            Date = contract.ContractDate;
            Key = $"{Exch},{Commodity},{Date}";
            TickSize = contract.TickSize.ToDouble();
            PriceStruct price = Util.PATS.Price(Index);
            LimitUP = price.LimitUp.Price.ToDouble();
            LimitDOWN = price.LimitDown.Price.ToDouble();
            //OrderTypes = new List<string>();
        }

        public bool Compare(params string[] filters)
        {
            bool re = false;
            if (filters == null || filters.Length <= 0)
            {
                return true;
            }
            for (int i = 0; i < filters.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        re = filters[i] == Exch;
                        break;
                    case 1:
                        re = re & filters[i] == Commodity;
                        break;
                    case 2:
                        re = re & filters[i] == Date;
                        break;
                }
            }

            return re;
        }

        /// <summary>
        /// 從PATS ContractStruct轉換成ProductInfo
        /// </summary>
        /// <param name="source"></param>        
        /// <returns></returns>
        //public static List<ProductInfo> Convert(Dictionary<int, ContractStruct> source, Dictionary<string, List<string>> orderTypes)
        public static List<ProductInfo> Convert(Dictionary<int, ContractStruct> source)
        {
            List<ProductInfo> re = new List<ProductInfo>();
            string[][] filters = Util.INI["SYS"]["FILTER"].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Select(e => e.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)).ToArray();

            if (filters == null || filters.Length <= 0)
            {
                foreach (var item in source)
                {
                    ProductInfo pi = new ProductInfo(item.Key, item.Value);
                    //if (orderTypes.ContainsKey(pi.Exch))
                    //{
                    //    pi.OrderTypes = orderTypes[pi.Exch];
                    //}
                    re.Add(pi);
                }
            }
            else
            {
                foreach (var filter in filters)
                {
                    foreach (var item in source)
                    {
                        ProductInfo pi = new ProductInfo(item.Key, item.Value);
                        //if (orderTypes.ContainsKey(pi.Exch))
                        //{
                        //    pi.OrderTypes = orderTypes[pi.Exch];
                        //}
                        if (pi.Compare(filter) && !re.Contains(pi))
                        {
                            re.Add(pi);
                            break;
                        }
                    }
                }
            }
            return re;
        }
    }
}
