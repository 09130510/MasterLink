using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;

namespace Util.Extension
{
	/// <summary>
	/// 擴充方法
	/// </summary>
	public static class Extension
	{
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
		/// <summary>
		/// Description(string) to TEnum, parse fail return default(TEnum)
		/// </summary>
		/// <typeparam name="TEnum"></typeparam>
		/// <param name="value"></param>
		/// <returns></returns>
		public static TEnum ToEnumByDescription<TEnum>(this string value) where TEnum : struct
		{
			FieldInfo[] fields = typeof(TEnum).GetFields();

			foreach (FieldInfo fi in fields)
			{
				IList<CustomAttributeData> attributes = fi.GetCustomAttributesData();
				if (attributes != null && attributes.Count > 0)
				{
					for (int i = 0; i < attributes.Count; i++)
					{
						if (attributes[i].ToString().Contains("DescriptionAttribute"))
						{
							if (Convert.ChangeType(attributes[i].ConstructorArguments[0].Value, attributes[i].ConstructorArguments[0].ArgumentType).Equals(value))
							{
								TEnum t = fi.Name.ToEnum<TEnum>();
								return t;
							}
						}
					}
				}
			}

			return default(TEnum);
		}		
		/// <summary>
		/// translate Enum to Array with Description
		/// </summary>
		/// <typeparam name="TEnum"></typeparam>		
		/// <param name="isSort">Default: false</param>
		/// <returns></returns>
		public static string[] ToArrayByDescription<TEnum>(bool isSort = false) where TEnum : struct
		{
			List<string> re = new List<string>();
			string[] names = Enum.GetNames(typeof(TEnum));
			foreach (var name in names)
			{
				TEnum t = name.ToEnum<TEnum>();
				FieldInfo fi = t.GetType().GetField(t.ToString());
				DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
				if (attributes != null && attributes.Length > 0)
				{
					re.Add(attributes[0].Description);
				}
			}
			if (isSort) re.Sort();
			return re.ToArray();
		}		
		/// <summary>
		/// Linq Distinct
		/// </summary>
		/// <typeparam name="TSource"></typeparam>
		/// <typeparam name="TKey"></typeparam>
		/// <param name="source"></param>
		/// <param name="keySelector"></param>
		/// <returns></returns>
		public static IEnumerable<TSource> Distinct<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		{
			HashSet<TKey> seenKeys = new HashSet<TKey>();
			foreach (TSource element in source)
			{
				var elementValue = keySelector(element);
				if (seenKeys.Add(elementValue))
				{
					yield return element;
				}
			}
		}
		/// <summary>
		/// Get Type Default Value
		/// </summary>
		/// <param name="t"></param>
		/// <returns></returns>
		public static object GetDefault(this Type t)
		{
			if (t.IsValueType)
			{
				return Activator.CreateInstance(t);
			}
			return null;
		}
		/// <summary>
		/// String to double, parse fail return default(double)
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public static double ToDouble(this string name)
		{
			double val;
			if (!double.TryParse(name, out val))
			{
				return default(double);
			}
			return val;
		}
		/// <summary>
		/// Object to double, parse fail return default(double)
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public static double ToDouble(this object name)
		{
			double val;
			if (!double.TryParse(name.ToString(), out val))
			{
				return default(double);
			}
			return val;
		}
		/// <summary>
		/// String to int, parse fail return default(int)
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public static int ToInt(this string name)
		{
			int val;
			if (!int.TryParse(name, out val))
			{
				return default(int);
			}
			return val;
		}
		/// <summary>
		/// object to int, parse fail return default(int)
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public static int ToInt(this object name)
		{
			int val;
			if (!int.TryParse(name.ToString(), out val))
			{
				return default(int);
			}
			return val;
		}
		/// <summary>
		/// String to decimal, parse fail return default(decimal)
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public static decimal ToDecimal(this string name)
		{
			decimal val;
			if (!decimal.TryParse(name, out val))
			{
				return default(decimal);
			}
			return val;
		}
		/// <summary>
		/// Object to decimal, parse fail return default(decimal)
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public static decimal ToDecimal(this object name)
		{
			decimal val;
			if (!decimal.TryParse(name.ToString(), out val))
			{
				return default(decimal);
			}
			return val;
		}
		/// <summary>
		/// String to bool, parse fail return default(bool)
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public static bool ToBool(this string name)
		{
			bool val;
			if (!bool.TryParse(name, out val))
			{
				return default(bool);
			}
			return val;
		}
		/// <summary>
		/// object to bool, parse fail return default(bool)
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public static bool ToBool(this object name)
		{
			bool val;
			if (!bool.TryParse(name.ToString(), out val))
			{
				return default(bool);
			}
			return val;
		}
		/// <summary>
		/// String to DateTime, parse fail return default(DateTime)
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public static DateTime ToDateTime(this string name)
		{
			DateTime val;
			if (!DateTime.TryParse(name, out val))
			{
				return default(DateTime);
			}
			return val;
		}
		/// <summary>
		/// Object to DateTime, parse fail return default(DateTime)
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public static DateTime ToDateTime(this object name)
		{
			DateTime val;
			if (!DateTime.TryParse(name.ToString(), out val))
			{
				return default(DateTime);
			}
			return val;
		}
		/// <summary>
		/// String to assign type, parse fail return  default value or default(T)
		/// </summary>
		/// <typeparam name="T">assign type</typeparam>
		/// <param name="value">value to be cast</param>
		/// <param name="defaultvalue">pase fail will return this value</param>
		/// <returns></returns>
		public static T CastTo<T>(this string value, T defaultvalue = default(T))
		{
			var converter = TypeDescriptor.GetConverter(typeof(T));
			if (converter != null && value != null)
			{
				//Cast ConvertFromString(string text) : object to (T)
				return (T)converter.ConvertFromString(value);
			}
			return defaultvalue;

		}
		/// <summary>
		/// object to assign type, parse fail return  default value or default(T)
		/// </summary>
		/// <typeparam name="T">assign type</typeparam>
		/// <param name="value">value to be cast</param>
		/// <param name="defaultvalue">pase fail will return this value</param>
		/// <returns></returns>
		public static T CastTo<T>(this object value, T defaultvalue = default(T))
		{
			var converter = TypeDescriptor.GetConverter(typeof(T));
			if (converter != null && value != null)
			{
				//Cast ConvertFromString(string text) : object to (T)
				return (T)converter.ConvertFromString(value.ToString());
			}
			return defaultvalue;

		}
		/// <summary>
		/// 把陣列內容轉成字串, 用指定的符號隔開
		/// 陣列沒內容傳回空字串
		/// </summary>
		/// <param name="array"></param>
		/// <param name="spreadItem">分隔字串, 預設為","</param>
		/// <returns></returns>
		public static string ToArrayString(this string[] array, char spreadItem = ',')
		{
			if (array != null && array.Length > 0)
			{
				string re = string.Empty;
				for (int i = 0; i < array.Length; i++)
				{
					if (!String.IsNullOrEmpty(array[i]))
					{
						re += array[i];
					}
					if (i + 1 < array.Length && !String.IsNullOrEmpty(array[i + 1]))
					{
						re += spreadItem;
					}
				}
				return re;
			}
			return string.Empty;
		}
		/// <summary>
		/// Get Enum Description Attribute, not have Description return Enum.ToString()
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
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
		/// Get PropertyInfo Description Attribute, not have Description return PropertyInfo.Name
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static string Description(this PropertyInfo value)
		{
			DescriptionAttribute[] attributes = (DescriptionAttribute[])value.GetCustomAttributes(typeof(DescriptionAttribute), false);
			if (attributes != null && attributes.Length > 0)
			{
				return attributes[0].Description;
			}
			return value.Name;
		}
		/// <summary>
		/// Get PropertyInfo Category Attribute, not have Category return PropertyName
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static string Category(this PropertyInfo value)
		{
			CategoryAttribute[] attributes = (CategoryAttribute[])value.GetCustomAttributes(typeof(CategoryAttribute), false);
			if (attributes != null && attributes.Length > 0)
			{
				return attributes[0].Category;
			}
			return value.Name;
		}
		
		/// <summary>
		/// 無參數Delegate的Invoke
		/// </summary>
		/// <param name="action"></param>
		/// <param name="control">要Invoke的Control</param>
		public static void Invoke(this Action action, object control)
		{
			Control c = null;
			if (control is Control)
			{
				c = (Control)control;
			}
			else if (control is ToolStripItem)
			{
				c = ((ToolStripItem)control).Owner;
			}

			if (c.InvokeRequired)
			{
				c.Invoke(action);
			}
			else
			{
				action();
			}
		}
		/// <summary>
		/// 無參數Delegate的BeginInvoke
		/// </summary>
		/// <param name="action"></param>
		/// <param name="control">要BeginInvoke的Control</param>
		public static void BeginInvoke(this Action action, object control)
		{
			Control c = null;
			if (control is Control)
			{
				c = (Control)control;
			}
			else if (control is ToolStripItem)
			{
				c = ((ToolStripItem)control).Owner;
			}			

			if (c != null && c.InvokeRequired)
			{
				c.BeginInvoke(action);
			}
			else
			{
				action();
			}
		}
		/// <summary>
		/// 有參數Delegate的Invoke
		/// </summary>
		/// <param name="d">Delegate</param>
		/// <param name="control">要Invoke的Control</param>
		/// <param name="args">Delegate的參數</param>
		public static void Invoke(this Delegate d, object control, params object[] args)
		{
			Control c = null;
			if (control is Control)
			{
				c = (Control)control;
			}
			else if (control is ToolStripItem)
			{
				c = ((ToolStripItem)control).Owner;
			}

			if (c != null && c.InvokeRequired)
			{
				c.Invoke(d, args);
			}
			else
			{
				d.Method.Invoke(d.Target, args);
			}
		}
		/// <summary>
		/// 有參數Delegate的BeginInvoke
		/// </summary>
		/// <param name="d">Delegate</param>
		/// <param name="control">要BeginInvoke的Control</param>
		/// <param name="args">Delegate的參數</param>
		public static void BeginInvoke(this Delegate d, object control, params object[] args)
		{
			Control c = null;
			if (control is Control)
			{
				c = (Control)control;
			}
			else if (control is ToolStripItem)
			{
				c = ((ToolStripItem)control).Owner;
			}
			if (c.InvokeRequired)
			{
				c.BeginInvoke(d, args);
			}
			else
			{
				d.Method.Invoke(d.Target, args);
			}
		}
	}
}
