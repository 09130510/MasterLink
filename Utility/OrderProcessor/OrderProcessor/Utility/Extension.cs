using System;
using System.Collections.Generic;
using System.Reflection;
using Util.Extension;

namespace OrderProcessor
{
    public static class Extension
    {
        public static char FIX(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            Value4FIXAttribute[] attributes = (Value4FIXAttribute[])fi.GetCustomAttributes(typeof(Value4FIXAttribute), false);
            if (attributes != null && attributes.Length > 0)
            {
                return attributes[0].Value;
            }
            return default(char);
        }
        public static char Capital(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            Value4CapitalAttribute[] attributes = (Value4CapitalAttribute[])fi.GetCustomAttributes(typeof(Value4CapitalAttribute), false);
            if (attributes != null && attributes.Length > 0)
            {
                return attributes[0].Value;
            }
            return default(char);
        }
        public static TEnum ToEnumByFIX<TEnum>(this char value) where TEnum : struct
        {
            FieldInfo[] fields = typeof(TEnum).GetFields();

            foreach (FieldInfo fi in fields)
            {
                IList<CustomAttributeData> attributes = fi.GetCustomAttributesData();
                if (attributes != null && attributes.Count > 0)
                {
                    for (int i = 0; i < attributes.Count; i++)
                    {
                        if (attributes[i].ToString().Contains("Value4FIXAttribute"))
                        {
                            if (Convert.ChangeType(attributes[i].ConstructorArguments[0].Value, attributes[i].ConstructorArguments[0].ArgumentType).Equals(value))
                            {                                
                                return fi.Name.ToEnum<TEnum>();
                            }
                        }
                    }
                }
            }
            return default(TEnum);
        }
        public static TEnum ToEnumByCapital<TEnum>(this char value) where TEnum : struct
        {
            FieldInfo[] fields = typeof(TEnum).GetFields();

            foreach (FieldInfo fi in fields)
            {
                IList<CustomAttributeData> attributes = fi.GetCustomAttributesData();
                if (attributes != null && attributes.Count > 0)
                {
                    for (int i = 0; i < attributes.Count; i++)
                    {
                        if (attributes[i].ToString().Contains("Value4CapitalAttribute"))
                        {
                            if (Convert.ChangeType(attributes[i].ConstructorArguments[0].Value, attributes[i].ConstructorArguments[0].ArgumentType).Equals(value) || fi.Name.Equals(value.ToString()))
                            {
                                return fi.Name.ToEnum<TEnum>();
                            }
                        }
                    }
                }
            }
            return default(TEnum);
        }
    }
}
