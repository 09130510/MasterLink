using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BLPServer.Class
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
    }
}
