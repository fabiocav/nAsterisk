using System;
using System.Collections.Generic;
using System.Text;

namespace nAsterisk.AGICommand
{
	public class SayDateTimeCommand : BaseAGICommand, IProvideCommandResult
	{
		private string _format;
		private string _timeZone;

		private Digits _pressedDigit;
		private Digits _escapeDigits;
		private DateTime _time;

		public SayDateTimeCommand(DateTime time, Digits escapeDigits)
			: this(time, escapeDigits, null, null) { }

		public SayDateTimeCommand(DateTime time, Digits escapeDigits, string format, string timeZone)
		{
			_time = time;
			_escapeDigits = escapeDigits;
			_format = format;
			_timeZone = timeZone;
		}

		/// <summary>Gets or sets the format.</summary>
		/// <value>The format.</value>
		/// <remarks>
		/// 	<list type="table">
		/// 		<listheader>
		/// 			<term>Format specifier</term>
		/// 			<description>Description</description>
		/// 		</listheader>
		/// 		<item>
		/// 			<term>A or a</term>
		/// 			<description>Day of week (Saturday, Sunday, ...)</description>
		/// 		</item>
		/// 		<item>
		/// 			<term>B or b or h</term>
		/// 			<description>Month name (January, February, ...)</description>
		/// 		</item>
		/// 		<item>
		/// 			<term>d or e</term>
		/// 			<description>numeric day of month (first, second, ..., thirty-first)</description>
		/// 		</item>
		/// 		<item>
		/// 			<term>Y</term>
		/// 			<description>Year</description>
		/// 		</item>
		/// 		<item>
		/// 			<term>I or l</term>
		/// 			<description>Hour, 12 hour clock </description>
		/// 		</item>
		/// 		<item>
		/// 			<term>H</term>
		/// 			<description>Hour, 24 hour clock (single digit hours preceded by "oh")</description>
		/// 		</item>
		/// 		<item>
		/// 			<term>k</term>
		/// 			<description>Hour, 24 hour clock (single digit hours NOT preceded by "oh")</description>
		/// 		</item>
		/// 		<item>
		/// 			<term>M</term>
		/// 			<description>Minute, with 00 pronounced as "o'clock"</description>
		/// 		</item>
		/// 		<item>
		/// 			<term>N</term>
		/// 			<description>Minute, with 00 pronounced as "hundred" (US military time)</description>
		/// 		</item>
		/// 		<item>
		/// 			<term>P or p</term>
		/// 			<description>AM or PM </description>
		/// 		</item>
		/// 		<item>
		/// 			<term>Q</term>
		/// 			<description>"today", "yesterday" or ABdY (*note: not standard strftime value)</description>
		/// 		</item>
		/// 		<item>
		/// 			<term>q</term>
		/// 			<description>"" (for today), "yesterday", weekday, or ABdY (*note: not standard strftime value)</description>
		/// 		</item>
		/// 		<item>
		/// 			<term>R</term>
		/// 			<description>24 hour time, including minute</description>
		/// 		</item>
		/// 	</list>
		/// </remarks>
		public string Format
		{
			get { return _format; }
			set { _format = value; }
		}

		/// <summary>Gets or sets the escape digits.</summary>
		/// <value>The escape digits.</value>
		public Digits EscapeDigits
		{
			get { return _escapeDigits; }
			set { _escapeDigits = value; }
		}

		/// <summary>Gets or sets the time.</summary>
		/// <value>The time.</value>
		public DateTime Time
		{
			get { return _time; }
			set { _time = value; }
		}

		/// <summary>Gets or sets the time zone.</summary>
		/// <value>The time zone.</value>
		/// <remarks>For valid timezone's look in your Asterisk server's "/usr/share/zoneinfo" directory.</remarks>
		public string TimeZone
		{
			get { return _timeZone; }
			set { _timeZone = value; }
		}

		public override string GetCommand()
		{
			string commandFormat = "SAY DATETIME {0} {1} {2} {3}";

			if (string.IsNullOrEmpty(_format) && string.IsNullOrEmpty(_timeZone))
				commandFormat = "SAY DATETIME {0} {1}";
			else if (string.IsNullOrEmpty(_format) || string.IsNullOrEmpty(_timeZone))
				commandFormat = "SAY DATETIME {0} {1} {3}"; // TODO: This is going to need testing I'm not 100% sure asterisk will be able to tell if {3} is a timezone or a format.

			return string.Format(commandFormat, (_time - (new DateTime(1970,1,1,0,0,0))).TotalSeconds, AsteriskAgi.GetDigitsString(_escapeDigits), _format, _timeZone);
		}

		public override bool IsSuccessfulResult(string result)
		{
			int code = -1;
			int.TryParse(result, out code);

			if (code > 0)
				_pressedDigit = AsteriskAgi.GetDigitsFromString(((Char)code).ToString());

			return (code != -1);
		}

		public Digits GetResult()
		{
			return _pressedDigit;
		}

		#region IProvideCommandResult Members

		object IProvideCommandResult.GetResult()
		{
			return this.GetResult();
		}

		#endregion
	}
}
