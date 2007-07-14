using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk
{
	public class AsteriskCommandException : AsteriskException
	{
		private AGICommand.AGICommandBase _command = null;
	
		public AsteriskCommandException(string msg, AGICommand.AGICommandBase command) : base(msg)
		{
			_command = command;
		}

		public AsteriskCommandException(string msg)
			: base(msg)
		{

		}

		public AGICommand.AGICommandBase Command
		{
			get { return _command; }
			set { _command = value; }
		}
	}
}
