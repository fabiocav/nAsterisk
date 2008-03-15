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
	public class SayDateTimeCommand : AGIReturnCommandBase<Digits>
	{
		private string _format;
		private string _timeZone;

		private Digits _pressedDigit;
		private Digits _escapeDigits;
		private DateTime _time;

		public SayDateTimeCommand(DateTime time, Digits escapeDigits)
			: this(time, escapeDigits, null, null) { }

		public SayDateTimeCommand(DateTime time, Digits escapeDigits, string format, string timeZone)
		{
			_time = time;
			_escapeDigits = escapeDigits;
			_format = format;
			_timeZone = timeZone;
		}

		/// <summary>Gets or sets the format.</summary>
		/// <value>The format.</value>
		/// <remarks>
		/// 	<list type="table">
		/// 		<listheader>
		/// 			<term>Format specifier</term>
		/// 			<description>Description</description>
		/// 		</listheader>
		/// 		<item>
		/// 			<term>A or a</term>
		/// 			<description>Day of week (Saturday, Sunday, ...)</description>
		/// 		</item>
		/// 		<item>
		/// 			<term>B or b or h</term>
		/// 			<description>Month name (January, February, ...)</description>
		/// 		</item>
		/// 		<item>
		/// 			<term>d or e</term>
		/// 			<description>numeric day of month (first, second, ..., thirty-first)</description>
		/// 		</item>
		/// 		<item>
		/// 			<term>Y</term>
		/// 			<description>Year</description>
		/// 		</item>
		/// 		<item>
		/// 			<term>I or l</term>
		/// 			<description>Hour, 12 hour clock </description>
		/// 		</item>
		/// 		<item>
		/// 			<term>H</term>
		/// 			<description>Hour, 24 hour clock (single digit hours preceded by "oh")</description>
		/// 		</item>
		/// 		<item>
		/// 			<term>k</term>
		/// 			<description>Hour, 24 hour clock (single digit hours NOT preceded by "oh")</description>
		/// 		</item>
		/// 		<item>
		/// 			<term>M</term>
		/// 			<description>Minute, with 00 pronounced as "o'clock"</description>
		/// 		</item>
		/// 		<item>
		/// 			<term>N</term>
		/// 			<description>Minute, with 00 pronounced as "hundred" (US military time)</description>
		/// 		</item>
		/// 		<item>
		/// 			<term>P or p</term>
		/// 			<description>AM or PM </description>
		/// 		</item>
		/// 		<item>
		/// 			<term>Q</term>
		/// 			<description>"today", "yesterday" or ABdY (*note: not standard strftime value)</description>
		/// 		</item>
		/// 		<item>
		/// 			<term>q</term>
		/// 			<description>"" (for today), "yesterday", weekday, or ABdY (*note: not standard strftime value)</description>
		/// 		</item>
		/// 		<item>
		/// 			<term>R</term>
		/// 			<description>24 hour time, including minute</description>
		/// 		</item>
		/// 	</list>
		/// </remarks>
		public string Format
		{
			get { return _format; }
			set { _format = value; }
		}

		/// <summary>Gets or sets the escape digits.</summary>
		/// <value>The escape digits.</value>
		public Digits EscapeDigits
		{
			get { return _escapeDigits; }
			set { _escapeDigits = value; }
		}

		/// <summary>Gets or sets the time.</summary>
		/// <value>The time.</value>
		public DateTime Time
		{
			get { return _time; }
			set { _time = value; }
		}

		/// <summary>Gets or sets the time zone.</summary>
		/// <value>The time zone.</value>
		/// <remarks>For valid timezone's look in your Asterisk server's "/usr/share/zoneinfo" directory.</remarks>
		public string TimeZone
		{
			get { return _timeZone; }
			set { _timeZone = value; }
		}

		public override string GetCommand()
		{
			string commandFormat = "SAY DATETIME {0} {1} {2} {3}";

			if (string.IsNullOrEmpty(_format) && string.IsNullOrEmpty(_timeZone))
				commandFormat = "SAY DATETIME {0} {1}";
			else if (string.IsNullOrEmpty(_format) || string.IsNullOrEmpty(_timeZone))
				commandFormat = "SAY DATETIME {0} {1} {3}"; // TODO: This is going to need testing I'm not 100% sure asterisk will be able to tell if {3} is a timezone or a format.

			return string.Format(commandFormat, (_time - (new DateTime(1970,1,1,0,0,0))).TotalSeconds, AsteriskAGI.GetDigitsString(_escapeDigits), _format, _timeZone);
		}

		public override Digits ProcessResponse(FastAGIResponse response)
		{
			if (response.ResultValue == "-1")
				throw new AGICommandException("SayDateTime Command Failed.");

			if (response.ResultValue != "0")
				_pressedDigit = AsteriskAGI.GetDigitsFromString(((Char)int.Parse(response.ResultValue)).ToString());

            return _pressedDigit;
		}
	}
}
