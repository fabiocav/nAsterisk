/**************************************************************************
// nAsterisk - .NET Asterisk Library 
//
// Copyright (c) 2007 by:
// Fabio Cavalcante (fabio(a)codesapien.com)
// Josh Perry (josh(a)6bit.com)
//
// Asterisk - Copyright (C) 1999 - 2006, Digium, Inc.
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU Library General Public License as published 
// by  the Free Software Foundation; either version 2 of the License or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful, but
// WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Library General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this package (See COPYING.LIB); if not, write to the Free Software
// Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA 02111-1307, USA.
/**************************************************************************/

using System;
using System.Collections.Generic;
using System.AddIn;
using System.Security;
using System.Security.Permissions;

namespace nAsterisk.AGI.ScriptHost.RemoteScriptManager
{
    [AddIn("nAsterisk.RemoteScriptManagers.GenericScriptManager", 
        Description="Dispatches AGI scripts and acts as a proxy in between the script and the host", 
        Publisher="nAsterisk", 
        Version="1.0.0.0")]
    public class GenericScriptManager : RemoteScriptManagerBase
    {
        protected override void OnExecute(IAGIScriptHost host)
        {

            Uri uri = new Uri(AGI.RequestInfo.Request);

            string scriptTypeName = ConfigurationSettings["AGIScriptType"];

            Type scriptType = null;

            try
            {
                new PermissionSet(PermissionState.Unrestricted).Assert();

                scriptType = Type.GetType(scriptTypeName);

                PermissionSet.RevertAssert();
            }
            catch (SecurityException exc)
            {
                throw new SecurityException("Unable to dinamically load requested type. The GenericScriptManager needs elevated permissions in order to" +
                    " properly function. For more restricted permissions, please use a differen Script Manager type", exc);
            }

            if (scriptType != null)
            {
                IAsteriskAGIScript script = (IAsteriskAGIScript)Activator.CreateInstance(scriptType);
                script.Execute(AGI, AGIVariables);
            }
        }
    }
}
