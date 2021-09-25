using ITNiels.BetterAppSettings;
using Microsoft.Extensions.Hosting;

namespace Microsoft.Extensions.DependencyInjection
{
	public static class ServiceCollectionExtensions
	{
		/// <summary>
		/// Adds BetterAppSettings to DI
		/// </summary>
		/// <typeparam name="T">Class that represent the json structure</typeparam>
		public static void AddBetterAppSettings<T>(this IServiceCollection services) where T : class, new()
			=> ConfigureBetterAppSettings<T>(services, new BetterAppSettingsOptions());

		/// <summary>
		/// Adds BetterAppSettings to DI
		/// </summary>
		/// <typeparam name="T">Class that represent the json structure</typeparam>
		/// <param name="options">An options object with all possible configuration properties</param>
		public static void AddBetterAppSettings<T>(this IServiceCollection services, BetterAppSettingsOptions options) where T : class, new()
			=> ConfigureBetterAppSettings<T>(services, options);

		/// <summary>
		/// Creates the instance and adds it to DI
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="services"></param>
		/// <param name="section"></param>
		/// <param name="filename"></param>
		/// <param name="workingDirectory"></param>
		private static void ConfigureBetterAppSettings<T>(this IServiceCollection services, BetterAppSettingsOptions options) where T : class, new()
		{
			// Set custom serializer settings
			if (options.SerializerSettings != null)
				SerializerOptions.OverrideSettings(options.SerializerSettings);

			// Create instance for DI
			services.AddSingleton<IBetterAppSettings<T>>(provider => {
				var environment = provider.GetService<IHostEnvironment>();

				return new BetterAppSettings<T>(environment, options);
			});
		}
	}
}
