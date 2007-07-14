using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk.AGICommand
{
	public class AnswerCommand : AGICommandBase
	{
		public override string GetCommand()
		{
			return "ANSWER";
		}

		public override void ProcessResponse(FastAGIResponse response)
		{
			if (response.ResultValue == "-1")
				throw new AsteriskException("Aswer Command Failed.");
		}
	}
}