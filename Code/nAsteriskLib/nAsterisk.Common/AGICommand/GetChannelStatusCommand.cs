using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk.AGICommand
{
	public class GetChannelStatusCommand : BaseAGICommand, IProviteCommandResult
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
			int status = 0;
			if (int.TryParse(result, out status))
			{
				_channelStatus = (ChannelStatus)status;
			}
			return true; 
		}

		public ChannelStatus GetResult()
		{
			return _channelStatus;
		}

		#region IProviteCommandResult Members

		object IProviteCommandResult.GetResult()
		{
			return this.GetResult();
		}

		#endregion
	}
}
