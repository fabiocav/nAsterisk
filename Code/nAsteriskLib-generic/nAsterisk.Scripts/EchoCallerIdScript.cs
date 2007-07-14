using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk.Scripts
{
	public class EchoCallerIdScript : IAsteriskAGIScript
	{
		#region IAsteriskAGIScript Members

		public void Execute(AsteriskAGI agi, Dictionary<string, string> vars)
		{
			agi.Answer();

			ChannelStatus status = agi.GetChannelStatus();

			Console.WriteLine("Caller ID is {0}", agi.CallerId);
		}

		#endregion
	}
}
