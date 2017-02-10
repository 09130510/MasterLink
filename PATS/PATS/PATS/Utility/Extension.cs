using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PATS.Utility
{
    public static class Extension
    {
        /// <summary>
        /// 同步委派更新UI
        /// </summary>
        /// <param name="control"></param>
        /// <param name="action"></param>
        public static void InvokeIfRequired(this Control control, MethodInvoker action)
        {
            if (control.InvokeRequired)//在非當前執行緒內 使用委派
            {
                control.Invoke(action);
            }
            else
            {
                action();
            }
        }
        /// <summary>
        /// 同步委派更新UI
        /// </summary>
        /// <param name="control"></param>
        /// <param name="action"></param>
        public static void InvokeIfRequired(this ToolStripItem control, MethodInvoker action)
        {
            if (control == null) { return; }
            if (control.Owner != null && control.Owner.InvokeRequired)//在非當前執行緒內 使用委派
            {
                try
                {
                    control.Owner.Invoke(action);
                }
                catch (Exception) { }
            }
            else
            {
                action();
            }
        }
        public static T InvokeIfRequired<T>(this Control control, Func<T> func)
        {
            if (control.InvokeRequired)//在非當前執行緒內 使用委派
            {
                return (T)control.Invoke(func);
            }
            else
            {
                return func();
            }
        }

        /// <summary>
        /// 非同步委派更新UI
        /// </summary>
        /// <param name="control"></param>
        /// <param name="action"></param>
        public static void BeginInvokeIfRequired(this Control control, MethodInvoker action)
        {
            if (control.InvokeRequired)//在非當前執行緒內 使用委派
            {
                control.BeginInvoke(action);
            }
            else
            {
                action();
            }
        }
        /// <summary>
        /// 非同步委派更新UI
        /// </summary>
        /// <param name="control"></param>
        /// <param name="action"></param>
        public static void BeginInvokeIfRequired(this ToolStripItem control, MethodInvoker action)
        {
            if (control == null) { return; }
            if (control.Owner != null && control.Owner.InvokeRequired)//在非當前執行緒內 使用委派
            {
                try
                {
                    control.Owner.BeginInvoke(action);
                }
                catch (Exception) { }
            }
            else
            {
                action();
            }
        }

        public static int NextNumber(this IList<int> list)
        {
            int next = 0;
            List<int> breaks = new List<int>();
            if (!(list?.Any() ?? false))
            {
                list.Add(next);
                return next;
            }
            var listAfterSorted = list.OrderBy(e => e);
            int min = listAfterSorted.Min();
            int max = listAfterSorted.Max()+1;

            listAfterSorted.Aggregate((seed, total) =>
            {
                int interval = 1;   
                var diff = (total - seed) - interval;

                while (diff>0)
                {
                    breaks.Add(total -= interval);
                    diff = (total - seed) - interval;
                }
                //if (diff > 0)
                //{
                //    breaks.Add(total - 1 );
                //}
                return total;
            });

            //next = list.OrderBy(e => e).Last() + 1;
            //var breaks = new List<int>();

            //if ((list != null) && (list.Any()))
            //{
            //    list.OrderBy(e => e).Aggregate((seed, aggr) =>
            //      {
            //          var diff = (aggr - seed) - 1;

            //          if (diff > 0)
            //              breaks.Add(aggr - 1);
            //          return aggr;
            //      });
            //}
            if (min != 0)
            {
                next = 0;
            }
            else
            {
                next = breaks.Any() ? breaks.OrderBy(e=>e).First() : max;
            }
            list.Add(next);
            return next;
        }

        public static int AscendingNo(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            CellSortingAttribute[] attributes = (CellSortingAttribute[])fi.GetCustomAttributes(typeof(CellSortingAttribute), false);
            return attributes != null && attributes.Length > 0 ? attributes[0].AscendingNo : (int)fi.GetValue(value);
        }
        public static int DescendingNo(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            CellSortingAttribute[] attributes = (CellSortingAttribute[])fi.GetCustomAttributes(typeof(CellSortingAttribute), false);
            return attributes!= null && attributes.Length >0?  attributes[0].DescendingNo : (int)fi.GetValue(value);
        }

        public static double ToDouble(this string value)
        {
            double val;
            if (!double.TryParse(value, out val)) {
                return default(double); }
            return val;
        }
    }
}
