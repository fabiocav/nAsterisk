using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk.AGICommand
{
	internal class SetPriorityCommand : BaseAGICommand
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

		public override bool IsSuccessfulResult(string result)
		{
			return result == "0";
		}
	}
}
