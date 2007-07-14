using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk.AGICommand
{
	public class SendTextCommand : AGINoReturnCommandBase
	{
		private string _text;

		public SendTextCommand(string text)
		{
			_text = text;
		}

		public string Text
		{
			get { return _text; }
			set { _text = value; }
		}

		public override string GetCommand()
		{
			return string.Format("SEND TEXT \"{0}\"", _text);
		}

		public override void ProcessResponse(FastAGIResponse response)
		{
			if (response.ResultValue == "-1")
				throw new AsteriskCommandException("SendText Command Failed.");
		}
	}
}