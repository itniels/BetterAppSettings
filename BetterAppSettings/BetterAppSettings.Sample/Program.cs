using BetterAppSettings.Sample.BL;
using ITNiels.BetterAppSettings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BetterAppSettings.Sample
{
	public class Program
	{
		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
				Host.CreateDefaultBuilder(args)
						.ConfigureAppConfiguration((context, builder) => {
						})
						.ConfigureServices((context, services) => {
							services.AddHostedService<Worker>();

							// Adding betterAppSettings with all options
							services.AddBetterAppSettings<ConfigModel>(new BetterAppSettingsOptions {
								Section = "Application",
								Filename = "appsettings.json",
								WorkingDirectory = null,
								SerializerSettings = null,
							});
						});
	}
}
