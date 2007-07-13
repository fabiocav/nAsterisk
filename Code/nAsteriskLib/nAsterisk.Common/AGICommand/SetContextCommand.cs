using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk.AGICommand
{
	internal class SetContextCommand : BaseAGICommand
	{
		private string _context;

		public SetContextCommand(string context)
		{
			_context = context;
		}

		public string Context
		{
			get { return _context; }
			set { _context = value; }
		}
	
		public override string GetCommand()
		{
			return string.Format("SET CONTEXT {0}", _context);
		}

		public override bool IsSuccessfulResult(string result)
		{
			return result == "0";
		}
	}
}
