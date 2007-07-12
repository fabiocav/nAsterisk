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
		/// Gets configuration from the config source
		/// </summary>
		public void Configure(Configuration.ITcpHostConfigurationSource config)
		{
			_mappings = config.GetUriMappings();
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

		/// <summary>
		/// Async target for _listener.BeginAcceptTcpClient
		/// </summary>
		/// <param name="iar"></param>
		private void GotConnection(IAsyncResult iar)
		{
			// End the accept
			TcpClient client = null;

			try
			{
				if (iar.IsCompleted)
					client = _listener.EndAcceptTcpClient(iar);
			}
			catch (ObjectDisposedException) { }
			
			if (client != null)
			{
				this.DispatchScript(client);

				// Restart the Accept
				_listener.BeginAcceptTcpClient(new AsyncCallback(GotConnection), null);
			}
		}

		private void DispatchScript(TcpClient client)
		{
			System.Threading.Thread t = new System.Threading.Thread(delegate()
			{
				using (Stream stream = client.GetStream())
				{
					AsteriskAgi agi = new AsteriskAgi(stream);
					agi.Init();

					Uri uri = new Uri(agi.Request);

					// Parse the query string variables
					Dictionary<string, string> vars;
					if (!string.IsNullOrEmpty(uri.Query))
					{
						// Get rid of the ? and split on &
						string[] qvars = uri.Query.Substring(1).Split('&');
						vars = new Dictionary<string, string>(qvars.Length);
						Array.ForEach<string>(qvars, delegate(string qvar)
						{
							string[] var = qvar.Split('=');
							vars.Add(var[0], var[1]);
						});
					}
					else
						vars = new Dictionary<string, string>();

					// Look at the incoming URL and match it to a script
					if (_mappings.ContainsKey(uri.AbsolutePath))
					{
						Type scriptType = _mappings[uri.AbsolutePath];
						IAsteriskAgiScript script = (IAsteriskAgiScript)Activator.CreateInstance(scriptType);
						script.Execute(agi, vars);
					}
				}

				client.Close();
			});

			t.Start();
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
