using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using Microsoft.WindowsAzure.Mobile.Service;
using Microsoft.WindowsAzure.Mobile.Service.Tables;
using ChipAquariumMobileService.DataObjects;

namespace ChipAquariumMobileService.Models
{
    public class ChipAquariumMobileContext : DbContext
    {
        private const string connectionStringName = "Name=MS_TableConnectionString";

        public ChipAquariumMobileContext() : base(connectionStringName)
        {
        } 

        public DbSet<WaterTemperature> WaterTemperatures { get; set; }

		public DbSet<DataObjects.Alert> WaterTemperatureAlerts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            string schema = ServiceSettingsDictionary.GetSchemaName();
            if (!string.IsNullOrEmpty(schema))
            {
                modelBuilder.HasDefaultSchema(schema);
            }

            modelBuilder.Conventions.Add(
                new AttributeToColumnAnnotationConvention<TableColumnAttribute, string>(
                    "ServiceTableColumn", (property, attributes) => attributes.Single().ColumnType.ToString()));
        }
    }

}
