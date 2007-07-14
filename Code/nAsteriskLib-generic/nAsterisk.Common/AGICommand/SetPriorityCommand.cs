using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk.AGICommand
{
	internal class SetPriorityCommand : AGINoReturnCommandBase
	{
		private int _priority;

		public SetPriorityCommand(int priority)
		{
			_priority = priority;
		}

		public int Priority
		{
			get { return _priority; }
			set { _priority = value; }
		}

		public override string GetCommand()
		{
			return string.Format("SET PRIORITY {0}", _priority);
		}

		public override void ProcessResponse(FastAGIResponse response)
		{
			if (response.ResultValue != "0")
				throw new AsteriskException("SetPriority Command Failed.");
		}
	}
}
