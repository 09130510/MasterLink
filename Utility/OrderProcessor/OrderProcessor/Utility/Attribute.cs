using System;

namespace OrderProcessor
{
    [AttributeUsage(AttributeTargets.Field)]
    public class Value4FIXAttribute : Attribute
    {
        public char Value { get; private set; }

        public Value4FIXAttribute(char v)
        {
            Value = v;
        }
    }
    [AttributeUsage(AttributeTargets.Field)]
    public class Value4CapitalAttribute : Attribute
    {
        public char Value { get; private set; }

        public Value4CapitalAttribute(char v)
        {
            Value = v;
        }
    }
}
