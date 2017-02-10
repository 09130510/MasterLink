using System;
using System.Reflection;
using System.Windows.Forms;

namespace Capital.Report.Class
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

        public static T InvokeIfRequired<T>(this Control control, Func<T> func)
        {
            if (control.InvokeRequired)
            {
                return (T)control.Invoke(func);
            }
            return func();
        }
        public static void InvokeIfRequired(this Control control, MethodInvoker action)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(action);
            }
            else
            {
                action();
            }
        }
        public static void InvokeIfRequired(this ToolStripItem control, MethodInvoker action)
        {
            if (control != null)
            {
                if ((control.Owner != null) && control.Owner.InvokeRequired)
                {
                    try
                    {
                        control.Owner.Invoke(action);
                    }
                    catch (Exception)
                    {
                    }
                }
                else
                {
                    action();
                }
            }
        }
    }
}
