using System;
using System.Collections.Generic;
using System.Text;

namespace FixInitiator.Class.Public
{
    #region 類別
    public class ComboItem : Object
    {
        protected string name_;
        protected byte value_;
        public ComboItem(string name, byte value)
        {
            name_ = name;
            value_ = value;
        }
        public override string ToString()
        {
            return name_;
        }

        public byte value
        {
            get
            {
                return value_;
            }
        }

    }

    #endregion

}
