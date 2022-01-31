using System;

namespace ITNiels.BetterAppSettings.BL.Exceptions
{
	public class BetterAppSettingsException : Exception
	{
		public BetterAppSettingsException(string message) : base(message) { }

		public BetterAppSettingsException(string message, Exception ex) : base(message, ex) { }
	}
}
