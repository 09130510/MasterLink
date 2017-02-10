using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PCF
{
    static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //-D 忽略成份資料日期判斷
            //-YTD 元大網址不加日期(只有&date=)
            foreach (var arg in args)
            {
                if (arg.ToUpper() == "-D") { Utility.IgnoreDate = true; }
                if (arg.ToUpper() == "-YTD") { Utility.YuantaNotInputDate = true; }
            }
            

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }
    }
}
