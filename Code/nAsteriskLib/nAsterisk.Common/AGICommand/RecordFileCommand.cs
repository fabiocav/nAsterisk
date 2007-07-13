using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk.AGICommand
{

	public class RecordFileCommand : BaseAGICommand, ISupportCommandResponse, IProvideCommandResult
	{
		private string _fileName;
		private string _format;
		private Digits _escapeDigits;
		private int _timeout = -1;
		private int? _offsetSample;
		private int? _silence;
		private bool _beep;

		private string _dtmfDigits;
		private string _validChars = "0123456789#*";

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

			return string.Format("RECORD FILE {0} {1} {2} {3}{4}{5}{6}", _fileName, _format, AsteriskAgi.GetDigitsString(_escapeDigits),
				_timeout, offsetSampleString, beepString, silenceString);
		}

		public override bool IsSuccessfulResult(string result)
		{
			return result != "-1";
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

		#region ISupportCommandResponse Members

		public void ProcessResponse(FastAGIResponse response)
		{
			if (response.Payload == "randomerror")
			{
				throw new AsteriskException(string.Format("Random Error. EndPos={0}, Error={1}", response.EndPosition, response.ResultValue));
			}
			
			if (response.Payload == "timeout")
			{
				throw new AsteriskException(string.Format("Timeout. EndPos={0}", response.EndPosition));
			}
			
			if (response.Payload == "hangup")
			{
				throw new HangUpException(string.Format("EndPos={0}", response.EndPosition));
			}
			
			if (response.Payload == "waitfor")
			{
				throw new AsteriskException(string.Format("Waitfor. EndPos={0}", response.EndPosition));
			}
			
			if (response.Payload == "writefile")
			{
				throw new AsteriskException(string.Format("Failure to write file."));
			}

			if (response.Payload == "dtmf")
			{
				_dtmfDigits = response.ResultValue;
			}
		}

		#endregion

		public string GetResult()
		{
			return _dtmfDigits;
		}

		#region IProvideCommandResult Members

		object IProvideCommandResult.GetResult()
		{
			return this.GetResult();
		}

		#endregion
	}
}
