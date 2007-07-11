using System;
using System.Collections.Generic;
using System.Text;
using nAsterisk;

namespace CliAgiHost
{
	class Program
	{
		static void Main(string[] args)
		{
			IAgiScriptHost host = new TcpAgiScriptHost();
			host.Start();
			
			Console.ReadLine();

			host.Stop();
		}
	}
}
