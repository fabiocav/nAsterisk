using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk.AGICommand
{
	public class AnswerCommand : BaseAGICommand
	{
		public override string GetCommand()
		{
			return "ANSWER";
		}

		public override bool IsSuccessfulResult(int result)
		{
			return result == 0;
		}
	}
}
