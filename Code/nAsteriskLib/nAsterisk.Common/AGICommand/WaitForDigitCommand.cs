using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk.AGICommand
{
	internal class WaitForDigitCommand : BaseAGICommand
	{
		private TimeSpan _timeout;
		private string _digit = "";

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

		public override bool IsSuccessfulResult(string result)
		{
			if (result == "0")
				return true;
			else if (result == "-1")
				return false;
			else
			{
				byte b = byte.Parse(result);
				_digit = System.Text.ASCIIEncoding.ASCII.GetString(new byte[] { b });

				return true;
			}
		}

		public string GetResponse()
		{
			return _digit;
		}
	}
}
