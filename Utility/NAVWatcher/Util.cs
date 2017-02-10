using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PriceLib.Redis;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;

namespace NAVWatcher
{
    public static class Util
    {
        public static RedisLib RedisLib;

        public static string VersionInfo(Form form)
        {            
            object[] attribute = form.GetType().Assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
            AssemblyDescriptionAttribute desc = (AssemblyDescriptionAttribute)(attribute[0]);
            attribute = form.GetType().Assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
            AssemblyTitleAttribute title = (AssemblyTitleAttribute)(attribute[0]);
            return string.Format("[{0}]  {1}  V{2}", title.Title, desc.Description, Application.ProductVersion);            
        }
        
    }
    public static class Extension
    {
        //非同步委派更新UI
        public static void InvokeIfRequired(
            this Control control, MethodInvoker action)
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
    }
}
