using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using nAsterisk.AGI;

namespace TestScripts
{
    public class EchoCallerIdScript : IAsteriskAGIScript
    {
        #region IAsteriskAGIScript Members

        public void Execute(nAsterisk.AGI.IAsteriskAGI agi, Dictionary<string, string> vars)
        {
            Console.WriteLine("Caller ID number is: {0}, name is : {1}", agi.RequestInfo.CallerId, agi.RequestInfo.CallerIdName);
        }

        #endregion
    }
}
