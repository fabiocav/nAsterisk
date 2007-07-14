using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk.AGICommand
{
	internal class SetExtensionCommand : AGINoReturnCommandBase
	{
		private string _extension;

		public SetExtensionCommand(string extension)
		{
			_extension = extension;
		}

		public string Extension
		{
			get { return _extension; }
			set { _extension = value; }
		}
	
		public override string GetCommand()
		{
			return string.Format("SET EXTENSION {0}", _extension);
		}

		public override void ProcessResponse(FastAGIResponse response)
		{
			//This command always returns a result value of 0.
		}
	}
}
