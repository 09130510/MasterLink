using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using SourceGrid;
using SourceGrid.Cells;
using SourceGrid.Cells.Controllers;

namespace SourceCell
{
	#region Delegate
	/// <summary>
	/// Click
	/// </summary>	
	/// <param name="cell">欄位</param>
	/// <param name="e"></param>
	public delegate void OnClickDelegate(CellBase cell, EventArgs e);
	/// <summary>
	/// DoubleClick
	/// </summary>	
	/// <param name="cell">欄位</param>
	/// <param name="e"></param>
	public delegate void OnDoubleClickDelegate(CellBase cell, EventArgs e);
	/// <summary>
	/// ValueChange
	/// </summary>	
	/// <param name="cell">欄位</param>
	/// <param name="e"></param>
	public delegate void OnValueChangedDelegate(CellBase cell, EventArgs e);
	/// <summary>
	/// MouseEnter
	/// </summary>	
	/// <param name="cell">欄位</param>
	/// <param name="e"></param>
	public delegate void OnMouseEnterDelegate(CellBase cell, EventArgs e);
	/// <summary>
	/// MouseLeave
	/// </summary>	
	/// <param name="cell">欄位</param>
	/// <param name="e"></param>
	public delegate void OnMouseLeaveDelegate(CellBase cell, EventArgs e);
	/// <summary>
	/// Value Changing
	/// </summary>	
	/// <param name="cell">欄位</param>
	/// <param name="e"></param>
	public delegate void OnValueChangingDelegate(CellBase cell, ValueChangeEventArgs e);
	/// <summary>
	/// 
	/// </summary>
	/// <param name="cell"></param>
	/// <param name="e"></param>
	public delegate void OnKeyDownDelegate(CellBase cell, KeyEventArgs e);
	/// <summary>
	/// 
	/// </summary>
	/// <param name="cell"></param>
	/// <param name="e"></param>
	public delegate void OnKeyPressDelegate(CellBase cell, KeyPressEventArgs e);
	/// <summary>
	/// 
	/// </summary>
	/// <param name="cell"></param>
	/// <param name="e"></param>
	public delegate void OnKeyUpDelegate(CellBase cell, KeyEventArgs e);
	/// <summary>
	/// 
	/// </summary>
	/// <param name="cell"></param>
	/// <param name="e"></param>
	public delegate void OnEditEndedDelegate(CellBase cell, EventArgs e);
	/// <summary>
	/// 
	/// </summary>
	/// <param name="cell"></param>
	/// <param name="e"></param>
	public delegate void OnEditStartedDelegate(CellBase cell, EventArgs e);
	/// <summary>
	/// 
	/// </summary>
	/// <param name="cell"></param>
	/// <param name="e"></param>
	public delegate void OnEditStartingDelegate(CellBase cell, CancelEventArgs e);
	/// <summary>
	/// 
	/// </summary>
	/// <param name="cell"></param>
	/// <param name="e"></param>
	public delegate void OnMouseDownDelegate(CellBase cell, MouseEventArgs e);
	/// <summary>
	/// 
	/// </summary>
	/// <param name="cell"></param>
	/// <param name="e"></param>
	public delegate void OnMouseUpDelegate(CellBase cell, MouseEventArgs e);
	/// <summary>
	/// 改變內容 背景色
	/// </summary>
	/// <param name="txt">內容</param>
	/// <param name="color">背景色</param>
	public delegate void SetCellValueColor(object txt, object color);
	/// <summary>
	/// 改變內容 背景色 字色
	/// </summary>
	/// <param name="txt">內容</param>
	/// <param name="bc">背景色</param>
	/// <param name="fc">字色</param>
	public delegate void SetValueBFColor(object txt, object bc, object fc);
	/// <summary>
	/// 改變內容
	/// </summary>
	/// <param name="txt">內容</param>
	public delegate void SetCellValue(object txt);
	/// <summary>
	/// 改變Checked狀態
	/// </summary>
	/// <param name="_Checked">Checked</param>
	public delegate void SetCheckBoxChecked(bool _Checked);
	/// <summary>
	/// 改變背景色
	/// </summary>
	/// <param name="_Color">背景色</param>
	public delegate void SetColor(object _Color);
	/// <summary>
	/// 設定圖片
	/// </summary>
	/// <param name="_img"></param>
	public delegate void SetImage(System.Drawing.Image _img);
	#endregion

	/// <summary>
	/// Source Cell Base
	/// </summary>
	public abstract class CellBase : IDisposable
	{
		#region Static
		/// <summary>
		/// 【邊框陰影色】Default：Color.White
		/// </summary>
		public static Color BorderShadowColor = Color.White;
		/// <summary>
		/// 【邊框色】Default：Color.Silver
		/// </summary>
		public static Color BorderLightColor = Color.Silver;
		#endregion

		#region Event
		/// <summary>
		/// Click
		/// </summary>
		public event OnClickDelegate OnClick;
		/// <summary>
		/// Double Click
		/// </summary>
		public event OnDoubleClickDelegate OnDoubleClick;
		/// <summary>
		/// Mouse Enter
		/// </summary>
		public event OnMouseEnterDelegate OnMouseEnter;
		/// <summary>
		/// Mouse Leave
		/// </summary>
		public event OnMouseLeaveDelegate OnMouseLeave;
		/// <summary>
		/// Value Change; 沒作用
		/// </summary>
		public event OnValueChangedDelegate OnValueChanged;
		/// <summary>
		/// Value Changing; 沒作用
		/// </summary>
		public event OnValueChangingDelegate OnValueChanging;
		/// <summary>
		/// 
		/// </summary>
		public event OnKeyDownDelegate OnKeyDown;
		/// <summary>
		/// 
		/// </summary>
		public event OnKeyPressDelegate OnKeyPress;
		/// <summary>
		/// 
		/// </summary>
		public event OnKeyUpDelegate OnKeyUp;
		/// <summary>
		/// 
		/// </summary>
		public event OnMouseDownDelegate OnMouseDown;
		/// <summary>
		/// 
		/// </summary>
		public event OnMouseUpDelegate OnMouseUp;
		/// <summary>
		/// 
		/// </summary>
		public event OnEditEndedDelegate OnEditEnded;
		/// <summary>
		/// 
		/// </summary>
		public event OnEditStartedDelegate OnEditStarted;
		/// <summary>
		/// 
		/// </summary>
		public event OnEditStartingDelegate OnEditStarting;
		#endregion

		#region Declaration
		private bool m_disposed = false;
		/// <summary>
		/// 字體
		/// </summary>
		public enum FontName
		{
			/// <summary>
			/// 
			/// </summary>
			Arial,
			/// <summary>
			/// 
			/// </summary>
			Century,
			/// <summary>
			/// 
			/// </summary>
			ComicSansMS,
			/// <summary>
			/// 
			/// </summary>
			Courier_New,
			/// <summary>
			/// 
			/// </summary>
			Dotum,
			/// <summary>
			/// 
			/// </summary>
			Gulim,
			/// <summary>
			/// 
			/// </summary>
			Gulimche,
			/// <summary>
			/// 
			/// </summary>
			Verdana,
			/// <summary>
			/// 
			/// </summary>
			新細明體
		}
		/// <summary>
		/// Cell Event Control
		/// </summary>
		protected CustomEvents control;
		/// <summary>
		/// Cell Tip Control
		/// </summary>
		protected ToolTipText tip = new ToolTipText();
		/// <summary>
		/// Tag
		/// </summary>
		private object tag;		
		/// <summary>
		/// Message
		/// </summary>
		private string message;		
		#endregion

		#region Property
		/// <summary>
		/// Tag
		/// </summary>
		public virtual object Tag
		{
			get { return tag; }
			set
			{
				tag = value;
			}
		}
		/// <summary>
		/// Default BackColor
		/// </summary>
		public abstract Color DefaultBackColor { get; set; }		
		/// <summary>
		/// Default FontColor
		/// </summary>
		public abstract Color DefaultFontColor { get; set; }		
		/// <summary>
		/// Default BackColor
		/// </summary>
		public abstract Color BackColor { get; set; }		
		/// <summary>
		/// Default FontColor
		/// </summary>
		public abstract Color FontColor { get; set; }		
		/// <summary>
		/// 圖示
		/// </summary>
		public abstract System.Drawing.Image Image { get; set; }
		/// <summary>
		/// Cell Event Controller
		/// </summary>
		public CustomEvents Controller { get { return control; } }
		/// <summary>
		/// Cell Message
		/// </summary>
		public string Message
		{
			get { return message; }
			set { this.message = value; }
		}
		/// <summary>
		/// CellContext
		/// </summary>
		public virtual CellContext Context { get; protected set; }
		/// <summary>
		/// 欄位
		/// </summary>
		public virtual ICell Cell { get; protected set; }
		/// <summary>
		/// Name
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// 編輯狀態
		/// </summary>		
		public virtual bool Enable { get; set; }
		/// <summary>
		/// 換行
		/// </summary>
		public virtual bool WordWrap { get; set; }
		#endregion

		/// <summary>
		/// 建構
		/// </summary>
		public CellBase()
		{
			control = new CustomEvents();
			control.Click += new EventHandler(DoClick);
			control.DoubleClick += new EventHandler(DoDoubleClick);
			control.ValueChanged += new EventHandler(DoValueChanged);
			control.ValueChanging += new ValueChangeEventHandler(DoValueChanging);
			control.MouseEnter += new EventHandler(DoMouseEnter);
			control.MouseLeave += new EventHandler(DoMouseLeave);
			control.KeyDown += new KeyEventHandler(DoKeyDown);
			control.KeyPress += new KeyPressEventHandler(DoKeyPress);
			control.KeyUp += new KeyEventHandler(DoKeyUp);
			control.MouseDown += new MouseEventHandler(DoMouseDown);
			control.MouseUp += new MouseEventHandler(DoMouseUp);
			control.EditEnded += new EventHandler(DoEditEnded);
			control.EditStarted += new EventHandler(DoEditStarted);
			control.EditStarting += new CancelEventHandler(DoEditStarting);
			tip.IsBalloon = true;
		}

		#region Delegate
		/// <summary>
		/// Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void DoClick(object sender, EventArgs e)
		{
			this.Context = (CellContext)sender;
			if (OnClick != null)
			{
				OnClick(this, e);
			}
		}
		/// <summary>
		/// Double Click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void DoDoubleClick(object sender, EventArgs e)
		{
			this.Context = (CellContext)sender;
			if (OnDoubleClick != null)
			{
				OnDoubleClick(this, e);
			}
		}
		/// <summary>
		/// Value Changing
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void DoValueChanging(object sender, SourceGrid.ValueChangeEventArgs e)
		{
			this.Context = (CellContext)sender;
			if (OnValueChanging != null)
			{
				OnValueChanging(this, e);
			}
		}
		/// <summary>
		/// Value Change
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void DoValueChanged(object sender, EventArgs e)
		{
			this.Context = (CellContext)sender;
			if (OnValueChanged != null)
			{
				OnValueChanged(this, e);
			}
		}
		/// <summary>
		/// Mouse Enter
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void DoMouseEnter(object sender, System.EventArgs e)
		{
			this.Context = (CellContext)sender;
			if (OnMouseEnter != null)
			{
				OnMouseEnter(this, e);
			}
		}
		/// <summary>
		/// Mouse Leave
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void DoMouseLeave(object sender, EventArgs e)
		{
			this.Context = (CellContext)sender;
			if (OnMouseLeave != null)
			{
				OnMouseLeave(this, e);
			}
		}
		/// <summary>
		/// Key Down
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void DoKeyDown(object sender, KeyEventArgs e)
		{
			this.Context = (CellContext)sender;
			if (OnKeyDown != null)
			{
				OnKeyDown(this, e);
			}
		}
		/// <summary>
		/// Key Press
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void DoKeyPress(object sender, KeyPressEventArgs e)
		{
			this.Context = (CellContext)sender;
			if (OnKeyPress != null)
			{
				OnKeyPress(this, e);
			}
		}
		/// <summary>
		/// Key Up
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void DoKeyUp(object sender, KeyEventArgs e)
		{
			this.Context = (CellContext)sender;
			if (OnKeyUp != null)
			{
				OnKeyUp(this, e);
			}
		}
		/// <summary>
		/// Mouse Down
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void DoMouseDown(object sender, MouseEventArgs e)
		{
			this.Context = (CellContext)sender;
			if (OnMouseDown != null)
			{
				OnMouseDown(this, e);
			}
		}
		/// <summary>
		/// Mouse Up
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void DoMouseUp(object sender, MouseEventArgs e)
		{
			this.Context = (CellContext)sender;
			if (OnMouseUp != null)
			{
				OnMouseUp(this, e);
			}
		}
		/// <summary>
		/// Edit Ended
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void DoEditEnded(object sender, EventArgs e)
		{
			this.Context = (CellContext)sender;
			if (OnEditEnded != null)
			{
				OnEditEnded(this, e);
			}
		}
		/// <summary>
		/// Edit Started
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void DoEditStarted(object sender, EventArgs e)
		{
			this.Context = (CellContext)sender;
			if (OnEditStarted != null)
			{
				OnEditStarted(this, e);
			}
		}
		/// <summary>
		/// EditStarting
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void DoEditStarting(object sender, CancelEventArgs e)
		{
			this.Context = (CellContext)sender;
			if (OnEditStarting != null)
			{
				OnEditStarting(this, e);
			}
		}

		#endregion

		/// <summary>
		/// 解構
		/// </summary>
		~CellBase()
		{
			RemoveDataBinding();
			control.Click -= new EventHandler(DoClick);
			control.DoubleClick -= new EventHandler(DoDoubleClick);
			control.ValueChanged -= new EventHandler(DoValueChanged);
			control.ValueChanging -= new ValueChangeEventHandler(DoValueChanging);
			control.MouseEnter -= new EventHandler(DoMouseEnter);
			control.MouseLeave -= new EventHandler(DoMouseLeave);
			control.KeyDown -= new KeyEventHandler(DoKeyDown);
			control.KeyPress -= new KeyPressEventHandler(DoKeyPress);
			control.KeyUp -= new KeyEventHandler(DoKeyUp);
			control.MouseDown -= new MouseEventHandler(DoMouseDown);
			control.MouseUp -= new MouseEventHandler(DoMouseUp);
			control.EditEnded -= new EventHandler(DoEditEnded);
			control.EditStarted -= new EventHandler(DoEditStarted);
			control.EditStarting -= new CancelEventHandler(DoEditStarting);
		}

		/// <summary>
		/// 字型設定
		/// </summary>
		/// <param name="v">Cell View</param>
		/// <param name="fontname">字型名稱</param>
		/// <param name="fontsize">大小</param>
		/// <param name="fontstyle">字型設定</param>
		protected void SetFontType(SourceGrid.Cells.Views.IView v, string fontname, float fontsize, FontStyle fontstyle)
		{
			v.Font = new Font(fontname, fontsize, fontstyle);
		}
		/// <summary>
		/// 欄位邊框
		/// </summary>
		/// <param name="v">Cell View</param>
		/// <param name="hasborder">是否有編框</param>
		protected void SetBorder(SourceGrid.Cells.Views.Cell v, bool hasborder)
		{
			if (hasborder)
			{
				v.Border = DevAge.Drawing.RectangleBorder.CreateInsetBorder(1, BorderShadowColor, BorderLightColor);
			}
			else
			{
				v.Border = DevAge.Drawing.RectangleBorder.NoBorder;
			}
		}
		/// <summary>
		/// 整Row上色
		/// </summary>
		/// <param name="_Color">null 則還原為預設值</param>
		public abstract void PaintRow(Color _Color);
		/// <summary>
		/// 整Row上色, 並記錄之前顏色
		/// </summary>
		/// <param name="_Color">顏色</param>
		public abstract void PaintRowByMemory(Color _Color);
		/// <summary>
		/// 整Row上色(還原成之前顏色)
		/// </summary>
		public abstract void PaintRowFromMemory();
		/// <summary>
		/// 選取/取消選取 欄位所在的Row
		/// </summary>
		/// <param name="IsSelect">是否選取</param>        
		public abstract void SelectRow(bool IsSelect);
		/// <summary>
		/// 選取/取消選取 欄位所在Row的某個Cell
		/// </summary>
		/// <param name="ColumnIndex">Cell的Column Index</param>
		/// <param name="IsSelect">是否選取</param>
		/// <param name="SelectionColor">選取顏色</param>
		public abstract void SelectCell(int ColumnIndex, bool IsSelect, Color? SelectionColor = null);
		/// <summary>
		/// 設定背景色
		/// </summary>
		/// <param name="_Color">要設定的顏色, null則還原為預設值</param>
		public abstract void SetBackColor(object _Color);
		/// <summary>
		/// 設定字色
		/// </summary>
		/// <param name="_Color">要設定的背景色, null則還原為預設值</param>
		public abstract void SetFontColor(object _Color);
		/// <summary>
		/// 設定欄位值
		/// </summary>
		/// <param name="_Txt">欄位值</param>
		public abstract void SetValue(object _Txt);
		/// <summary>
		/// 設定內容及背景色
		/// </summary>
		/// <param name="_Txt">內容</param>
		/// <param name="_Color">背景色;null自動代入DefaultBackColor</param>
		public abstract void SetValueColor(object _Txt, object _Color);
		/// <summary>
		/// 設定內容、背景色、字色
		/// </summary>
		/// <param name="_Txt">內容</param>
		/// <param name="_BC">背景色; null代入DefaultBackColor</param>
		/// <param name="_FC">字色; null代入DefaultFontColor</param>
		public abstract void SetValueColor(object _Txt, object _BC, object _FC);
		/// <summary>
		/// 設定圖片
		/// </summary>
		/// <param name="_Image"></param>
		public abstract void SetImage(System.Drawing.Image _Image);
		/// <summary>
		/// 資料綁定
		/// 2014/02/07 Modify For Array Binding
		/// </summary>
		/// <param name="type"></param>
		/// <param name="PropertyName"></param>
		/// <param name="LinkObject"></param>
		/// <param name="Index"></param>
		/// <returns></returns>
		public virtual bool SetDataBinding(Type type, string PropertyName, object LinkObject, int Index = -1) { return false; }
		/// <summary>
		/// 移除資料綁定
		/// </summary>
		public virtual void RemoveDataBinding()
		{
			IController controller = this.Cell.FindController<BindProperty>();
			while (controller != null)
			{
				this.Cell.RemoveController(controller);
				controller = this.Cell.FindController<BindProperty>();
			}
		}

		#region IDisposable 成員
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this); //要求系統不要呼叫指定物件的完成項。
		}

		protected void Dispose(bool IsDisposing)
		{
			if (m_disposed) return;

			if (IsDisposing)
			{
				DoDispose();
			}
			m_disposed = true;
		}

		protected virtual void DoDispose()
		{
			RemoveDataBinding();
			control.Click -= new EventHandler(DoClick);
			control.DoubleClick -= new EventHandler(DoDoubleClick);
			control.ValueChanged -= new EventHandler(DoValueChanged);
			control.ValueChanging -= new ValueChangeEventHandler(DoValueChanging);
			control.MouseEnter -= new EventHandler(DoMouseEnter);
			control.MouseLeave -= new EventHandler(DoMouseLeave);
			control.KeyDown -= new KeyEventHandler(DoKeyDown);
			control.KeyPress -= new KeyPressEventHandler(DoKeyPress);
			control.KeyUp -= new KeyEventHandler(DoKeyUp);
			control.MouseDown -= new MouseEventHandler(DoMouseDown);
			control.MouseUp -= new MouseEventHandler(DoMouseUp);
			control.EditEnded -= new EventHandler(DoEditEnded);
			control.EditStarted -= new EventHandler(DoEditStarted);
			control.EditStarting -= new CancelEventHandler(DoEditStarting);
		}
		#endregion
	}
}
