using System;
using System.Collections.Generic;

namespace GarageLogic
{
    public class GarageManager
    {
        private readonly Dictionary<string, GarageRecord> r_GarageRecords = new Dictionary<string, GarageRecord>();

        public bool IsVehicleInGarage(string i_LicenseID)
        {
            return r_GarageRecords.ContainsKey(i_LicenseID);
        }

        public void InsertVehicle(GarageRecord i_Record)
        {
            string licenseID = i_Record.Vehicle.LicenseID;

            if (IsVehicleInGarage(licenseID))
            {
                r_GarageRecords[licenseID].VehicleStatus = eVehicleStatus.InRepair;
            }
            else
            {
                r_GarageRecords.Add(licenseID, i_Record);
            }
        }

        public void InflateWheelsToMax(string i_LicenseID)
        {
            if (!IsVehicleInGarage(i_LicenseID))
            {
                throw new ArgumentException("Vehicle not found in garage");
            }

            foreach (Wheel wheel in r_GarageRecords[i_LicenseID].Vehicle.Wheels)
            {
                float airToAdd = wheel.MaxAirPressure - wheel.CurrentAirPressure;
                wheel.Inflate(airToAdd);
            }
        }

        public void FillEnergy(string i_LicenseID, float i_AmountToAdd, eFuelType? i_FuelType = null)
        {
            if (!IsVehicleInGarage(i_LicenseID))
            {
                throw new ArgumentException("Vehicle not found in garage");
            }

            SourceOfEnergy energySource = r_GarageRecords[i_LicenseID].Vehicle.SourceOfEnergy;

            if (energySource is FuelEnergy fuelEnergy)
            {
                if (i_FuelType == null)
                {
                    throw new ArgumentException("Fuel type must be specified");
                }

                fuelEnergy.AddEnergy(i_AmountToAdd, i_FuelType.Value);
            }
            else if (energySource is ElectricEnergy electricEnergy)
            {
                if (i_FuelType != null)
                {
                    throw new ArgumentException("cant be fuel type when charging an electric vehicle");
                }

                electricEnergy.AddEnergy(i_AmountToAdd);
            }
            else
            {
                throw new ArgumentException("Unknown energy source type");
            }
        }

        public void ChangeVehicleStatus(string i_LicenseID, eVehicleStatus i_NewStatus)
        {
            if (!IsVehicleInGarage(i_LicenseID))
            {
                throw new ArgumentException("Vehicle not found in garage");
            }

            r_GarageRecords[i_LicenseID].VehicleStatus = i_NewStatus;
        }

        public List<string> GetLicenseNumbersByStatus(eVehicleStatus? i_StatusFilter = null)
        {
            List<string> licenseIDNumbers = new List<string>();

            foreach (KeyValuePair<string, GarageRecord> entry in r_GarageRecords)
            {
                if (!i_StatusFilter.HasValue || entry.Value.VehicleStatus == i_StatusFilter.Value)
                {
                    licenseIDNumbers.Add(entry.Key);
                }
            }

            return licenseIDNumbers;
        }

        public string GetVehicleDetails(string i_LicenseID)
        {
            if (!IsVehicleInGarage(i_LicenseID))
            {
                throw new ArgumentException("Vehicle not found in garage");
            }

            GarageRecord record = r_GarageRecords[i_LicenseID];

            return record.ToString();
        }
    }
}
