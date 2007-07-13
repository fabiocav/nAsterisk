using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk.AGICommand
{
	public class SayDateCommand : BaseAGICommand, IProvideCommandResult
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
			return string.Format("SAY DATE {0} {1}", (_date.ToUniversalTime().Ticks / TimeSpan.TicksPerSecond), AsteriskAgi.GetDigitsString(_escapeDigits));
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