using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk.AGICommand
{
	public class GetChannelStatusCommand : BaseAGICommand, ISupportCommandResponse
	{
		private string _channelName;

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

		public override bool IsSuccessfulResult(int result)
		{
			return true; 
		}

		#region ISupportCommandResponse<string> Members
		
		public void ProcessResponse(string response)
		{
			
		}
	
		#endregion
	}
}
