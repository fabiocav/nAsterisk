using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk.AGICommand
{
	internal class TDDModeCommand : BaseAGICommand
	{
		private bool _enabled = false;

		public TDDModeCommand(bool enabled)
		{
			_enabled = enabled;
		}

		public bool Enabled
		{
			get { return _enabled; }
			set { _enabled = value; }
		}

		public override string GetCommand()
		{
			return string.Format("TDD MODE {0}", _enabled ? "on" : "off");
		}

		public override bool IsSuccessfulResult(string result)
		{
			if (result == "1")
				return true;
			else if (result == "0")
				throw new AsteriskException("The channel is not TDD capable.");
			else
				return false;
		}
	}
}
