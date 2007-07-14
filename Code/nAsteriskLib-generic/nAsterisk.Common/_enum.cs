using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk
{
	public enum ChannelStatus
	{
		Available		= 0,
		Reserved		= 1,
		OffHook			= 2,
		Dialed			= 3,
		LocalRinging	= 4,
		RemoteRinging	= 5,
		Up				= 6,
		Busy			= 7
	}

	public enum AsteriskVerboseLevel
	{
		Error = 1,
		Info,
		Trace,
		Debug
	}
}
