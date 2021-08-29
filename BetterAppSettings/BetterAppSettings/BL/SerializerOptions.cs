using Newtonsoft.Json;

namespace ITNiels.BetterAppSettings
{
	public static class SerializerOptions
	{
		public static JsonSerializerSettings JsonSerializerSettings { get; private set; }

		static SerializerOptions()
		{
			// Set defaults
			JsonSerializerSettings = new JsonSerializerSettings { };
		}

		/// <summary>
		/// Override the default settings with a custom JsonSerializerSettings object
		/// </summary>
		/// <param name="options"></param>
		public static void OverrideSettings(JsonSerializerSettings options)
		{
			JsonSerializerSettings = options;
		}
	}
}