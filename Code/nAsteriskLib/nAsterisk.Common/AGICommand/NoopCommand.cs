using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk.AGICommand
{
	public class NoopCommand : BaseAGICommand
	{
		public NoopCommand()
		{}

		public override string GetCommand()
		{
			return "NOOP";
		}

		public override bool IsSuccessfulResult(string result)
		{
			return true;
		}
	}
}
