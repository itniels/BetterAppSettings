using Newtonsoft.Json;

namespace ITNiels.BetterAppSettings
{
	public class BetterAppSettingsOptions
	{
		/// <summary>
		/// Defines the section to read/write from. <br />
		/// Default: If no section is defined the whole file will be used
		/// </summary>
		public string Section { get; set; }

		/// <summary>
		/// Filename of the settings json. <br />
		/// Default: 'appsettings.json' if left empty
		/// </summary>
		public string Filename { get; set; } = "appsettings.json";

		/// <summary>
		/// The directory where the settings file is located. <br />
		/// Default: Environment.ContentRoot
		/// </summary>
		public string WorkingDirectory { get; set; }

		/// <summary>
		/// Json options for serialization can be overriden here. <br />
		/// Default: Using default JsonConvert settings
		/// </summary>
		public JsonSerializerSettings SerializerSettings { get; set; }
	}
}
