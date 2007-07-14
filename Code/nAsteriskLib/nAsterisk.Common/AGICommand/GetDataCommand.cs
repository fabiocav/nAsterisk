using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk.AGICommand
{
    public class GetDataCommand : AGIReturnCommandBase<GetDataCommandResult>
    {
        private string _fileToStream;
        private int _timeout;
        private int _maxDigits;
        private GetDataCommandResult _result;

        public GetDataCommand(string fileToStream)
            : this(fileToStream, 0, 1024) { }

        public GetDataCommand(string fileToStream, int timeout)
            : this(fileToStream, timeout, 1024) { }

        public GetDataCommand(string fileToStream, int timeout, int maxDigits)
        {
            _fileToStream = fileToStream;
            _timeout = timeout;
            _maxDigits = maxDigits;
        }

        public int MaxDigits
        {
            get { return _maxDigits; }
            set { _maxDigits = value; }
        }

        public int Timeout
        {
            get { return _timeout; }
            set { _timeout = value; }
        }

        public string FileToStream
        {
            get { return _fileToStream; }
            set { _fileToStream = value; }
        }

        public override string GetCommand()
        {
            if (string.IsNullOrEmpty(_fileToStream))
            {
                throw new InvalidOperationException("The GetDataCommand requires FileToStream to be set.");
            }

            string command = string.Format("GET DATA {0}", _fileToStream);

            if (_timeout != 0)
                command = string.Format("{0} {1}", command, _timeout);

            if (_maxDigits != 1024)
                command = string.Format("{0} {1}", command, _maxDigits);

            return command;
        }

        public override GetDataCommandResult ProcessResponse(FastAGIResponse response)
        {
            if (response.ResultValue == "-1")
                throw new AsteriskCommandException("GetData Command Failed.");

            _result = new GetDataCommandResult();

            _result.ResultingDtmfData = response.ResultValue;
            _result.Timeout = response.Payload == "timeout";

            return _result;
        }
    }
}
