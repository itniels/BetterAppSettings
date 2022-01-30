# BetterAppSettings
A better version of IOptions for .net core with the ability to save.

## Supported framework versions
- [ ] .NET 6.0 (Soon)
- [X] .NET 5.0

# Installation
Install latest NuGet from here: https://www.nuget.org/packages/ITNiels.BetterAppSettings/

# Usage
Please check out the sample app to get a complete overview.

## Using dependency Injection
It will be added like this, in Startup.cs. The method takes an optional `BetterAppSettingsOptions` object that contains all settings available.
```C#
public static IHostBuilder CreateHostBuilder(string[] args) =>
				Host.CreateDefaultBuilder(args)
						.ConfigureAppConfiguration((context, builder) => {
						})
						.ConfigureServices((context, services) => {
							services.AddHostedService<Worker>();

							// Adding BetterAppSettings with all options
							services.AddBetterAppSettings<ConfigModel>(new BetterAppSettingsOptions {
								Section = "Application",
								Filename = "appsettings.json",
								WorkingDirectory = null,
								SerializerSettings = null,
							});
						});
```

## Add a config class
Default values below will be writting to the json on first save.
```C#
public class ConfigModel
	{
		public string Name { get; set; } = "Default Name";
		public int Counter { get; set; } = 5;
		public DateTime? Time { get; set; }
		public IList<StorageItem> Items { get; set; } = new List<StorageItem>();
	}
```

## Accessing and writting values
Here is a complete implementation
```C#
public class Worker
{
  private readonly IBetterAppSettings<ConfigModel> _settings;

  public Worker(IBetterAppSettings<ConfigModel> storage)
  {
    _settings = storage;
  }

  public void Sample()
  {
    // Access value
    var val = _settings.Values.Name;

    // Change value in memory
    _settings.Values.Name = "New name";

    // Save values to disk
    _settings.Save();
  }
}
```
