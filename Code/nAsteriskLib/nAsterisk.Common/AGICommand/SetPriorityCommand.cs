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
	internal class SetPriorityCommand : AGINoReturnCommandBase
	{
		private int _priority;

		public SetPriorityCommand(int priority)
		{
			_priority = priority;
		}

		public int Priority
		{
			get { return _priority; }
			set { _priority = value; }
		}

		public override string GetCommand()
		{
			return string.Format("SET PRIORITY {0}", _priority);
		}

		public override void ProcessResponse(FastAGIResponse response)
		{
			if (response.ResultValue != "0")
				throw new AsteriskCommandException("SetPriority Command Failed.");
		}
	}
}
