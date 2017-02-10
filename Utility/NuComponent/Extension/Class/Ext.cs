using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace Util.Extension.Class
{
    public static class Ext
    {
        public static string VersionInfo(Form form)
        {
            #region Version Info
            object[] attribute = form.GetType().Assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
            AssemblyDescriptionAttribute desc = (AssemblyDescriptionAttribute)(attribute[0]);
            attribute = form.GetType().Assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
            AssemblyTitleAttribute title = (AssemblyTitleAttribute)(attribute[0]);
            return string.Format("[{0}]  {1}  V{2}", title.Title, desc.Description, Application.ProductVersion);
            #endregion
        }
    }
}
