using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk.AGICommand
{
	public class SayAlphaCommand : BaseAGICommand, IProvideCommandResult
	{
		private string _chars;

		private Digits _pressedDigit;
		private Digits _escapeDigits;

		public SayAlphaCommand(string chars, Digits escapeDigits)
		{
			_chars = chars;
			_escapeDigits = escapeDigits;
		}

		public string Chars
		{
			get { return _chars; }
			set { _chars = value; }
		}

		public Digits EscapeDigits
		{
			get { return _escapeDigits; }
			set { _escapeDigits = value; }
		}

		public override string GetCommand()
		{
			return string.Format("SAY ALPHA \"{0}\" \"{1}\"", _chars, AsteriskAgi.GetDigitsString(_escapeDigits));
		}

		public override bool IsSuccessfulResult(string result)
		{
			int code = -1;
			int.TryParse(result, out code);

			if (code > 0)
				_pressedDigit = AsteriskAgi.GetDigitsFromString(((Char)code).ToString());

			return (code != -1);
		}

		public Digits GetResult()
		{
			return _pressedDigit;
		}

		#region IProvideCommandResult Members

		object IProvideCommandResult.GetResult()
		{
			return this.GetResult();
		}

		#endregion
	}
}