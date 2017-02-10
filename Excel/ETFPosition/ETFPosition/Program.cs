using ETFPosition.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ETFPosition
{
    static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Extractor.ExtractResourceToFile("ETFPosition.Config.ini", "Config.ini", false);
            Extractor.ExtractResourceToFile("ETFPosition.Position.xlsx", "Position.xlsx", false);
            Application.Run(new frmMain());
        }
    }
}
