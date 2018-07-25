using ChipAquariumMobileService.Alert;
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
                using (var dbContext = new ChipAquariumMobileContext())
                {
                    var alert = new WaterTemperatureOutOfRangeAlert(dbContext, waterTemperature);
                    if (alert.ShouldRaise())
                    {
                        await alert.RaiseAsync();
                    }
                    else
                    {
                        alert.Clear();
                    }

                    await dbContext.SaveChangesAsync();
                }
            });
        }
    }
}