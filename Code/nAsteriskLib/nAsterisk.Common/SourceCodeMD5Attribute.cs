/**************************************************************************
// nAsterisk - .NET Asterisk Library 
//
// Copyright (c) 2007 by:
// Fabio Cavalcante (fabio(a)codesapien.com)
// Josh Perry (josh(a)6bit.com)
// Justin Long (dukk(a)dukk.org)
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
using System.Text;

namespace nAsterisk
{
    /// <summary>
    /// The MD5 Sum of all source files used to build this assembly.
    /// This allows independent confirmation of what source
    /// version the assembly was built against,
    /// assuming you have access to the source tree.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly)]
    public class SourceCodeMD5Attribute : Attribute
    {
        private string _md5;

        public SourceCodeMD5Attribute(string md5)
        {
            _md5 = md5;
        }

        public string SourceCodeMD5
        {
            get { return _md5; }
        }
    }
}
