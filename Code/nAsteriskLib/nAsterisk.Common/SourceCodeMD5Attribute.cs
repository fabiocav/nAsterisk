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
