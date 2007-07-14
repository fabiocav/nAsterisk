using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk.AGICommand
{
	public class SetAutoHangUpCommand : AGINoReturnCommandBase
	{
		private int _time;

		public SetAutoHangUpCommand(int time)
		{
			_time = time;
		}

		public int Time
		{
			get { return _time; }
			set { _time = value; }
		}

		public override string GetCommand()
		{
			return string.Format("SET AUTOHANGUP {0}", _time);
		}

		public override void ProcessResponse(FastAGIResponse response)
		{
			if (response.ResultValue == "-1")
				throw new AsteriskCommandException("SetAutoHangUp Command Failed.");
		}
	}
}
