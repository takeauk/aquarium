using System;
using System.Collections.Specialized;
using System.Web.Configuration;

namespace ChipAquariumMobileService.Config
{
    public class AppNameValueSetting
    {
        private static readonly NameValueCollection items = WebConfigurationManager.AppSettings;

        public static float NormalTemperatureRangeFrom
        {
            get { return Convert.ToSingle(items["NormalTemperatureRangeFrom"]); }
        }

        public static float NormalTemperatureRangeTo
        {
            get { return Convert.ToSingle(items["NormalTemperatureRangeTo"]); }
        }

        public static TimeSpan AlertRenotifyElapsedTime
        {
            get { return TimeSpan.Parse(items["AlertRenotifyElapsedTime"]); }
        }

        public static string AlertMailToAddress
        {
            get { return items["AlertMailToAddress"]; }
        }

        public static string AlertMailFromAddress
        {
            get { return items["AlertMailFromAddress"]; }
        }

        public static string AlertMailFromDisplayName
        {
            get { return items["AlertMailFromDisplayName"]; }
        }

        public static string SendGridApiKey
        {
            get { return items["SendGridApiKey"]; }
        }
    }
}