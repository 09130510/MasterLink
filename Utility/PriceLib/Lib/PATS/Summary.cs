using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PriceLib.PATS
{
    public class Summary : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region Variable
        private string m_Key;
        private string m_ExchangeName;
        private string m_ContractName;
        private string m_ContractDate;
        private string m_TraderAccount;
        private int m_BLots;
        private double m_BAmount;
        private int m_ALots;
        private double m_AAmount;
        #endregion

        #region Property
        public string Key
        {
            get { return m_Key; }
            set
            {
                if (value == m_Key) { return; }
                m_Key = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Key)));
            }
        }
        public string ExchangeName
        {
            get { return m_ExchangeName; }
            set
            {
                if (value == m_ExchangeName) { return; }
                m_ExchangeName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ExchangeName)));
            }
        }
        public string ContractName
        {
            get { return m_ContractName; }
            set
            {
                if (value == m_ContractName) { return; }
                m_ContractName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ContractName)));
            }
        }
        public string ContractDate
        {
            get { return m_ContractDate; }
            set
            {
                if (value == m_ContractDate) { return; }
                m_ContractDate = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ContractDate)));
            }
        }
        public string TraderAccount
        {
            get { return m_TraderAccount; }
            set
            {
                if (value == m_TraderAccount) { return; }
                m_TraderAccount = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TraderAccount)));
            }
        }
        public int BLots
        {
            get { return m_BLots; }
            set
            {
                if (value == m_BLots) { return; }
                m_BLots = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BLots)));
            }
        }
        public double BAmount
        {
            get { return m_BAmount; }
            set
            {
                if (value == m_BAmount) { return; }
                m_BAmount = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BAmount)));
            }
        }
        public int ALots
        {
            get { return m_ALots; }
            set
            {
                if (value == m_ALots) { return; }
                m_ALots = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ALots)));
            }
        }
        public double AAmount
        {
            get { return m_AAmount; }
            set
            {
                if (value == m_AAmount) { return; }
                m_AAmount = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AAmount)));
            }
        }


        #endregion

        public override string ToString()
        {
            return $"{TraderAccount}, {Key}, {ALots}, {BLots}, {AAmount}, {BAmount}";
        }
    }
}
