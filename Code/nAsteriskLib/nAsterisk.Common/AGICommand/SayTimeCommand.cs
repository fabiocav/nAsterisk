using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk.AGICommand
{
	public class SayTimeCommand : BaseAGICommand, IProvideCommandResult
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
			return string.Format("SAY TIME {0} {1}", ((_time - new DateTime(1970,1,1,0,0,0)).TotalSeconds), AsteriskAgi.GetDigitsString(_escapeDigits));
		}

		public override bool IsSuccessfulResult(string result)
		{
			int code = -1;
			int.TryParse(result, out code);

			if (code > 0)
				_pressedDigit = AsteriskAGI.GetDigitsFromString(((Char)code).ToString());

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
