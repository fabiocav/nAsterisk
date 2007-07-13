using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk.AGICommand
{
	internal class SendImageCommand : BaseAGICommand
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

		public override bool IsSuccessfulResult(string result)
		{
			return result == "0";
		}
	}
}
