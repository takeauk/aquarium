using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChipAquariumMobileService.Models
{
    public class RenotifyAlertDateTime
    {
        private readonly DateTimeOffset offset;

        public RenotifyAlertDateTime(DateTimeOffset source, TimeSpan elapsed)
        {
            offset = source.Add(elapsed);
        }

        public DateTimeOffset Offset
        {
            get
            {
                return offset;
            }
        }
    }
}