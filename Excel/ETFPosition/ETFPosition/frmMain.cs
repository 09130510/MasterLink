using ETFPosition.Class;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ETFPosition
{
    public partial class frmMain : Form
    {
        #region Variable
        private char[] SPLIT = new char[] { '|' };
        private Dictionary<string, double> m_FXRate = new Dictionary<string, double>();

        private string DataDate { get { return dtpDataDate.Value.ToString("yyyyMMdd"); } }
        private string ForeignDataDate { get { return dtpForeignDataDate.Value.ToString("yyyyMMdd"); } }
        private string FileName { get { return $"{DataDate}.xlsx"; } }
        private string FullFileName { get { return $"{Application.StartupPath}/Report/{FileName}"; } }
        #endregion

        public frmMain()
        {
            InitializeComponent();

            Util.Init();
            Text = Util.VersionInfo(this);
            //dtpDataDate.Value = DateTime.Now.AddDays(-1);
            //dtpForeignDataDate.Value = DateTime.Now.AddDays(-2);
            string futdate = Util.SQL.Query<string>("EXEC GETINVENTORYDATE 'FUT'").FirstOrDefault();
            string foreigndate = Util.SQL.Query<string>("EXEC GETINVENTORYDATE 'FFUT'").FirstOrDefault();
             
            dtpDataDate.Value = DateTime.ParseExact(futdate, "yyyyMMdd", null, System.Globalization.DateTimeStyles.AllowWhiteSpaces);
            dtpForeignDataDate.Value = DateTime.ParseExact(foreigndate, "yyyyMMdd", null, System.Globalization.DateTimeStyles.AllowWhiteSpaces);
            chkAuto.Checked = bool.Parse(Util.INI["SYSTEM"]["AUTOGEN"]);
            ToolStripControlHost host = new ToolStripControlHost(dtpDataDate);
            toolStrip1.Items.Insert(1, host);
            host = new ToolStripControlHost(dtpForeignDataDate);
            toolStrip1.Items.Insert(3, host);
            host = new ToolStripControlHost(chkAuto);
            toolStrip1.Items.Add(host);
            

            Util.FX.OnValueUpdated += FX_OnValueUpdated;
            var subfx = Util.INI["SYSTEM"]["FXRATECHANNEL"].Split(SPLIT, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in subfx)
            {
                var subitems = item.Split('.');
                if (!m_FXRate.ContainsKey(subitems[1]))
                {
                    m_FXRate.Add(subitems[1], -1D);
                }
                Util.FX.Subscribe(subitems[0], subitems[1]);
            }
        }
        private void tsGenerate_Click(object sender, EventArgs e)
        {
            string[] stkacno = Util.INI["SYSTEM"]["STKACCNO"].Split(SPLIT, StringSplitOptions.RemoveEmptyEntries);
            string[] futacno = Util.INI["SYSTEM"]["FUTACCNO"].Split(SPLIT, StringSplitOptions.RemoveEmptyEntries);
            string[] foreignfutacno = Util.INI["SYSTEM"]["FOREIGNACCNO"].Split(SPLIT, StringSplitOptions.RemoveEmptyEntries);

            IEnumerable<ContractValue> contracts = Util.SQL.Query<ContractValue>("SELECT DISTINCT HEAD AS ID,Currency AS Curncy,CValue FROM tblFuture ORDER BY ID");
            IEnumerable<STK> stks = Util.SQL.Query<STK>($"SELECT [ACNO] AS AccNo,[COMPANY_NO] AS ID,[COMPANY_NAME] AS Name,[AVA_STOCK_NOS] +[UN_STOCK_NOS] AS Lots,[MARKET_PRICE] AS Price FROM[ETFForBrian].[dbo].[W0_STOCK_PART_DAILY] WHERE PART_DATE='{DataDate}' AND ACNO IN ('{string.Join("','", stkacno)}') ORDER BY ACNO");
            IEnumerable<FUT> futs = Util.SQL.Query<FUT>($"SELECT [INVESTOR_ACNO] AS AccNo,[COMMODITY_ID] AS ID,[COMMODITY_NAME] AS NAME,[SETTLEMENT_MONTH] AS YM,[NON_COVER_QTY] AS LOTS,[SETTLEMENT_PRICE] AS PRICE,[CONTRACT_SIZE] AS CVALUE FROM[ETFForBrian].[dbo].[W0_FUTOPT_PART_DAILY] WHERE STRIKE_PRICE = 0 AND PART_DATE ='{DataDate}' AND INVESTOR_ACNO IN ('{string.Join("','", futacno)}') ORDER BY INVESTOR_ACNO ");
            IEnumerable<FUT> foreignfuts = Util.SQL.Query<FUT>($"SELECT [INVESTOR_ACNO] AS AccNo,[COMMODITY_ID] AS ID,[COMMODITY_NAME] AS NAME,[SETTLEMENT_MONTH] AS YM,[NON_COVER_QTY] AS LOTS,[SETTLEMENT_PRICE] AS PRICE,[CONTRACT_SIZE] AS CVALUE FROM[ETFForBrian].[dbo].[W0_FOREIGN_FUTOPT_PART_DAILY] WHERE STRIKE_PRICE = 0 AND PART_DATE ='{ForeignDataDate}' AND INVESTOR_ACNO IN ('{string.Join("','", foreignfutacno)}') ORDER BY INVESTOR_ACNO ");

            //按資料數量加Row
            ExcelNamedRange accno = Util.Book.Names["STKACCNO"];
            Util.Position.InsertRow(accno.Start.Row + 1, Math.Max(stks.Count(), futs.Count()), accno.Start.Row);
            //資料日期
            ExcelNamedRange datadate = Util.Book.Names["DATADATE"];
            ExcelNamedRange foreigndatadate = Util.Book.Names["FOREIGNDATADATE"];
            datadate.Value = dtpDataDate.Value.ToString("yyyy/MM/dd");
            foreigndatadate.Value = dtpForeignDataDate.Value.ToString("yyyy/MM/dd");
            //契約乘數
            _ContractValue(contracts);
            //匯率
            _FxRate();
            //股票
            _Stk(stks);
            //期貨
            _Fut(futs);
            //國外期貨
            _Fut(foreignfuts);

            //調整欄位大小
            Util.Position.Calculate();
            for (int i = 1; i < 20; i++)
            {
                Util.Position.Column(i).AutoFit();
                Util.Position.Column(i).BestFit = true;
            }
            //存檔
            Util.SaveAs(FullFileName);
            tsStatusTxt.InvokeIfRequired(() =>
            {
                tsStatusTxt.Text = "報表產生完成！";
            });
            //tsOpen_Click(tsOpen, EventArgs.Empty);
        }
        private void tsSetting_CheckedChanged(object sender, EventArgs e)
        {
            SetBounds(Location.X, Location.Y, Width, tsSetting.Checked ? 263 : 101);
        }
        private void tsOpen_Click(object sender, EventArgs e)
        {
            if (!File.Exists(FullFileName))
            {
                MessageBox.Show("找不到報表檔！");
                return;
            }
            Microsoft.Office.Interop.Excel.Application application = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook data = application.Workbooks.Open(FullFileName, Type.Missing, true);
            application.Visible = true;
        }
        private void tsMail_Click(object sender, EventArgs e)
        {
            string[] stkaccno = Util.INI["SYSTEM"]["STKACCNO"].Split(SPLIT, StringSplitOptions.RemoveEmptyEntries);
            string[] stklendingaccno = Util.INI["SYSTEM"]["STKLENDINGACCNO"].Split(SPLIT, StringSplitOptions.RemoveEmptyEntries);
            string[] futaccno = Util.INI["SYSTEM"]["FUTACCNO"].Split(SPLIT, StringSplitOptions.RemoveEmptyEntries);
            string[] foreignfutaccno = Util.INI["SYSTEM"]["FOREIGNACCNO"].Split(SPLIT, StringSplitOptions.RemoveEmptyEntries);
            string[] mailto = Util.INI["SYSTEM"]["MAILTO"].Split(SPLIT, StringSplitOptions.RemoveEmptyEntries);
            var stk = $"[{string.Join("], [", stkaccno)}]";
            var stklending = $"[{string.Join("], [", stklendingaccno)}]";
            var fut = $"[{string.Join("], [", futaccno)}]";
            var foreignfut = $"[{string.Join("], [", foreignfutaccno)}]";
            FileInfo fi = new FileInfo(FullFileName);


            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("JESSTSAI@MASTERLINK.COM.TW", "期貨自營程式化報表");

            mail.Subject = $"{DataDate} ETF策略部位控管表";
            mail.Body = $"產生時間：{fi.LastWriteTime}\r\n\r\n股票帳號：{stk}\r\n\r\n借券帳號：{stklending}\r\n\r\n期貨帳號：{fut}\r\n\r\n國外期貨資料日期：{ForeignDataDate}\r\n國外期貨帳號：{foreignfut}\r\n\r\n\r\n";
            foreach (var item in mailto)
            {
                mail.To.Add(new MailAddress(item));
            }
            mail.CC.Add(mail.From);

            Attachment att = new Attachment(FullFileName);
            att.Name = FileName;
            mail.Attachments.Add(att);
            using (SmtpClient client = new SmtpClient("e1.masterlink.com.tw"))
            {
                client.Send(mail);
            }
            tsStatusTxt.InvokeIfRequired(() =>
            {
                tsStatusTxt.Text = "郵件已寄出！";
            });
        }

        private void FX_OnValueUpdated(string channel, string item, string value)
        {
            if (!m_FXRate.ContainsKey(item)) { return; }

            lock (m_FXRate)
            {
                m_FXRate[item] = double.Parse(value);
                tsFX.InvokeIfRequired(() => { tsFX.Text = $"{item}:{value}"; });
                int cnt = m_FXRate.Values.Count(e => e == -1D);
                if (cnt <= 0)
                {
                    tsGenerate.InvokeIfRequired(() => { tsGenerate.Enabled = true; });
                    tsFX.InvokeIfRequired(() => { tsFX.Text = "匯率資料接收完成！"; });
                    Util.FX.UnsubscribeAll();
                }
            }
        }
        #region Private
        private void _ContractValue(IEnumerable<ContractValue> Contracts)
        {
            ExcelNamedRange cvalue = Util.Book.Names["REF_CVALUE"];
            //塞值
            int rowStart = cvalue.Start.Row + 2;
            int rowCnt = 0;

            foreach (var contract in Contracts)
            {
                int row = rowStart + rowCnt;
                Util.ContractValue.Cells[row, 1].Value = contract.ID.Trim();
                Util.ContractValue.Cells[row, 2].Value = contract.CValue;
                Util.ContractValue.Cells[row, 3].Value = contract.Curncy.Trim();
                rowCnt++;
            }
        }
        private void _FxRate()
        {
            ExcelNamedRange fxrate = Util.Book.Names["REF_FXRATE"];
            //轉換+排序
            Dictionary<string, double> list = new Dictionary<string, double>();
            list.Add("TWD", 1D);
            foreach (var rate in m_FXRate)
            {
                switch (rate.Key)
                {
                    case "TWD CMPN Curncy":
                        list.Add("USD", rate.Value);
                        break;
                    case "JPYTWD CMPN Curncy":
                        list.Add("JPY", rate.Value);
                        break;
                    case "CNYTWD CMPN Curncy":
                        list.Add("CNY", rate.Value);
                        break;
                    case "HKDTWD CMPN Curncy":
                        list.Add("HKD", rate.Value);
                        break;
                }
            }

            //塞值
            int rowStart = fxrate.Start.Row + 2;
            int rowCnt = 0;
            var rates = list.OrderBy(e => e.Key);
            foreach (var rate in rates)
            {
                int row = rowStart + rowCnt;
                Util.FxRate.Cells[row, 1].Value = rate.Key;
                Util.FxRate.Cells[row, 2].Value = rate.Value;
                rowCnt++;
            }
        }
        private void _Stk(IEnumerable<STK> STKs)
        {
            ExcelNamedRange accno = Util.Book.Names["STKACCNO"];
            ExcelNamedRange id = Util.Book.Names["STKID"];
            ExcelNamedRange name = Util.Book.Names["STKNAME"];
            ExcelNamedRange lots = Util.Book.Names["STKLOTS"];
            ExcelNamedRange price = Util.Book.Names["STKPRICE"];
            ExcelNamedRange cvalue = Util.Book.Names["STKCVALUE"];
            ExcelNamedRange delta = Util.Book.Names["STKDELTA"];

            //塞值
            int rowCnt = 0;
            foreach (var stk in STKs)
            {
                int row = accno.Start.Row + rowCnt;
                Util.Position.Cells[row, accno.Start.Column].Value = stk.AccNo.Trim();
                Util.Position.Cells[row, id.Start.Column].Value = stk.ID.Trim();
                Util.Position.Cells[row, name.Start.Column].Value = stk.Name.Trim();
                Util.Position.Cells[row, lots.Start.Column].Value = stk.Lots;
                Util.Position.Cells[row, price.Start.Column].Value = stk.Price;
                Util.Position.Cells[row, cvalue.Start.Column].Value = stk.CValue;
                //放Delta公式
                Util.Position.Cells[row, delta.Start.Column].FormulaR1C1 = "RC[-3]*RC[-2]*RC[-1]";
                rowCnt++;
            }
        }
        private void _Fut(IEnumerable<FUT> FUTs)
        {
            ExcelNamedRange accno = Util.Book.Names["FUTACCNO"];
            ExcelNamedRange id = Util.Book.Names["FUTID"];
            ExcelNamedRange ym = Util.Book.Names["FUTYM"];
            ExcelNamedRange name = Util.Book.Names["FUTNAME"];
            ExcelNamedRange curncy = Util.Book.Names["FUTCURNCY"];
            ExcelNamedRange lots = Util.Book.Names["FUTLOTS"];
            ExcelNamedRange price = Util.Book.Names["FUTPRICE"];
            ExcelNamedRange cvalue = Util.Book.Names["FUTCVALUE"];
            ExcelNamedRange fxrate = Util.Book.Names["FUTFXRATE"];
            ExcelNamedRange delta = Util.Book.Names["FUTDELTA"];

            //塞值
            int rowCnt = 0;
            foreach (var fut in FUTs)
            {
                int row = accno.Start.Row + rowCnt;
                fut.RowIndex = row;
                Util.Position.Cells[row, accno.Start.Column].Value = fut.AccNo.Trim();
                Util.Position.Cells[row, id.Start.Column].Value = fut.ID.Trim();
                Util.Position.Cells[row, ym.Start.Column].Value = fut.YM.Trim();
                Util.Position.Cells[row, name.Start.Column].Value = fut.Name.Trim();
                Util.Position.Cells[row, curncy.Start.Column].Formula = fut.CurncyFormula;
                Util.Position.Cells[row, lots.Start.Column].Value = fut.Lots;
                Util.Position.Cells[row, price.Start.Column].Value = fut.Price;
                Util.Position.Cells[row, cvalue.Start.Column].Formula = fut.CValueFormula;
                Util.Position.Cells[row, fxrate.Start.Column].Formula = fut.FxRateFormula;
                Util.Position.Cells[row, delta.Start.Column].FormulaR1C1 = "RC[-4]*RC[-3]*RC[-2]*RC[-1]";
                rowCnt++;
            }
        }
        #endregion

        private void chkAuto_CheckedChanged(object sender, EventArgs e)
        {
            Util.INI["SYSTEM"]["AUTOGEN"] = chkAuto.Checked.ToString();
            Util.WriteConfig();

            if (!chkAuto.Checked) { return; }
            ThreadPool.QueueUserWorkItem((args) =>
            {
                while (!tsGenerate.Enabled)
                {
                    Thread.Sleep(1);
                }
                tsGenerate_Click(tsGenerate, EventArgs.Empty);
                tsMail_Click(tsMail, EventArgs.Empty);
            });
        }
    }
}