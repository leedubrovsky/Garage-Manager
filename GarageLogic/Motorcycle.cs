using System;
using System.Collections.Generic;

namespace GarageLogic
{
    public class Motorcycle : Vehicle
    {
        private const int k_NumWheels = 2;
        private const float k_MaxWheelAirPressure = 30f;
        private eLicenseType m_LicenseType;
        private int m_EngineCapacity;

        public Motorcycle(string i_LicenseID, string i_ModelName)
            : base(i_LicenseID, i_ModelName)
        {
        }

        public static int NumWheels
        {
            get
            {
                return k_NumWheels;
            }
        }

        public static float MaxAirPressure
        {
            get
            {
                return k_MaxWheelAirPressure;
            }
        }

        public eLicenseType LicenseType
        {
            get
            {
                return m_LicenseType;
            }
            set
            {
                if (!Enum.IsDefined(typeof(eLicenseType), value))
                {
                    throw new ArgumentException("Invalid license type for motorcycle.");
                }

                m_LicenseType = value;
            }
        }

        public int EngineCapacity
        {
            get
            {
                return m_EngineCapacity;
            }
            set
            {
                if (value < 0)
                {
                    throw new ValueRangeException(1f, float.MaxValue);
                }

                m_EngineCapacity = value;
            }
        }

        public override Dictionary<string, string> GetSpecificDetails()
        {
            Dictionary<string, string> details = new Dictionary<string, string>();

            details.Add("License type", m_LicenseType.ToString());
            details.Add("Engine capacity (cc)", m_EngineCapacity.ToString());

            return details;
        }

        public override void SetSpecificFields(string i_FieldName, string i_Value)
        {
            switch (i_FieldName)
            {
                case "License type":
                    LicenseType = (eLicenseType)Enum.Parse(typeof(eLicenseType), i_Value);
                    break;

                case "Engine capacity (cc)":
                    EngineCapacity = int.Parse(i_Value);
                    break;

                default:
                    throw new ArgumentException($"Unknown field name: {i_FieldName}");
            }
        }

        public override int GetRequiredNumWheels()
        {
            return NumWheels;
        }

        public override float GetMaxAirPressurePerWheel()
        {
            return MaxAirPressure;
        }
    }
}
