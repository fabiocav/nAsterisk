using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk.AGICommand
{
	public class ReceiveCharCommand : BaseAGICommand, IProviteCommandResult
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

		public override bool IsSuccessfulResult(string result)
		{
			int code = -1;
			int.TryParse(result, out code);

			if (code > 0)
				_character = (Char)code;

			return code != -1;
		}

		public Char? GetResult()
		{
			return _character;
		}

		#region IProviteCommandResult Members

		object IProviteCommandResult.GetResult()
		{
			return this.GetResult();
		}

		#endregion
	}
}
