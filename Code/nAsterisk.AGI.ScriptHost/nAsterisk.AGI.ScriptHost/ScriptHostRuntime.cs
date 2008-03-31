using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using nAsterisk.Configuration;
using System.Configuration;
using nAsterisk.AGI.ScriptHost.Configuration;

namespace nAsterisk.AGI.ScriptHost
{
    public static class ScriptHostRuntime
    {
        private static AGIScriptHostSection _agiScriptHostConfiguration;
        private static Dictionary<string, string> _scriptMappings;
        private static TcpAGIIsolatedScriptHost _host;

        public static Dictionary<string, string> GetConfigurationSource()
        {
            if (_scriptMappings == null)
            {
                _scriptMappings = new Dictionary<string, string>();

                for (int i = 0; i < AGIScriptHostConfiguration.AGIScriptMappings.Count; i++)
                {
                    AGIScriptMappingElement scriptMapping = AGIScriptHostConfiguration.AGIScriptMappings[i];
                    
                    _scriptMappings.Add(scriptMapping.Name, scriptMapping.ScriptManagerName);
                }
            }

            //Return a copy of the mappings
            return new Dictionary<string,string>(_scriptMappings);
        }

        public static AGIScriptHostSection AGIScriptHostConfiguration
        {
            get
            {
                if (_agiScriptHostConfiguration == null)
                {
                    _agiScriptHostConfiguration = ConfigurationManager.GetSection("nAsterisk.agiScriptHost") as AGIScriptHostSection;
                }

                return _agiScriptHostConfiguration;
            }
        }

        public static void Start()
        {
            _host = new TcpAGIIsolatedScriptHost();

            _host.Configure(GetConfigurationSource());

            _host.Start();
        }

        public static void AddMapping(string name, Type scriptType)
        {
            _host.AddUri(name, scriptType);
        }

        public static void Stop()
        {
            _host.Stop();
        }
    }
}
