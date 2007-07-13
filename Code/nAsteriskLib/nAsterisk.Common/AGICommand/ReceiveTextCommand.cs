using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk.AGICommand
{
	internal class ReceiveTextCommand : BaseAGICommand
	{
		private int _timeout;
		private string _text;

		public ReceiveTextCommand(int timeout)
		{
			_timeout = timeout;
		}

		public string Text
		{
			get { return _text; }
			set { _text = value; }
		}
	
		public int Timeout
		{
			get { return _timeout; }
			set { _timeout = value; }
		}
	
		public override string GetCommand()
		{
			return string.Format("RECEIVE TEXT {0}", _timeout);
		}

		public override bool IsSuccessfulResult(string result)
		{
			if (result == "-1")
				return false;
			else
				_text = result;

			return true;
		}

		public string GetResult()
		{
			return _text;
		}
	}
}
