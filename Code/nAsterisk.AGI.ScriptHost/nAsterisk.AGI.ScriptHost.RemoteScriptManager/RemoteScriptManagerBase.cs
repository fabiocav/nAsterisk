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
using System.Linq;
using System.Text;

namespace nAsterisk.AGI.ScriptHost.RemoteScriptManager
{
    public class RemoteScriptManagerBase : IRemoteScriptManager, IAGIChannel
    {
        private IAGIScriptHost _hostAGI;
        private AsteriskAGI _asteriskAGI;
        Dictionary<string, string> _agiVariables;
        Dictionary<string, string> _configurationSettings;

        #region IRemoteScriptManager Members

        public void Execute(IAGIScriptHost host, Dictionary<string, string> configurationSettings)
        {
            _hostAGI = host;

            _asteriskAGI = new AsteriskAGI(this);
            _asteriskAGI.Init();

            Uri uri = new Uri(_asteriskAGI.RequestInfo.Request);

            _agiVariables = parseQueryStrings(uri.Query);

            _configurationSettings = configurationSettings;

            OnExecute(host);
        }

        #endregion

        private Dictionary<string, string> parseQueryStrings(string queryString)
        {
            Dictionary<string, string> vars;

            if (!string.IsNullOrEmpty(queryString))
            {
                // Get rid of the ? and split on &
                string[] qvars = queryString.Substring(1).Split('&');
                vars = new Dictionary<string, string>(qvars.Length);
                Array.ForEach<string>(qvars, delegate(string qvar)
                {
                    string[] var = qvar.Split('=');
                    vars.Add(var[0], var[1]);
                });
            }
            else
                vars = new Dictionary<string, string>();

            return vars;
        }

        protected virtual void OnExecute(IAGIScriptHost host)
        {}

        protected IAGIScriptHost Host
        {
            get { return _hostAGI; }
        }

        protected AsteriskAGI AGI
        {
            get { return _asteriskAGI; }
        }

        protected Dictionary<string, string> AGIVariables
        {
            get { return _agiVariables; }
        }

        protected Dictionary<string, string> ConfigurationSettings
        {
            get { return _configurationSettings; }
        }

        #region IAGIChannel Members

        public string GetAGIVariables()
        {
            return _hostAGI.GetAGIVariables();
        }

        public string GetResponse()
        {
            return _hostAGI.GetResponse();
        }

        public void SendCommand(string command)
        {
            _hostAGI.SendCommand(command);
        }

        #endregion
    }
}
