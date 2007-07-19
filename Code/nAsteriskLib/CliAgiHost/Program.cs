/**************************************************************************
// nAsterisk - .NET Asterisk Library 
//
// Copyright (c) 2007 by:
// Fabio Cavalcante (fabio(a)codesapien.com)
// Josh Perry (josh(a)6bit.com)
// Justin Long (dukk(a)dukk.org)
//
// Asterisk - Copyright (C) 1999 - 2006, Digium, Inc.
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU Library General Public License as published 
// by  the Free Software Foundation; either version 2 of the License or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful, but
// WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Library General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this package (See COPYING.LIB); if not, write to the Free Software
// Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA 02111-1307, USA.
/**************************************************************************/

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
