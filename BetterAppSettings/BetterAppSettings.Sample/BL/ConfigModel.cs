using System;
using System.Collections.Generic;

namespace BetterAppSettings.Sample.BL
{
	public class ConfigModel
	{
		public string Name { get; set; } = "Default Name";
		public int Counter { get; set; } = 5;
		public DateTime? Time { get; set; }
		public IList<StorageItem> Items { get; set; } = new List<StorageItem>();
	}
}
