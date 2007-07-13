using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk.AGICommand
{
	public class SayAlphaCommand : BaseAGICommand, IProvideCommandResult
	{
		private string _chars;
		private string _pressedDigit;

		private EscapeDigits _escapeDigits;

		public SayAlphaCommand(string chars, EscapeDigits escapeDigits)
		{
			_chars = chars;
			_escapeDigits = escapeDigits;
		}

		public string Chars
		{
			get { return _chars; }
			set { _chars = value; }
		}

		public EscapeDigits EscapeDigits
		{
			get { return _escapeDigits; }
			set { _escapeDigits = value; }
		}

		public override string GetCommand()
		{
			return string.Format("SAY ALPHA \"{0}\" \"{1}\"", _chars, AsteriskAgi.GetEscapeDigitsString(_escapeDigits));
		}

		public override bool IsSuccessfulResult(string result)
		{
			int code = -1;
			int.TryParse(result, out code);

			if (code > 0)
				_pressedDigit = ((Char)code).ToString();

			return (code != -1);
		}

		public string GetResult()
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