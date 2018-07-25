using System;
using Microsoft.WindowsAzure.Mobile.Service;

namespace ChipAquariumMobileService.DataObjects
{
	public class WaterTemperatureAlert : EntityData
	{
		public int AquariumId { get; set; }

		public bool Active { get; set; }

		public DateTimeOffset ActivateAt { get; set;}

        public DateTimeOffset NotifyAt { get; set; }
	}
}

