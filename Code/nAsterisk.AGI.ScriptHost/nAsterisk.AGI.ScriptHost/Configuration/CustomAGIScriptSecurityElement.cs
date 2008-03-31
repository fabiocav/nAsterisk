using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security;
using System.Configuration;

namespace nAsterisk.AGI.ScriptHost.Configuration
{
    public class CustomAGIScriptSecurityElement : ConfigurationElement
    {
        public CustomAGIScriptSecurityElement()
        {

        }

        protected override bool OnDeserializeUnrecognizedElement(string elementName, System.Xml.XmlReader reader)
        {
            if (elementName == "PermissionSet")
            {
                SecurityElement = SecurityElement.FromString(reader.ReadOuterXml());
                return true;
            }
            else
            {
                return base.OnDeserializeUnrecognizedElement(elementName, reader);
            }
        }

        public SecurityElement SecurityElement
        {
            get;
            set;
        }
    }
}
