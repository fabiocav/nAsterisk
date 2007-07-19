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

namespace nAsterisk.AGICommand
{
	public class ReceiveCharCommand : AGIReturnCommandBase<Char?>
	{
		private int _timeout;
		private Char? _character = null;

		public ReceiveCharCommand()
		{}

		public ReceiveCharCommand(int timeout)
		{
			_timeout = timeout;
		}

		public int Timeout
		{
			get { return _timeout; }
			set { _timeout = value; }
		}

		public override string GetCommand()
		{
			string command = "RECEIVE CHAR";

			if (_timeout > 0)
				command = string.Format("{0} {1}", command, _timeout);

			return command;
		}

		public override Char? ProcessResponse(FastAGIResponse response)
		{
			if (response.ResultValue == "-1")
				throw new AsteriskCommandException("ReceiveChar Command Failed.");

			if (response.ResultValue != "0")
				_character = (Char)int.Parse(response.ResultValue);

            return _character;
		}
	}
}
