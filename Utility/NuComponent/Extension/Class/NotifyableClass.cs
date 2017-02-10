using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;


namespace Util.Extension.Class
{
	/// <summary>
	/// Notifyable
	/// </summary>
	public abstract class NotifyableClass : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged
		/// <summary>
		/// Property Change
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
		protected virtual void OnPropertyChanged(string propertyName )
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
	}
}
