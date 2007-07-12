using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk.AGICommand
{
	internal class EnableMusicCommand : BaseAGICommand
	{
		private bool _enabled;

		public EnableMusicCommand(bool enabled)
		{
			_enabled = enabled;
		}

		public bool Enabled
		{
			get { return _enabled;}
			set { _enabled = value;}
		}
	
		public override string GetCommand()
		{
			return string.Format("SET MUSIC {0}", _enabled ? "on" : "off");
		}

		public override bool IsSuccessfulResult(string result)
		{
			return result == "0";
		}
	}
}