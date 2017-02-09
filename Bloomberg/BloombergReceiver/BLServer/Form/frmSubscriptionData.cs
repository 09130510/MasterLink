using BLPServer.Class;
using WeifenLuo.WinFormsUI.Docking;

namespace BLPServer
{
    public partial class frmSubscriptionData : DockContent
    {
        public frmSubscriptionData()
        {
            InitializeComponent();

            SetCell();
        }

        public void SetCell()
        {
            if (Utility.Display != null)
            {
                Utility.Display.SetCell(grid1);
            }
        }
    }
}
