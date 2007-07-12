using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk.AGICommand
{
	public class GetChannelStatusCommand : BaseAGICommand
	{
		private string _channelName;
		private ChannelStatus _channelStatus;

		public GetChannelStatusCommand()
			: this(string.Empty) { }

		public GetChannelStatusCommand(string channelName)
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
			string command = "CHANNEL STATUS";
			if (!string.IsNullOrEmpty(_channelName))
				command = string.Format("{0} {1}", command, _channelName);
			
			return command;
		}

		public override bool IsSuccessfulResult(string result)
		{

			_channelStatus = (ChannelStatus)Enum.Parse(typeof(ChannelStatus), result);
			return true; 
		}

		public ChannelStatus GetResponse()
		{
			return _channelStatus;
		}
	}
}
