using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk.AGICommand
{
	public class NoopCommand : AGICommandBase
	{
		public NoopCommand()
		{}

		public override string GetCommand()
		{
			return "NOOP";
		}

		public override void ProcessResponse(FastAGIResponse response)
		{
			//This command always return a result value of 0.
		}
	}
}
