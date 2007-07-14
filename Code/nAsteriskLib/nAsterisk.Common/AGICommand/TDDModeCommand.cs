using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk.AGICommand
{
	internal class TDDModeCommand : AGICommandBase
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

		public override void ProcessResponse(FastAGIResponse response)
		{
			if (response.ResultValue == "0")
				throw new AsteriskException("TDDMode Command Failed. The channel is not TDD capable.");
		}

	}
}
