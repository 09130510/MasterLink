using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETFPosition.Class
{
    public abstract class Position
    {
        /// <summary>
        /// 設公式用的
        /// </summary>
        public virtual int RowIndex { get; set; } = -1;

        public virtual string AccNo { get; set; }
        public virtual string ID { get; set; }
        public virtual string Name { get; set; }
        public virtual int Lots { get; set; }
        public virtual double Price { get; set; }
        public virtual int CValue { get; set; }
        public virtual double Delta { get { return Lots * Price * CValue; } }
    }
}