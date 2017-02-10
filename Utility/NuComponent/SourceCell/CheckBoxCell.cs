using System;
using System.Drawing;
using System.Reflection;
using SourceGrid.Cells.Controllers;
using SourceGrid.Selection;

namespace SourceCell
{
	/// <summary>
	/// CheckBox的Cell
	/// </summary>
	public class CheckBoxCell : CellBase
	{
		/// <summary>
		/// 【CheckBox背景色】Default：Color.White
		/// </summary>
		public static Color CheckBoxBackColor = Color.White;
		/// <summary>
		/// 【CheckBox字色】Default：Color.Black
		/// </summary>
		public static Color CheckBoxFontColor = Color.Black;

		#region Declaration
		/// <summary>
		/// Cell
		/// </summary>
		private SourceGrid.Cells.CheckBox field;
		//x /// <summary>
		//x /// Value
		//x /// </summary>
		//x private bool fvalue = false;
		/// <summary>
		/// 標題
		/// </summary>
		private string caption = string.Empty;
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
		/// CheckBox方塊對齊方式
		/// </summary>
		private DevAge.Drawing.ContentAlignment checkboxAlignment = DevAge.Drawing.ContentAlignment.MiddleLeft;
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
		//x /// <summary>
		//x /// 編輯狀態
		//x /// </summary>
		//x private bool enable = true;
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
		#endregion

		#region Property
		/// <summary>
		/// SourceGrid.Cells.ICell
		/// </summary>
		public override SourceGrid.Cells.ICell Cell { get { return field; } protected set { field = (SourceGrid.Cells.CheckBox)value; } }
		/// <summary>
		/// Cell
		/// </summary>
		public SourceGrid.Cells.CheckBox Field
		{
			get { return field; }
			private set { }
		}
		/// <summary>
		/// 標題; 預設值: String.Empty
		/// </summary>
		public string Caption
		{
			get { return caption; }
			set
			{
				caption = value;
				if (field != null)
				{
					field.Caption = caption;
				}
			}
		}
		/// <summary>
		/// Cell Value; 預設值: False
		/// </summary>
		public bool Checked
		{
			get { return (bool)field.Checked; }
			set
			{
				SetChecked(value);
				//x fvalue = value;
				//x if (field != null)
				//x {
				//x     field.Value = value;
				//x }
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
			get
			{
				return em;
			}
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
		/// CheckBox對齊方式; 預設值: MiddleLeft
		/// </summary>
		public DevAge.Drawing.ContentAlignment CheckBoxAlignment
		{
			get { return checkboxAlignment; }
			set
			{
				checkboxAlignment = value;
				if (field != null)
				{
					((SourceGrid.Cells.Views.CheckBox)field.View).CheckBoxAlignment = checkboxAlignment;
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
					SetFontType((SourceGrid.Cells.Views.CheckBox)field.View, fontname.ToString(), fontsize, fontstyle);
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
					SetFontType((SourceGrid.Cells.Views.CheckBox)field.View, fontname.ToString(), fontsize, fontstyle);
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
					SetFontType((SourceGrid.Cells.Views.CheckBox)field.View, fontname.ToString(), fontsize, fontstyle);
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
					SetBorder((SourceGrid.Cells.Views.CheckBox)field.View, hasborder);
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
		/// Tip內容
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
				if (dbackcolor == new Color()) { dbackcolor = CheckBoxBackColor; }
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
				if (dfontcolor == new Color()) { dfontcolor = CheckBoxFontColor; }
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
		#endregion

		/// <summary>
		/// 建構; 商品資料建構時輸入後無法修改，欄位資料可使用範例方式建立
		/// 範例: _CheckBoxCell(){BackColor =Color.White}
		/// </summary>        
		public CheckBoxCell()
		{			
			SetCell();
		}

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
				if (field.Grid!= null)
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
					(field.Grid.Selection as SelectionBase).Border.SetColor((Color)SelectionColor);
				}
				//x }
				//x else
				//x {
				//x }
				field.Grid.Selection.SelectCell(new SourceGrid.Position(field.Row.Index, ColumnIndex), IsSelect);
			}
		}
		/// <summary>
		/// 設定欄位值
		/// </summary>
		/// <param name="_Checked">欄位值</param>
		public void SetChecked(bool _Checked)
		{
			if (field != null)
			{
				if (field.Grid != null)
				{
					if (field.Grid.InvokeRequired)
					{
						SetCheckBoxChecked d = new SetCheckBoxChecked(SetChecked);
						field.Grid.Invoke(d, new object[] { _Checked });
					}
					else
					{
						//x fvalue = _Checked;
						field.Value = _Checked;
						field.Grid.InvalidateCell(field);
					}
				}
				else
				{
					//x fvalue = _Checked;
					field.Value = _Checked;
				}
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
		/// 整Row上色, 並記錄之前顏色
		/// </summary>
		/// <param name="_Color">顏色</param>
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
		/// 整Row上色(還原成之前顏色)
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
		/// 沒作用
		/// </summary>
		/// <param name="_Txt">沒作用</param>
		public override void SetValue(object _Txt)
		{
			//x throw new NotImplementedException();
		}
		/// <summary>
		/// 沒作用
		/// </summary>
		/// <param name="_Txt">沒作用</param>
		/// <param name="_Color">沒作用</param>
		public override void SetValueColor(object _Txt, object _Color)
		{
			//throw new NotImplementedException();
		}
		/// <summary>
		/// 沒作用
		/// </summary>
		/// <param name="_Txt">沒作用</param>
		/// <param name="_BC">沒作用</param>
		/// <param name="_FC">沒作用</param>
		public override void SetValueColor(object _Txt, object _BC, object _FC)
		{
			//x throw new NotImplementedException();
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
			if (p.PropertyType != typeof(bool)) { return false; }
			this.Cell.AddController(new BindProperty(this.Cell, p, LinkObject, Index));
			return true;
		}
		#endregion

		#region Private
		/// <summary>
		/// 建立欄位
		/// </summary>
		private void SetCell()
		{
			SourceGrid.Cells.Views.CheckBox v = new SourceGrid.Cells.Views.CheckBox();
			//對齊方式
			v.TextAlignment = textAlignment;
			v.CheckBoxAlignment = checkboxAlignment;
			v.BackColor = DefaultBackColor;
			v.ForeColor = DefaultFontColor;
			v.WordWrap = WordWrap;
			//字型
			SetFontType(v, fontname.ToString(), fontsize, fontstyle);
			//邊框
			SetBorder(v, hasborder);

			field = new SourceGrid.Cells.CheckBox(caption, false);
			field.View = v;
			field.Editor.EditableMode = em;
			field.ColumnSpan = columnspan;
			field.RowSpan = rowspan;
			field.Tag = base.Tag;
			field.Editor.EnableEdit = this.Enable;
			field.AddController(Controller);
		}
		#endregion
	}
}
