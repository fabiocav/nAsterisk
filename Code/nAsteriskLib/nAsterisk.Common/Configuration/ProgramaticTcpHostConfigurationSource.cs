using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk.Configuration
{
	public class ProgramaticTcpHostConfigurationSource : ITcpHostConfigurationSource
	{
		Dictionary<string, Type> _uriMappings;

		public ProgramaticTcpHostConfigurationSource(Dictionary<string, Type> uriMappings)
		{
			_uriMappings = uriMappings;
		}

		#region IConfigurationSource Members

		public Dictionary<string, Type> GetUriMappings()
		{
			return new Dictionary<string, Type>(_uriMappings);
		}

		#endregion
	}
}
