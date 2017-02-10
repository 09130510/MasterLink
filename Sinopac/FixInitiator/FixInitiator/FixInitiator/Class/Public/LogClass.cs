using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace FixInitiator.Class.Public
{
    class LogClass
    {
        private static string logPath = System.AppDomain.CurrentDomain.BaseDirectory;
        private static string path = string.Empty;
        public static void CreatPath(string subDir = "")
        {
            if (subDir != string.Empty)
                path = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, subDir);
            else
                path = System.AppDomain.CurrentDomain.BaseDirectory;
            try
            {
                //目錄存在則砍掉
                if (!Directory.Exists(path))
                    // Directory.Delete(path, true);
                    Directory.CreateDirectory(path);
            }
            catch (Exception ex)
            {
                throw (ex);
                //MainClass.mainClass.srnItems.lbLogs.InvokeIfNeeded(() => string.Format("{0}\tCreatPath Exception,Message: {1}", DateTime.Now.ToString("hh:mm:ss.ffff"), ex.Message));
            }

        }
        public static void Writing(string word, string name)
        {

            StreamWriter sw;
            try
            {
                using (sw = new StreamWriter(string.Format(@"{0}\{1}.txt", path, name), true, Encoding.GetEncoding("big5")))
                {
                    sw.WriteLine(word);
                    sw.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}\tWriting Exception,word {1},name:{2}", DateTime.Now.ToString("hh:mm:ss.ffff"), word, name);
                //try again
                using (sw = new StreamWriter(string.Format(@"{0}\{1}.txt", path, name), true, Encoding.GetEncoding("big5")))
                {
                    sw.WriteLine(word);
                    sw.Close();
                }

            }

        }
    }
}
