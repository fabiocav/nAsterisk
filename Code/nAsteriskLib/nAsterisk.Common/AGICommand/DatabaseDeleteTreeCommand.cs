using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk.AGICommand
{
	public class DatabaseDeleteTreeCommand : BaseAGICommand
	{
		private string _family;
		private string _keyTree;

		public DatabaseDeleteTreeCommand(string family)
			: this(family, null) { }

		public DatabaseDeleteTreeCommand(string family, string keyTree)
		{
			_family = family;
			_keyTree = keyTree;
		}

		public string KeyTree
		{
			get { return _keyTree; }
			set { _keyTree = value; }
		}

		public string Family
		{
			get { return _family; }
			set { _family = value; }
		}

		public override string GetCommand()
		{
			if (string.IsNullOrEmpty(_family))
			{
				throw new InvalidOperationException("The DatabaseDeleteTreeCommand requires Family to be set.");
			}

			string command = string.Format("DATABASE DELTREE {0}", _family); 

			if (!string.IsNullOrEmpty(_keyTree))
				command = string.Format("{0} {1}", command, _keyTree);
			
			return command;
		}

		public override bool IsSuccessfulResult(string result)
		{
			int code = 0;
			int.TryParse(result, out code);

			return code == 1;
		}
	}
}
