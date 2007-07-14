using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk.AGICommand
{
	public class SayPhoneticCommand : AGICommandBase, IProvideCommandResult
	{
		private string _message;

		private Digits _pressedDigit;
		private Digits _escapeDigits;

		public SayPhoneticCommand(string message, Digits escapeDigits)
		{
			_message = message;
			_escapeDigits = escapeDigits;
		}

		public string Message
		{
			get { return _message; }
			set { _message = value; }
		}

		public Digits EscapeDigits
		{
			get { return _escapeDigits; }
			set { _escapeDigits = value; }
		}

		public override string GetCommand()
		{
			return string.Format("SAY PHONETIC \"{0}\" \"{1}\"", _message, AsteriskAGI.GetDigitsString(_escapeDigits));
		}

		public override void ProcessResponse(FastAGIResponse response)
		{
			if (response.ResultValue == "-1")
				throw new AsteriskException("SayPhonetic Command Failed.");

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