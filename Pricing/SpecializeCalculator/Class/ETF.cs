using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SourceCell;
using System.ComponentModel;

namespace PriceCalculator
{
    public class ETF : NotifyableClass
    {
        public const string SELECTALL = "SELECT ETFCode, FundAssetValue, PublicShares FROM ETFFORBRIAN..ETFDAILY ";
        public static string SELECTONE = SELECTALL + " WHERE ETFCODE=@ETFCode";
        
        #region Variable
        private bool m_Lock = false;
        private decimal m_YstNAV = 0M;
        private decimal m_LockYstNAV = 0M;
        #endregion

        #region Property
        public string ETFCode { get; set; }
        public double FundAssetValue { get; set; }
        public double PublicShares { get; set; }
        public decimal YstNAV
        {
            get
            {
                if (m_Lock)
                {
                    return m_LockYstNAV <= 0M ? (decimal)(FundAssetValue / PublicShares) : m_LockYstNAV;
                }
                if (m_YstNAV <= 0M)
                {
                    m_YstNAV = PublicShares == 0D ? 0M : (decimal)(FundAssetValue / PublicShares);
                    OnPropertyChanged(nameof(YstNAV));
                }
                return m_YstNAV;
            }
            set
            {
                if (value == m_YstNAV) { return; }
                m_YstNAV = value;
                OnPropertyChanged(nameof(YstNAV));
            }
        }
        //{
        //    get
        //    {
        //        return PublicShares == 0 ? 0M : (decimal)(FundAssetValue / PublicShares);
        //    }
        //}
        #endregion

        #region Public
        public void LockYstNAV(decimal lockYstNAV)
        {
            m_Lock = true;
            m_LockYstNAV = lockYstNAV;
            OnPropertyChanged(nameof(YstNAV));
        }
        public void UnlockYstNAV()
        {
            m_Lock = false;
            //m_LockYstNAV = 0M;            
            OnPropertyChanged(nameof(YstNAV));
        }
        #endregion
    }
}
