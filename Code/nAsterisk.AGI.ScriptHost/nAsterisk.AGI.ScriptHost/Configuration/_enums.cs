using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nAsterisk.AGI.ScriptHost.Configuration
{
    public enum ScriptPermissionSet
    {
        FullTrust,
        Internet,
        Intranet,
        Host,
        Custom
    }

    public enum ScriptIsolationMode
    {
        AppDomain,
        Process,
        None
    }
}
