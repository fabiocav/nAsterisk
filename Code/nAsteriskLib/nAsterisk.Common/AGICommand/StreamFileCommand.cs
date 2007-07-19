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
    internal class StreamFileCommand : AGIReturnCommandBase<StreamFileResult>
    {
        private string _filename = "";
        private string _escapedigits = "\"\"";
        private int _offset = -1;
        private Digits _digit;

        public StreamFileCommand(string filename, string escapedigits, int offset)
            : this(filename, offset)
        {
            _escapedigits = escapedigits;
        }

        public StreamFileCommand(string filename, string escapedigits)
            : this(filename)
        {
            _escapedigits = escapedigits;
        }

        public StreamFileCommand(string filename, int offset)
            : this(filename)
        {
            _offset = offset;
        }

        public StreamFileCommand(string filename)
        {
            _filename = filename;
        }

        public int Offset
        {
            get { return _offset; }
            set { _offset = value; }
        }

        public string EscapeDigits
        {
            get { return _escapedigits = null; }
            set { _escapedigits = value; }
        }

        public string FileName
        {
            get { return _filename; }
            set { _filename = value; }
        }

        public Digits Digit
        {
            get { return _digit; }
        }

        public override string GetCommand()
        {
            string cmd = string.Format("STREAM FILE {0} {1}", _filename, _escapedigits);
            if (_offset >= 0)
                cmd += " " + _offset.ToString();

            return cmd;
        }

        public override StreamFileResult ProcessResponse(FastAGIResponse response)
        {
            if (response.ResultValue == "-1")
				throw new AsteriskCommandException("StreamFile Command Failed.");

            if (response.EndPosition != null)
                _offset = response.EndPosition.Value;

            if (response.ResultValue != "0")
                _digit = AsteriskAGI.GetDigitsFromString(((Char)int.Parse(response.ResultValue)).ToString());

            return new StreamFileResult(_digit, _offset);
        }
    }

    public class StreamFileResult
    {
        private int _offset;
        private Digits _digit;

        public StreamFileResult(Digits digit, int offset)
        {
            _digit = digit;
            _offset = offset;
        }

        public Digits PressedDigit
        {
            get { return _digit; }
        }

        public int EndPosition
        {
            get { return _offset; }
        }
    }
}
