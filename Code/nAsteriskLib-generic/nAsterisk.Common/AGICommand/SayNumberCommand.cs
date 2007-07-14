using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk.AGICommand
{
	public class SayNumberCommand : AGICommandBase, IProvideCommandResult
	{
		private int _number;
		private Digits _escapeDigits;
		private Digits _pressedDigit;

		public SayNumberCommand(int number, Digits escapeDigits)
		{
			_number = number;
			_escapeDigits = escapeDigits;
		}

		public Digits EscapeDigits
		{
			get { return _escapeDigits; }
			set { _escapeDigits = value; }
		}

		public int Number
		{
			get { return _number; }
			set { _number = value; }
		}

		public override string GetCommand()
		{
			return string.Format("SAY NUMBER {0} {1}", _number.ToString(), AsteriskAGI.GetDigitsString(_escapeDigits));
		}

		public override void ProcessResponse(FastAGIResponse response)
		{
			if (response.ResultValue == "-1")
				throw new AsteriskException("SayNumber Command Failed.");

			if (response.ResultValue != "0")
				_pressedDigit = AsteriskAGI.GetDigitsFromString(((Char)int.Parse(response.ResultValue)).ToString());
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
