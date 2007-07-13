using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk.AGICommand
{
	internal class SetExtensionCommand : BaseAGICommand
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

		public override bool IsSuccessfulResult(string result)
		{
			return result == "0";
		}
	}
}
