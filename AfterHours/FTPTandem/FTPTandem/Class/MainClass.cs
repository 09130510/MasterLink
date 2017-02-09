using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FtpLib;
using System.IO;
using Tool.Public;
using System.Data;
using System.Net.Mail;
using FTPTandem.UI.Public;
using System.Reflection;
using System.Net;
using System.ComponentModel;
using DownloadJPX;
using System.Timers;

namespace FTPTandem.Class
{
    class MainClass
    {
        private static MainClass mainClass_;
        private string ftpIP_ = Properties.Settings.Default.FtpIP;
        private string ftpID_ = Properties.Settings.Default.UserName;
        private string ftpPassword_ = Properties.Settings.Default.Password;
        private string ftpFileName_ = Properties.Settings.Default.FileName;
        private string ftpDirectory_ = Properties.Settings.Default.FTPDirectory;

        private string sqlIP_ = Properties.Settings.Default.SQL_Server;
        private string sqlDB_ = Properties.Settings.Default.SQL_DB;
        private string sqlID_ = Properties.Settings.Default.SQL_ID;
        private string sqlPassword_ = Properties.Settings.Default.SQL_Password;

        #region Download
        private WebClient m_Web;

        private string m_URL = Properties.Settings.Default.Url;
        private string m_DownloadPage = Properties.Settings.Default.DownloadPage;
        private string m_FileURL;
        private string m_FileName;
        private string m_FilePath = @"./File/";
        private string m_BakPath = @"./File/Bak/";
        private DateTime m_FileDate;
        private Timer m_Timer = new Timer(120000);
        
        private string FileName
        {
            get { return string.Format("{0}{1}.csv", m_FilePath, m_FileName); }
            set
            {
                if (value == m_FileName) { return; }
                m_FileName = value;
            }
        }
        private string BakFileName
        {
            get { return string.Format("{0}{1}_{2}.csv", m_BakPath, m_FileName, DateTime.Now.ToString("HHmmss.fff")); }
        }
        private Uri DownloadURL { get { return new Uri(string.Format("{0}{1}", m_URL, m_DownloadPage)); } }
        private Uri FileURL { get { return new Uri(string.Format("{0}{1}", m_URL, m_FileURL)); } }
        
        #endregion

        private DataBase dataBase_ = new DataBase();
        private SrnItems srnItems_ = new SrnItems();
        //public event OnMessgaeDelegate OnMessage;

        private MainClass()
        {
            
            
        }

        public void InitDataBase(string server,string db, string id,string password,DateTime date)
        {
            dataBase_.SetParam(DBTypes.SQL, server, id, password);
            dataBase_.FillSettlePrice(date);
            
        }

        #region Download
        public void StartDownload()
        {
            
            if (!Directory.Exists(m_FilePath)) { Directory.CreateDirectory(m_FilePath); }
            if (!Directory.Exists(m_BakPath)) { Directory.CreateDirectory(m_BakPath); }
            if (m_Web == null)
            {
                m_Web = new WebClient();
                m_Web.DownloadStringCompleted += DownloadStringCompleted;
                m_Web.DownloadFileCompleted += DownloadFileCompleted;
                Parse.Init(sqlIP_, sqlDB_, sqlID_, sqlPassword_);                
            }
            string msg = string.Format("{0}\t{1}:{2}, Get download url", DateTime.Now.ToString("hh:mm:ss.ffff"), MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
            srnItems_.lbLogs.InvokeIfNeeded(() => srnItems_.lbLogs.Items.Insert(0, msg));
            m_Web.DownloadStringAsync(DownloadURL);
            m_Timer.Elapsed += (sender, e) =>
            {
                InitDataBase(sqlIP_, sqlDB_, sqlID_, sqlPassword_, srnItems.dtTransDate.Value);
                StartFTP();
                string[] emails = srnItems.txtEmails.Lines.ToArray();
                SendEmail(srnItems.dtTransDate.Value, emails);
                m_Timer.Stop();
            };
            m_Timer.Start();
        }
        private void DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            string msg = string.Empty;
            try
            {
                string html = e.Result;
                int idx = html.IndexOf("component-file", 0);
                if (idx != -1)
                {
                    int StartIdx = html.IndexOf(">As of the setting", idx);
                    int EndIdx = html.IndexOf("</th>", StartIdx);
                    string DateStr = html.Substring(StartIdx + 1, EndIdx - StartIdx - 1).Trim();
                    int DateIdx = DateStr.IndexOf("(") + 1;
                    m_FileDate = DateTime.Parse(DateStr.Substring(DateIdx, DateStr.Length - DateIdx - 1));
                    msg = string.Format("{0}\t{1}:{2}, Data Date: {3}", DateTime.Now.ToString("hh:mm:ss.ffff"), MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name, m_FileDate);
                    srnItems_.lbLogs.InvokeIfNeeded(() => srnItems_.lbLogs.Items.Insert(0, msg));
                    //if (DateTime.Now.Hour >= 15)
                    //{
                    //    if (m_FileDate.Date != DateTime.Now.Date || m_FileDate.Month != DateTime.Now.Month)
                    //    {
                    //        return;
                    //    }
                    //}
                    //else
                    //{
                    //    if (m_FileDate.Date != DateTime.Now.AddDays(-1).Date || m_FileDate.Month != DateTime.Now.Month)
                    //    {
                    //        return;
                    //    }
                    //}
                    FileName = m_FileDate.ToString("yyyyMMdd");
                    StartIdx = html.IndexOf("<a href=", idx);
                    EndIdx = html.IndexOf("rel=", StartIdx);
                    m_FileURL = html.Substring(StartIdx + 9, EndIdx - StartIdx - 11).Trim();
                    msg = string.Format("{0}\t{1}:{2}, Download File: {3}", DateTime.Now.ToString("hh:mm:ss.ffff"), MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name, FileURL);
                    srnItems_.lbLogs.InvokeIfNeeded(() => srnItems_.lbLogs.Items.Insert(0, msg));
                    m_Web.DownloadFileAsync(FileURL, FileName);
                }
            }
            catch (Exception)
            {
                Console.WriteLine(e.Error);
            }
        }
        private void DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            string msg = string.Empty;
            if (e.Error == null && File.Exists(FileName))
            {
                msg = string.Format("{0}\t{1}:{2}, Download completed", DateTime.Now.ToString("hh:mm:ss.ffff"), MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                srnItems_.lbLogs.InvokeIfNeeded(() => srnItems_.lbLogs.Items.Insert(0, msg));
                using (StreamReader sr = new StreamReader(FileName, Encoding.UTF8))
                {
                    while (!sr.EndOfStream)
                    {
                        string[] line = sr.ReadLine().Split(',');
                        if (line.Length == 12 && line[11] == "TOPIX" && line[1].Contains("FUT"))
                        {
                            Parse p = new Parse(m_FileDate, line);
                            p.Write();
                            msg = string.Format("{0}\t{1}:{2}, {3}{4} {5}", DateTime.Now.ToString("hh:mm:ss.ffff"), MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name, p.PID, p.YM,p.SettlePrice);
                            srnItems_.lbLogs.InvokeIfNeeded(() => srnItems_.lbLogs.Items.Insert(0,msg));
                        }
                    }
                }
                File.Move(FileName, BakFileName);
                
            }
        }
        #endregion
        
        public void StartFTP()
        {
            string msg = string.Empty;
            if (dataBase_.dtSettlePrice.Rows.Count > 0)
            {
                if (File.Exists(ftpFileName_))
                    File.Delete(ftpFileName_);
                using (FtpConnection ftp = new FtpConnection(ftpIP_, ftpID_, ftpPassword_))
                {

                    using (StreamWriter streamWriter = new StreamWriter(ftpFileName_, false))
                    {

                        foreach (DataRow item in dataBase_.dtSettlePrice.Rows)
                            streamWriter.WriteLine(string.Format("{0,-16}{1,-6}  {2:000000.000000}  ", item["Pid"], item["YM"], item["SettlePrice"]));
                        streamWriter.Flush();

                    }

                    ftp.Open();
                    ftp.Login();
                    try
                    {
                        ftp.SetCurrentDirectory(ftpDirectory_);
                        ftp.PutFile(ftpFileName_, ftpFileName_);
                        msg = string.Format("{0}\t{1}:{2},Finish FTP Closing to {3}", DateTime.Now.ToString("hh:mm:ss.ffff"), MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name, ftpIP_);
                        srnItems_.lbLogs.InvokeIfNeeded(() => srnItems_.lbLogs.Items.Insert(0, msg));

                    }
                    catch (FtpException ex)
                    {
                        msg = String.Format("{0}\t{1}:{2}, Exception ErrCode: {3}, Message: {4}", DateTime.Now.ToString("hh:mm:ss.ffff"), MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name, ex.ErrorCode, ex.Message);
                        srnItems_.lbLogs.InvokeIfNeeded(() => srnItems_.lbLogs.Items.Insert(0, msg));
                    }
                }
            }
            else
            {
                msg = string.Format("{0}\t{1}:{2},本日無資料 !", DateTime.Now.ToString("hh:mm:ss.ffff"), MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name, ftpIP_);
                srnItems_.lbLogs.InvokeIfNeeded(() => srnItems_.lbLogs.Items.Insert(0, msg));
            }
         
          
        }

        public void SendEmail(DateTime date,string[] emails)
        {
            string msg = string.Empty;
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("jackiehung@masterlink.com.tw"); //設定寄件者
                mail.Subject = string.Format("{0},國外期結算價", date.ToString("yyyy/MM/dd"));
                for (int i = 0; i < emails.Length; ++i)
                {
                    if (emails[i] != string.Empty)
                        mail.To.Add(emails[i]);
                }
                if (dataBase_.dtSettlePrice.Rows.Count > 0)
                {
                    if (File.Exists(ftpFileName_))
                    {
                        Attachment attch = new Attachment(ftpFileName_);
                        mail.Attachments.Add(attch);
                        mail.Body = string.Empty;
                    }
                    else
                    {
                        mail.Body = string.Format("{0} 不存在,請通知 IT ", ftpFileName_);
                    }
                }
                else
                {
                    mail.Body = "SQL 本日無結算價資料";
                }
                

                SmtpClient smtpclient = new SmtpClient();
                smtpclient.Host = "e1.masterlink.com.tw"; //設定SMTP Server
                smtpclient.Port = 25; //設定Port
                smtpclient.Send(mail);
                msg = string.Format("{0}\t{1}:{2}, Email Finished", DateTime.Now.ToString("hh:mm:ss.ffff"), MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name);
                srnItems_.lbLogs.InvokeIfNeeded(() => srnItems_.lbLogs.Items.Insert(0, msg));
                 
            }
            catch (Exception ex)
            {
                msg = String.Format("{0}\t{1}:{2}, Exception StackTrace: {3}, Message: {4}", DateTime.Now.ToString("hh:mm:ss.ffff"), MethodBase.GetCurrentMethod().ReflectedType.Name, MethodBase.GetCurrentMethod().Name, ex.StackTrace, ex.Message);
                srnItems_.lbLogs.InvokeIfNeeded(() => srnItems_.lbLogs.Items.Insert(0, msg));
            }
            //File.Delete(ftpFileName_);
            
        }
       
        #region 屬性
        public static MainClass mainClass
        {
            get
            {
                if (mainClass_ == null)
                    mainClass_ = new MainClass();
                return mainClass_;
            }
        }
        public SrnItems srnItems
        {
            get
            {
                return srnItems_;
            }
        }
        #endregion
    }
}
