using System;
using System.Configuration;

namespace ChipAquariumMobileService.Config
{
	public class WaterTemperatureSettings : ConfigurationSection
	{
        [ConfigurationProperty("normalRange")]
        public WaterTemperatureNormalRange NormalRange
		{
			get
			{
				return (WaterTemperatureNormalRange)base["normalRange"];
			}

			set
			{
				base["normalRange"] = value;
			}
		}

        [ConfigurationProperty("alert")]
        public WaterTemperatureAlertSettings Alert
        {
			get
			{
				return (WaterTemperatureAlertSettings)base["alert"];
			}

			set
			{
				base["alert"] = value;
			}
		}

        private static WaterTemperatureSettings settings;

        public static WaterTemperatureSettings Get()
		{
            if(settings == null)
            {
                settings = ConfigurationManager.GetSection("waterTemperature") as WaterTemperatureSettings;
                if (settings == null) throw new InvalidOperationException("Could not load the water temperature settings.");
            }

            return settings;
		}
	}
}

