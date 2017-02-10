using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RtNAV.JSON
{
    public class YT
    {
        /// <summary>
        /// 基金代碼
        /// </summary>
        public string fundId { get; set; }
        /// <summary>
        /// 上市代碼
        /// </summary>
        public string etfId { get; set; }
        /// <summary>
        /// 名稱
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// English名稱
        /// </summary>
        public string ename { get; set; }
        /// <summary>
        /// 昨收淨值
        /// </summary>
        public string yestNav { get; set; }
        /// <summary>
        /// 預估淨值
        /// </summary>
        public string nav { get; set; }
        /// <summary>
        /// 淨值漲跌幅
        /// </summary>
        public string navFluct { get; set; }
        /// <summary>
        /// 昨收市價
        /// </summary>
        public string yestPrice { get; set; }
        /// <summary>
        /// 最新市價
        /// </summary>
        public string price { get; set; }
        /// <summary>
        /// 市價漲跌幅
        /// </summary>
        public string priceFluct { get; set; }
        /// <summary>
        /// 指標昨收價格
        /// </summary>
        public string yestIndex { get; set; }
        /// <summary>
        /// 指標最新價格
        /// </summary>
        public string index { get; set; }
        /// <summary>
        /// 指標價格漲跌幅
        /// </summary>
        public string indexFluct { get; set; }
        /// <summary>
        /// 更新時間
        /// </summary>
        public string updateTime { get; set; }
        /// <summary>
        /// 可否申購
        /// </summary>
        public string AllowMark { get; set; }
        /// <summary>
        /// 可否贖回
        /// </summary>
        public string RedemMark { get; set; }
        /// <summary>
        /// 是否為營業日
        /// </summary>
        public string BussMark { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string navDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string rnav { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string inav { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string chkNavDate { get; set; }
    }
}
