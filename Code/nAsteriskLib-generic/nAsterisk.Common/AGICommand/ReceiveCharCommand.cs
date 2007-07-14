using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk.AGICommand
{
	public class ReceiveCharCommand : AGIReturnCommandBase<Char?>
	{
		private int _timeout;
		private Char? _character = null;

		public ReceiveCharCommand()
		{}

		public ReceiveCharCommand(int timeout)
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
			string command = "RECEIVE CHAR";

			if (_timeout > 0)
				command = string.Format("{0} {1}", command, _timeout);

			return command;
		}

		public override Char? ProcessResponse(FastAGIResponse response)
		{
			if (response.ResultValue == "-1")
				throw new AsteriskException("ReceiveChar Command Failed.");

			if (response.ResultValue != "0")
				_character = (Char)int.Parse(response.ResultValue);

            return _character;
		}
	}
}
