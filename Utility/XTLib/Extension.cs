using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;

namespace XTLib
{
    public static class Extension
    {
        public static string Description(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes != null && attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            return value.ToString();
        }
        /// <summary>
        /// String to TEnum, parse fail return default(TEnum)
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public static TEnum ToEnum<TEnum>(this string name) where TEnum : struct
        {
            TEnum val;
            if (!Enum.TryParse<TEnum>(name, out val))
            {
                return default(TEnum);
            }
            return val;
        }
        /// <summary>
        /// Char to TEnum, parse fail return default(TEnum)
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public static TEnum ToEnum<TEnum>(this char name) where TEnum : struct
        {
            return ToEnum<TEnum>(name.ToString());
        }
        /// <summary>
        /// Object to TEnum, parse fail return default(TEnum)
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public static TEnum ToEnum<TEnum>(this object name) where TEnum : struct
        {
            return ToEnum<TEnum>(name.ToString());
        }
    }
}
