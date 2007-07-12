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

			return command.GetResponse();
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

			return command.GetResponse();
		}

		public void DatabasePut(DatabasePutCommand command)
		{
			processCommand(command);
		}

		public string Execute(ExecuteCommand command)
		{
			processCommand(command);

			return command.GetResponse();
		}

		public string GetData(GetDataCommand command)
		{
			processCommand(command);

			return command.GetResponse();
		}

		public string GetVariable(GetVariableCommand command)
		{
			processCommand(command);

			return command.GetResponse();
		}

		private void processCommand(BaseAGICommand command)
		{
			this.SendCommand(command);
			string response = this.ReadResponse();

			if (!command.IsSuccessfulResult(GetResult(response)))
				throw new AsteriskException("Command Failed");

			if (command is ISupportCommandResponse)
			{
				((ISupportCommandResponse)command).ProcessResponse(GetResponsePayload(response));
			}
		}

		private string GetResponsePayload(string response)
		{
			if (response.IndexOf("(") > 0)
			{
				int payloadStartIndex = response.IndexOf("(") + 1;

				return response.Substring(payloadStartIndex, response.IndexOf(")") - payloadStartIndex);
			}

			return null;
		}

		private string GetResult(string response)
		{
			int resultEndIndex = response.IndexOf(" ");

			if (resultEndIndex < 0)
				return response.Substring(7);

			return response.Substring(7, resultEndIndex - 7);
		}

		private void SendCommand(BaseAGICommand command)
		{
			string commandString = command.GetCommand();

			commandString += "\n";
			
			_writer.Write(commandString);
			_writer.Flush();
		}

		private string ReadResponse()
		{
			string response = _reader.ReadLine();

			int resultCode = int.Parse(response.Substring(0, 3));
			if (resultCode != 200)
			{
				throw new AsteriskException(response.Substring(4));
			}

			return response.Substring(4);
		}
		#endregion
	}
}
