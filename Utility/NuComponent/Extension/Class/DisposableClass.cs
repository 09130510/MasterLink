using System;

namespace Util.Extension.Class
{
	/// <summary>
	/// 可釋放類別
	/// </summary>
	public abstract class DisposableClass : IDisposable
	{
		private bool m_disposed = false;

		#region IDisposable 成員
		/// <summary>
		/// 可釋放類別
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this); //要求系統不要呼叫指定物件的完成項。
		}
		/// <summary>
		/// Dispose
		/// </summary>
		/// <param name="IsDisposing"></param>
		protected void Dispose(bool IsDisposing)
		{
			if (m_disposed) return;

			if (IsDisposing)
			{
				DoDispose();
			}
			m_disposed = true;
		}
		/// <summary>
		/// Do something when disposing
		/// </summary>
		protected abstract void DoDispose();
		#endregion
	}
}
