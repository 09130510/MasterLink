using System.Drawing;
using SourceGrid.Selection;

namespace SourceCell
{
	/// <summary>
	/// 列標題
	/// </summary>
	public class RHeaderCell : CellBase
	{
		#region Static
		/// <summary>
		/// 【Header背景色】Default：Color.DarkGray
		/// </summary>
		public static Color HeaderBackColor = Color.DimGray;
		/// <summary>
		/// 【Header字色】Default：Color.Black
		/// </summary>
		public static Color HeaderFontColor = Color.White;
		#endregion

		#region Declaration
		/// <summary>
		/// Cell
		/// </summary>		
		private SourceGrid.Cells.RowHeader field;
		/// <summary>
		/// Value
		/// </summary>
		private string fvalue;
		//x /// <summary>
		//x /// 是否有排序功能
		//x /// </summary>
		//x private bool sortable = false;
		/// <summary>
		/// 合併欄位
		/// </summary>
		private int columnspan = 1;
		/// <summary>
		/// 欄位合併
		/// </summary>
		private int rowspan = 1;
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
		#endregion

		#region Variable
		private object[] preColor;
		/// <summary>
		/// 有無Tip
		/// </summary>
		private bool usetip = false;
		/// <summary>
		/// Tip內容
		/// </summary>
		private string tooltip = string.Empty;
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
		public override SourceGrid.Cells.ICell Cell { get { return field; } protected set { field = (SourceGrid.Cells.RowHeader)value; } }
		/// <summary>
		/// Cell
		/// </summary>
		public SourceGrid.Cells.RowHeader Field
		{
			get { return field; }
			private set { }
		}
		/// <summary>
		/// 標題
		/// </summary>
		public string Caption
		{
			get { return fvalue; }
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
					field.ColumnSpan = value;
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
					field.RowSpan = value;
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
					SetFontType((SourceGrid.Cells.Views.RowHeader)field.View, fontname.ToString(), fontsize, fontstyle);
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
					SetFontType((SourceGrid.Cells.Views.RowHeader)field.View, fontname.ToString(), fontsize, fontstyle);
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
					SetFontType((SourceGrid.Cells.Views.RowHeader)field.View, fontname.ToString(), fontsize, fontstyle);
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
					field.Tag = base.Tag;
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
				if (dbackcolor == new Color()) { dbackcolor = HeaderBackColor; }
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
				if (dfontcolor == new Color()) { dfontcolor = HeaderFontColor; }
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
		//x /// <summary>
		//x /// 沒作用
		//x /// </summary>
		//x public bool EnableEdit { get { return false; } set { } }
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
		#endregion

		/// <summary>
		/// 建構
		/// </summary>
		public RHeaderCell()
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
					((SourceGrid.Cells.Views.RowHeader)field.View).Background = null;
					DevAge.Drawing.VisualElements.RowHeader bg = new DevAge.Drawing.VisualElements.RowHeader();
					bg.Border = DevAge.Drawing.RectangleBorder.CreateInsetBorder(1, BorderShadowColor, BorderLightColor);

					if (_Color != null)
					{
						bg.BackColor = (Color)_Color;
					}
					else
					{
						//還原顏色                
						bg.BackColor = DefaultBackColor;
					}
					((SourceGrid.Cells.Views.RowHeader)field.View).Background = bg;

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
						if (field.Grid.GetCell(j, i) != null && !(field.Grid.GetCell(j, i).GetType().FullName.Equals("SourceGrid.Cells.RowHeader")))
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
						if (field.Grid.GetCell(j, i) != null && !(field.Grid.GetCell(j, i).GetType().FullName.Equals("SourceGrid.Cells.RowHeader")))
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
						if (field.Grid.GetCell(j, i) != null && !(field.Grid.GetCell(j, i).GetType().FullName.Equals("SourceGrid.Cells.RowHeader")))
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
		/// Set Caption
		/// </summary>
		/// <param name="_Txt">Caption</param>
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
						fvalue = _Txt.ToString();
						field.Value = _Txt;

						field.Grid.InvalidateCell(field);
					}
				}
				else
				{
					fvalue = _Txt.ToString();
					field.Value = _Txt;
				}
			}
		}
		/// <summary>
		/// 設定內容及背景色
		/// </summary>
		/// <param name="_Caption">內容</param>
		/// <param name="_Color">背景色;null自動代入DefaultBackColor</param>
		public override void SetValueColor(object _Caption, object _Color)
		{
			SetValue(_Caption);
			SetBackColor(_Color);
		}
		/// <summary>
		/// 設定內容、背景色、字色
		/// </summary>
		/// <param name="_Caption">內容</param>
		/// <param name="_BC">背景色; null代入DefaultBackColor</param>
		/// <param name="_FC">字色; null代入DefaultFontColor</param>
		public override void SetValueColor(object _Caption, object _BC, object _FC)
		{
			SetValue(_Caption);
			SetFontColor(_FC);
			SetBackColor(_BC);
		}
		#endregion

		#region Private
		/// <summary>
		/// 建立欄位
		/// </summary>
		private void SetCell()
		{
			//x DevAge.Drawing.VisualElements.BackgroundLinearGradient bbg = new DevAge.Drawing.VisualElements.BackgroundLinearGradient(DefaultBackColor, DefaultBackColor, 0);
			DevAge.Drawing.VisualElements.RowHeader bg = new DevAge.Drawing.VisualElements.RowHeader();
			bg.BackgroundColorStyle = DevAge.Drawing.BackgroundColorStyle.None;
			//x bg.Border = DevAge.Drawing.RectangleBorder.CreateInsetBorder(1,BorderShadowColor, BorderLightColor);
			bg.BackColor = DefaultBackColor;

			SourceGrid.Cells.Views.RowHeader v = new SourceGrid.Cells.Views.RowHeader();			
			//對齊方式
			v.TextAlignment = textAlignment;
			v.ForeColor = DefaultFontColor;
			v.Background = bg;
			v.WordWrap = WordWrap;
			//字型
			SetFontType(v, fontname.ToString(), fontsize, fontstyle);

			field = new SourceGrid.Cells.RowHeader(fvalue);
			field.View = v;			

			//合併欄位
			field.ColumnSpan = columnspan;
			field.RowSpan = rowspan;
			field.Tag = base.Tag;
			
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
					tip.IsBalloon = false;
					field.AddController(tip);
					field.ToolTipText = tooltip;
				}
				else
				{
					field.RemoveController(tip);
				}
			}
		}
		#endregion
	}
}
