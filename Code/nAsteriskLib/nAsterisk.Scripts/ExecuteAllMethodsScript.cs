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

			
			ChannelStatus status = agi.GetChannelStatus();
			Console.WriteLine("Called GetChannelStatus: {0}", status);


			agi.DatabasePut("test", "blah", "foobar");
			Console.WriteLine("Called DatabasePut set var to 'foobar'");


			string var = agi.DatabaseGet("test", "blah");
			Console.WriteLine("Called DatabaseGet: {0}", var);


			agi.DatabaseDelete("test", "blah");
			Console.WriteLine("Called DatabaseDelete");
			// Test that the Delete worked
			try
			{
				var = agi.DatabaseGet("test", "blah");
				Console.WriteLine("DatabaseGet Succeded, it shouldn't have: {0}", var);
			}
			catch (AsteriskException)
			{
				Console.WriteLine("DatabaseGet Failed, this is good.");
			}


			Console.WriteLine("Waiting 2 seconds for digit");
			string digit = agi.WaitForDigit(TimeSpan.FromSeconds(2));
			Console.WriteLine("Called WaitForDigit: {0}", digit);


			agi.VerboseLog("Test Error Log", AsteriskVerboseLevel.Error);
			agi.VerboseLog("Test Info Log", AsteriskVerboseLevel.Info);
			agi.VerboseLog("Test Trace Log", AsteriskVerboseLevel.Trace);
			agi.VerboseLog("Test Debug Log", AsteriskVerboseLevel.Debug);
			Console.WriteLine("Called VerboseLog");


			try
			{
				Console.WriteLine("Calling EnableTDD(true)");
				agi.EnableTDD(true);
				Console.WriteLine("Called EnableTDD(true)");
			}
			catch (AsteriskException)
			{
				Console.WriteLine("Tried to enable TDD but the channel is not capable.");
			}


			agi.StreamFile("vm-savedto");
			Console.WriteLine("Called StreamFile");


			agi.HangUp();
			Console.WriteLine("Called Hangup");
		}

		#endregion
	}
}
