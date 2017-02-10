using System.Drawing;
using System.Windows.Forms;
using Util.Extension.Class;
using WeifenLuo.WinFormsUI.Docking;
using System.IO;

namespace SinopacHK.Class
{
    public class Unit : NotifyDisposableClass
    {
        #region Variable
        private DockContent m_LogForm;
        private DockContent m_ConnectForm;
        private DockContent m_SettingForm;
        private DockContent m_AliveForm = new frmAlive();
        private DockContent m_MatchForm = new frmMatch();
        private DockContent m_OrderForm;

        private int m_TickCount;
        private int m_OrderQty = 1;
        private bool m_OrderAlert = true;
        #endregion

        #region Property
        public Deal Deal { get; private set; }
        public Tick Tick { get; private set; }

        public DockContent LogForm
        {
            get
            {
                if (m_LogForm == null || m_LogForm.IsDisposed)
                {
                    m_LogForm = new frmLog();
                }
                return m_LogForm;
            }
        }
        public DockContent ConnectForm
        {
            get
            {
                if (m_ConnectForm == null || m_ConnectForm.IsDisposed)
                {
                    m_ConnectForm = new frmConnectSetting();
                }
                return m_ConnectForm;
            }
        }
        public DockContent AliveForm
        {
            get
            {
                if (m_AliveForm == null || m_AliveForm.IsDisposed)
                {
                    m_AliveForm = new frmAlive();
                }
                return m_AliveForm;
            }
        }
        public DockContent MatchForm
        {
            get
            {
                if (m_MatchForm == null || m_MatchForm.IsDisposed)
                {
                    m_MatchForm = new frmMatch();
                }
                return m_MatchForm;
            }
        }
        public DockContent OrderForm
        {
            get
            {
                if (m_OrderForm == null || m_OrderForm.IsDisposed)
                {
                    m_OrderForm = new frmOrder();
                }
                return m_OrderForm;
            }
        }
        public DockContent SettingForm
        {
            get
            {
                if (m_SettingForm == null || m_SettingForm.IsDisposed)
                {
                    m_SettingForm = new frmSetting();
                }
                return m_SettingForm;
            }
        }
        public ProductInfo Product { get { return ProductCollection.Selected; } }
        public int TickCount
        {
            get { return m_TickCount; }
            set
            {
                if (m_TickCount != default(int) && value == m_TickCount) { return; }
                m_TickCount = value;
                _DisposeTick();
                Tick = new Tick(this, m_TickCount);
            }
        }
        public int OrderQty
        {
            get { return m_OrderQty; }
            set
            {
                if (value == m_OrderQty) { return; }
                m_OrderQty = value;
            }
        }
        public bool OrderAlert
        {
            get { return m_OrderAlert; }
            set
            {
                if (value == m_OrderAlert) { return; }
                m_OrderAlert = value;
            }
        }
        #endregion

        public Unit()
        {
            Deal = new Deal(this);
        }
        protected override void DoDispose()
        {
            _DisposeTick();
        }

        #region Public        
        public void SaveLayout()
        {
            ((frmAlive)AliveForm).SaveLayout();
            ((frmMatch)MatchForm).SaveLayout();
        }
        /// <summary>
        /// 載入視窗設定
        /// </summary>
        public void LoadLayout(DockPanel panel)
        {
            if (!File.Exists(Utility.DockLayoutFile))
            {
                LogForm.Show(panel, DockState.DockBottomAutoHide);
                ConnectForm.Show(panel, DockState.DockRight);
                SettingForm.Show(panel, DockState.DockRight);
                OrderForm.Show(panel);
                AliveForm.Show(OrderForm.Pane, DockAlignment.Right, 0.5);
                MatchForm.Show(AliveForm.Pane, DockAlignment.Bottom, 0.5);                
            }
            else
            {
                panel.LoadFromXml(Utility.DockLayoutFile, delegate(string persistString)
                {
                    if (persistString == typeof(frmLog).ToString()) { return LogForm; }
                    if (persistString == typeof(frmConnectSetting).ToString()) { return ConnectForm; }
                    if (persistString == typeof(frmSetting).ToString()) { return SettingForm; }
                    if (persistString == typeof(frmOrder).ToString()) { return OrderForm; }
                    if (persistString == typeof(frmAlive).ToString()) { return AliveForm; }
                    if (persistString == typeof(frmMatch).ToString()) { return MatchForm; }                    
                    return null;
                });
            }
            OrderForm.Activate();            
        }
        #endregion

        #region Private
        private void _DisposeTick() { if (Tick != null) { Tick.Dispose(); } }
        #endregion
    }
}