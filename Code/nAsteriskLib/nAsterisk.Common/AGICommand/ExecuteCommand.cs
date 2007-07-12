using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk.AGICommand
{
	public class ExecuteCommand : BaseAGICommand
	{
		private string _application;
		private string _options;

		public ExecuteCommand(string application, string options)
		{
			_application = application;
			_options = options;
		}

		public string Options
		{
			get { return _options; }
			set { _options = value; }
		}

		public string Application
		{
			get { return _application; }
			set { _application = value; }
		}

		public override string GetCommand()
		{
			return string.Format("EXEC {0} {1}", _application, _options);
		}

		public override bool IsSuccessfulResult(string result)
		{
			int code;
			int.TryParse(result, out code);

			return code != -2;
		}
	}
}
