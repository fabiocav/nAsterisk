using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk.Configuration
{
	public interface ITcpHostConfigurationSource
	{
		Dictionary<string, Type> GetUriMappings();
	}
}
