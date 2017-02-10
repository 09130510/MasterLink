using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using DevAge.Drawing;
using SourceGrid.Cells.Controllers;
using SourceGrid.Selection;

namespace SourceCell
{
	//x public class CustomCell : SourceGrid.Cells.Cell
	//x {
	//x     private string displayText = null;
	//x     public override string DisplayText
	//x     {
	//x         get
	//x         {
	//x             if (displayText == null)
	//x             {
	//x                 return base.DisplayText;
	//x             }
	//x             else
	//x             {
	//x                 return displayText;
	//x             }
	//x         }
	//x         set
	//x         {
	//x             displayText = value;
	//x         }
	//x     }
	//x }
	/// <summary>
	/// TextBox 的Cell
	/// </summary>
	public class TextCell : CellBase
	{
		/// <summary>
		/// NumericUpDown才有作用
		/// </summary>
		public event CancelEventHandler OnBeforeIncrement;
		/// <summary>
		/// NumericUpDown才有作用
		/// </summary>
		public event EventHandler OnAfterIncrement;
		/// <summary>
		/// NumericUpDown才有作用
		/// </summary>
		public event CancelEventHandler OnBeforeDecrement;
		/// <summary>
		/// NumericUpDown才有作用
		/// </summary>
		public event EventHandler OnAfterDecrement;

		/// <summary>
		/// 【Text背景色】Default：Color.White
		/// </summary>
		public static Color TextBackColor = Color.White;
		/// <summary>
		/// 【Text字色】Default：Color.Black
		/// </summary>
		public static Color TextFontColor = Color.Black;
		
		#region Declaration
		/// <summary>
		/// 欄位型態
		/// </summary>
		public enum TextType
		{
			/// <summary>
			/// 整數
			/// </summary>
			Int,
			/// <summary>
			/// 文字
			/// </summary>
			String,
			/// <summary>
			/// 日期時間
			/// </summary>
			DateTime,
			/// <summary>
			/// 小數
			/// </summary>
			Double,
			/// <summary>
			/// 整數，且有上下箭頭
			/// </summary>
			Numeric,
			/// <summary>
			/// %; 0.05 顯示為5%
			/// </summary>
			Percent,
			/// <summary>
			/// 時間
			/// </summary>
			Time,
			/// <summary>
			/// Decimal
			/// </summary>
			Decimal,
			/// <summary>
			/// 按鈕
			/// </summary>
			Button,
			/// <summary>
			/// 日期
			/// </summary>
			Date
		}
		/// <summary>
		/// Cell
		/// </summary>
		private SourceGrid.Cells.Cell field;
		/// <summary>
		/// Value
		/// </summary>
		private object fvalue;
		/// <summary>
		/// 欄位型態
		/// </summary>
		private TextType texttype;
		/// <summary>
		/// 合併欄位
		/// </summary>
		private int columnspan = 1;
		/// <summary>
		/// 合併欄位
		/// </summary>
		private int rowspan = 1;
		/// <summary>
		/// 點擊修改模式
		/// </summary>
		private SourceGrid.EditableMode em = SourceGrid.EditableMode.Default;
		/// <summary>
		/// 內容對齊方式
		/// </summary>
		private DevAge.Drawing.ContentAlignment textAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;
		/// <summary>
		/// 字型名稱
		/// </summary>
		private FontName fontname = FontName.Arial;
		/// <summary>
		/// 字型大小
		/// </summary>
		private float fontsize = 8;
		/// <summary>
		/// 字型樣式
		/// </summary>
		private FontStyle fontstyle = FontStyle.Regular;
		/// <summary>
		/// 有無邊框
		/// </summary>
		private bool hasborder = false;
		/// <summary>
		/// 有無Tip
		/// </summary>
		private bool usetip = false;
		/// <summary>
		/// Tip內容
		/// </summary>
		private string tooltip = string.Empty;

		/// <summary>
		/// 格式化
		/// </summary>
		private string format = string.Empty;
		/// <summary>
		/// 邊框
		/// </summary>
		private RectangleBorder border = RectangleBorder.CreateInsetBorder(1, BorderShadowColor, BorderLightColor);
		#endregion

		#region Variable
		private object[] preColor;
		/// <summary>
		/// Default BackColor
		/// </summary>
		private Color dbackcolor;
		/// <summary>
		/// BackColor
		/// </summary>
		private Color backcolor;
		/// <summary>
		/// Default FontColor
		/// </summary>
		private Color dfontcolor;
		/// <summary>
		/// FontColor
		/// </summary>
		private Color fontcolor;
		private System.Drawing.Image image;
		//! 2014/01/27 For NumericUpDown
		private Type m_NumericType = null;
		private decimal? m_Maximum = null;
		private decimal? m_Minimum = null;
		private decimal? m_Increment = null;
		#endregion

		#region Property
		/// <summary>
		/// SourceGrid.Cells.ICell
		/// </summary>
		public override SourceGrid.Cells.ICell Cell { get { return field; } protected set { field = (SourceGrid.Cells.Cell)value; } }
		/// <summary>
		/// Cell
		/// </summary>
		public SourceGrid.Cells.Cell Field
		{
			get { return field; }
		}
		/// <summary>
		/// Cell Value
		/// </summary>
		public object Value
		{
			get
			{
				if (fvalue != null)
				{
					return fvalue;
				}
				else
				{
					switch (texttype)
					{
						case TextType.Int:
						case TextType.Double:
						case TextType.Numeric:
						case TextType.Percent:
						case TextType.Decimal:
							return 0;
						case TextType.String:
							return string.Empty;
						case TextType.DateTime:
							return DateTime.Now;
						case TextType.Time:
							return DateTime.Now.ToShortTimeString();
						case  TextType.Date:
							return DateTime.Now.ToLongDateString();							
						default:
							return string.Empty;
					}
				}
			}
			set
			{
				fvalue = value;
				if (field != null)
				{
					field.Value = value;
				}
			}
		}
		/// <summary>
		/// 欄位型態
		/// </summary>
		public TextType CellType
		{
			get { return texttype; }
			set
			{
				texttype = value;
				SetTextType();
			}
		}
		/// <summary>
		/// 合併欄位; 預設值: 1
		/// </summary>
		public int ColumnSpan
		{
			get { return columnspan; }
			set
			{
				columnspan = value;
				if (field != null)
				{
					field.ColumnSpan = columnspan;
				}
			}
		}
		/// <summary>
		/// 合併欄位; 預設值: 1
		/// </summary>
		public int RowSpan
		{
			get { return rowspan; }
			set
			{
				rowspan = value;
				if (field != null)
				{
					field.RowSpan = rowspan;
				}
			}
		}
		/// <summary>
		/// 欄位修改模式
		/// </summary>
		public SourceGrid.EditableMode EditMode
		{
			get { return em; }
			set
			{
				em = value;
				if (field != null)
				{
					field.Editor.EditableMode = em;
				}
			}
		}
		/// <summary>
		/// 內容對齊方式;　預設值:MiddleCenter
		/// </summary>
		public DevAge.Drawing.ContentAlignment TextAlignment
		{
			get { return textAlignment; }
			set
			{
				textAlignment = value;
				if (field != null)
				{
					field.View.TextAlignment = textAlignment;
				}
			}
		}
		/// <summary>
		/// 字形; 預設Arial
		/// </summary>
		public new FontName FontName
		{
			get { return fontname; }
			set
			{
				fontname = value;
				if (field != null)
				{
					SetFontType((SourceGrid.Cells.Views.Cell)field.View, fontname.ToString(), fontsize, fontstyle);
				}
			}
		}
		/// <summary>
		/// 字型大小; 預設 8
		/// </summary>
		public float FontSize
		{
			get { return fontsize; }
			set
			{
				fontsize = value;
				if (field != null)
				{
					SetFontType((SourceGrid.Cells.Views.Cell)field.View, fontname.ToString(), fontsize, fontstyle);
				}
			}
		}
		/// <summary>
		/// 字型樣式; 預設 一般
		/// </summary>
		public FontStyle FontStyle
		{
			get { return fontstyle; }
			set
			{
				fontstyle = value;
				if (field != null)
				{
					SetFontType((SourceGrid.Cells.Views.Cell)field.View, fontname.ToString(), fontsize, fontstyle);
				}
			}
		}
		/// <summary>
		/// 是否有邊框; 預設值: False
		/// </summary>
		public bool HasBorder
		{
			get { return hasborder; }
			set
			{
				hasborder = value;
				if (field != null)
				{
					SetBorder((SourceGrid.Cells.Views.Cell)field.View, hasborder);
					if (hasborder && this.border != null)
					{
						field.View.Border = this.border;
					}
				}
			}
		}
		/// <summary>
		/// 設定邊框樣式
		/// </summary>
		public RectangleBorder Border
		{
			get { return this.border; }
			set
			{
				this.border = value;
				if (field != null && this.HasBorder && this.border != null)
				{
					field.View.Border = this.border;
				}
			}
		}
		/// <summary>
		/// 是否有Tip; 預設值: False
		/// </summary>
		public bool UseTip
		{
			get { return usetip; }
			set
			{
				usetip = value;
				SetTip();
			}
		}
		/// <summary>
		/// Tip內容
		/// </summary>
		public string ToolTip
		{
			get { return tooltip; }
			set
			{
				tooltip = value;
				if (field != null && usetip)
				{
					field.ToolTipText = tooltip;
				}
			}
		}
		/// <summary>
		/// 編輯狀態; 預設值: false
		/// </summary>
		public override bool Enable
		{
			get { return base.Enable; }
			set
			{
				base.Enable = value;
				if (field != null)
				{
					field.Editor.EnableEdit = Enable;
				}
			}
		}
		/// <summary>
		/// 換行
		/// </summary>
		public override bool WordWrap
		{
			get
			{				
				return base.WordWrap;
			}
			set
			{
				base.WordWrap = value;
				if (field!= null)
				{
					field.View.WordWrap = value;
				}
			}
		}

		/// <summary>
		/// Tag
		/// </summary>
		public override object Tag
		{
			get
			{
				return base.Tag;
			}
			set
			{
				base.Tag = value;
				if (field != null)
				{
					field.Tag = Tag;
				}
			}
		}
		/// <summary>
		/// Default BackColor
		/// </summary>
		public override Color DefaultBackColor
		{
			get
			{
				if (dbackcolor == new Color()) { dbackcolor = TextBackColor; }
				return dbackcolor;
			}
			set
			{
				dbackcolor = value;
				if (BackColor == new Color()) { BackColor = dbackcolor; }
			}
		}
		/// <summary>
		/// Default FontColor
		/// </summary>
		public override Color DefaultFontColor
		{
			get
			{
				if (dfontcolor == new Color()) { dfontcolor = TextFontColor; }
				return dfontcolor;
			}
			set
			{
				dfontcolor = value;
				if (FontColor == new Color()) { FontColor = dfontcolor; }
			}
		}
		/// <summary>
		/// BackColor
		/// </summary>
		public override Color BackColor
		{
			get { return backcolor; }
			set
			{
				backcolor = value;
				SetBackColor(value);
			}
		}
		/// <summary>
		/// FontColor
		/// </summary>
		public override Color FontColor
		{
			get { return fontcolor; }
			set
			{
				fontcolor = value;
				SetFontColor(value);
			}
		}
		/// <summary>
		/// 設定圖片
		/// </summary>
		public override Image Image
		{
			get
			{
				return image;
			}
			set
			{
				image = value;
				SetImage(value);
			}
		}
		/// <summary>
		/// 格式化
		/// </summary>
		public string Format
		{
			get { return format; }
			set
			{
				format = value;
				SetTextType();
			}
		}

		//! 2014/01/27 ForNumericUpDown
		/// <summary>
		/// UpDown Button的Type, 非Numeric無作用
		/// </summary>
		public Type NumericType
		{
			get { return m_NumericType; }
			set
			{
				if (value == m_NumericType) return;
				m_NumericType = value;
				if (CellType == TextType.Numeric) SetTextType();
			}
		}
		/// <summary>
		/// UpDown Button的最大值, 非Numeric無作用
		/// </summary>
		public decimal? Maximum
		{
			get { return m_Maximum; }
			set
			{
				if (value == m_Maximum) return;
				m_Maximum = value;
				if (CellType == TextType.Numeric) SetTextType();
			}
		}
		/// <summary>
		/// UpDown Button的最小值, 非Numeric無作用
		/// </summary>
		public decimal? Minimum
		{
			get { return m_Minimum; }
			set
			{
				if (value == m_Minimum) return;
				m_Minimum = value;
				if (CellType == TextType.Numeric) SetTextType();
			}
		}
		/// <summary>
		/// UpDown Button的增減值, 非Numeric無作用
		/// </summary>
		public decimal? Increment
		{
			get { return m_Increment; }
			set
			{
				if (value == m_Increment) return;
				m_Increment = value;
				//x if (CellType == TextType.Numeric) SetTextType();
				if (CellType == TextType.Numeric)
				{
					((SourceGrid.Cells.Editors.NumericUpDown)field.Editor).Increment = (decimal)value;
				}
			}
		}
		#endregion

		/// <summary>
		/// 建構; 商品資料建構時輸入後無法修改，欄位資料可使用範例方式建立
		/// 範例: _TextCell(){BackColor =Color.White}
		/// </summary>		
		public TextCell()
		{
			SetCell();
		}
		/// <summary>
		/// 建構
		/// </summary>
		/// <param name="Value"></param>
		/// <param name="CellType"></param>
		public TextCell(object Value, Type CellType)
		{
			this.Value = Value;
			SourceGrid.Cells.Views.Cell v = new SourceGrid.Cells.Views.Cell();
			//對齊方式
			v.TextAlignment = textAlignment;
			v.BackColor = DefaultBackColor;
			v.ForeColor = DefaultFontColor;
			//字型			
			SetFontType(v, fontname.ToString(), fontsize, fontstyle);

			//邊框
			SetBorder(v, hasborder);
			if (this.border != null && this.hasborder)
			{
				v.Border = border;
			}


			field = new SourceGrid.Cells.Cell(Value, CellType);			
			if (CellType==typeof(string) )
			{
				this.CellType = TextType.String;
			}
			else if (CellType == typeof(int))
			{
				this.CellType = TextType.Int;
			}
			else if (CellType == typeof(double) )
			{
				this.CellType = TextType.Double;
			}
			else if (CellType == typeof(decimal))
			{
				this.CellType = TextType.Decimal;
			}
			else if (CellType == typeof(DateTime))
			{
				this.CellType = TextType.DateTime;
			}			

			field.View = v;
			
			//Tip
			SetTip();			
			//合併欄位
			field.ColumnSpan = columnspan;
			field.RowSpan = rowspan;
			//Tag
			field.Tag = base.Tag;
			//EnableEdit
			field.Editor.EnableEdit = this.Enable;
			//Cell Event Control
			field.AddController(Controller);					
		}

		#region Delegate
		/// <summary>
		/// Value Changed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected override void DoValueChanged(object sender, EventArgs e)
		{
			this.Value = this.Cell.Value;
			base.DoValueChanged(sender, e);			
		}
		#endregion

		#region Public
		/// <summary>
		/// 設定背景色
		/// </summary>
		/// <param name="_Color">要設定的背景色, null則還原為預設值</param>
		public override void SetBackColor(object _Color)
		{
			if (field != null)
			{
				if (field.Grid != null && field.Grid.InvokeRequired)
				{

					SetColor d = new SetColor(SetBackColor);
					field.Grid.Invoke(d, new object[] { _Color });
				}
				else
				{
					if (_Color != null)
					{
						field.View.BackColor = (Color)_Color;
					}
					else
					{
						//還原顏色                
						field.View.BackColor = DefaultBackColor;
					}
					backcolor= field.View.BackColor;
					if (field.Grid != null)
					{
						//HACK RowSpan和ColumnSpan時會只更新一半
						//SourceGrid.Range r = new SourceGrid.Range(field.Row.Index, field.Column.Index, field.Row.Index , field.Column.Index );
						//field.Grid.InvalidateRange(r);
						field.Grid.InvalidateCell(field);
					}
				}
			}
		}
		/// <summary>
		/// 設定字色
		/// </summary>
		/// <param name="_Color">要設定的背景色, null則還原為預設值</param>
		public override void SetFontColor(object _Color)
		{
			if (field != null)
			{
				if (field.Grid != null && field.Grid.InvokeRequired)
				{

					SetColor d = new SetColor(SetFontColor);
					field.Grid.Invoke(d, new object[] { _Color });
				}
				else
				{
					if (_Color != null)
					{
						field.View.ForeColor = (Color)_Color;
					}
					else
					{
						//還原顏色                
						field.View.ForeColor = DefaultFontColor;
					}
					fontcolor = field.View.ForeColor;
					if (field.Grid != null)
					{
						//HACK RowSpan和ColumnSpan時會只更新一半
						//SourceGrid.Range r = new SourceGrid.Range(field.Row.Index, field.Column.Index, field.Row.Index , field.Column.Index );
						//field.Grid.InvalidateRange(r);
						field.Grid.InvalidateCell(field);
					}
				}
			}
		}
		/// <summary>
		/// 設定圖片
		/// </summary>
		/// <param name="_Image"></param>
		public override void SetImage(Image _Image)
		{
			if (field != null)
			{
				if (field.Grid != null)
				{
					if (field.Grid.InvokeRequired)
					{
						SetImage d = new SetImage(SetImage);
						field.Grid.Invoke(d, new object[] { _Image });
					}
					else
					{
						field.Image = _Image;
						field.Grid.InvalidateCell(field);
					}
				}
				else
				{
					field.Image = _Image;
				}
			}
		}
		/// <summary>
		/// 設定內容及背景色
		/// </summary>
		/// <param name="_Txt">內容</param>
		/// <param name="_Color">背景色;null自動代入DefaultBackColor</param>
		public override void SetValueColor(object _Txt, object _Color)
		{
			SetValue(_Txt);
			SetBackColor(_Color);
		}
		/// <summary>
		/// 設定內容、背景色、字色
		/// </summary>
		/// <param name="_Txt">內容</param>
		/// <param name="_BC">背景色; null代入DefaultBackColor</param>
		/// <param name="_FC">字色; null代入DefaultFontColor</param>
		public override void SetValueColor(object _Txt, object _BC, object _FC)
		{
			SetValue(_Txt);
			SetBackColor(_BC);
			SetFontColor(_FC);
		}
		/// <summary>
		/// 設定欄位值
		/// </summary>
		/// <param name="_Txt">欄位值</param>
		public override void SetValue(object _Txt)
		{
			if (field != null)
			{
				if (field.Grid != null)
				{
					if (field.Grid.InvokeRequired)
					{
						SetCellValue d = new SetCellValue(SetValue);
						field.Grid.Invoke(d, new object[] { _Txt });
					}
					else
					{
						fvalue = _Txt;
						field.Value = _Txt;

						field.Grid.InvalidateCell(field);
					}
				}
				else
				{
					fvalue = _Txt;
					field.Value = _Txt;
				}
			}
		}
		/// <summary>
		/// 選取/取消選取 欄位所在的Row
		/// </summary>
		/// <param name="IsSelect">是否選取</param>        
		public override void SelectRow(bool IsSelect)
		{
			if (field.Grid != null)
			{
				field.Grid.Selection.SelectRow(field.Row.Index, IsSelect);
			}
		}
		/// <summary>
		/// 選取/取消選取 欄位所在Row的某個Cell
		/// </summary>
		/// <param name="ColumnIndex">Cell的Column Index</param>
		/// <param name="IsSelect">是否選取</param>
		/// <param name="SelectionColor">選取顏色</param>
		public override void SelectCell(int ColumnIndex, bool IsSelect, Color? SelectionColor = null)
		{
			if (field.Grid != null)
			{
				field.Grid.Selection.ResetSelection(false);
				//x if (IsSelect)
				//x {
				if (SelectionColor != null)
				{
					//x (field.Grid.Selection as SelectionBase).Border.SetColor((Color)SelectionColor);
					DevAge.Drawing.RectangleBorder border = (field.Grid.Selection as SelectionBase).Border;
					border.SetColor((Color)SelectionColor);
					//x border.SetWidth(1);
					(field.Grid.Selection as SelectionBase).Border = border;
				}
				//x }
				//x else
				//x {
				//x }
				field.Grid.Selection.SelectCell(new SourceGrid.Position(field.Row.Index, ColumnIndex), IsSelect);
			}
		}
		/// <summary>
		/// 整Row上色
		/// </summary>
		/// <param name="_Color">null 則還原為預設值</param>
		public override void PaintRow(Color _Color)
		{
			if (field.Grid != null)
			{
				for (int i = 0; i < field.Grid.Columns.Count; i++)
				{
					for (int j = field.Row.Index; j < field.Row.Index + this.RowSpan; j++)
					{
						if (field.Grid.GetCell(j, i) != null && !(field.Grid.GetCell(j, i).GetType().FullName.Equals("SourceGrid.Cells.ColumnHeader")))
						{
							field.Grid.GetCell(j, i).View.BackColor = _Color;
							//改背景色,要更新原本記住的顏色
							if (this.preColor[i] != null)
							{
								this.preColor[i] = _Color;
							}
						}
					}
				}

				SourceGrid.Range r = new SourceGrid.Range(field.Row.Index, 0, field.Row.Index + this.RowSpan - 1, field.Grid.Columns.Count - 1);
				field.Grid.InvalidateRange(r);
			}
		}
		/// <summary>
		/// 整Row上色, 並記住之前顏色
		/// </summary>
		/// <param name="_Color">上色</param>
		public override void PaintRowByMemory(Color _Color)
		{
			if (field.Grid != null)
			{
				if (this.preColor == null)
				{
					this.preColor = new object[field.Grid.Columns.Count];
				}

				for (int i = 0; i < field.Grid.Columns.Count; i++)
				{
					for (int j = field.Row.Index; j < field.Row.Index + this.RowSpan; j++)
					{

						if (field.Grid.GetCell(j, i) != null && !(field.Grid.GetCell(j, i).GetType().FullName.Equals("SourceGrid.Cells.ColumnHeader")))
						{
							if (this.preColor[i] == null)
							{
								this.preColor[i] = field.Grid.GetCell(j, i).View.BackColor;
							}
							field.Grid.GetCell(j, i).View.BackColor = _Color;
						}
					}
				}
				SourceGrid.Range r = new SourceGrid.Range(field.Row.Index, 0, field.Row.Index + this.RowSpan - 1, field.Grid.Columns.Count - 1);
				field.Grid.InvalidateRange(r);
			}
		}
		/// <summary>
		/// 整Row上色(還原為之前記住的顏色)
		/// </summary>
		public override void PaintRowFromMemory()
		{
			if (field.Grid != null)
			{

				for (int i = 0; i < field.Grid.Columns.Count; i++)
				{
					for (int j = field.Row.Index; j < field.Row.Index + this.RowSpan; j++)
					{
						if (field.Grid.GetCell(j, i) != null && !(field.Grid.GetCell(j, i).GetType().FullName.Equals("SourceGrid.Cells.ColumnHeader")))
						{
							if (this.preColor != null && this.preColor[i] != null)
							{
								Color c = (Color)this.preColor[i];
								field.Grid.GetCell(j, i).View.BackColor = c;
							}
						}
					}
					this.preColor[i] = null;
				}
				SourceGrid.Range r = new SourceGrid.Range(field.Row.Index, 0, field.Row.Index + this.RowSpan - 1, field.Grid.Columns.Count - 1);
				field.Grid.InvalidateRange(r);
			}
		}
		/// <summary>
		/// 物件資料綁定
		/// 2014/02/07 Modify for Array Binding
		/// </summary>
		/// <param name="type">物件型態</param>
		/// <param name="PropertyName">綁定的物件屬性</param>
		/// <param name="LinkObject">綁定的物件</param>
		/// <param name="Index">綁定的物件的index</param>
		/// <returns>是否成功</returns>
		public override bool SetDataBinding(Type type, string PropertyName, object LinkObject, int Index =  -1)
		{
			PropertyInfo p = type.GetProperty(PropertyName);
			//! 2013/07/30 有點多餘 會因為型態擋掉太多東西
			//x switch (this.CellType)
			//x {
			//x     case TextType.Int:
			//x         if (p.PropertyType != typeof(int)) { return false; }
			//x         break;
			//x     case TextType.String:
			//x         if (p.PropertyType != typeof(string)) { return false; }
			//x         break;
			//x     case TextType.DateTime:
			//x     case TextType.Time:
			//x         break;
			//x     case TextType.Double:
			//x     case TextType.Percent:
			//x         if (p.PropertyType != typeof(double)) { return false; }
			//x         break;
			//x     case TextType.Numeric:
			//x         if (p.PropertyType != typeof(int)) { ret__urn false; }
			//x         break;
			//x     case TextType.Decimal:
			//x         if (p.PropertyType != typeof(decimal)) { return false; }
			//x         break;
			//x }
			this.Cell.AddController(new BindProperty(this.Cell, p, LinkObject,Index)); ;
			return true;
		}
		#endregion

		#region Private
		/// <summary>
		/// 建立欄位
		/// </summary>
		private void SetCell()
		{
			SourceGrid.Cells.Views.Cell v = new SourceGrid.Cells.Views.Cell();
			//對齊方式
			v.TextAlignment = textAlignment;
			v.BackColor = DefaultBackColor;
			v.ForeColor = DefaultFontColor;
			v.WordWrap = WordWrap;
			//字型			
			SetFontType(v, fontname.ToString(), fontsize, fontstyle);

			//邊框
			SetBorder(v, hasborder);
			if (this.border != null && this.hasborder)
			{
				v.Border = border;
			}

			field = new SourceGrid.Cells.Cell();
			field.View = v;

			//欄位型態
			SetTextType();
			//Tip
			SetTip();
			//初始值
			field.Value = fvalue;
			//合併欄位
			field.ColumnSpan = columnspan;
			field.RowSpan = rowspan;
			//Tag
			field.Tag = base.Tag;
			//EnableEdit
			field.Editor.EnableEdit = this.Enable;
			//Cell Event Control
			field.AddController(Controller);
		}
		/// <summary>
		/// 設定Tip
		/// </summary>        
		private void SetTip()
		{
			if (field != null)
			{
				if (usetip)
				{
					field.AddController(tip);
					field.ToolTipText = tooltip;
				}
				else
				{
					field.RemoveController(tip);
				}
			}
		}
		/// <summary>
		/// 設定欄位型態
		/// </summary>
		private void SetTextType()
		{
			if (field != null)
			{
				switch (texttype)
				{
					case TextType.Int:
						field.Editor = new SourceGrid.Cells.Editors.TextBox(typeof(int));
						if (!String.IsNullOrEmpty(format)) field.Editor.TypeConverter = new DevAge.ComponentModel.Converter.NumberTypeConverter(typeof(int), format);
						break;
					case TextType.String:
						field.Editor = new SourceGrid.Cells.Editors.TextBox(typeof(string));
						break;
					case TextType.DateTime:
						field.Editor = new SourceGrid.Cells.Editors.DateTimePicker();
						break;
					case TextType.Double:
						field.Editor = new SourceGrid.Cells.Editors.TextBox(typeof(double));
						if (!String.IsNullOrEmpty(format)) field.Editor.TypeConverter = new DevAge.ComponentModel.Converter.NumberTypeConverter(typeof(double), format);
						break;
					case TextType.Numeric:
						//! 2014/01/27 ForNumericUpDown
						//x field.Editor = new SourceGrid.Cells.Editors.NumericUpDown(typeof(int), 1000, 0, 1);						
						if (NumericType != null && Maximum != null && Minimum != null && Increment != null)
						{
							SourceGrid.Cells.Editors.NumericUpDown editor = new SourceGrid.Cells.Editors.NumericUpDown(NumericType, (decimal)Maximum, (decimal)Minimum, (decimal)Increment);
							//小數點位數寫死兩位
							editor.Control.DecimalPlaces = 2;
							field.Editor = editor;
							editor.BeforeValueIncrement += (sender, e) => { if (OnBeforeIncrement != null)OnBeforeIncrement(sender, e); };
							editor.AfterValueIncrement += (sender, e) => { if (OnAfterIncrement != null)OnAfterIncrement(sender, e); };
							editor.BeforeValueDecrement += (sender, e) => { if (OnBeforeDecrement != null)OnBeforeDecrement(sender, e); };
							editor.AfterValueDecrement += (sender, e) => { if (OnAfterDecrement != null)OnAfterDecrement(sender, e); };
						}
						else
						{
							field.Editor = new SourceGrid.Cells.Editors.NumericUpDown(typeof(int), 1000, 0, 1);
						}
						break;
					case TextType.Percent:
						field.Editor = SourceGrid.Cells.Editors.Factory.Create(typeof(double), fvalue, true, null, false,  new DevAge.ComponentModel.Converter.PercentTypeConverter(typeof(double)), null);					 
						break;
					case TextType.Time:
						field.Editor = new SourceGrid.Cells.Editors.TimePicker();
						break;
					case TextType.Date:
						field.Editor = new SourceGrid.Cells.Editors.DatePicker();
						break;
					case TextType.Decimal:
						field.Editor = new SourceGrid.Cells.Editors.TextBox(typeof(decimal));
						if (!String.IsNullOrEmpty(format)) field.Editor.TypeConverter = new DevAge.ComponentModel.Converter.NumberTypeConverter(typeof(decimal), format);
						break;
					case TextType.Button:
						field.Editor = new SourceGrid.Cells.Editors.Button ();
						//x if (!String.IsNullOrEmpty(format)) field.Editor.TypeConverter = new DevAge.ComponentModel.Converter.NumberTypeConverter(typeof(decimal), format);
						break;
				}
				field.Editor.EditableMode = em;
				field.Editor.EnableEdit = this.Enable;
			}
		}
		#endregion
		//x protected override void DoDispose()
		//x {
		//x     base.DoDispose();
		//x     field = null;
		//x }
		
	}
}