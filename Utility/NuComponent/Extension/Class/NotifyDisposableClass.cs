using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Util.Extension.Class
{
	/// <summary>
	/// 屬性改變&可釋放 類別
	/// </summary>
	public abstract class NotifyDisposableClass : INotifyPropertyChanged, IDisposable
	{
		private bool m_disposed = false;

		#region INotifyPropertyChanged
		/// <summary>
		/// 屬性改變
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;
		/// <summary>
		/// 屬性改變(不驗證屬性是否存在)
		/// </summary>
		/// <param name="propertyName"></param>
		protected virtual void OnPropertyChangedNoVerify(string propertyName)
		{
			if (null != PropertyChanged)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		/// <summary>
		/// 屬性改變
		/// </summary>
		/// <param name="propertyName"></param>
		protected virtual void OnPropertyChanged(string propertyName)
		{
			//x Validate the property name in debug builds                    
			VerifyProperty(propertyName);
			if (null != PropertyChanged)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		//x [System.Diagnostics.Conditional("DEBUG")]                
		private void VerifyProperty(string propertyName)
		{
			Type type = this.GetType();
			// Look for a *public* property with the specified name                    
			System.Reflection.PropertyInfo pi = type.GetProperty(propertyName);
			if (pi == null)
			{
				// There is no matching property - notify the developer                        
				string msg = "OnPropertyChanged was invoked with invalid " +
											"property name {0}. {0} is not a public " +
											"property of {1}.";
				msg = String.Format(msg, propertyName, type.FullName);
				//System.Diagnostics.Debug.Fail(msg);
				MessageBox.Show(msg, "Notify Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		#endregion

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
