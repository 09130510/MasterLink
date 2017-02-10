using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace SinopacHK.Class
{
    public static class Extension
    {
        public static RowType Row(this PropertyInfo value)
        {
            CellAttribute[] attributes = (CellAttribute[])value.GetCustomAttributes(typeof(CellAttribute), false);
            if (attributes != null && attributes.Length > 0)
            {
                return attributes[0].Row;
            }
            return default(RowType);
        }
        public static ColumnType Col(this PropertyInfo value)
        {
            CellAttribute[] attributes = (CellAttribute[])value.GetCustomAttributes(typeof(CellAttribute), false);
            if (attributes != null && attributes.Length > 0)
            {
                return attributes[0].Column;
            }
            return default(ColumnType);
        }
        public static Type ValueType(this PropertyInfo value)
        {
            CellAttribute[] attributes = (CellAttribute[])value.GetCustomAttributes(typeof(CellAttribute), false);
            if (attributes != null && attributes.Length > 0)
            {
                return attributes[0].ValueType;
            }
            return typeof(string);
        }
        public static object Default(this PropertyInfo value)
        {
            CellAttribute[] attributes = (CellAttribute[])value.GetCustomAttributes(typeof(CellAttribute), false);
            if (attributes != null && attributes.Length > 0)
            {
                return attributes[0].Default;
            }
            return "";
        }
        public static string Format(this PropertyInfo value)
        {
            CellAttribute[] attributes = (CellAttribute[])value.GetCustomAttributes(typeof(CellAttribute), false);
            if (attributes != null && attributes.Length > 0)
            {
                return attributes[0].Format;
            }
            return "";
        }
    }
}
