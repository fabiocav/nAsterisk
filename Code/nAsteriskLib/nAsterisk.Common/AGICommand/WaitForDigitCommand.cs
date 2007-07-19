using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk.AGICommand
{
	internal class WaitForDigitCommand : AGIReturnCommandBase<Digits>
	{
		private int _timeout;
		private Digits _digit;

		public WaitForDigitCommand(int timeout)
		{
			_timeout = timeout;
		}

		public int Timeout
		{
			get { return _timeout; }
			set { _timeout = value; }
		}

		public override string GetCommand()
		{
			return string.Format("WAIT FOR DIGIT {0}", _timeout);
		}

		public override Digits ProcessResponse(FastAGIResponse response)
		{
			if (response.ResultValue == "-1")
				throw new AsteriskCommandException("WaitForDigit Command Failed.");

			if (response.ResultValue != "0")
			{
				int digitCode;
				if (int.TryParse(response.ResultValue, out digitCode))
				{
					_digit = AsteriskAGI.GetDigitsFromString(((Char)digitCode).ToString());
				}
			}

            return _digit;
		}
	}
}
