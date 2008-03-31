using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Security;

namespace nAsterisk.AGI.ScriptHost.Configuration
{
    public class AGIScriptHostSection : ConfigurationSection
    {

        [ConfigurationProperty("scriptMappings", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(AGIScriptMappingElements),
            AddItemName = "mapping",
            ClearItemsName = "clearMappings",
            RemoveItemName = "removeMappings")]
        public AGIScriptMappingElements AGIScriptMappings
        {
            get { return (AGIScriptMappingElements)this["scriptMappings"]; }
            set { this["scriptMappings"] = value; }
        }
    }
}
