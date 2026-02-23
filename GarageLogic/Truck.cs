using System;
using System.Collections.Generic;

namespace GarageLogic
{
    public class Truck : Vehicle
    {
        private const int k_NumWheels = 12;
        private const float k_MaxWheelAirPressure = 27f;
        private const float k_MaxFuelCapacity = 135f;
        private const eFuelType k_FuelType = eFuelType.Soler;
        private bool m_DoesItCarryHazardousMaterials;
        private float m_TrunkVolume;

        public Truck(string i_LicenseID, string i_ModelName)
            : base(i_LicenseID, i_ModelName)
        {
            SourceOfEnergy = new FuelEnergy(MaxFuelCapacity, FuelType, 0f);
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

        public static float MaxFuelCapacity
        {
            get
            {
                return k_MaxFuelCapacity;
            }
        }

        public static eFuelType FuelType
        {
            get
            {
                return k_FuelType;
            }
        }

        public bool CarriesHazardousMaterials
        {
            get
            {
                return m_DoesItCarryHazardousMaterials;
            }
            set
            {
                m_DoesItCarryHazardousMaterials = value;
            }
        }

        public float TrunkVolume
        {
            get
            {
                return m_TrunkVolume;
            }
            set
            {
                if (value < 0)
                {
                    throw new ValueRangeException(0f, float.MaxValue);
                }

                m_TrunkVolume = value;
            }
        }

        public override Dictionary<string, string> GetSpecificDetails()
        {
            Dictionary<string, string> details = new Dictionary<string, string>();

            details.Add("Carries hazardous materials", m_DoesItCarryHazardousMaterials.ToString());
            details.Add("Trunk volume", m_TrunkVolume.ToString());

            return details;
        }

        public override void SetSpecificFields(string i_FieldName, string i_Value)
        {
            switch (i_FieldName)
            {
                case "Carries hazardous materials":
                    CarriesHazardousMaterials = bool.Parse(i_Value);
                    break;

                case "Trunk volume":
                    TrunkVolume = float.Parse(i_Value);
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
