using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk.AGICommand
{
	public interface ISupportCommandResponse
	{
		void ProcessResponse(FastAGIResponse response);
	}
}
