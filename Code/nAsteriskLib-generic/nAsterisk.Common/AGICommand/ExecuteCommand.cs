using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk.AGICommand
{
	public class ExecuteCommand : AGICommandBase, IProvideCommandResult
	{
		private string _application;
		private string _options;
		private string _applicationReturnValue;

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

		public override void ProcessResponse(FastAGIResponse response)
		{
			_applicationReturnValue = response.ResultValue;

			if (response.ResultValue == "-2")
				throw new AsteriskException("Execute Command Failed.");
		}

		public string GetResult()
		{
			return _applicationReturnValue;
		}

		#region IProviteCommandResult Members

		object IProvideCommandResult.GetResult()
		{
			return this.GetResult();
		}

		#endregion
	}
}
