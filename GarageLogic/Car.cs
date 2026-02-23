using System;
using System.Collections.Generic;

namespace GarageLogic
{
    public class Car : Vehicle
    {
        private const int k_NumWheels = 5;
        private const float k_MaxWheelAirPressure = 32f;
        private eColorForCar m_Color;
        private eNumberOfDoors m_NumberOfDoors;

        public Car(string i_LicenseID, string i_ModelName)
            : base(i_LicenseID, i_ModelName)
        {
        }

        public eColorForCar Color
        {
            get
            {
                return m_Color;
            }
            set
            {
                if (!Enum.IsDefined(typeof(eColorForCar), value))
                {
                    throw new ArgumentException("Invalid color for car.");
                }

                m_Color = value;
            }
        }

        public eNumberOfDoors NumberOfDoors
        {
            get
            {
                return m_NumberOfDoors;
            }
            set
            {
                if (!Enum.IsDefined(typeof(eNumberOfDoors), value))
                {
                    throw new ValueRangeException(2, 5);
                }

                m_NumberOfDoors = value;
            }
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

        public override Dictionary<string, string> GetSpecificDetails()
        {
            Dictionary<string, string> details = new Dictionary<string, string>();

            details.Add("Color", m_Color.ToString());
            details.Add("Number of doors", ((int)m_NumberOfDoors).ToString());

            return details;
        }

        public override void SetSpecificFields(string i_NameOfField, string i_Value)
        {
            switch (i_NameOfField)
            {
                case "Color":
                    Color = (eColorForCar)Enum.Parse(typeof(eColorForCar), i_Value);
                    break;

                case "Number of doors":
                    NumberOfDoors = (eNumberOfDoors)Enum.Parse(typeof(eNumberOfDoors), i_Value);
                    break;

                default:
                    throw new ArgumentException($"Unknown field name: {i_NameOfField}");
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
