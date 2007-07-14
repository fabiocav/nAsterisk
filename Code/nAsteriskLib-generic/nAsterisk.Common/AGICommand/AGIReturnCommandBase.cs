using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk.AGICommand
{
    /// <summary>
    /// Base class for commands that return some type of data
    /// </summary>
    /// <typeparam name="T">The return type from the command</typeparam>
    public abstract class AGIReturnCommandBase<T> : AGICommandBase
    {
        public abstract override string GetCommand();
        public abstract T ProcessResponse(FastAGIResponse response);
    }
}
