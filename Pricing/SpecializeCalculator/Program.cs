﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PriceCalculator
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
            Util.MainForm = new frmMain();
            Application.Run(Util.MainForm);
        }
    }
}