using Microsoft.WindowsAzure.Mobile.Service;
using System.Linq;
using System.Threading.Tasks;
using ChipAquariumMobileService.Models;
using ChipAquariumMobileService.DataObjects;
using Microsoft.WindowsAzure.Mobile.Service.ScheduledJobs;
using System.Threading;
using System.Collections.Generic;
using ChipAquariumMobileService.Config;
using System;
using ChipAquariumMobileService.Alert;

namespace ChipAquariumMobileService.ScheduledJobs
{
    public class WaterTemperatureMonitor : ScheduledJob
    {
        private ChipAquariumMobileContext dbContext;

        protected override void Initialize(ScheduledJobDescriptor scheduledJobDescriptor,
            CancellationToken cancellationToken)
        {
            base.Initialize(scheduledJobDescriptor, cancellationToken);

            dbContext = new ChipAquariumMobileContext();
        }

        public override async Task ExecuteAsync()
        {
            foreach (var waterTemperature in SelectWaterTemperatures())
            {
                foreach(var alert in CreateAlerts(waterTemperature))
                {
                    if(alert.ShouldRaise())
                    {
                        await alert.RaiseAsync();
                        break;
                    }

                    alert.Clear();
                }
            }

            await dbContext.SaveChangesAsync();
        }

        private IReadOnlyCollection<WaterTemperature> SelectWaterTemperatures() => dbContext.WaterTemperatures.
                GroupBy(x => x.AquariumId).Select(group => group.OrderByDescending(x => x.CreatedAt).FirstOrDefault()).ToList().AsReadOnly();

        private IEnumerable<IAlert> CreateAlerts(WaterTemperature waterTemperature)
        {
            yield return new NotBeenReceivedAlert(dbContext, waterTemperature);
            yield return new WaterTemperatureOutOfRangeAlert(dbContext, waterTemperature);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                dbContext.Dispose();
            }
        }
    }
}