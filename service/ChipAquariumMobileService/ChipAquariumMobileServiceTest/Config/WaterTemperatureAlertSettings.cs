using System;
using System.Configuration;

namespace ChipAquariumMobileService.Config
{
	public class WaterTemperatureAlertSettings : ConfigurationElement
	{
        [ConfigurationProperty("renotifyElapsedTime")]
        public TimeSpan RenotifyElapsedTime
        {
            get
            {
                return (TimeSpan)base["renotifyElapsedTime"];
            }

            set
            {
                base["renotifyElapsedTime"] = value;
            }
        }

        [ConfigurationProperty("notifyMailAddress")]
        public string NotifyMailAddress
        {
            get
            {
                return (string)base["notifyMailAddress"];
            }

            set
            {
                base["notifyMailAddress"] = value;
            }
        }
    }
}

