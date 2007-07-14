using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk.AGICommand
{
	internal class VerboseCommand : AGINoReturnCommandBase
	{
		private string _message;
		private AsteriskVerboseLevel _level;

		public VerboseCommand(string message, AsteriskVerboseLevel level)
		{
			_message = message;
			_level = level;
		}

		public AsteriskVerboseLevel Level
		{
			get { return _level; }
			set { _level = value; }
		}
	
		public string Message
		{
			get { return _message; }
			set { _message = value; }
		}
	
		public override string GetCommand()
		{
			return string.Format("VERBOSE \"{0}\" {1}", _message, (int)_level);
		}

		public override void ProcessResponse(FastAGIResponse response)
		{}
	}
}
