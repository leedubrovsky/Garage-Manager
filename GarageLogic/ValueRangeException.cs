using System;

namespace GarageLogic
{
    public class ValueRangeException : Exception
    {
        public float MinValue { get; }
        public float MaxValue { get; }
        public ValueRangeException(float i_MinValue, float i_MaxValue)
            : base(string.Format("Value out of range allowed range is: {0} - {1}", i_MinValue, i_MaxValue))
        {
            MinValue = i_MinValue;
            MaxValue = i_MaxValue;
        }
    }
}
