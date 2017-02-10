using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Util.Extension;
using SourceCell;
using Util.Extension.Class;

namespace ComponentTest
{
	public partial class Form1 : Form
	{
		

		public enum OrderSide
		{
			None = 0,
			Buy = 1,
			Sell = 2,
			Both = 3
		}
		public enum CPMask
		{
			[Description("+C-P")]
			LCSP = 10,	//001 010
			[Description("-C+P")]
			SCLP = 17,	//010 001
			[Description("+C+P")]
			LCLP = 9,	//001 001
			[Description("-C-P")]
			SCSP = 18,	//010 010	
			[Description("N")]
			N = 27		//011 011
		}

		public Form1()
		{
			InitializeComponent();
			OrderSide Call = OrderSide.Buy;
			OrderSide Put = OrderSide.Both;
			CPMask mask = CPMask.LCSP;

			Console.WriteLine(((int)Call & ((int)mask >> 3)).ToString().ToEnum<OrderSide>());
			Console.WriteLine(((int)Put & (((int)mask | 56) - 56)).ToString().ToEnum<OrderSide>());
		}

		#region Control Text
		private void button1_Click(object sender, EventArgs e)
		{
			AlertBox.Alert(this, AlertBoxButton.Msg_OK, "Test", "ForTest");
		}
		private void button2_Click(object sender, EventArgs e)
		{
			AlertBox.Alert(this, AlertBoxButton.OKCancel, "Test", "ForTest");
		}
		private void button3_Click(object sender, EventArgs e)
		{
			AlertBox.Alert(this, AlertBoxButton.Error_OK, "Test", new MsgLine("Line1", "Message1"), new MsgLine("Line2", "Message2"));
		}
		private void button4_Click(object sender, EventArgs e)
		{
			AlertBox.Alert(null, AlertBoxButton.OKCancel, "Test", new MsgLine("Line1", "Message1"), new MsgLine("Line2", "Message2"));
		}
		private void button9_MouseEnter(object sender, EventArgs e)
		{
			Button btn = ((Button)sender);
			btn.Size = new Size(32, 32);
			btn.BringToFront();
			btn.Font = new Font(btn.Font, FontStyle.Bold);
			btn.ForeColor = Color.Crimson;			
			//if (sender!= button9)
			//{
			//    button9.SendToBack();
			//}						
		}
		private void button9_MouseLeave(object sender, EventArgs e)
		{
			Button btn = ((Button)sender);
			if (sender != button9)
			{
				btn.SendToBack();
				btn.Font = new Font(btn.Font, FontStyle.Regular);
			}
			btn.ForeColor = Color.Black;
			btn.Size = new Size(28, 28);
		}
		private void checkBox5_MouseEnter(object sender, EventArgs e)
		{
			CheckBox chk = ((CheckBox)sender);
			chk.Size = new Size(50, 30);
			chk.BringToFront();
			chk.Font = new Font(chk.Font, FontStyle.Bold);
			chk.ForeColor = Color.Crimson;
		}
		private void checkBox5_MouseLeave(object sender, EventArgs e)
		{
			CheckBox chk = ((CheckBox)sender);
			if (sender != checkBox5)
			{
				chk.SendToBack();
				chk.Font = new Font(chk.Font, FontStyle.Regular);
			}
			chk.ForeColor = Color.Black;
			chk.Size = new Size(42, 22);
		}
		private void radioButton5_MouseEnter(object sender, EventArgs e)
		{
			RadioButton rad = ((RadioButton)sender);
			rad.Size = new Size(50, 30);
			rad.BringToFront();
			rad.Font = new Font(rad.Font, FontStyle.Bold);
			rad.ForeColor = Color.DodgerBlue;
			//rad.BackColor = Color.White;
		}
		private void radioButton5_MouseLeave(object sender, EventArgs e)
		{
			RadioButton rad = ((RadioButton)sender);
			if (sender != radioButton5)
			{
				rad.SendToBack();
				rad.Font = new Font(rad.Font, FontStyle.Regular);
			}
			rad.ForeColor = Color.Black;
			//rad.BackColor = rad.Checked?Color.LightSteelBlue:Color.FromName("Control");
			rad.Size = new Size(42, 22);
		}
		private void radioButton5_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton rad = (RadioButton)sender;
			if (rad.Checked)
			{
				rad.BackColor = Color.LightSteelBlue;
			}
			else
			{
				rad.BackColor = Color.FromName("Control");
			}
		}
		#endregion
		enum aa
		{
			a,
			aa,
			aaa
		}

		
	}
	public static class Extensions
	{
		public static T Create<T>(this T @this)
			where T : class, new()
		{
			return Utility<T>.Create();
		}
	}

	public static class Utility<T>
		where T : class, new()
	{
		static Utility()
		{
			Create = System.Linq.Expressions.Expression.Lambda<Func<T>>(System.Linq.Expressions.Expression.New(typeof(T).GetConstructor(Type.EmptyTypes))).Compile();
		}
		public static Func<T> Create { get; private set; }
	}
}
