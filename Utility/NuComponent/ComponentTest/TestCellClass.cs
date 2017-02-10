using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SourceCell;
using Util.Extension.Class;

namespace ComponentTest
{
	public class TestCellClass : DisposableClass
	{
		private TextCell m_Cell;
		public TextCell Cell
		{
			//x ddddd
			//? ddd
			//? ddd
			//TODORUP WU0 
			//UNDONE
			//HACK

			get
			{
				if (m_Cell == null)
				{
					m_Cell = new TextCell() { CellType = TextCell.TextType.String, Value = "Test" };
					m_Cell.OnClick += new OnClickDelegate(m_Cell_OnClick);
				}
				return m_Cell;
			}
		}

		private void m_Cell_OnClick(CellBase cell, EventArgs e)
		{
			cell.SetValue("Click");
		}

		protected override void DoDispose()
		{			
			if (m_Cell != null)
			{
				m_Cell.OnClick -= new OnClickDelegate(m_Cell_OnClick);
				m_Cell.Dispose();
			}
			m_Cell = null;			
		}
	}
}
