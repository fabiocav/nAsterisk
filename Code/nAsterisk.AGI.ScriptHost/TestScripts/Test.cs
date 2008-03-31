using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using nAsterisk.AGI;

namespace TestScripts
{
    public class Test : IAsteriskAGIScript
    {
        #region IAsteriskAGIScript Members

        public void Execute(IAsteriskAGI agi, Dictionary<string, string> vars)
        {
            agi.Answer();

            agi.SayDigits("12345", nAsterisk.AGI.Command.Digits.None);

            agi.HangUp();
        }

        #endregion
    }
}
