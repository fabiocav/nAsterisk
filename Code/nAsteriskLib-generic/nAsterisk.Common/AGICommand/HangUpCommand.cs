using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk.AGICommand
{
	public class HangUpCommand : AGINoReturnCommandBase
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

		public override void ProcessResponse(FastAGIResponse response)
		{
			if (response.ResultValue == "-1")
				throw new AsteriskException("HangUp Command Failed.");
		}
	}
}
