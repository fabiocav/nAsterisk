using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk.Common
{
	public class AGIContext
	{
		private static AGIContext _context;


		private IAsteriskAGI _agi;

		internal AGIContext(IAsteriskAGI agi)
		{
			_agi = agi;
		}

		public static AGIContext Current
		{
			get { return _context; }
			internal set { _context = value; }
		}

		public IAsteriskAGI AGI
		{
			get { return _agi; }
		}
	}
}
