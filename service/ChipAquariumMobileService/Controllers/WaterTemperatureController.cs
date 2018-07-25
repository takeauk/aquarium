using ChipAquariumMobileService.DataObjects;
using ChipAquariumMobileService.Models;
using Microsoft.WindowsAzure.Mobile.Service;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace ChipAquariumMobileService.Controllers
{
    public class WaterTemperaturesController : TableController<WaterTemperature>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);

            var context = new ChipAquariumMobileContext();
            DomainManager = new EntityDomainManager<WaterTemperature>(context, Request, Services);
        }

        public IQueryable<WaterTemperature> GetAllWaterTemperatures()
        {
            return Query();
        }

        public async Task<IHttpActionResult> PostWaterTemperature(WaterTemperature item)
        {
            WaterTemperature current = await InsertAsync(item);

            ExecuteAlertTaskAsync(current);

            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        private void ExecuteAlertTaskAsync(WaterTemperature waterTemperature)
        {
            Task.Run(async () =>
            {
                using (var context = new ChipAquariumMobileContext())
                {
                    var includeWaterTemperatureNormalRange = new IncludeWaterTemperatureNormalRange(waterTemperature.Temperature).Test();

                    var alert = context.WaterTemperatureAlerts.SingleOrDefault(x => x.AquariumId == waterTemperature.AquariumId);

                    if (includeWaterTemperatureNormalRange)
                    {
                        if (alert == null || !alert.Active) return;

                        var task = new WaterTemperatureAlertTask(alert);
                        task.Disactivate();
                    }
                    else
                    {
                        if (alert != null && alert.Active) return;

                        var newAlert = new WaterTemperatureAlert()
                        {
                            Id = waterTemperature.Id,
                            AquariumId = waterTemperature.AquariumId
                        };

                        var task = new WaterTemperatureAlertTask(newAlert);
                        task.Activate();
                        task.SendMail(waterTemperature.Temperature);
                    }

                    await context.SaveChangesAsync();
                }
            });
        }
    }
}