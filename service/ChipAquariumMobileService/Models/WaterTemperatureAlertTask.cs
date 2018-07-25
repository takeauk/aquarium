using ChipAquariumMobileService.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChipAquariumMobileService.Models
{
    public class WaterTemperatureAlertTask
    {
        private WaterTemperatureAlert alert;

        public WaterTemperatureAlertTask(WaterTemperatureAlert alert)
        {
            this.alert = alert;
        }

        public void Activate()
        {
            this.alert.Active = true;
            this.alert.ActivateAt = DateTimeOffset.Now;
        }

        public void Disactivate()
        {
            this.alert.Active = false;
        }

        public void SendMail(float temperature)
        {
            var mail = new WaterTemperatureAlertMail(alert.AquariumId, temperature);
            mail.SendAsync(new MailTransport()).Wait();

            alert.NotifyAt = DateTimeOffset.Now;
        }
    }
}