using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk.AGICommand
{
	public class SayAlphaCommand : AGIReturnCommandBase<Digits>
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
			return string.Format("SAY ALPHA \"{0}\" \"{1}\"", _chars, AsteriskAGI.GetDigitsString(_escapeDigits));
		}

		public override Digits ProcessResponse(FastAGIResponse response)
		{
			if (response.ResultValue == "-1")
				throw new AsteriskException("SayAlpha Command Failed.");

			if (response.ResultValue != "0")
				_pressedDigit = AsteriskAGI.GetDigitsFromString(((Char)int.Parse(response.ResultValue)).ToString());

            return _pressedDigit;
		}
	}
}