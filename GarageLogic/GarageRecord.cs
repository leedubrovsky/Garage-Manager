using System.Collections.Generic;
using System.Text;
using System;

namespace GarageLogic
{
    public  class GarageRecord
    {
        private readonly string r_OwnerName;
        private readonly string r_OwnerPhone;
        private eVehicleStatus m_VehicleStatus;
        private readonly Vehicle r_Vehicle;

        public GarageRecord(string i_OwnerName, string i_OwnerPhone, Vehicle i_Vehicle)
        {
            r_OwnerName = i_OwnerName;
            r_OwnerPhone = i_OwnerPhone;
            r_Vehicle = i_Vehicle;
            m_VehicleStatus = eVehicleStatus.InRepair;
        }

        public string OwnerName
        {
            get 
            {
                return r_OwnerName;
            }
        }

        public string OwnerPhone
        {
            get 
            {
                return r_OwnerPhone;
            }
        }

        public eVehicleStatus VehicleStatus
        {
            get 
            {
                return m_VehicleStatus; 
            }
            set 
            {
                m_VehicleStatus = value; 
            }
        }

        public Vehicle Vehicle
        {
            get 
            {
                return r_Vehicle; 
            }
        }

        public override string ToString()
        {
            StringBuilder stringBuilderForPrintDetails = new StringBuilder();

            stringBuilderForPrintDetails.AppendLine($"License ID: {r_Vehicle.LicenseID}");
            stringBuilderForPrintDetails.AppendLine($"Model: {r_Vehicle.ModelName}");
            stringBuilderForPrintDetails.AppendLine($"Owner: {r_OwnerName}, Phone: {r_OwnerPhone}");
            stringBuilderForPrintDetails.AppendLine($"Status: {m_VehicleStatus}");
            stringBuilderForPrintDetails.AppendLine($"Energy: {r_Vehicle.SourceOfEnergy.GetEnergyDetails()}");
            stringBuilderForPrintDetails.AppendLine("Wheels:");

            foreach (Wheel wheel in r_Vehicle.Wheels)
            {
                stringBuilderForPrintDetails.AppendLine($"  Manufacturer: {wheel.ManufacturerName}, Air: {wheel.CurrentAirPressure}/{wheel.MaxAirPressure}");
            }

            foreach (KeyValuePair<string, string> specialDetailProparty in r_Vehicle.GetSpecificDetails())
            {
                stringBuilderForPrintDetails.AppendLine($"{specialDetailProparty.Key}: {specialDetailProparty.Value}");
            }

            return stringBuilderForPrintDetails.ToString();
        }


    }
}
