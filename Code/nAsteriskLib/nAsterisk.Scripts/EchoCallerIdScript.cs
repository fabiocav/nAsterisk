using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk.Scripts
{
	public class EchoCallerIdScript : IAsteriskAgiScript
	{
		#region IAsteriskAgiScript Members

		public void Execute(AsteriskAgi agi, Dictionary<string, string> vars)
		{
			agi.Answer();

			ChannelStatus status = agi.GetChannelStatus();

			Console.WriteLine("Caller ID is {0}", agi.CallerId);
		}

		#endregion
	}
}
