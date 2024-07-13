using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeasurementUnit
{

    /// <summary>
    /// Struct representing a length value.
    /// </summary>
    [System.Serializable]
    public struct Length
    {

        /// <summary>
        /// Length units.
        /// </summary>
        public enum Unit { Metres, Yards }

        /// <summary>
        /// Metre to yard conversion factor.
        /// </summary>
        public const float METRE_TO_YARD = 1.093613f;

        /// <summary>
        /// Metre to yard conversion factor.
        /// </summary>
        public const float YARD_TO_METRE = 0.9144f;

        /// <summary>
        /// The current value of the length, in metres.
        /// </summary>
        [SerializeField] private float value;

#if UNITY_EDITOR
        // Needed for custom drawer
        [SerializeField] private float unitValue;
        [SerializeField] private Unit unit;
#endif

        public Length(float value, Unit unit)
        {
            this.value = unit == Unit.Metres ? value : value * METRE_TO_YARD;

#if UNITY_EDITOR
            this.unitValue = this.value;
            this.unit = unit;
#endif
        }

        public static Length operator +(Length a) => a;
        public static Length operator -(Length a) => new(-a.value, Unit.Metres);

        public static Length operator +(Length a, Length b) => new(a.value + b.value, Unit.Metres);
        public static Length operator -(Length a, Length b) => new(a.value - b.value, Unit.Metres);
        // public static Area operator *(Length a, Length b) => new(Math.Abs(a.value * b.value), Area.Unit.SquareMetres);
        // public static Speed operator /(Length a, float t) => new(a.value / t, Speed.Unit.MetresPerSecond);

        public static bool operator ==(Length a, Length b) => a.value == b.value;
        public static bool operator !=(Length a, Length b) => a.value != b.value;
        public static bool operator >(Length a, Length b) => a.value > b.value;
        public static bool operator <(Length a, Length b) => a.value < b.value;
        public static bool operator >=(Length a, Length b) => a.value >= b.value;
        public static bool operator <=(Length a, Length b) => a.value <= b.value;

        /// <summary>
        /// Returns the length in the SI measurement unit.
        /// </summary>
        /// <returns>Value of the length in the SI measurement unit.</returns>
        public readonly float Get() => value;

        /// <summary>
        /// Returns the length in the given measurement unit.
        /// </summary>
        /// <param name="unit">Desired unit.</param>
        /// <returns>Value of the length.</returns>
        public readonly float Get(Unit unit)
        {
            if (unit == Unit.Yards)
                return value * METRE_TO_YARD;

            return value;
        }

        /// <summary>
        /// Sets the value of the length.
        /// </summary>
        /// <param name="value">New value.</param>
        /// <param name="unit">New unit.</param>
        public void Set(float value, Unit unit)
        {
            this.value = unit == Unit.Metres ? value : value * YARD_TO_METRE;
        }

        public override readonly string ToString() => ToString(Unit.Metres);

        /// <summary>
        /// Returns the fully qualified type name of this instance, in the desired measurement unit.
        /// </summary>
        /// <param name="unit">The desired unit.</param>
        /// <returns>The fully qualified type name.</returns>
        public readonly string ToString(Unit unit) => Get(unit) + " " + (unit == Unit.Metres ? "m" : "y");

        // public override readonly int GetHashCode() => Hashco.Combine(value);

#nullable disable
        public override readonly bool Equals(object obj)
        {
            if (obj == null || obj is not Length b)
                return false;

            return value == b.value;
        }
#nullable restore


    }

}
