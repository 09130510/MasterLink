using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Util.Extension.Class
{
	/// <summary>
	/// 訊息中心訊息
	/// </summary>
	public class Notification
	{
		private readonly object m_Sender;
		private readonly object m_Message;
		/// <summary>
		/// 傳送者
		/// </summary>
		public object Sender { get { return m_Sender; } }
		//內容
		public object Message { get { return m_Message; } }
		/// <summary>
		/// 訊息中心訊息
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="message"></param>
		public Notification(object sender, object message)
		{
			m_Sender = sender;
			m_Message = message;
		}
		/// <summary>
		/// 空訊息
		/// </summary>
		public static Notification Empty
		{
			get
			{
				return new Notification(null, null);
			}
		}
	}
}
