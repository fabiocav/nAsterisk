using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk.AGICommand
{
	public class DatabaseGetCommand : AGICommandBase, IProvideCommandResult
	{
		private string _family;
		private string _key;
		private string _resultingValue;

		public DatabaseGetCommand(string family, string key)
		{
			_family = family;
			_key = key;
		}

		public string Key
		{
			get { return _key; }
			set { _key = value; }
		}

		public string Family
		{
			get { return _family; }
			set { _family = value; }
		}


		public override string GetCommand()
		{
			if (string.IsNullOrEmpty(_family) || string.IsNullOrEmpty(_key))
			{
				throw new InvalidOperationException("The DatabaseGetCommand requires Family AND Key to be set.");
			}

			return string.Format("DATABASE GET {0} {1}", _family, _key);
		}

		public string GetResult()
		{
			return _resultingValue;
		}

		public override void ProcessResponse(FastAGIResponse response)
		{
			if (response.ResultValue == "0")
				throw new AsteriskException("DatabaseGet Command Failed.");

			_resultingValue = response.Payload;
		}

		#region IProviteCommandResult Members

		object IProvideCommandResult.GetResult()
		{
			return this.GetResult();
		}

		#endregion
	}
}
