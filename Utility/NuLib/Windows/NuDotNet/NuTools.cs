using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Reflection;

namespace NuDotNet
{
    public static class NuTools
    {
        public static string DATE_YYYY_MM_DD = @"\d{4}-\d{1,2}-\d{1,2}";

        #region ListFolder
        /// <summary>
        /// Find all folder that include sub-folder in this path
        /// </summary>
        /// <param name="lstFolder">all folder list</param>
        /// <param name="sPath"></param>
        /// <returns></returns>
        public static int FindFolderList(ref List<string> lstFolder,string sPath)
        {
            DirectoryInfo dir = null;

            if (!Directory.Exists(sPath))
                return 0;

            dir = new DirectoryInfo(sPath);
            foreach (DirectoryInfo folder in dir.GetDirectories())
            {
                DirectoryInfo[] sub_folder = dir.GetDirectories();
                if (sub_folder.Length > 0)
                    FindFolderList(ref lstFolder, folder.FullName);

                lstFolder.Add(folder.FullName);
            }
            return lstFolder.Count;
        }
        #endregion

        #region DeletFolderByRegex
        /// <summary>
        /// Delete folder by regex
        /// </summary>
        /// <param name="lstFolder">folder list collections</param>
        /// <param name="sCompare">compare date</param>
        /// <param name="RegFmt">regex</param>
        public static void DeleteFolderByRegex(ref List<string> lstFolder, string sCompare, string RegFmt)
        {
            for (int i = 0; i < lstFolder.Count; i++)
            {
                DirectoryInfo dir = new DirectoryInfo(lstFolder[i]);

                if (! Regex.IsMatch(dir.Name, RegFmt) )
                {
                    continue;
                }

				if (string.Compare(dir.Name, sCompare) < 0 && Directory.Exists(dir.FullName))
				{
					try
					{
						dir.Delete(true);
					}
					catch (IOException ex)
					{
						//throw;
					}					
				}
            }
        }
        #endregion

        #region get version
        /// <summary>
        /// NuDotNet Version number
        /// </summary>
        /// <returns></returns>
        public static string GetNuDotNetVersion()
        {
            return String.Format("NuDotNet Ver : {0}", Assembly.GetExecutingAssembly().GetName().Version.ToString(3));
        }
        #endregion
    }
}
