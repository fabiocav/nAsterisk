using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk
{
	public class HangupException : AsteriskException
	{
		public HangupException() : base("Remote connection disconnected") { }
		public HangupException(string msg) : base(msg) { }
	}
}
