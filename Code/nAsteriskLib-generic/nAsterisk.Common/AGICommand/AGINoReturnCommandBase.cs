using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk.AGICommand
{
    /// <summary>
    /// Base class for commands that do not return any data
    /// </summary>
    public class AGINoReturnCommandBase : AGICommandBase
    {
        public abstract override string GetCommand();
        public abstract void ProcessResponse(FastAGIResponse response);
    }
}
