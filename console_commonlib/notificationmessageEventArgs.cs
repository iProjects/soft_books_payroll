/*
 * Created by: "kevin mutugi, kevinmk30@gmail.com, +254717769329"
 * Date: 04/02/2020
 * Time: 02:31
 */
using System;

namespace console_commonlib
{
	/// <summary>
	/// Description of notificationmessageEventArgs.
	/// </summary>
	public class notificationmessageEventArgs: EventArgs
	{
		public notificationmessageEventArgs(string _message){
			this.message = _message; 
		}
		public notificationmessageEventArgs(string _message, string _tag){
			this.message = _message; 
			this.TAG = _tag; 
		}
		public string message { get; private set; } 
		public string TAG { get; private set; } 
		 
	}
}
