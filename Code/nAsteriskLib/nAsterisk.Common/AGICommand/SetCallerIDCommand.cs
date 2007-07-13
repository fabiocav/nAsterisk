using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk.AGICommand
{
	public class SetCallerIDCommand : BaseAGICommand
	{
		private string _number;

		public SetCallerIDCommand(string number)
		{
			_number = number;
		}

		public string Number
		{
			get { return _number; }
			set { _number = value; }
		}

		public override string GetCommand()
		{
			return string.Format("SET CALLERID {0}", _number);
		}

		public override bool IsSuccessfulResult(string result)
		{
			return true; // NOTE: Not worth parsing it always returns "200 result=1"
		}
	}
}