using System;
using System.Collections.Generic;

namespace GarageLogic
{
    public abstract class Vehicle
    {
        private readonly string r_ModelName;
        private readonly string r_LicenseID;
        private readonly List<Wheel> r_Wheels = new List<Wheel>();
        private SourceOfEnergy m_EnergySource;

        public Vehicle(string i_LicenseID, string i_ModelName)
        {
            r_LicenseID = i_LicenseID;
            r_ModelName = i_ModelName;
        }

        public string LicenseID
        {
            get
            {
                return r_LicenseID;
            }
        }

        public string ModelName
        {
            get
            {
                return r_ModelName;
            }
        }

        public List<Wheel> Wheels
        {
            get
            {
                return r_Wheels;
            }
        }

        public SourceOfEnergy SourceOfEnergy
        {
            get
            {
                return m_EnergySource;
            }
            set
            {
                m_EnergySource = value;
            }
        }

        public float EnergyPercentage
        {
            get
            {
                return m_EnergySource.EnergyPercentageLeftInEnergySource;
            }
        }

        public void AddWheel(Wheel i_WheelToAdd, int i_ExpectedNumberOfWheels)
        {
            if (i_WheelToAdd == null)
            {
                throw new ArgumentException("wheel cannot be null");
            }
            if (r_Wheels.Count >= i_ExpectedNumberOfWheels)
            {
                throw new ArgumentException("too many wheels");
            }

            r_Wheels.Add(i_WheelToAdd);
        }

        public void SetWheels(string i_ManufacturerName, float i_CurrentAirPressure, float i_MaxAirPressure, int i_NumWheels)
        {
            r_Wheels.Clear();

            for (int i = 0; i < i_NumWheels; i++)
            {
                Wheel wheel = new Wheel(i_ManufacturerName, i_CurrentAirPressure, i_MaxAirPressure);
                r_Wheels.Add(wheel);
            }
        }

        public abstract int GetRequiredNumWheels();

        public abstract float GetMaxAirPressurePerWheel();

        public abstract Dictionary<string, string> GetSpecificDetails();

        public abstract void SetSpecificFields(string i_FieldName, string i_Value);
    }
}
