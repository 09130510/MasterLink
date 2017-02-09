using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace DownloadJPX
{
    public partial class Form1 : Form
    {
        private WebClient m_Web = new WebClient();
        
        private string m_URL = "http://www.jpx.co.jp";
        private string m_DownloadPage = "/english/markets/derivatives/jnet-derivative/index.html";
        private string m_FileURL;
        private string m_FileName;
        private string m_FilePath = @"./File/";
        private string m_BakPath = @"./File/Bak/";

        
        private string FileName
        {
            get { return  string.Format("{0}{1}.csv", m_FilePath, m_FileName); }
            set {
                if (value ==m_FileName)
                {
                    return;
                }
                m_FileName = value;
                //if (File.Exists(FileName))
                //{
                //    File.Move(FileName, BakFileName);
                //}
                
            }
        }
        private string BakFileName
        {
            get { return string.Format("{0}{1}_{2}.csv", m_BakPath, m_FileName, DateTime.Now.ToString("HHmmss.fff")); }
        }
        private Uri DownloadURL { get { return new Uri(string.Format("{0}{1}", m_URL, m_DownloadPage)); } }
        private Uri FileURL { get { return new Uri(string.Format("{0}{1}", m_URL, m_FileURL)); } }

        public Form1()
        {
            InitializeComponent();

            if (!Directory.Exists(m_FilePath)) { Directory.CreateDirectory(m_FilePath); }
            if (!Directory.Exists(m_BakPath)) { Directory.CreateDirectory(m_BakPath); }

            m_Web.DownloadStringCompleted += DownloadStringCompleted;
            m_Web.DownloadFileCompleted += DownloadFileCompleted;
            m_Web.DownloadStringAsync(DownloadURL);
        }

        private void DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error==null && File.Exists(FileName))
            {
                using (StreamReader sr  =  new StreamReader(FileName, Encoding.UTF8))
                {
                    while (!sr.EndOfStream)
                    {
                        string[] line = sr.ReadLine().Split(',');
                        if (line.Length ==12 &&line[11] =="TOPIX" && line[1].Contains("FUT"))
                        {
                            Parse p = new Parse(DateTime.Now, line);
                        }
                    }                    
                }
                File.Move(FileName, BakFileName);
            }
        }

        private void DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
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
                    DateTime FileDate = DateTime.Parse(DateStr.Substring(DateIdx, DateStr.Length - DateIdx - 1));
                    if (DateTime.Now.Hour >= 15 )
                    {
                        if (FileDate.Date != DateTime.Now.Date || FileDate.Month != DateTime.Now.Month)
                        {
                            return;
                        }
                    }
                    else
                    {
                        if (FileDate.Date != DateTime.Now.AddDays(-1).Date || FileDate.Month != DateTime.Now.Month)
                        {
                            return;
                        }
                    }
                    FileName = FileDate.ToString("yyyyMMdd");
                    StartIdx = html.IndexOf("<a href=", idx);
                    EndIdx = html.IndexOf("rel=", StartIdx);
                    m_FileURL = html.Substring(StartIdx + 9, EndIdx - StartIdx - 11).Trim();
                    m_Web.DownloadFileAsync(FileURL, FileName);
                }
            }
            catch (Exception)
            {
                Console.WriteLine(e.Error);
            }
        }
    }
}
