/**************************************************************************
// nAsterisk - .NET Asterisk Library 
//
// Copyright (c) 2007 by:
// Fabio Cavalcante (fabio(a)codesapien.com)
// Josh Perry (josh(a)6bit.com)
// Justin Long (dukk(a)dukk.org)
//
// Asterisk - Copyright (C) 1999 - 2006, Digium, Inc.
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU Library General Public License as published 
// by  the Free Software Foundation; either version 2 of the License or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful, but
// WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Library General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this package (See COPYING.LIB); if not, write to the Free Software
// Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA 02111-1307, USA.
/**************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using nAsterisk.AGI.Command;

using nAsterisk.AGI;

namespace nAsterisk.Scripts
{
	public class ExecuteAllMethodsScript : IAsteriskAGIScript
	{
		#region IAsteriskAGIScript Members

		public void Execute(IAsteriskAGI agi, Dictionary<string, string> vars)
		{
			try
			{
				agi.Answer();
				Console.WriteLine("Called Answer");

				this.RunScript(agi, vars);

				agi.HangUp();
				Console.WriteLine("Called Hangup");
			}
			catch (HangUpException)
			{
				Console.WriteLine("###Asterisk hung up...");
			}
		}

		private void RunScript(IAsteriskAGI agi, Dictionary<string, string> vars)
		{
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
			Digits pressedDigit = agi.WaitForDigit(2000);

			Console.WriteLine("Called WaitForDigit: {0}", pressedDigit.ToString());


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


			StreamFileResult result = agi.StreamFile("hang-on-a-second-angry", "1234567890*#");
			Console.WriteLine("Called StreamFile");


			//Console.WriteLine("Please record your message now");
			//agi.RecordFile("blahrec", "wav", Digits.AllNumbers | Digits.AllSymbols, -1);
			//Console.WriteLine("Called RecordFile");


			//agi.StreamFile("blahrec");


			agi.SayNumber(30023, Digits.None);
			Console.WriteLine("Called SayNumber");


			agi.SayDigits("1234567890#*", Digits.None);
			Console.WriteLine("Called SayDigits");


			agi.SayAlpha("ABCDEF", Digits.None);
			Console.WriteLine("Called SayAlpha");


			agi.SayPhonetic("WT", Digits.None);
			Console.WriteLine("Called SayPhonetic");


			agi.SetAutoHangUp(10);
			Console.WriteLine("Called SetAutoHangup");


			agi.SayDateTime(DateTime.Now, Digits.None, "\"ABdY 'digits/at' IMp\"", "UTC");
			agi.SayDateTime(DateTime.Now, Digits.None);
			Console.WriteLine("Called SayDateTime");


			agi.SetVariable("foo", "bar");
			Console.WriteLine("Called SetVariable set var to 'bar'");


			var = agi.GetVariable("foo");
			Console.WriteLine("Called GetVariable: {0}", var);


			agi.EnableMusic(true);
			Console.WriteLine("Called EnableMusic(true), pausing for 2 seconds");
			System.Threading.Thread.Sleep(5000);
			agi.EnableMusic(false);
			Console.WriteLine("Called EnableMusic(false)");
			System.Threading.Thread.Sleep(2000);


			//agi.SetExtension("856");
			//Console.WriteLine("Called SetExtension");
		}

		#endregion
	}
}
