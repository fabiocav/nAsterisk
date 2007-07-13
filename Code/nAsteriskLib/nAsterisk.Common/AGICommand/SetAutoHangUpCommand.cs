using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk.AGICommand
{
	public class SetAutoHangUpCommand : BaseAGICommand
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

		public override bool IsSuccessfulResult(string result)
		{
			int code = -1;
			int.TryParse(result, out code);

			return (code == 0);
		}
	}
}
