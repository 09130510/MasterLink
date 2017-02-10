using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.ComponentModel;

namespace PriceCalculator
{
    /// <summary>
    /// 擴充方法
    /// </summary>
    public static class Extension
    {
        /// <summary>
        /// String to TEnum, parse fail return default(TEnum)
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public static TEnum ToEnum<TEnum>(this string name) where TEnum : struct
        {
            TEnum val;
            if (!Enum.TryParse(name, out val))
            {
                return default(TEnum);
            }
            return val;
        }
        /// <summary>
        /// Char to TEnum, parse fail return default(TEnum)
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public static TEnum ToEnum<TEnum>(this char name) where TEnum : struct
        {
            return ToEnum<TEnum>(name.ToString());
        }
        /// <summary>
        /// Object to TEnum, parse fail return default(TEnum)
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public static TEnum ToEnum<TEnum>(this object name) where TEnum : struct
        {
            return ToEnum<TEnum>(name.ToString());
        }
        public static T Convert<T>(this object value)
        {
            var converter = TypeDescriptor.GetConverter(typeof(T));
            string valueStr = value.ToString();
            if (converter != null && !string.IsNullOrEmpty(valueStr))
            {
                return (T)converter.ConvertFromString(valueStr);
            }
            return default(T);
        }
        /// <summary>
        /// Get Type Default Value
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static object GetDefault(this Type t)
        {
            if (t.IsValueType)
            {
                return Activator.CreateInstance(t);
            }
            return null;
        }

        /// <summary>
        /// 無參數Delegate的Invoke
        /// </summary>
        /// <param name="action"></param>
        /// <param name="control">要Invoke的Control</param>
        public static void Invoke(this Action action, object control)
        {
            Control c = null;
            if (control is Control)
            {
                c = (Control)control;
            }
            else if (control is ToolStripItem)
            {
                c = ((ToolStripItem)control).Owner;
            }

            if (c.InvokeRequired)
            {
                c.Invoke(action);
            }
            else
            {
                action();
            }
        }
        /// <summary>
        /// 無參數Delegate的BeginInvoke
        /// </summary>
        /// <param name="action"></param>
        /// <param name="control">要BeginInvoke的Control</param>
        public static void BeginInvoke(this Action action, object control)
        {
            Control c = null;
            if (control is Control)
            {
                c = (Control)control;
            }
            else if (control is ToolStripItem)
            {
                c = ((ToolStripItem)control).Owner;
            }

            if (c != null && c.InvokeRequired)
            {
                c.BeginInvoke(action);
            }
            else
            {
                action();
            }
        }
        /// <summary>
        /// 有參數Delegate的Invoke
        /// </summary>
        /// <param name="d">Delegate</param>
        /// <param name="control">要Invoke的Control</param>
        /// <param name="args">Delegate的參數</param>
        public static void Invoke(this Delegate d, object control, params object[] args)
        {
            Control c = null;
            if (control is Control)
            {
                c = (Control)control;
            }
            else if (control is ToolStripItem)
            {
                c = ((ToolStripItem)control).Owner;
            }

            if (c != null && c.InvokeRequired)
            {
                c.Invoke(d, args);
            }
            else
            {
                d.Method.Invoke(d.Target, args);
            }
        }
        /// <summary>
        /// 有參數Delegate的BeginInvoke
        /// </summary>
        /// <param name="d">Delegate</param>
        /// <param name="control">要BeginInvoke的Control</param>
        /// <param name="args">Delegate的參數</param>
        public static void BeginInvoke(this Delegate d, object control, params object[] args)
        {
            Control c = null;
            if (control is Control)
            {
                c = (Control)control;
            }
            else if (control is ToolStripItem)
            {
                c = ((ToolStripItem)control).Owner;
            }
            if (c.InvokeRequired)
            {
                c.BeginInvoke(d, args);
            }
            else
            {
                d.Method.Invoke(d.Target, args);
            }
        }
        /// <summary>
        /// 非同步委派更新UI
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
        /// 非同步委派更新UI
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
        /// Get PropertyInfo Description Attribute, not have Description return PropertyInfo.Name
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool isCellProperty(this PropertyInfo value)
        {
            DisplayCellAttribute[] attributes = (DisplayCellAttribute[])value.GetCustomAttributes(typeof(DisplayCellAttribute), false);
            return attributes != null && attributes.Length > 0;
            //if (attributes != null && attributes.Length > 0)
            //{
            //    return !String.IsNullOrEmpty(attributes[0].Caption);
            //}
            //return false;
        }
        /// <summary>
        /// Get PropertyInfo Description Attribute, not have Description return PropertyInfo.Name
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Caption(this PropertyInfo value)
        {
            DisplayCellAttribute[] attributes = (DisplayCellAttribute[])value.GetCustomAttributes(typeof(DisplayCellAttribute), false);
            if (attributes != null && attributes.Length > 0)
            {
                return attributes[0].Caption;
            }
            return string.Empty;
        }
        /// <summary>
        /// Get PropertyInfo Description Attribute, not have Description return PropertyInfo.Name
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double Seqno(this PropertyInfo value)
        {
            DisplayCellAttribute[] attributes = (DisplayCellAttribute[])value.GetCustomAttributes(typeof(DisplayCellAttribute), false);
            if (attributes != null && attributes.Length > 0)
            {
                return attributes[0].Seqno;
            }
            return -1;
        }        
        /// <summary>
        /// Get PropertyInfo Description Attribute, not have Description return PropertyInfo.Name
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string FL(this Enum value)
        {
            ForeignLetterAttribute[] attributes = (ForeignLetterAttribute[])value.GetType().GetCustomAttributes(typeof(ForeignLetterAttribute), false);
            if (attributes != null && attributes.Length > 0)
            {
                return attributes[0].Letter;
            }
            return string.Empty;
        }
    }
}
