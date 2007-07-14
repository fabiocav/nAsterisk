using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk.AGICommand
{
	public class SayDateCommand : AGIReturnCommandBase<Digits>
	{
		private Digits _pressedDigit;
		private Digits _escapeDigits;
		private DateTime _date;

		public SayDateCommand(DateTime date, Digits escapeDigits)
		{
			_date = date;
			_escapeDigits = escapeDigits;
		}

		public Digits EscapeDigits
		{
			get { return _escapeDigits; }
			set { _escapeDigits = value; }
		}

		public DateTime Date
		{
			get { return _date; }
			set { _date = value; }
		}

		public override string GetCommand()
		{
			return string.Format("SAY DATE {0} {1}", (_date.ToUniversalTime().Ticks / TimeSpan.TicksPerSecond), AsteriskAGI.GetDigitsString(_escapeDigits));
		}

		public override Digits ProcessResponse(FastAGIResponse response)
		{
			if (response.ResultValue == "-1")
				throw new AsteriskException("Say Date Command Failed.");

			if (response.ResultValue != "0")
				_pressedDigit = AsteriskAGI.GetDigitsFromString(((Char)int.Parse(response.ResultValue)).ToString());

            return _pressedDigit;
		}
	}
}