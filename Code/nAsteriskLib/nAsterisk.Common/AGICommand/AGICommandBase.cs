using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk.AGICommand
{
	public abstract class AGICommandBase
	{
		public abstract string GetCommand();
		public abstract void ProcessResponse(FastAGIResponse response);
	}
}
