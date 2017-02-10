using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace NuDotNet
{
    public static class NuCryptography
    {
        #region 非對稱式加密
        /// <summary>
        /// 非對稱式加密 - MD5
        /// </summary>
        /// <param name="key">加密使用的key</param>
        /// <param name="val">加密的資料</param>
        /// <returns>MD5 字串</returns>
        public static string GenMD5(string key, string val)
        {
            string input = val + key;
            MD5 oMd5 = MD5.Create();
            byte[] MyinputBytes = Encoding.Default.GetBytes(input);
            byte[] MyhashBytes = oMd5.ComputeHash(MyinputBytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < MyhashBytes.Length; i++)
            {
                sb.Append(MyhashBytes[i].ToString("X2"));
            }
            return sb.ToString();
        }
        #endregion

        #region 對稱式加解密
        /// <summary>
        /// 對稱式加解密 - 加密
        /// </summary>
        /// <param name="sVal">加密的資料</param>
        /// <returns>加密後的字串</returns>
        public static string EnCode(string sVal)
        {
            byte[] Key = { 123, 62, 23, 56, 132, 146, 152, 33 };
            byte[] IV = { 12, 100, 123, 123, 123, 123, 23, 03 };
            byte[] bval, bpwd;

            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            ICryptoTransform ict = null;
            bval = Encoding.Default.GetBytes(sVal);
            ict = des.CreateEncryptor(Key, IV);
            bpwd = ict.TransformFinalBlock(bval, 0, bval.Length);
            return Convert.ToBase64String(bpwd);
        }
        /// <summary>
        /// 對稱式加解密 - 解密
        /// </summary>
        /// <param name="sVal">加密後的字串</param>
        /// <returns>解密的字串</returns>
        public static string DeCode(string sVal)
        {
            byte[] Key = { 123, 62, 23, 56, 132, 146, 152, 33 };
            byte[] IV = { 12, 100, 123, 123, 123, 123, 23, 03 };

            byte[] bval, bpwd;
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            ICryptoTransform ict = null;
            bval = Convert.FromBase64String(sVal);
            ict = des.CreateDecryptor(Key, IV);
            bpwd = ict.TransformFinalBlock(bval, 0, bval.Length);
            return Encoding.Default.GetString(bpwd);
        }
        #endregion

    }
}
