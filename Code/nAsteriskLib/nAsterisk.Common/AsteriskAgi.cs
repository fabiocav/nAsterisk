using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using nAsterisk.AGICommand;

namespace nAsterisk
{
	public class AsteriskAgi
	{
		#region member vars
		private Stream _stream;
		private StreamWriter _writer;
		private StreamReader _reader;

		Dictionary<string, string> _variables = new Dictionary<string,string>();
		#endregion

		#region constructor
		public AsteriskAgi(Stream stream)
		{
			_stream = stream;
			_writer = new StreamWriter(_stream, System.Text.ASCIIEncoding.ASCII);
			_reader = new StreamReader(_stream, System.Text.ASCIIEncoding.ASCII);
		}
		#endregion

		public void Init()
		{
			this.ParseVariables();
		}

		private void ParseVariables()
		{
			// Read one name value pair per line until we get a blank line
			try
			{
				while (true)
				{
					string line = _reader.ReadLine();
					if (line != "")
					{
						string[] var = line.Split(new string[] { ": " }, 2, StringSplitOptions.None);
						_variables.Add(var[0].ToLower(), var[1]);
						Console.WriteLine("Got agi variable {0}:{1}", var[0], var[1]);
					}
					else
					{
						Console.WriteLine("Done reading agi variables");
						break;
					}
				}
			}
			catch (Exception) { }
		}

		#region AgiProperties
		public bool IsNetworkScript
		{
			get
			{
				if(_variables.ContainsKey("agi_network"))
				{
					return _variables["agi_network"] == "yes" ? true : false;
				}
				else
					return false;
			}
		}

		public string NetworkScriptPath
		{
			get { return _variables.ContainsKey("agi_network_script")?_variables["agi_network_script"]:""; }
		}

		public string Request
		{
			get { return _variables["agi_request"]; }
		}

		public string ChannelName
		{
			get { return _variables["agi_channel"]; }
		}

		public string Language
		{
			get { return _variables["agi_language"]; }
		}

		public string ChannelType
		{
			get { return _variables["agi_type"]; }
		}

		public string CallerId
		{
			get { return _variables["agi_callerid"]; }
		}

		public string CallerIdName
		{
			get { return _variables["agi_calleridname"]; }
		}

		//TODO: What is this?
		public string CallingPres
		{
			get { return _variables["agi_callingpres"]; }
		}

		public string ANI
		{
			get { return _variables["agi_callingani2"]; }
		}

		//TODO: What is this?
		public string CallingTON
		{
			get { return _variables["agi_callington"]; }
		}

		//TODO: What is this?
		public string CallingTNS
		{
			get { return _variables["agi_callingtns"]; }
		}

		public string CallId
		{
			get { return _variables["agi_uniqueid"]; }
		}

		public string DNID
		{
			get { return _variables["agi_dnid"]; }
		}

		public string RDNIS
		{
			get { return _variables["agi_rdnis"]; }
		}

		public string Context
		{
			get { return _variables["agi_context"]; }
		}

		public string Extension
		{
			get { return _variables["agi_extension"]; }
		}

		public string Priority
		{
			get { return _variables["agi_priority"]; }
		}

		public string Enhanced
		{
			get { return _variables["agi_enhanced"]; }
		}

		public string AccountCode
		{
			get { return _variables["agi_accountcode"]; }
		}
		#endregion

		#region AgiMethods
		public void Answer()
		{
			AnswerCommand command = new AnswerCommand();

			processCommand(command);
		}

		public ChannelStatus GetChannelStatus()
		{
			GetChannelStatusCommand command = new GetChannelStatusCommand();
			
			processCommand(command);

			return command.GetResult();
		}

		public ChannelStatus GetChannelStatus(string channelName)
		{
			GetChannelStatusCommand command = new GetChannelStatusCommand(channelName);

			processCommand(command);

			return command.GetResult();
		}

		public void DatabaseDelete(string family, string key)
		{
			DatabaseDeleteCommand command = new DatabaseDeleteCommand(family, key);

			processCommand(command);
		}

		public void DatabaseDeleteTree(string family)
		{
			DatabaseDeleteTreeCommand command = new DatabaseDeleteTreeCommand(family);

			processCommand(command);
		}

		public void DatabaseDeleteTree(string family, string keeTree)
		{
			DatabaseDeleteTreeCommand command = new DatabaseDeleteTreeCommand(family, keeTree);

			processCommand(command);
		}

		public string DatabaseGet(string family, string key)
		{
			DatabaseGetCommand command = new DatabaseGetCommand(family, key);

			processCommand(command);

			return command.GetResult();
		}

		public void DatabasePut(string family, string keyTree, string value)
		{
			DatabasePutCommand command = new DatabasePutCommand(family, keyTree, value);

			processCommand(command);
		}

		public string Execute(string application, string options)
		{
			ExecuteCommand command = new ExecuteCommand(application, options);

			processCommand(command);

			return command.GetResult();
		}

		public string GetData(string fileToStream)
		{
			GetDataCommand command = new GetDataCommand(fileToStream);

			processCommand(command);

			return command.GetResult();
		}

		public string GetData(string fileToStream, int timeout)
		{
			GetDataCommand command = new GetDataCommand(fileToStream, timeout);

			processCommand(command);

			return command.GetResult();
		}

		public string GetData(string fileToStream, int timeout, int maxDigits)
		{
			GetDataCommand command = new GetDataCommand(fileToStream, timeout, maxDigits);

			processCommand(command);

			return command.GetResult();
		}

		public string GetVariable(string variableName)
		{
			GetVariableCommand command = new GetVariableCommand(variableName);

			processCommand(command);

			return command.GetResult();
		}

		public void HangUp()
		{
			HangUpCommand command = new HangUpCommand();

			processCommand(command);
		}

		public void HangUp(string channelName)
		{
			HangUpCommand command = new HangUpCommand(channelName);

			processCommand(command);
		}

		public void Noop()
		{
			NoopCommand command = new NoopCommand();

			processCommand(command);
		}

		public Char? ReceiveChar()
		{
			ReceiveCharCommand command = new ReceiveCharCommand();

			processCommand(command);

			return command.GetResult();
		}

		public Char? ReceiveChar(int timeout)
		{
			ReceiveCharCommand command = new ReceiveCharCommand(timeout);

			processCommand(command);

			return command.GetResult();
		}

		public Digits SayAlpha(string chars, Digits escapeDigits)
		{
			SayAlphaCommand command = new SayAlphaCommand(chars, escapeDigits);

			processCommand(command);

			return command.GetResult();
		}

		public Digits SayDigits(string number, Digits escapeDigits)
		{
			SayDigitsCommand command = new SayDigitsCommand(number, escapeDigits);

			processCommand(command);

			return command.GetResult();
		}

		public Digits SayDate(DateTime date, Digits escapeDigits)
		{
			SayDateCommand command = new SayDateCommand(date, escapeDigits);

			processCommand(command);

			return command.GetResult();
		}

		public Digits SayDateTime(DateTime time, Digits escapeDigits)
		{
			return sayDateTime(new SayDateTimeCommand(time, escapeDigits));
		}

		public Digits SayDateTime(DateTime time, Digits escapeDigits, string format, string timezone)
		{
			return sayDateTime(new SayDateTimeCommand(time, escapeDigits, format, timezone));
		}

		private Digits sayDateTime(SayDateTimeCommand command)
		{
			processCommand(command);

			return command.GetResult();
		}

		public Digits SayPhonetic(string message, Digits escapeDigits)
		{
			SayPhoneticCommand command = new SayPhoneticCommand(message, escapeDigits);

			processCommand(command);

			return command.GetResult();
		}

		public void SetAutoHangUp(int time)
		{
			SetAutoHangUpCommand command = new SetAutoHangUpCommand(time);

			processCommand(command);
		}

		public Digits SayTime(DateTime time, Digits escapeDigits)
		{
			SayTimeCommand command = new SayTimeCommand(time, escapeDigits);

			processCommand(command);

			return command.GetResult();
		}

		public void SendText(string text)
		{
			SendTextCommand command = new SendTextCommand(text);

			processCommand(command);
		}

		public void SetCallerID(string number)
		{
			SetCallerIDCommand command = new SetCallerIDCommand(number);

			processCommand(command);
		}

		public void EnableMusic(bool enabled)
		{
			EnableMusicCommand command = new EnableMusicCommand(enabled);
			processCommand(command);
		}

		public void SetExtension(string extension)
		{
			SetExtensionCommand command = new SetExtensionCommand(extension);
			processCommand(command);
		}

		public void SetPriority(int priority)
		{
			SetPriorityCommand command = new SetPriorityCommand(priority);
			processCommand(command);
		}

		public void SetVariable(string name, string value)
		{
			SetVariableCommand command = new SetVariableCommand(name, value);
			processCommand(command);
		}

		#region StreamFile overloads
		public string StreamFile(string filename, string escapedigits, int offset)
		{
			StreamFileCommand command = new StreamFileCommand(filename, escapedigits, offset);
			processCommand(command);

			return command.GetResult();
		}

		public string StreamFile(string filename, string escapedigits)
		{
			StreamFileCommand command = new StreamFileCommand(filename, escapedigits);
			processCommand(command);

			return command.GetResult();
		}

		public string StreamFile(string filename, int offset)
		{
			StreamFileCommand command = new StreamFileCommand(filename, offset);
			processCommand(command);

			return command.GetResult();
		}

		public string StreamFile(string filename)
		{
			StreamFileCommand command = new StreamFileCommand(filename);
			processCommand(command);

			return command.GetResult();
		}
		#endregion

		public void EnableTDD(bool enabled)
		{
			TDDModeCommand command = new TDDModeCommand(enabled);
			processCommand(command);
		}

		public void VerboseLog(string message, AsteriskVerboseLevel level)
		{
			VerboseCommand command = new VerboseCommand(message, level);
			processCommand(command);
		}

		public string WaitForDigit(TimeSpan timeout)
		{
			WaitForDigitCommand command = new WaitForDigitCommand(timeout);

			int oldtimeout = _stream.ReadTimeout;
			_stream.ReadTimeout = (int)timeout.TotalMilliseconds + 250;
			
			processCommand(command);
			string response = command.GetResult();

			_stream.ReadTimeout = oldtimeout;

			return response;
		}

		public Digits SayNumber(int number, Digits escapeDigits)
		{
			SayNumberCommand command = new SayNumberCommand(number, escapeDigits);

			processCommand(command);

			return command.GetResult();
		}

		public object ProcessCommand(BaseAGICommand command)
		{
			this.processCommand(command);

			if (command is IProvideCommandResult)
				return ((IProvideCommandResult)command).GetResult();

			return null;
		}

		private void processCommand(BaseAGICommand command)
		{
			this.sendCommand(command);

			FastAGIResponse response = this.readResponse();

			if (!command.IsSuccessfulResult(response.ResultValue))
				throw new AsteriskException("Command Failed");

			if (command is ISupportCommandResponse)
			{
				((ISupportCommandResponse)command).ProcessResponse(response.Payload);
			}
		}

		private void sendCommand(BaseAGICommand command)
		{
			string commandString = command.GetCommand();

			commandString += "\n";
			
			_writer.Write(commandString);
			_writer.Flush();
		}

		private FastAGIResponse readResponse()
		{
			string response = _reader.ReadLine();

			FastAGIResponse agiResponse = FastAGIResponse.ParseResponse(response);

			if (agiResponse.ResponseCode != 200)
			{
				throw new AsteriskException(response.Substring(4));
			}

			return agiResponse;
		}

		public static string GetDigitsString(Digits digits)
		{
			StringBuilder sb = new StringBuilder();

			if (digits == Digits.None) sb.Append("\"\"");
			if ((digits & Digits.Zero) == Digits.Zero) sb.Append('0');
			if ((digits & Digits.One) == Digits.One) sb.Append('1');
			if ((digits & Digits.Two) == Digits.Two) sb.Append('2');
			if ((digits & Digits.Three) == Digits.Three) sb.Append('3');
			if ((digits & Digits.Four) == Digits.Four) sb.Append('4');
			if ((digits & Digits.Five) == Digits.Five) sb.Append('5');
			if ((digits & Digits.Six) == Digits.Six) sb.Append('6');
			if ((digits & Digits.Seven) == Digits.Seven) sb.Append('7');
			if ((digits & Digits.Eight) == Digits.Eight) sb.Append('8');
			if ((digits & Digits.Nine) == Digits.Nine) sb.Append('9');
			if ((digits & Digits.Pound) == Digits.Pound) sb.Append('#');
			if ((digits & Digits.Asterisk) == Digits.Asterisk) sb.Append('*');

			return sb.ToString();
		}

		public static Digits GetDigitsFromString(string digitsString)
		{
			Digits digits = Digits.None;

			foreach (Char c in digitsString)
			{
				switch (c)
				{
					case '0': digits |= Digits.Zero; break;
					case '1': digits |= Digits.One; break;
					case '2': digits |= Digits.Two; break;
					case '3': digits |= Digits.Three; break;
					case '4': digits |= Digits.Four; break;
					case '5': digits |= Digits.Five; break;
					case '6': digits |= Digits.Six; break;
					case '7': digits |= Digits.Seven; break;
					case '8': digits |= Digits.Eight; break;
					case '9': digits |= Digits.Nine; break;
					case '*': digits |= Digits.Asterisk; break;
					case '#': digits |= Digits.Pound; break;
				}
			}

			return digits;
		}

		#endregion
	}
}
