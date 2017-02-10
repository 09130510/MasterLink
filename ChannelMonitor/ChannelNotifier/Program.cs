using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notifier
{
    static class Program
    {
        public static frmStopNotify SettingForm;
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {           

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            SettingForm = new frmStopNotify();
            Application.Run(SettingForm);
        }
    }
}
