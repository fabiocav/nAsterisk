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

namespace nAsterisk.AGI.ScriptHost
{
    public class AGIScriptHost : IAGIScriptHost
    {
        IAGIChannel _agiChannel;
        TcpAGIIsolatedScriptHost _scriptHost;
        string _agiVariables;

        public AGIScriptHost(IAGIChannel agiChannel, TcpAGIIsolatedScriptHost scriptHost)
        {
            _agiChannel = agiChannel;
            _scriptHost = scriptHost;

            _agiVariables = _agiChannel.GetAGIVariables();
        }

        #region IHostAGI Members

        public string GetAGIVariables()
        {
            return _agiVariables;
        }

        public string GetResponse()
        {
            return _agiChannel.GetResponse();
        }

        public void SendCommand(string command)
        {
            _agiChannel.SendCommand(command);
        }

        public string GetMappedTypeName(string mappingName)
        {
            return _scriptHost.GetMappedTypeName(mappingName);
        }

        #endregion
    }
}
