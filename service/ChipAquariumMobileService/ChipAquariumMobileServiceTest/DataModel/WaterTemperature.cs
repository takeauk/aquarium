using System;
using System.Runtime.Serialization;

namespace ChipAquariumMobileServiceTest.DataModel
{
    [DataContract]
    public class WaterTemperature
    {
        [DataMember(Name = "aquariumId")]
        public int AquariumId { get; set; }

        [DataMember(Name = "measurementAt")]
        public DateTime MeasurementAtInternal { get; set; }

        public DateTimeOffset MeasurementAt
        {
            set { MeasurementAtInternal = value.DateTime; }
            get { return new DateTimeOffset(MeasurementAtInternal); }
        }

        [DataMember(Name = "temperature")]
        public float Temperature { get; set; }

        public override string ToString()
        {
            return string.Format("AquariumId:{0}", AquariumId) +
                string.Format(", MeasurementAt:{0}", MeasurementAt) +
                string.Format(", Temperature:{0}", Temperature);
        }
    }
}