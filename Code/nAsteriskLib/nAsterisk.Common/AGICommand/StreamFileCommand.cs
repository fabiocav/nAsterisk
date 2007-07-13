using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk.AGICommand
{
	internal class StreamFileCommand : BaseAGICommand
	{
		private string _filename = "";
		private string _escapedigits = "\"\"";
		private int _offset = -1;
		private Digits _digit;

		public StreamFileCommand(string filename, string escapedigits, int offset) : this(filename, offset)
		{
			_escapedigits = escapedigits;
		}

		public StreamFileCommand(string filename, string escapedigits) : this(filename)
		{
			_escapedigits = escapedigits;
		}

		public StreamFileCommand(string filename, int offset) : this(filename)
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

		public override bool IsSuccessfulResult(string result)
		{
			if (result == "0")
				return true;
			else if (result == "-1")
				return false;
			else
			{
				char cdigit = (char)int.Parse(result);
				_digit = AsteriskAgi.GetDigitsFromString(new string(cdigit, 1));

				return true;
			}
		}

		public StreamFileResult GetResult()
		{
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
	
		public int EndingOffset
		{
			get { return _offset;}
		}
	}
}
