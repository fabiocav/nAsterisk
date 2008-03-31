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

namespace nAsterisk.AGI.Command
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

            return _variableValue;
		}
	}
}
