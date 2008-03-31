using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace nAsterisk.AGI.ScriptHost.Configuration
{
    public class AGIScriptMappingElement : ConfigurationElement
    {
        public AGIScriptMappingElement()
        {

        }

        public AGIScriptMappingElement(string name)
        {
            Name = name;
        }

        [ConfigurationProperty("name", DefaultValue = "mapping",
            IsRequired = true, IsKey = true)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("scriptManagerName", DefaultValue = "nAsterisk.RemoteScriptManagers.GenericScriptManager", IsRequired = true)]
        public string ScriptManagerName
        {
            get { return (string)this["scriptManagerName"]; }
            set { this["scriptManagerName"] = value; }
        }

        [ConfigurationProperty("isolationMode", DefaultValue = "AppDomain", IsRequired = false)]
        public ScriptIsolationMode IsolationMode
        {
            get { return (ScriptIsolationMode)this["isolationMode"]; }
            set { this["isolationMode"] = value; }
        }

        [ConfigurationProperty("permissionSet", DefaultValue = "FullTrust", IsRequired = false)]
        public ScriptPermissionSet PermissionSet
        {
            get { return (ScriptPermissionSet)this["permissionSet"]; }
            set { this["permissionSet"] = value; }
        }

        [ConfigurationProperty("customPermission", IsRequired = false)]
        public CustomAGIScriptSecurityElement CustomPermissionSet
        {
            get { return (CustomAGIScriptSecurityElement)this["customPermission"]; }
            set { this["customPermission"] = value; }
        }

        [ConfigurationProperty("managerSettings", IsDefaultCollection=false, IsRequired=false)]
        [ConfigurationCollection(typeof(NameValueConfigurationCollection))]
        public NameValueConfigurationCollection ManagerSettings
        {
            get { return (NameValueConfigurationCollection)this["managerSettings"]; }
            set { this["managerSettings"] = value; }
        }
    }
}
