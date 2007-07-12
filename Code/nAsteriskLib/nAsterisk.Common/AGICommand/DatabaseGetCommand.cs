using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk.AGICommand
{
	public class DatabaseGetCommand : BaseAGICommand
	{
		private string _family;
		private string _key;

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

		public override bool IsSuccessfulResult(int result)
		{
			return result == 1;
		}
	}
}
