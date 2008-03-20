using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk.AGI
{
    public interface IAGIChannel
    {
        void SendCommand(string command);
        string GetResponse();
        string GetAGIVariables();
    }
}
