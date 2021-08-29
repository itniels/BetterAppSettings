namespace ITNiels.BetterAppSettings
{
	/// <summary>
	/// Interface for DI to supply access to settings
	/// </summary>
	/// <typeparam name="T">Class that describe the json structure</typeparam>
	public interface IBetterAppSettings<T>
	{
		/// <summary>
		/// Access to the values
		/// </summary>
		T Values { get; }

		/// <summary>
		/// Writes changes to disk
		/// </summary>
		void Save();
	}
}
