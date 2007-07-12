using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

using nAsterisk;
using nAsterisk.Configuration;
using nAsterisk.Scripts;

namespace CliAgiHost
{
	class Program
	{
		static void Main(string[] args)
		{
			Dictionary<string, Type> mappings = new Dictionary<string, Type>();
			mappings.Add("/blahblah", typeof(EchoCallerIdScript));

			ITcpHostConfigurationSource config = new ProgramaticTcpHostConfigurationSource(mappings);
			TcpAgiScriptHost host = new TcpAgiScriptHost();
			host.Configure(config);
			host.Start();
			
			Console.ReadLine();

			host.Stop();
		}
	}
}
