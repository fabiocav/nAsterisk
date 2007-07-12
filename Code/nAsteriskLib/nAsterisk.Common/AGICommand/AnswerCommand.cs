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

		public override bool IsSuccessfulResult(string result)
		{
			int code = 1;
			int.TryParse(result, out code);

			return code == 0;
		}
	}
}
