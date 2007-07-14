using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

using nAsterisk;
using nAsterisk.Configuration;
using nAsterisk.Scripts;

namespace CliAGIHost
{
	class Program
	{
		static void Main(string[] args)
		{
			Dictionary<string, Type> mappings = new Dictionary<string, Type>();
			mappings.Add("/blahblah", typeof(ExecuteAllMethodsScript));

			ITcpHostConfigurationSource config = new ProgramaticTcpHostConfigurationSource(mappings);
			TcpAGIScriptHost host = new TcpAGIScriptHost();
			host.Configure(config);
			host.Start();
			
			Console.ReadLine();

			host.Stop();
		}
	}
}
