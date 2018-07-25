using ChipAquariumMobileService.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChipAquariumMobileService.Models
{
    public class WaterTemperatureOutOfRangeAlertDataAccess : IAlertDataAccess
    {
        private static readonly Guid alertId = new Guid("4942CD57-4CDE-411C-9A18-D216B9C86DD5");
        private readonly AlertDataAccess dataAccess;

        public WaterTemperatureOutOfRangeAlertDataAccess(ChipAquariumMobileContext dbContext, int aquariumId)
        {
            dataAccess = new AlertDataAccess(dbContext, aquariumId, alertId);
        }

        public void Activate() => dataAccess.Activate();

        public void Deactivate() => dataAccess.Deactivate();

        public bool HasExceededNotifyAt(DateTimeOffset value) => dataAccess.HasExceededNotifyAt(value);

        public void Notify() => dataAccess.Notify();
    }
}