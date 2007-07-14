using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk.AGICommand
{
	public class SayTimeCommand : AGIReturnCommandBase<Digits>
	{
		private Digits _pressedDigit;
		private Digits _escapeDigits;
		private DateTime _time;

		public SayTimeCommand(DateTime time, Digits escapeDigits)
		{
			_time = time;
			_escapeDigits = escapeDigits;
		}

		public Digits EscapeDigits
		{
			get { return _escapeDigits; }
			set { _escapeDigits = value; }
		}

		public DateTime Date
		{
			get { return _time; }
			set { _time = value; }
		}

		public override string GetCommand()
		{
			return string.Format("SAY TIME {0} {1}", ((_time - new DateTime(1970,1,1,0,0,0)).TotalSeconds), AsteriskAGI.GetDigitsString(_escapeDigits));
		}

		public override Digits ProcessResponse(FastAGIResponse response)
		{
			if (response.ResultValue == "-1")
				throw new AsteriskException("SayTime Command Failed.");

			if (response.ResultValue != "0")
				_pressedDigit = AsteriskAGI.GetDigitsFromString(((Char)int.Parse(response.ResultValue)).ToString());

            return _pressedDigit;
		}
	}
}
