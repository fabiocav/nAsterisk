using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace nAsterisk
{
	public class AsteriskAgi
	{
		private Stream _stream;
		private StreamWriter _writer;
		private StreamReader _reader;

		public AsteriskAgi(Stream stream)
		{
			_stream = stream;
			_writer = new StreamWriter(_stream, System.Text.ASCIIEncoding.ASCII);
			_reader = new StreamReader(_stream, System.Text.ASCIIEncoding.ASCII);
		}

		public void Answer()
		{
			this.SendCommand("ANSWER");

			this.CheckSuccess();
		}

		public ChannelStatus GetChannelStatus(string channelName)
		{
			this.SendCommand("CHANNEL STATUS", channelName);

			int ret = this.ReadIntVar();
			return (ChannelStatus)ret;
		}

		public void DatabaseDelete(string family, string key)
		{
			this.SendCommand("DATABASE DEL", family, key);

			this.CheckSuccess();
		}

		public void DatabaseDeleteTree(string family, string keytree)
		{
			this.SendCommand("DATABASE DELTREE ", family, keytree);

			this.CheckSuccess();
		}


		private void SendCommand(string command, params string[] vars)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(command);

			Array.ForEach<string>(vars, delegate(string var) { sb.AppendFormat(" {0}", var); });

			_writer.WriteLine(sb.ToString());
			_writer.Flush();
		}

		private void CheckSuccess()
		{
			int ret = this.ReadIntVar();
			if( ret != 0)
				throw new AsteriskException("Deltree Failed");
		}

		private int ReadIntVar()
		{
			string var = _reader.ReadLine();
			return int.Parse(var);
		}
	}
}
