using ChipAquariumMobileService.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChipAquariumMobileService.Models
{
    public class IncludeWaterTemperatureNormalRange
    {
        private float temperature;

        public IncludeWaterTemperatureNormalRange(float temperature)
        {
            this.temperature = temperature;
        }

        public bool Test()
        {
            return AppNameValueSetting.NormalTemperatureRangeFrom <= temperature && 
                temperature <= AppNameValueSetting.NormalTemperatureRangeTo;
        }
    }
}