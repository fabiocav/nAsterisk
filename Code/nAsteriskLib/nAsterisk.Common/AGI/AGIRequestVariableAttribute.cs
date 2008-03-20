using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk.AGI
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple=false, Inherited= true)]
    public class AGIRequestVariableAttribute : Attribute
    {
        public AGIRequestVariableAttribute(string agiVariableName)
        {
            AGIVariableName = agiVariableName;
        }

        public string AGIVariableName { get; private set; }
    }
}
