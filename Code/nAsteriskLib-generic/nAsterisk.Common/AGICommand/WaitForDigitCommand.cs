using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk.AGICommand
{
	internal class WaitForDigitCommand : AGICommandBase, IProvideCommandResult
	{
		private TimeSpan _timeout;
		private Char? _digit;

		public WaitForDigitCommand(TimeSpan timeout)
		{
			_timeout = timeout;
		}

		public TimeSpan Timeout
		{
			get { return _timeout; }
			set { _timeout = value; }
		}

		public override string GetCommand()
		{
			return string.Format("WAIT FOR DIGIT {0}", _timeout.TotalMilliseconds);
		}

		public override void ProcessResponse(FastAGIResponse response)
		{
			if (response.ResultValue == "-1")
				throw new AsteriskException("WaitForDigit Command Failed.");

			if (response.ResultValue != "0")
			{
				int digitCode;
				if (int.TryParse(response.ResultValue, out digitCode))
				{
					_digit = (Char)digitCode;
				}
			}
		}

		public Char? GetResult()
		{
			return _digit;
		}

		#region IProviteCommandResult Members

		object IProvideCommandResult.GetResult()
		{
			return this.GetResult();
		}

		#endregion
	}
}