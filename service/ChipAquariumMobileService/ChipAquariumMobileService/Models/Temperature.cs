using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChipAquariumMobileService.Models
{
    public struct Temperature : IEquatable<Temperature>
    {
        private readonly float value;

        private Temperature(float value)
        {
            this.value = value;
        }

        public override bool Equals(object obj) => obj is Temperature ? this.Equals((Temperature)obj) : false;

        public override int GetHashCode() => value.GetHashCode();

        public static bool operator ==(Temperature l, Temperature r) => l.Equals(r);

        public static bool operator !=(Temperature l, Temperature r) => !l.Equals(r);

        public bool Equals(Temperature other) => value == other.value;

        public Temperature FromDegreeCelsius(float value)
        {
            return new Temperature(value + 273.15f);
        }
    }
}