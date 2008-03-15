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

	public class RecordFileCommand : AGIReturnCommandBase<Char?>
	{
		private string _fileName;
		private string _format;
		private Digits _escapeDigits;
		private int _timeout = -1;
		private int? _offsetSample;
		private int? _silence;
		private bool _beep;

		private Char? _dtmfDigit;
		private string _validChars = "0123456789#*";

		private const string CommonExceptionMessage = "RecordFile Command Failed.";

		public RecordFileCommand(string fileName, string format, Digits escapeDigits, int timeout)
			: this(fileName, format, escapeDigits, timeout, null, false, null) { }

		public RecordFileCommand(string fileName, string format, Digits escapeDigits, int? silence, int timeout)
			: this(fileName, format, escapeDigits, timeout, null, false, silence) { }

		public RecordFileCommand(string fileName, string format, Digits escapeDigits, int timeout, bool beep)
			: this(fileName, format, escapeDigits, timeout, null, beep, null) { }

		public RecordFileCommand(string fileName, string format, Digits escapeDigits, int timeout, bool beep, int? silence)
			: this(fileName, format, escapeDigits, timeout, null, beep, silence) { }


		public RecordFileCommand(string fileName, string format, Digits escapeDigits, int timeout, int? offsetSamples)
			: this(fileName, format, escapeDigits,timeout, offsetSamples, false, null) { }
		
		public RecordFileCommand(string fileName, string format, Digits escapeDigits, int timeout, int? offsetSamples, int? silence)
			: this(fileName, format, escapeDigits, timeout, offsetSamples, false, silence) { }

		public RecordFileCommand(string fileName, string format, Digits escapeDigits, int timeout, int? offsetSamples, bool beep)
			: this(fileName, format, escapeDigits, timeout, offsetSamples, beep, null) { }

		public RecordFileCommand(string fileName, string format, Digits escapeDigits, int timeout, int? offsetSamples, bool beep, int? silence)
		{
			_fileName = fileName;
			_format = format;
			_escapeDigits = escapeDigits;
			_timeout = timeout;
			_offsetSample = offsetSamples;
			_beep = beep;
			_silence = silence;
		}

		public bool Beep
		{
			get { return _beep; }
			set { _beep = value; }
		}

		public Digits EscapeDigits
		{
			get { return _escapeDigits; }
			set { _escapeDigits = value; }
		}

		public int Timeout
		{
			get { return _timeout; }
			set { _timeout = value; }
		}

		public int? OffsetSample
		{
			get { return _offsetSample; }
			set { _offsetSample = value; }
		}

		public int? Silence
		{
			get { return _silence; }
			set { _silence = value; }
		}

		public string Format
		{
			get { return _format; }
			set { _format = value; }
		}

		public string FileName
		{
			get { return _fileName; }
			set { _fileName = value; }
		}

		public override string GetCommand()
		{
			string silenceString = _silence != null ? string.Format(" s={0}", _silence) : string.Empty;
			string beepString = _beep ? " beep" : string.Empty;
			string offsetSampleString = _offsetSample != null ? string.Format(" {0}", _offsetSample) : string.Empty;

			return string.Format("RECORD FILE {0} {1} {2} {3}{4}{5}{6}", _fileName, _format, AsteriskAGI.GetDigitsString(_escapeDigits),
				_timeout, offsetSampleString, beepString, silenceString);
		}

		private bool isValidDtmfString(string _number)
		{
			foreach (char c in _number)
			{
				if (!_validChars.Contains(c.ToString()))
					return false;
			}

			return true;
		}

		public override Char? ProcessResponse(FastAGIResponse response)
		{
			if (response.ResultValue == "-1")
			{
				

				if (response.Payload == "waitfor")
				{
					throw new AGICommandException(string.Format("{0} Waitfor. EndPos={1}", CommonExceptionMessage, response.EndPosition));
				}

				if (response.Payload == "writefile")
				{
					throw new AGICommandException(string.Format("{0} Failure to write file.", CommonExceptionMessage));
				}
			}
			else if (response.ResultValue == "0")
			{
				if (response.Payload == "hangup")
				{
					throw new HangUpException(string.Format("{0} EndPos={1}", CommonExceptionMessage, response.EndPosition));
				}

				if (response.Payload == "timeout")
				{
					throw new AGICommandException(string.Format("{0} Timeout. EndPos={1}", CommonExceptionMessage, response.EndPosition));
				}
			}
			else if (response.Payload == "randomerror")
			{
				throw new AGICommandException(string.Format("{0} Random Error. EndPos={1}, Error={2}", CommonExceptionMessage, response.EndPosition, 
						response.ResultValue));
			}

			if (response.Payload == "dtmf")
			{
				_dtmfDigit = (Char)int.Parse(response.ResultValue);
			}

            return _dtmfDigit;
		}
	}
}
