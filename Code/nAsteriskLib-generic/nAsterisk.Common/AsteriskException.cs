using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk
{
	public class AsteriskException : Exception
	{
		public AsteriskException(string msg, Exception inner)
			: base(msg, inner)
		{

		}

		public AsteriskException(string msg)
			: base(msg)
		{

		}
	}
}
