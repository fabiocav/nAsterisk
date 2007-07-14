using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk.AGICommand
{
	internal class SendImageCommand : AGINoReturnCommandBase
	{
		private string _image;

		public SendImageCommand(string image)
		{
			_image = image;
		}

		public string Image
		{
			get { return _image; }
			set { _image = value; }
		}
	
		public override string GetCommand()
		{
			return string.Format("SEND IMAGE {0}", _image);
		}

		public override void ProcessResponse(FastAGIResponse response)
		{
			if (response.ResultValue == "-1")
				throw new AsteriskException("SendImage Command Failed.");
		}
	}
}
