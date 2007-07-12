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

			this.SendCommand(command);

			this.CheckSuccess(command);
		}

		public ChannelStatus GetChannelStatus(GetChannelStatusCommand command)
		{
			this.SendCommand(command);

			int ret = this.ReadIntVar();
			return (ChannelStatus)ret;
		}

		public void DatabaseDelete(DatabaseDeleteCommand command)
		{
			this.SendCommand(command);

			this.CheckSuccess(command);
		}

		public void DatabaseDeleteTree(DatabaseDeleteTreeCommand command)
		{
			this.SendCommand(command);

			this.CheckSuccess(command);
		}

		private void SendCommand(BaseAGICommand command)
		{
			string commandString = command.GetCommand();

			commandString += "\n";
			
			_writer.Write(commandString);
			_writer.Flush();
		}
		#endregion

		private void CheckSuccess(BaseAGICommand command)
		{
			string var = _reader.ReadLine();

			int result = int.Parse(var.Substring(0, 3));

			if (result == 200)
			{
				int resultIndex = var.IndexOf("result=") + 7;
				string resultValue = var.Substring(resultIndex, var.IndexOf(" ",resultIndex));

				if (!command.IsSuccessfulResult(resultValue))
					throw new AsteriskException("Command Failed");

				if (command is ISupportCommandResponse)
				{
				    if (var.Length >= 13)
				        ((ISupportCommandResponse)command).ProcessResponse(var.Substring(13));
				}
			}
			else
			{
				throw new AsteriskException(var.Substring(4));
			}
		}

		private int ReadIntVar()
		{
			string var = _reader.ReadLine();
			return int.Parse(var);
		}
	}
}
