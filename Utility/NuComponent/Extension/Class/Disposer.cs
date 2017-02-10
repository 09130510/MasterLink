using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Util.Extension.Class
{
	public abstract class Disposer<TKeyType, TValueType, TValue>:IDisposable where TValueType: Type
	{
		private Dictionary<TKeyType, TValueType> m_Collection;
		private TValue m_Value;
		private Action m_OnDisposed;

		public Disposer(Dictionary<TKeyType, TValueType> collection, TValue value, Action onDisposed)
		{
			m_Collection = collection;
			m_Value = value;
			m_OnDisposed = onDisposed;
		}
		public abstract void Dispose();		
	}
}
