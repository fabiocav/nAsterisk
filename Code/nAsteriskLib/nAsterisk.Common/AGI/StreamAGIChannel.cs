using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace nAsterisk.AGI
{
    public class StreamAGIChannel : IAGIChannel
    {
        private StreamReader _reader;
        private StreamWriter _writer;

        public StreamAGIChannel(Stream stream)
        {
            _writer = new StreamWriter(stream, System.Text.ASCIIEncoding.ASCII);
            _reader = new StreamReader(stream, System.Text.ASCIIEncoding.ASCII);
        }

        #region IAGIChannel Members

        public void SendCommand(string command)
        {
            try
            {
                _writer.Write(command);
                _writer.Flush();
            }
            catch (IOException)
            {
                throw new HangUpException();
            }
        }

        public string GetResponse()
        {
            string response = _reader.ReadLine();
            
            if (response == null)
            {
                throw new HangUpException();
            }

            return response;
        }

        public string GetAGIVariables()
        {
            StringBuilder agiVariablesBuilder = new StringBuilder();

            try
            {
                while (true)
                {
                    string line = _reader.ReadLine();
                    
                    if (line == "")
                        break;
                 
                    agiVariablesBuilder.AppendLine(line);
                }
            }
            catch (Exception) { }

            return agiVariablesBuilder.ToString();
        }

        #endregion
    }
}
