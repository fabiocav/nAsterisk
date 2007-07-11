using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk.AGICommand
{
	public abstract class BaseAGICommand
	{
		public abstract string GetCommand();
		public abstract bool IsSuccessfulResult(int result);
	}
}
