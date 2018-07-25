using Microsoft.WindowsAzure.Mobile.Service;
using System;

namespace ChipAquariumMobileService.DataObjects
{
    public class WaterTemperature : EntityData
    {
        public int AquariumId { get; set; }

        public DateTimeOffset MeasurementAt { get; set; }

        public float Temperature { get; set; }
    }
}