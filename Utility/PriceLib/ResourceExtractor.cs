using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace PriceLib
{
    public static class ResourceExtractor
    {        
        public static void ExtractResourceToFile(string resourceName, string filename, bool overwrite)
        {
            if (overwrite) { DeleteResourceFile(filename); }

            //怕沒刪掉又寫不上去會出錯
            if (File.Exists(filename)) { return; }

            using (Stream resources = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            {
                using (FileStream stream = new FileStream(filename, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    byte[] buffer = new byte[resources.Length];
                    resources.Read(buffer, 0, buffer.Length);
                    stream.Write(buffer, 0, buffer.Length);
                }
            }
        }

        private static void DeleteResourceFile(string filename)
        {
            if (File.Exists(filename)) { File.Delete(filename); }
        }
    }
}
