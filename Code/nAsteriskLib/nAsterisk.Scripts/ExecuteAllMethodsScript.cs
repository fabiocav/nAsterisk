using System;
using System.Collections.Generic;
using System.Text;
using nAsterisk.AGICommand;

namespace nAsterisk.Scripts
{
	public class ExecuteAllMethodsScript : IAsteriskAgiScript
	{
		#region IAsteriskAgiScript Members

		public void Execute(AsteriskAgi agi, Dictionary<string, string> vars)
		{
			agi.Answer();
			Console.WriteLine("Called Answer");

			GetChannelStatusCommand gcscommand = new GetChannelStatusCommand();
			ChannelStatus status = agi.GetChannelStatus(gcscommand);
			Console.WriteLine("Called GetChannelStatus: {0}", status);


			DatabasePutCommand dpcommand = new DatabasePutCommand("test", "blah", "foobar");
			agi.DatabasePut(dpcommand);
			Console.WriteLine("Called DatabasePut");


			DatabaseGetCommand dgcommand = new DatabaseGetCommand("test", "blah");
			string var = agi.DatabaseGet(dgcommand);
			Console.WriteLine("Called DatabaseGet: {0}", var);


			DatabaseDeleteCommand ddcommand = new DatabaseDeleteCommand("test", "blah");
			agi.DatabaseDelete(ddcommand);
			Console.WriteLine("Called DatabaseDelete");
			


		}

		#endregion
	}
}
