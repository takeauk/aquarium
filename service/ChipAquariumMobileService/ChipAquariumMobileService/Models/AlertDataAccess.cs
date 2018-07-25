using ChipAquariumMobileService.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChipAquariumMobileService.Models
{
    public class AlertDataAccess : IAlertDataAccess
    {
        private readonly DataObjects.Alert alert;

        public AlertDataAccess(ChipAquariumMobileContext dbContext, int aquariumId, Guid alertId)
        {
            this.alert = dbContext.WaterTemperatureAlerts.SingleOrDefault(x => x.AquariumId == aquariumId && x.AlertId == alertId);
            if (this.alert == null)
            {
                this.alert = new DataObjects.Alert()
                {
                    AquariumId = aquariumId,
                    AlertId = alertId
                };

                dbContext.WaterTemperatureAlerts.Add(this.alert);
            }
        }

        public void Activate()
        {
            alert.Active = true;
            alert.ActivateAt = DateTimeOffset.Now;
        }

        public void Deactivate()
        {
            alert.Active = false;
        }

        public void Notify()
        {
            alert.NotifyAt = DateTimeOffset.Now;
        }

        public bool HasExceededNotifyAt(DateTimeOffset value) => alert.NotifyAt < value;
    }
}