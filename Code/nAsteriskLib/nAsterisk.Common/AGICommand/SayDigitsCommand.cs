using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk.AGICommand
{
	public class SayDigitsCommand : BaseAGICommand, IProvideCommandResult
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

			return string.Format("SAY DIGITS {0} {1}", _number, AsteriskAgi.GetDigitsString(_escapeDigits));
		}

		private void validateNumber(string _number)
		{
			foreach (char c in _number)
			{
				if (!_validChars.Contains(c.ToString()))
					throw new ArgumentException("The Number contains invalid digits.");
			}
		}

		public override bool IsSuccessfulResult(string result)
		{
			int code = -1;
			int.TryParse(result, out code);

			if (code > 0)
				_pressedDigit = AsteriskAgi.GetDigitsFromString(((Char)code).ToString());

			return code != -1;
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
