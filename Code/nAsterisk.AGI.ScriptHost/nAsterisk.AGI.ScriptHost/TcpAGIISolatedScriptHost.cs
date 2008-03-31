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
using System.AddIn.Hosting;
using System.IO;
using nAsterisk.AGI.ScriptHost.Configuration;
using System.Security;
using System.Security.Policy;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Collections.ObjectModel;
using System.Linq;

namespace nAsterisk.AGI.ScriptHost
{
    public class TcpAGIIsolatedScriptHost : TcpAGIScriptHost
    {
        private Dictionary<string, string> _scriptMappings;
 
        protected override void DispatchScript(System.Net.Sockets.TcpClient client)
        {
            System.Threading.Thread t = new System.Threading.Thread(delegate()
            {
                using (Stream stream = client.GetStream())
                {
                    StreamAGIChannel agiChannel = new StreamAGIChannel(stream);

                    AGIScriptHost host = new AGIScriptHost(agiChannel, this);

                    AGIRequestInfo requestInfo = new AGIRequestInfo(host.GetAGIVariables());
                    
                    IRemoteScriptManager remoteAGIScriptManager = getRequestToken(requestInfo);

                    if (remoteAGIScriptManager == null)
                        return;

                    try
                    {
                        remoteAGIScriptManager.Execute(host, getManagerConfigurationSettings(requestInfo));
                    }
                    catch (Exception exc)
                    {
                        Console.WriteLine(string.Format("A script failed with the following exception: {0}", exc.ToString()));
                    }
                    finally
                    {
                        remoteAGIScriptManager = null;
                    }
                }

                client.Close();
            });

            t.Start();
        }

        private Dictionary<string, string> getManagerConfigurationSettings(AGIRequestInfo requestInfo)
        {
            Dictionary<string, string> settings = new Dictionary<string, string>();

            Uri uri = new Uri(requestInfo.Request);


            if (ScriptHostRuntime.AGIScriptHostConfiguration.AGIScriptMappings[uri.AbsolutePath] != null)
            {
                foreach (string key in ScriptHostRuntime.AGIScriptHostConfiguration.AGIScriptMappings[uri.AbsolutePath].ManagerSettings.AllKeys)
                {

                    string value = ScriptHostRuntime.AGIScriptHostConfiguration.AGIScriptMappings[uri.AbsolutePath].ManagerSettings[key].Value;
                    string name = ScriptHostRuntime.AGIScriptHostConfiguration.AGIScriptMappings[uri.AbsolutePath].ManagerSettings[key].Name;

                    settings.Add(name, value);
                }
            }

            return settings;
        }

        private IRemoteScriptManager getRequestToken(AGIRequestInfo requestInfo)
        {
            Uri uri = new Uri(requestInfo.Request);

            IRemoteScriptManager manager = null;

            if (_scriptMappings.ContainsKey(uri.AbsolutePath))
            {
                AGIScriptMappingElement scriptMapping = ScriptHostRuntime.AGIScriptHostConfiguration.AGIScriptMappings[uri.AbsolutePath];

                AddInToken remoteManagerToken = getRemoteScriptManagerToken(scriptMapping.ScriptManagerName);

                AppDomain targetDomain = AppDomain.CurrentDomain;

                if (scriptMapping.IsolationMode == ScriptIsolationMode.AppDomain)
                {
                    PermissionSet grantPermissionSet = getPermissionSet(scriptMapping);

                    AppDomainSetup setup = new AppDomainSetup();
                    setup.ShadowCopyFiles = "true";
                    setup.ApplicationBase = AppDomain.CurrentDomain.BaseDirectory;

                    targetDomain = AppDomain.CreateDomain(uri.AbsolutePath, null, setup, grantPermissionSet);
                    manager = remoteManagerToken.Activate<IRemoteScriptManager>(targetDomain);
                }
                else if (scriptMapping.IsolationMode == ScriptIsolationMode.Process)
                {
                    manager = remoteManagerToken.Activate<IRemoteScriptManager>(new AddInProcess(), getPermissionSet(scriptMapping));
                }
                else
                {
                    manager = remoteManagerToken.Activate<IRemoteScriptManager>(targetDomain);
                }

            }

            return manager;
        }

        private PermissionSet getPermissionSet(AGIScriptMappingElement scriptMapping)
        {
            PermissionSet permission = null;

            switch (scriptMapping.PermissionSet)
            {
                case ScriptPermissionSet.FullTrust:
                    permission = PolicyLevel.CreateAppDomainLevel().GetNamedPermissionSet("FullTrust");
                    break;
                case ScriptPermissionSet.Internet:
                    permission = PolicyLevel.CreateAppDomainLevel().GetNamedPermissionSet("Internet");
                    break;
                case ScriptPermissionSet.Intranet:
                    permission = PolicyLevel.CreateAppDomainLevel().GetNamedPermissionSet("Intranet");
                    break;
                case ScriptPermissionSet.Host:
                    permission = AppDomain.CurrentDomain.ApplicationTrust.DefaultGrantSet.PermissionSet;
                    break;
                case ScriptPermissionSet.Custom:
                    permission = new PermissionSet(PermissionState.None);
                    permission.FromXml(scriptMapping.CustomPermissionSet.SecurityElement);
                    break;
            }

            return permission;
        }

        private PermissionSet getAddinPermissionSet(SecurityElement securityElement)
        {
            PermissionSet permission = new PermissionSet(null);
            permission.FromXml(securityElement);

            return permission;
        }

        private AddInSecurityLevel getAddinSecurityLevel(AGIScriptMappingElement scriptMapping)
        {
            switch (scriptMapping.PermissionSet)
            {
                case ScriptPermissionSet.FullTrust:
                    return AddInSecurityLevel.FullTrust;
                case ScriptPermissionSet.Internet:
                    return AddInSecurityLevel.Internet;
                case ScriptPermissionSet.Intranet:
                    return AddInSecurityLevel.Intranet;
                case ScriptPermissionSet.Host:
                    return AddInSecurityLevel.Host;
            }

            return AddInSecurityLevel.Internet;
        }

        private static AddInToken getRemoteScriptManagerToken(string remoteScriptManagerName)
        {
            string addInRoot = Environment.CurrentDirectory;

            AddInStore.Rebuild(addInRoot);
            Collection<AddInToken> addInTokens = AddInStore.FindAddIns(typeof(IRemoteScriptManager), addInRoot);

            AddInToken token = addInTokens.First<AddInToken>(currentToken => currentToken.Name == remoteScriptManagerName);
            
            return token;
        }

        public string GetMappedTypeName(string mappingName)
        {
            if (!_scriptMappings.ContainsKey(mappingName))
                return null;

            return _scriptMappings[mappingName];
        }

        public override void Configure(nAsterisk.Configuration.ITcpHostConfigurationSource config)
        {
            throw new NotSupportedException("This type does not support configuration providers with loaded types.");
        }

        private new Dictionary<string, Type> Mappings
        {
            get { throw new NotSupportedException(); }
        }

        public void Configure(Dictionary<string, string> scriptMappings)
        {
            _scriptMappings = scriptMappings;
        }
    }
}
