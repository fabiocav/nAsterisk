using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk.AGICommand
{
	public class DatabasePutCommand : BaseAGICommand
	{
		private string _family;
		private string _key;
		private string _value;

		public DatabasePutCommand(string family, string keyTree, string value)
		{
			_family = family;
			_key = keyTree;
			_value = value;
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

		public string Value
		{
			get { return _value; }
			set { _value = value; }
		}

		public override string GetCommand()
		{
			if (string.IsNullOrEmpty(_family) || string.IsNullOrEmpty(_key) || string.IsNullOrEmpty(_value))
			{
				throw new InvalidOperationException("The DatabasePutCommand requires Family, Key and Value to be set.");
			}

			return string.Format("DATABASE PUT {0} {1} {2}", _family, _key, _value);
		}

		public override bool IsSuccessfulResult(string result)
		{
			int code = 0;
			int.TryParse(result, out code);

			return code == 1;
		}
	}
}
