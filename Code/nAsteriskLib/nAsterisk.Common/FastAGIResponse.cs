using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace nAsterisk
{
	public class FastAGIResponse
	{
		private int _responseCode;
		private string _resultValue;
		private string _payload;

		private FastAGIResponse()
		{}

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

		public static FastAGIResponse ParseResponse(string responseString)
		{
			Regex responseRegex = new Regex(@"^(?<ResponseCode>\d{3})\sresult=(?<ResultCode>[^\s$]*)(?:\s\((?<ResultPayload>[^)]*)\)){0,1}(?:\sendpos=(?<EndPosition>\d*)){0,1}$", RegexOptions.Singleline);

			if (responseRegex.IsMatch(responseString))
			{
				FastAGIResponse fastAgiResponse = new FastAGIResponse();

				Match responseMatch = responseRegex.Match(responseString);

				fastAgiResponse._responseCode = int.Parse(responseMatch.Groups["ResponseCode"].Value);
				fastAgiResponse._resultValue = responseMatch.Groups["ResultCode"].Value;
				
				if (responseMatch.Groups["ResultPayload"] != null)
					fastAgiResponse._payload = responseMatch.Groups["ResultPayload"].Value;

				return fastAgiResponse;
			}

			throw new ArgumentException("The response string argument was invalid.");
		}
	}
}
