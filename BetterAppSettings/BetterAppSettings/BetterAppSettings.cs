using System;
using System.IO;
using ITNiels.BetterAppSettings.BL.Exceptions;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ITNiels.BetterAppSettings
{
	public class BetterAppSettings<T> : IBetterAppSettings<T> where T : class, new()
	{
		private static object _lockObj = new object();
		private readonly T _values;
		private readonly IHostEnvironment _environment;
		private readonly string _section;
		private readonly string _file;
		private readonly string _workingDirectory;
		private readonly string _settingsPath;

		public BetterAppSettings(IHostEnvironment environment, BetterAppSettingsOptions options) : base()
		{
			try
			{
				_environment = environment;
				_section = options.Section;
				_file = options.Filename;
				_workingDirectory = options.WorkingDirectory;

				// Sanity checks
				if (_environment is null) throw new Exception("IHostEnvironment cannot be null");
				if (string.IsNullOrEmpty(_file)) throw new Exception("Filename cannot be null or empty");

				var fileProvider = _environment.ContentRootFileProvider;
				var fileInfo = fileProvider.GetFileInfo(_file);
				var physicalPath = string.IsNullOrWhiteSpace(_workingDirectory) ? fileInfo.PhysicalPath : Path.Combine(_workingDirectory, _file);

				// Path
				_settingsPath = physicalPath;

				// Try read file to memory or create a new instance
				_values = LoadJsonFromFile();
			}
			catch (Exception ex)
			{
				throw new BetterAppSettingsException("Unable to initialize BetterAppSettings", ex);
			}
		}

		/// <summary>
		/// Get values from memory cache
		/// </summary>
		public T Values => _values;

		/// <summary>
		/// Write to disk only values in T
		/// </summary>
		public void Save()
		{
			try
			{
				// Make sure we only save one at a time
				lock (_lockObj)
				{
					var jObj = JsonConvert.DeserializeObject<JObject>(File.ReadAllText(_settingsPath), SerializerOptions.JsonSerializerSettings);
					var sectionObject = jObj.TryGetValue(_section, out JToken section) ? JsonConvert.DeserializeObject<T>(section.ToString(), SerializerOptions.JsonSerializerSettings) : (Values ?? new T());
					var newSection = JObject.Parse(JsonConvert.SerializeObject(sectionObject));

					if (!string.IsNullOrWhiteSpace(_section))
						jObj[_section] = JObject.Parse(JsonConvert.SerializeObject(_values));
					else
						jObj = JObject.Parse(JsonConvert.SerializeObject(_values));

					File.WriteAllText(_settingsPath, JsonConvert.SerializeObject(jObj, Formatting.Indented));
				}
			}
			catch (Exception ex)
			{
				throw new BetterAppSettingsException("Unable to save settings to disk!", ex);
			}
		}

		/// <summary>
		/// Loads the content of the Json file to _values on load <br />
		/// If file foes not exist a new object will be instantated with default values instead
		/// </summary>
		private T LoadJsonFromFile()
		{
			try
			{
				var json = File.ReadAllText(_settingsPath);

				// If we read the whole file
				if (string.IsNullOrWhiteSpace(_section))
					return JsonConvert.DeserializeObject<T>(json, SerializerOptions.JsonSerializerSettings);

				// If we are only reading a section
				var jObject = JsonConvert.DeserializeObject<JObject>(File.ReadAllText(_settingsPath), SerializerOptions.JsonSerializerSettings);

				if (!jObject.TryGetValue(_section, out JToken section))
					throw new Exception("Unable to parse BetterAppSettings section");

				return JsonConvert.DeserializeObject<T>(section.ToString(), SerializerOptions.JsonSerializerSettings);
			}
			// We accept that a fiel might not exist yet, and we return an empty object
			catch (FileNotFoundException)
			{
				return new T();
			}
			// For all other cases we throw an exception
			catch (Exception ex)
			{
				throw new BetterAppSettingsException("Unable to load json from disk", ex);
			}
		}
	}
}
