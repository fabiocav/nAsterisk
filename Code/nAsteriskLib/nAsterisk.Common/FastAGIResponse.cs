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
using System.Text.RegularExpressions;

namespace nAsterisk
{
	public class FastAGIResponse
	{
		private int _responseCode;
		private int? _endPosition;
		private string _resultValue;
		private string _payload;
		private string _rawResponse;

		private FastAGIResponse()
		{ }

		public string Payload
		{
			get { return _payload; }
			set { _payload = value; }
		}

		public string ResultValue
		{
			get { return _resultValue; }
			set { _resultValue = value; }
		}

		public int ResponseCode
		{
			get { return _responseCode; }
			set { _responseCode = value; }
		}

		public int? EndPosition
		{
			get { return _endPosition; }
			set { _endPosition = value; }
		}

		public string RawResponse
		{
			get { return _rawResponse; }
			set { _rawResponse = value; }
		}

		public override string ToString()
		{
			return _rawResponse;
		}

		public static FastAGIResponse ParseResponse(string responseString)
		{
			Regex responseRegex = new Regex(@"^(?<ResponseCode>\d{3})\sresult=(?<ResultCode>[^\s$]*)(?:\s\((?<ResultPayload>[^)]*)\)){0,1}(?:\sendpos=(?<EndPosition>\d*)){0,1}$", RegexOptions.Singleline);
			Match responseMatch = responseRegex.Match(responseString);

			if (responseMatch.Success)
			{
				FastAGIResponse fastAGIResponse = new FastAGIResponse();

				fastAGIResponse._rawResponse = responseString;
				fastAGIResponse._responseCode = int.Parse(responseMatch.Groups["ResponseCode"].Value);
				fastAGIResponse._resultValue = responseMatch.Groups["ResultCode"].Value;

				if (responseMatch.Groups["ResultPayload"].Success)
					fastAGIResponse._payload = responseMatch.Groups["ResultPayload"].Value;

				if (responseMatch.Groups["EndPosition"].Success)
					fastAGIResponse._endPosition = int.Parse(responseMatch.Groups["EndPosition"].Value);

				return fastAGIResponse;
			}

			throw new ArgumentException("The response string argument was invalid.");
		}
	}
}
