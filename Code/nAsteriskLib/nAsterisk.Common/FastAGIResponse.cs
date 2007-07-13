using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace nAsterisk
{
	public class FastAGIResponse
	{
		private int _responseCode;
		private int? _endPosition;
		private string _resultValue;
		private string _payload;
		private string _rawResponse;

		private FastAGIResponse()
		{ }

		public string Payload
		{
			get { return _payload; }
			set { _payload = value; }
		}

		public string ResultValue
		{
			get { return _resultValue; }
			set { _resultValue = value; }
		}

		public int ResponseCode
		{
			get { return _responseCode; }
			set { _responseCode = value; }
		}

		public int? EndPosition
		{
			get { return _endPosition; }
			set { _endPosition = value; }
		}

		public string RawResponse
		{
			get { return _rawResponse; }
			set { _rawResponse = value; }
		}

		public override string ToString()
		{
			return _rawResponse;
		}

		public static FastAGIResponse ParseResponse(string responseString)
		{
			Regex responseRegex = new Regex(@"^(?<ResponseCode>\d{3})\sresult=(?<ResultCode>[^\s$]*)(?:\s\((?<ResultPayload>[^)]*)\)){0,1}(?:\sendpos=(?<EndPosition>\d*)){0,1}$", RegexOptions.Singleline);
			Match responseMatch = responseRegex.Match(responseString);

			if (responseMatch.Success)
			{
				FastAGIResponse fastAgiResponse = new FastAGIResponse();

				fastAgiResponse._rawResponse = responseString;
				fastAgiResponse._responseCode = int.Parse(responseMatch.Groups["ResponseCode"].Value);
				fastAgiResponse._resultValue = responseMatch.Groups["ResultCode"].Value;

				if (responseMatch.Groups["ResultPayload"].Success)
					fastAgiResponse._payload = responseMatch.Groups["ResultPayload"].Value;

				if (responseMatch.Groups["EndPosition"].Success)
					fastAgiResponse._endPosition = int.Parse(responseMatch.Groups["EndPosition"].Value);

				return fastAgiResponse;
			}

			throw new ArgumentException("The response string argument was invalid.");
		}
	}
}
