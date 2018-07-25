using Microsoft.WindowsAzure.Mobile.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChipAquariumMobileService.Models
{
    public class AquariumMobileInitializer : ClearDatabaseSchemaIfModelChanges<ChipAquariumMobileContext>
    {
    }
}