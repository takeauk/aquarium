using ChipAquariumMobileService.Config;
using ChipAquariumMobileService.DataObjects;
using ChipAquariumMobileService.Models;
using System;
using System.Threading.Tasks;

namespace ChipAquariumMobileService.Alert
{
    public class WaterTemperatureOutOfRangeAlert : IAlert
    {
        private readonly WaterTemperature waterTemperature;
        private MailNotificationAlert alert;

        public WaterTemperatureOutOfRangeAlert(ChipAquariumMobileContext dbContext, WaterTemperature waterTemperature)
        {
            this.waterTemperature = waterTemperature;
            this.alert = new MailNotificationAlert(new WaterTemperatureOutOfRangeAlertDataAccess(dbContext, waterTemperature.AquariumId), 
                new WaterTemperatureOutOfRangeAlertMail(waterTemperature.AquariumId, waterTemperature.Temperature));
        }

        public bool ShouldRaise() => AppSettingsReference.NormalTemperatureRangeFrom <= waterTemperature.Temperature &&
                waterTemperature.Temperature <= AppSettingsReference.NormalTemperatureRangeTo;

        public void Clear()
        {
            alert.Clear();
        }

        public async Task RaiseAsync()
        {
            await alert.RaiseAsync();
        }
    }
}