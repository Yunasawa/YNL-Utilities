using System;

namespace YNL.Extensions.Addons
{
    [System.Serializable]
    public struct MRange : IEquatable<MRange>
    {
        public float Min;
        public float Max;

        public float Average => (Min + Max) / 2;
        public float Distance => Max - Min;

        public MRange(float min, float max)
        {
            Min = min;
            Max = max;
        }

        public bool InRange(float number, bool equalLeft = true, bool equalRight = true)
        {
            if (equalLeft && !equalRight) return number >= Min && number < Max;
            if (!equalLeft && equalRight) return number > Min && number <= Max;
            if (equalLeft && equalRight) return number >= Min && number <= Max;
            return number > Min && number < Max;
        }

        public bool InOpenInterval(float number) => InRange(number, false, false); // (a, b)
        public bool InCloseInterval(float number) => InRange(number, true, true); // [a, b]
        public bool InLeftCloseInterval(float number) => InRange(number, true, false); // [a, b)
        public bool InRightCloseInterval(float number) => InRange(number, false, true); // (a, b]

        public override string ToString() => $"Min: {Min} | Max: {Max} | Average: {Average} | Distance: {Distance}";
        public override bool Equals(object obj) => base.Equals(obj);
        public bool Equals(MRange range) => this == range;
        public override int GetHashCode() => base.GetHashCode();

        public static MRange operator +(MRange a) => new MRange(+a.Min, +a.Max);
        public static MRange operator +(MRange a, MRange b) => new MRange(a.Min + b.Min, a.Max + b.Max);
        public static MRange operator -(MRange a) => new MRange(-a.Min, -a.Max);
        public static MRange operator -(MRange a, MRange b) => new MRange(a.Min - b.Min, a.Max - b.Max);
        public static MRange operator *(MRange a, MRange b) => new MRange(a.Min * b.Min, a.Max * b.Max);
        public static MRange operator /(MRange a, MRange b) => new MRange(a.Min / b.Min, a.Max / b.Max);
        public static MRange operator %(MRange a, MRange b) => new MRange(a.Min % b.Min, a.Max % b.Max);
        public static bool operator ==(MRange a, MRange b) => a.Min == b.Min && a.Max == b.Max ? true : false;
        public static bool operator !=(MRange a, MRange b) => a.Min != b.Min || a.Max != b.Max ? true : false;
        public static bool operator <(MRange a, MRange b) => a.Distance < b.Distance;
        public static bool operator <=(MRange a, MRange b) => a.Distance <= b.Distance;
        public static bool operator >(MRange a, MRange b) => a.Distance > b.Distance;
        public static bool operator >=(MRange a, MRange b) => a.Distance >= b.Distance;
    }
}