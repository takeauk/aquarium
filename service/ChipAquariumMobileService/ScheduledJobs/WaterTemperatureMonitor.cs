using Microsoft.WindowsAzure.Mobile.Service;
using System.Linq;
using System.Threading.Tasks;
using ChipAquariumMobileService.Models;
using ChipAquariumMobileService.DataObjects;
using Microsoft.WindowsAzure.Mobile.Service.ScheduledJobs;
using System.Threading;
using System.Collections.Generic;
using ChipAquariumMobileService.Config;

namespace ChipAquariumMobileService.ScheduledJobs
{
    public class WaterTemperatureMonitor : ScheduledJob
    {
        private ChipAquariumMobileContext context;

        protected override void Initialize(ScheduledJobDescriptor scheduledJobDescriptor,
            CancellationToken cancellationToken)
        {
            base.Initialize(scheduledJobDescriptor, cancellationToken);

            context = new ChipAquariumMobileContext();
        }

        public override async Task ExecuteAsync()
        {
            foreach (var item in SelectAlertWaterTemperatures().ToArray())
            {
                ExecuteCore(item);
            }

            await context.SaveChangesAsync();
        }

        private void ExecuteCore(WaterTemperature waterTemperature)
        {
            var alert = FindAlert(waterTemperature.AquariumId);
            if (alert == null || !alert.Active) return;

            if (!ShouldReNotify(alert, waterTemperature)) return;

            var task = new WaterTemperatureAlertTask(alert);
            task.Activate();
            task.SendMail(waterTemperature.Temperature);
        }

        private IEnumerable<WaterTemperature> SelectAlertWaterTemperatures()
        {
            return context.WaterTemperatures.
                GroupBy(x => x.AquariumId).Select(group => group.OrderByDescending(x => x.CreatedAt).FirstOrDefault())
                .Where(x => !new IncludeWaterTemperatureNormalRange(x.Temperature).Test());
        }

        private WaterTemperatureAlert FindAlert(int aquariumId)
        {
            return context.WaterTemperatureAlerts.SingleOrDefault(x => x.AquariumId == aquariumId);
        }

        private bool ShouldReNotify(WaterTemperatureAlert alert, WaterTemperature temperature)
        {
            var renotifyAlertDateTime = new RenotifyAlertDateTime(alert.NotifyAt, AppNameValueSetting.AlertRenotifyElapsedTime);
            return renotifyAlertDateTime.Offset < temperature.UpdatedAt;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                context.Dispose();
            }
        }
    }
}