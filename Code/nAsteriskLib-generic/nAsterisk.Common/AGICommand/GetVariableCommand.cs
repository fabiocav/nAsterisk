using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk.AGICommand
{
	public class GetVariableCommand : AGIReturnCommandBase<string>
	{
		private string _variableName;
		private string _variableValue;

		public GetVariableCommand(string variableName)
		{
			_variableName = variableName;
		}

		public string VariableName
		{
			get { return _variableName; }
			set { _variableName = value; }
		}

		public override string GetCommand()
		{
			if (string.IsNullOrEmpty(_variableName))
			{
				throw new InvalidOperationException("The GetVariableCommand requires VariableName to be set.");
			}

			return string.Format("GET VARIABLE {0}", _variableName);
		}

		public override string ProcessResponse(FastAGIResponse response)
		{
			if (response.ResultValue == "1")
				_variableValue = response.Payload;

            return _variableName;
		}
	}
}
