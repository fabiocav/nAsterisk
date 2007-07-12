using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk.AGICommand
{
	internal class SetVariableCommand : BaseAGICommand
	{
		private string _name;
		private string _value;
	
		public SetVariableCommand(string name, string value)
		{
			_name = name;
			_value = value;
		}

		public string Value
		{
			get { return _value; }
			set { _value = value; }
		}

		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}

		public override string GetCommand()
		{
			return string.Format("SET VARIABLE {0} {1}", _name, _value);
		}

		public override bool IsSuccessfulResult(string result)
		{
			return result == "1";
		}
	}
}
