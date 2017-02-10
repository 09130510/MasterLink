using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Util.Extension.Class;

namespace ComponentTest
{
	public partial class WeakTest : Form
	{
		private TestCellClass m_cell;
		private WeakReference m_WR;
		enum Testtttt
		{
			[Description("T")]
			t,
			[Description("E")]
			e,
			[Description("S")]
			s
		}

		public WeakTest()
		{
			InitializeComponent();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			m_cell = new TestCellClass();
			m_WR = new WeakReference(m_cell);

			grid1.Redim(1, 1);
			grid1[0, 0] = m_cell.Cell.Field;
			grid1.Refresh();
			Config config = new Config("", "");
			
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Console.WriteLine(m_WR.Target == null);
			m_cell.Dispose();			
			m_cell = null;
			Thread.Sleep(10000);
			Console.WriteLine(m_WR.Target == null);
			grid1.Rows.Clear();
			GC.Collect();
			Thread.Sleep(1000);
			Console.WriteLine(m_WR.Target == null);
		}

		private void button3_Click(object sender, EventArgs e)
		{
			List<TestCellClass> list = new List<TestCellClass>();
			for (int i = 0; i < 1000000; i++)
			{
				list.Add( new TestCellClass());
			}

			for (int i = 999999; i <=0; i--)
			{
				list[i].Dispose();
				list[i] = null;
			}
			list.Clear();
			GC.Collect();
			////m_WR = new WeakReference(new TestCellClass());
			////Console.WriteLine(m_WR.Target == null);
			////Thread.Sleep(1000);
			////Console.WriteLine(m_WR.Target == null);
			////GC.Collect();
			////Console.WriteLine(m_WR.Target == null);
		}
	}
}
