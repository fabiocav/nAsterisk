using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk.AGICommand
{
	public class SayDigitsCommand : AGIReturnCommandBase<Digits>
	{
		private string _number;
		private Digits _escapeDigits;
		private Digits _pressedDigit;

		private string _validChars = "0123456789#*";

		public SayDigitsCommand(string number, Digits escapeDigits)
		{
			_number = number;
			_escapeDigits = escapeDigits;
		}

		public Digits EscapeDigits
		{
			get { return _escapeDigits; }
			set { _escapeDigits = value; }
		}

		public string Number
		{
			get { return _number; }
			set { _number = value; }
		}


		public override string GetCommand()
		{
			validateNumber(_number);

			return string.Format("SAY DIGITS {0} {1}", _number, AsteriskAGI.GetDigitsString(_escapeDigits));
		}

		private void validateNumber(string _number)
		{
			foreach (char c in _number)
			{
				if (!_validChars.Contains(c.ToString()))
					throw new ArgumentException("The Number contains invalid digits.");
			}
		}

		public override Digits ProcessResponse(FastAGIResponse response)
		{
			if (response.ResultValue == "-1")
				throw new AsteriskCommandException("SayDigits Command Failed.");

			if (response.ResultValue != "0")
				_pressedDigit = AsteriskAGI.GetDigitsFromString(((Char)int.Parse(response.ResultValue)).ToString());

            return _pressedDigit;
		}
	}
}
