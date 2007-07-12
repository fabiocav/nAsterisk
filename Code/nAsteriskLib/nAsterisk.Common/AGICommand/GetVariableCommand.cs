using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk.AGICommand
{
	public class GetVariableCommand : BaseAGICommand, ISupportCommandResponse, IProvideCommandResult
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

		public override bool IsSuccessfulResult(string result)
		{
			int code = 0;
			int.TryParse(result, out code);

			return code == 1;
		}

		public string GetResult()
		{
			return _variableValue;
		}

		#region ISupportCommandResponse Members

		void ISupportCommandResponse.ProcessResponse(string response)
		{
			_variableValue = response;
		}

		#endregion

		#region IProviteCommandResult Members

		object IProvideCommandResult.GetResult()
		{
			return this.GetResult();
		}

		#endregion
	}
}
