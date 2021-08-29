using System;
using System.Threading;
using System.Threading.Tasks;
using BetterAppSettings.Sample.BL;
using ITNiels.BetterAppSettings;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BetterAppSettings.Sample
{
	public class Worker : BackgroundService
	{
		private readonly ILogger<Worker> _logger;
		private readonly IBetterAppSettings<ConfigModel> _settings;

		public Worker(ILogger<Worker> logger, IBetterAppSettings<ConfigModel> storage)
		{
			_logger = logger;
			_settings = storage;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			while (!stoppingToken.IsCancellationRequested)
			{
				// Read a value
				_logger.LogInformation($"Count: {_settings.Values.Counter}");

				// Set values
				_settings.Values.Time = DateTime.UtcNow;
				_settings.Values.Counter++;

				if (_settings.Values.Items.Count < 5)
					_settings.Values.Items.Add(new StorageItem { Id = _settings.Values.Counter, Name = "Yay an item!" });

				// Save settings to disk
				_settings.Save();

				// Log out values
				_logger.LogInformation($"Time: {_settings.Values.Time} | Count: {_settings.Values.Counter} | Items: {_settings.Values.Items?.Count}");

				// Wait for 1 second
				await Task.Delay(TimeSpan.FromSeconds(1));
			}
		}
	}
}
