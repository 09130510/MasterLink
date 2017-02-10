using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PriceCalculator
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DisplayCellAttribute : Attribute
    {
        public string Caption { get; set; }
        public double Seqno { get; set; }

        public DisplayCellAttribute(string caption, double seqno)
        {
            Caption = caption;
            Seqno = seqno;
        }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class ForeignLetterAttribute : Attribute
    {
        public string Letter { get; set; }        

        public ForeignLetterAttribute(string letter)
        {
            Letter = letter;            
        }
    }
}
