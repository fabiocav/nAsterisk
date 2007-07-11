using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace nAsterisk
{
	public class TcpAgiScriptHost : IAgiScriptHost
	{
		#region MemberVars
		TcpListener _listener;
		Dictionary<string, Type> _mappings = new Dictionary<string,Type>();
		#endregion

		#region Constructors
		public TcpAgiScriptHost()
			: this(4573)
		{

		}

		public TcpAgiScriptHost(int port) : this(IPAddress.Any, port)
		{

		}

		public TcpAgiScriptHost(IPAddress addr, int port) : this(new IPEndPoint(addr, port))
		{

		}

		public TcpAgiScriptHost(IPEndPoint localEP)
		{
			_listener = new TcpListener(localEP);

		}
		#endregion

		/// <summary>
		/// Reads URI/script associations from the config source
		/// </summary>
		private void SetupUris()
		{

		}

		public void AddUri(string uri, Type scriptType)
		{
			if (_mappings.ContainsKey(uri))
				throw new ArgumentException("specified uri is already mapped to a script", "uri");

			if (scriptType.IsSubclassOf(typeof(IAsteriskAgiScript)))
				_mappings.Add(uri, scriptType);
			else
				throw new ArgumentException("script type must implement IAsteriskAgiScript", "scriptType");

			_mappings.Add(uri, scriptType);
		}

		private void GotConnection(IAsyncResult iar)
		{
			
			// End the accept
			TcpClient client = null;
			
			if(iar.IsCompleted)
				client = _listener.EndAcceptTcpClient(iar);
			
			if (client != null)
			{
				this.DispatchScript(client);

				// Restart the Accept
				_listener.BeginAcceptTcpClient(new AsyncCallback(GotConnection), null);
			}
		}

		private void DispatchScript(TcpClient client)
		{
			Stream stream = client.GetStream();
			//TODO: Look at the incoming URL and match it to a script


			// This is just for testing
			using (StreamReader sr = new StreamReader(stream, System.Text.ASCIIEncoding.ASCII))
			{
				char[] buf = new char[1024];
				int read = sr.Read(buf, 0, 1024);

				Console.Write(new string(buf, 0, read));
			}
		}

		#region IAgiScriptHost Members

		public void Start()
		{
			_listener.Start();
			_listener.BeginAcceptTcpClient(new AsyncCallback(GotConnection), null);
		}

		public void Stop()
		{
			_listener.Stop();
		}

		#endregion
	}
}
