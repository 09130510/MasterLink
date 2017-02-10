using System;

namespace PCF.Class
{
    interface IPCF
    {
        DateTime DataDate { get;  }
        string ETFCode { get;  }
        string PID { get; set; }
        string Name { get; set; }
        double PCFUnits { get; set; }
        double TotalUnits { get; set; }
        decimal Weights { get; set; }

        string Insert();
    }
}
