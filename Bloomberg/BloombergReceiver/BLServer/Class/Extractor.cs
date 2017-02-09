using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BLPServer.Class
{
    public static class Extractor
    {
        public static void ExtractResourceToFile(string resourceName, string filename, bool overWrite)
        {
            if (overWrite) { DeleteResourceFile(filename); }
            if (File.Exists(filename)) { return; }

            using (Stream s = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            using (FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                byte[] b = new byte[s.Length];
                s.Read(b, 0, b.Length);
                fs.Write(b, 0, b.Length);
            }
        }
        public static void DeleteResourceFile(string filename)
        {
            if (File.Exists(filename)) { File.Delete(filename); }
        }
    }
}
