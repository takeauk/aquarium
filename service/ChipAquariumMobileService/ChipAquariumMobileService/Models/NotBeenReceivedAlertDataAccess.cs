using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChipAquariumMobileService.Models
{
    public class NotBeenReceivedAlertDataAccess : IAlertDataAccess
    {
        private static readonly Guid alertId = new Guid("8B17C079-719C-4921-B1AA-37B615BB16FA");
        private readonly AlertDataAccess dataAccess;

        public NotBeenReceivedAlertDataAccess(ChipAquariumMobileContext dbContext, int aquariumId)
        {
            this.dataAccess = new AlertDataAccess(dbContext, aquariumId, alertId);
        }

        public void Activate() => dataAccess.Activate();

        public void Deactivate() => dataAccess.Deactivate();

        public bool HasExceededNotifyAt(DateTimeOffset value) => dataAccess.HasExceededNotifyAt(value);

        public void Notify() => dataAccess.Notify();
    }
}