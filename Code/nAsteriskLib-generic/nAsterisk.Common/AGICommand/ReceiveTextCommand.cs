using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk.AGICommand
{
	internal class ReceiveTextCommand : AGICommandBase, IProvideCommandResult
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

		public override void ProcessResponse(FastAGIResponse response)
		{
			if (response.ResultValue == "-1")
				throw new AsteriskException("ReceiveText Command Failed.");

			_text = response.ResultValue;
		}

		public string GetResult()
		{
			return _text;
		}

		#region IProvideCommandResult Members

		object IProvideCommandResult.GetResult()
		{
			return this.GetResult();
		}

		#endregion
	}
}
