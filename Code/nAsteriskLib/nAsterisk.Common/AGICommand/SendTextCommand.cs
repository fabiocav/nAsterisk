using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk.AGICommand
{
	public class SendTextCommand : BaseAGICommand
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

		public override bool IsSuccessfulResult(string result)
		{
			int code = 1;
			int.TryParse(result, out code);

			return (code == 0);
		}
	}
}