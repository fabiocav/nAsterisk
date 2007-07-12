using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk.AGICommand
{
	public class HangUpCommand : BaseAGICommand
	{
		private string _channelName;

		public HangUpCommand()
		{}

		public HangUpCommand(string channelName)
		{
			_channelName = channelName;
		}

		public string ChannelName
		{
			get { return _channelName; }
			set { _channelName = value; }
		}

		public override string GetCommand()
		{
			string command = "HANGUP";

			if (!string.IsNullOrEmpty(_channelName))
				command = string.Format("{0} {1}", command, _channelName);

			return command;
		}

		public override bool IsSuccessfulResult(string result)
		{
			int code = -1;
			int.TryParse(result, out code);

			return code == 1;
		}
	}
}
