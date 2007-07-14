using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk.AGICommand
{
	public class GetChannelStatusCommand : AGICommandBase, IProvideCommandResult
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

		public override void ProcessResponse(FastAGIResponse response)
		{
			if (Enum.IsDefined(typeof(ChannelStatus), response.ResultValue))
				_channelStatus = (ChannelStatus)Enum.Parse(typeof(ChannelStatus), response.ResultValue);
		}

		public ChannelStatus GetResult()
		{
			return _channelStatus;
		}

		#region IProviteCommandResult Members

		object IProvideCommandResult.GetResult()
		{
			return this.GetResult();
		}

		#endregion
	}
}
