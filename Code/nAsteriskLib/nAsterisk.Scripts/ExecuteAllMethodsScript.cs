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
			Console.WriteLine("Called DatabasePut set var to 'foobar'");


			DatabaseGetCommand dgcommand = new DatabaseGetCommand("test", "blah");
			string var = agi.DatabaseGet(dgcommand);
			Console.WriteLine("Called DatabaseGet: {0}", var);


			DatabaseDeleteCommand ddcommand = new DatabaseDeleteCommand("test", "blah");
			agi.DatabaseDelete(ddcommand);
			Console.WriteLine("Called DatabaseDelete");
			// Test that the Delete worked
			try
			{
				var = agi.DatabaseGet(dgcommand);
				Console.WriteLine("DatabaseGet Succeded, it shouldn't have: {0}", var);
			}
			catch (AsteriskException)
			{
				Console.WriteLine("DatabaseGet Failed, this is good.");
			}


			Console.WriteLine("Waiting 2 seconds for digit");
			string digit = agi.WaitForDigit(TimeSpan.FromSeconds(2));
			Console.WriteLine("Called WaitForDigit: {0}", digit);


			agi.VerboseLog("Test Log", AsteriskVerboseLevel.Error);
			Console.WriteLine("Called VerboseLog");
		}

		#endregion
	}
}
