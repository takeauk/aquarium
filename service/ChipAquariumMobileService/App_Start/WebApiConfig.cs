using ChipAquariumMobileService.Models;
using Microsoft.WindowsAzure.Mobile.Service;
using System.Data.Entity;
using System.Web.Http;

namespace ChipAquariumMobileService
{
    public static class WebApiConfig
    {
        public static void Register()
        {
            ConfigOptions options = new ConfigOptions();

            HttpConfiguration config = ServiceConfig.Initialize(new ConfigBuilder(options));

            config.Formatters.JsonFormatter.SerializerSettings.DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Include;
            config.Formatters.JsonFormatter.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Include;
            
            Database.SetInitializer(new AquariumMobileInitializer());
        }
    }
}

