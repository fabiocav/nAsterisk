using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk.AGICommand
{
	public class GetDataCommandResult
	{
		private bool _timeout;
		private string _resultingDtmfData;

		public string ResultingDtmfData
		{
			get { return _resultingDtmfData; }
			set { _resultingDtmfData = value; }
		}

		public bool Timeout
		{
			get { return _timeout; }
			set { _timeout = value; }
		}
	}
}
