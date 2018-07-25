using ChipAquariumMobileService.Config;
using ChipAquariumMobileService.DataObjects;
using ChipAquariumMobileService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ChipAquariumMobileService.Alert
{
    public class NotBeenReceivedAlert : IAlert
    {
        private readonly DateTimeOffset now;
        private readonly WaterTemperature waterTemperature;
        private readonly MailNotificationAlert alert;

        public NotBeenReceivedAlert(ChipAquariumMobileContext dbContext, WaterTemperature waterTemperature)
        {
            this.now = DateTimeOffset.Now;
            this.waterTemperature = waterTemperature;

            this.alert = new MailNotificationAlert(new WaterTemperatureOutOfRangeAlertDataAccess(dbContext, waterTemperature.AquariumId),
                new NotBeenReceivedAlertMail(waterTemperature.MeasurementAt));
        }

        public bool ShouldRaise() =>
            waterTemperature.MeasurementAt.Add(AppSettingsReference.ReceiveSpanPermissibleRangeTime) < now;

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