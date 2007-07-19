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
	internal class WaitForDigitCommand : AGIReturnCommandBase<Digits>
	{
		private int _timeout;
		private Digits _digit;

		public WaitForDigitCommand(int timeout)
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
			return string.Format("WAIT FOR DIGIT {0}", _timeout);
		}

		public override Digits ProcessResponse(FastAGIResponse response)
		{
			if (response.ResultValue == "-1")
				throw new AsteriskCommandException("WaitForDigit Command Failed.");

			if (response.ResultValue != "0")
			{
				int digitCode;
				if (int.TryParse(response.ResultValue, out digitCode))
				{
					_digit = AsteriskAGI.GetDigitsFromString(((Char)digitCode).ToString());
				}
			}

            return _digit;
		}
	}
}
