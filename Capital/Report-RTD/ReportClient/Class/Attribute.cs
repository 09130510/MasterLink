using System;

namespace Capital.Report.Class
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CellAttribute : Attribute
    {
        #region Property
        public RowType Row { get; private set; }
        public ColumnType Column { get; private set; }
        public Type ValueType { get; private set; }
        public object Default { get; private set; }
        public string Format { get; private set; }
        #endregion

        public CellAttribute(RowType row, ColumnType col)
        {
            Row = row;
            Column = col;
        }
        public CellAttribute(RowType row, ColumnType col, Type type)
            : this(row, col)
        {
            ValueType = type;
            Default = GetDefault(type);
            Format = "";
        }
        public CellAttribute(RowType row, ColumnType col, Type type, object defaultValue, string format)
            : this(row, col, type)
        {
            Default = defaultValue;
            Format = format;
        }

        #region Private
        private object GetDefault(Type type)
        {
            if (type.IsValueType)
            {
                return Activator.CreateInstance(type);
            }
            return null;
        }
        #endregion
    }
}
