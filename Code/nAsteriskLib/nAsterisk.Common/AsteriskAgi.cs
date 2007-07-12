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

		public ChannelStatus GetChannelStatus(GetChannelStatusCommand command)
		{
			processCommand(command);

			return command.GetResult();
		}

		public void DatabaseDelete(DatabaseDeleteCommand command)
		{
			processCommand(command);
		}

		public void DatabaseDeleteTree(DatabaseDeleteTreeCommand command)
		{
			processCommand(command);
		}

		public string DatabaseGet(DatabaseGetCommand command)
		{
			processCommand(command);

			return command.GetResult();
		}

		public void DatabasePut(DatabasePutCommand command)
		{
			processCommand(command);
		}

		public string Execute(ExecuteCommand command)
		{
			processCommand(command);

			return command.GetResult();
		}

		public string GetData(GetDataCommand command)
		{
			processCommand(command);

			return command.GetResult();
		}

		public string GetVariable(GetVariableCommand command)
		{
			processCommand(command);

			return command.GetResult();
		}

		public void HangUp(HangUpCommand command)
		{
			processCommand(command);
		}

		public void Noop()
		{
			NoopCommand command = new NoopCommand();

			processCommand(command);
		}

		public Char? ReceiveChar(ReceiveCharCommand command)
		{
			processCommand(command);

			return command.GetResult();
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
		#endregion
	}
}
