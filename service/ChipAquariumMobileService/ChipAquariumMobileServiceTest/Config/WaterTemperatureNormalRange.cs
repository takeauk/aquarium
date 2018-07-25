using System.Configuration;

namespace ChipAquariumMobileService.Config
{
	public class WaterTemperatureNormalRange : ConfigurationElement
	{
		[ConfigurationProperty("from")]
		public double From
		{
			get
			{
				return (double)base["from"];
			}

			set
			{
				base["from"] = value;
			}
		}

		[ConfigurationProperty("to")]
		public double To
		{
			get
			{
				return (double)base["to"];
			}

			set
			{
				base["to"] = value;
			}
		}
	}
}

