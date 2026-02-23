using System;
using System.Collections.Generic;
using System.IO;
//using System.Linq;
using GarageLogic;

namespace ConsoleUI
{
    internal class ConsoleUIManager
    {
        private readonly GarageManager r_GarageManager = new GarageManager();

        public void Run()
        {
            bool exit = false;

            while (!exit)
            {
                MenuPrinter.PrintMenu();
                string userChoice = Console.ReadLine();

                switch (userChoice)
                {
                    case "1":
                        loadVehiclesFromData();
                        break;
                    case "2":
                        insertNewVehicle();
                        break;
                    case "3":
                        displayLicenseIDs();
                        break;
                    case "4":
                        changeVehicleStatus();
                        break;
                    case "5":
                        inflateWheelsToMax();
                        break;
                    case "6":
                        refuelVehicle();
                        break;
                    case "7":
                        rechargeVehicle();
                        break;
                    case "8":
                        displayVehicleDetails();
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        MenuPrinter.PrintError("Invalid input. Please choose a number between 0 and 8.");
                        break;
                }
            }
        }

        private void loadVehiclesFromData()
        {
            string[] linesInDataBase = File.ReadAllLines("Vehicles.db");

            foreach (string lineOfDataBase in linesInDataBase)
            {
                if (lineOfDataBase.StartsWith("*****"))
                {
                    break;
                }

                string[] divideEachLineWithComma = lineOfDataBase.Split(',');
                string vehicleType = divideEachLineWithComma[0].Trim();
                string licenseID = divideEachLineWithComma[1].Trim();
                string modelName = divideEachLineWithComma[2].Trim();
                float energyPercentage = float.Parse(divideEachLineWithComma[3].Trim());
                string wheelManufacturer = divideEachLineWithComma[4].Trim();
                float currentAirPressure = float.Parse(divideEachLineWithComma[5].Trim());
                string ownerName = divideEachLineWithComma[6].Trim();
                string ownerPhone = divideEachLineWithComma[7].Trim();
                Vehicle vehicle = VehicleCreator.CreateVehicle(vehicleType, licenseID, modelName);
                int numWheels = vehicle.GetRequiredNumWheels();
                float maxAirPressure = vehicle.GetMaxAirPressurePerWheel();

                vehicle.SetWheels(wheelManufacturer, currentAirPressure, maxAirPressure, numWheels);

                float maxEnergy = vehicle.SourceOfEnergy.MaxEnergy;
                float currentEnergyAmount = (energyPercentage / 100f) * maxEnergy;

                if (vehicle.SourceOfEnergy is FuelEnergy fuelEnergy)
                {
                    fuelEnergy.AddEnergy(currentEnergyAmount, fuelEnergy.FuelType);
                }
                else if (vehicle.SourceOfEnergy is ElectricEnergy electricEnergy)
                {
                    electricEnergy.AddEnergy(currentEnergyAmount);
                }

                Dictionary<string, string> specificFields = vehicle.GetSpecificDetails();

                int indexOfDividedSpecialProperties = 0;

                foreach (string fieldName in specificFields.Keys)
                {
                    string value = divideEachLineWithComma[8 + indexOfDividedSpecialProperties].Trim();

                    vehicle.SetSpecificFields(fieldName, value);
                    indexOfDividedSpecialProperties++;
                }

                GarageRecord eachRecordInGarageRecord = new GarageRecord(ownerName, ownerPhone, vehicle);

                r_GarageManager.InsertVehicle(eachRecordInGarageRecord);
            }

            MenuPrinter.PrintSuccess("Vehicles loaded successfully");
        }


        private void insertNewVehicle()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("Insert a new vehicle to the garage");
                Console.WriteLine("Enter vehicle type:");
                string i_VehicleType = Console.ReadLine();

                List<string> supportedTypes = VehicleCreator.SupportedTypes;

                if (!supportedTypes.Contains(i_VehicleType))
                {
                    MenuPrinter.PrintError($"Invalid vehicle type");

                    return;
                }

                Console.WriteLine("Enter license ID:");
                string i_LicenseID = Console.ReadLine();

                if (r_GarageManager.IsVehicleInGarage(i_LicenseID))
                {
                    MenuPrinter.PrintSuccess($"Vehicle with license ID {i_LicenseID} is already in the garage so changing status to InRepair");
                    r_GarageManager.ChangeVehicleStatus(i_LicenseID, eVehicleStatus.InRepair);

                    return;
                }

                Console.WriteLine("Enter model name:");
                string i_ModelName = Console.ReadLine();

                Console.WriteLine("Enter energy percentage (0-100):");
                if (!float.TryParse(Console.ReadLine(), out float energyPercentage))
                {
                    MenuPrinter.PrintError("Invalid input - enter a number for energy percentage");

                    return;
                }

                if (energyPercentage < 0 || energyPercentage > 100)
                {
                    MenuPrinter.PrintError("Energy percentage must be between 0 and 100");

                    return;
                }

                Console.WriteLine("Enter wheel manufacturer:");
                string wheelManufacturer = Console.ReadLine();

                Console.WriteLine("Enter current air pressure:");
                if (!float.TryParse(Console.ReadLine(), out float currentAirPressure))
                {
                    MenuPrinter.PrintError("Invalid input. Please enter a number for air pressure");

                    return;
                }

                Vehicle vehicle = VehicleCreator.CreateVehicle(i_VehicleType, i_LicenseID, i_ModelName);

                float maxAirPressure = vehicle.GetMaxAirPressurePerWheel();

                if (currentAirPressure < 0 || currentAirPressure > maxAirPressure)
                {
                    MenuPrinter.PrintError($"Air pressure must be between 0 and {maxAirPressure}");

                    return;
                }

                int numWheels = vehicle.GetRequiredNumWheels();

                vehicle.SetWheels(wheelManufacturer, currentAirPressure, maxAirPressure, numWheels);

                float maxEnergy = vehicle.SourceOfEnergy.MaxEnergy;
                float currentEnergyAmount = (energyPercentage / 100f) * maxEnergy;

                if (vehicle.SourceOfEnergy is FuelEnergy fuelEnergy)
                {
                    fuelEnergy.AddEnergy(currentEnergyAmount, fuelEnergy.FuelType);
                }
                else if (vehicle.SourceOfEnergy is ElectricEnergy electricEnergy)
                {
                    electricEnergy.AddEnergy(currentEnergyAmount);
                }

                Console.WriteLine("Enter owner's name:");
                string ownerName = Console.ReadLine();

                Console.WriteLine("Enter owner's phone number:");
                string ownerPhone = Console.ReadLine();

                Dictionary<string, string> specificFields = vehicle.GetSpecificDetails();

                foreach (string fieldName in specificFields.Keys)
                {
                    Console.WriteLine($"Enter value for '{fieldName}':");
                    string value = Console.ReadLine();

                    try
                    {
                        vehicle.SetSpecificFields(fieldName, value);
                    }
                    catch (Exception ex)
                    {
                        MenuPrinter.PrintError($"Error setting field '{fieldName}': {ex.Message}");

                        return;
                    }
                }

                GarageRecord eachRecordInGarageRecord = new GarageRecord(ownerName, ownerPhone, vehicle);

                r_GarageManager.InsertVehicle(eachRecordInGarageRecord);
                MenuPrinter.PrintSuccess("Vehicle inserted to the garage successfully.");
            }
            catch (Exception ex)
            {
                MenuPrinter.PrintError(ex.Message);
            }
        }

        private void displayLicenseIDs()
        {
            Console.WriteLine("Do you want to filter by status? (y/n):");
            string filterInput = Console.ReadLine();
            List<string> licenseIDs;

            if (filterInput.ToLower() == "y")
            {
                Console.WriteLine("Enter status (InRepair / Fixed / Paid): ");
                string statusInput = Console.ReadLine();

                if (Enum.TryParse<eVehicleStatus>(statusInput, true, out eVehicleStatus status))
                {
                    Console.WriteLine($"You selected status: {status}");
                    licenseIDs = r_GarageManager.GetLicenseNumbersByStatus(status);
                }
                else
                {
                    MenuPrinter.PrintError("Invalid status. Please enter: InRepair, Fixed, or Paid");

                    return;
                }
            }
            else
            {
                licenseIDs = r_GarageManager.GetLicenseNumbersByStatus();
            }

            Console.WriteLine();
            Console.WriteLine("License numbers:");

            if (licenseIDs.Count == 0)
            {
                Console.WriteLine("No vehicles found");
            }
            else
            {
                foreach (string id in licenseIDs)
                {
                    Console.WriteLine(id);
                }
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        private void changeVehicleStatus()
        {
            Console.Write("Enter license ID: ");
            string i_LicenseID = Console.ReadLine();

            Console.Write("Enter new status (InRepair, Fixed, Paid): ");
            string statusInput = Console.ReadLine();

            if (Enum.TryParse<eVehicleStatus>(statusInput, true, out eVehicleStatus status))
            {
                r_GarageManager.ChangeVehicleStatus(i_LicenseID, status);
                Console.WriteLine("Status updated.");
            }
            else
            {
                MenuPrinter.PrintError("Invalid status. Please enter: InRepair, Fixed, or Paid");
            }

            Console.ReadKey();
        }

        private void inflateWheelsToMax()
        {
            Console.WriteLine("Enter license ID:");
            string i_LicenseID = Console.ReadLine();

            try
            {
                r_GarageManager.InflateWheelsToMax(i_LicenseID);
                MenuPrinter.PrintSuccess("All wheels inflated to max");
            }
            catch (Exception ex)
            {
                MenuPrinter.PrintError(ex.Message);
            }
        }

        private void refuelVehicle()
        {
            Console.WriteLine("Enter license ID:");
            string i_LicenseID = Console.ReadLine();

            Console.WriteLine("Enter amount of fuel to add:");
            if (!float.TryParse(Console.ReadLine(), out float i_AmountToAdd))
            {
                MenuPrinter.PrintError("Invalid number");

                return;
            }

            Console.WriteLine("Enter fuel type (Soler, Octan95, Octan96, Octan98):");
            string fuelTypeStr = Console.ReadLine();

            if (Enum.TryParse<eFuelType>(fuelTypeStr, true, out eFuelType i_FuelType))
            {
                try
                {
                    r_GarageManager.FillEnergy(i_LicenseID, i_AmountToAdd, i_FuelType);
                    MenuPrinter.PrintSuccess("Vehicle refueled");
                }
                catch (Exception ex)
                {
                    MenuPrinter.PrintError(ex.Message);
                }
            }
            else
            {
                MenuPrinter.PrintError("Invalid fuel type");
            }
        }

        private void rechargeVehicle()
        {
            Console.WriteLine("Enter license ID:");
            string i_LicenseID = Console.ReadLine();

            Console.WriteLine("Enter number of hours to charge:");
            if (!float.TryParse(Console.ReadLine(), out float i_AmountToAdd))
            {
                MenuPrinter.PrintError("Invalid number");

                return;
            }

            try
            {
                r_GarageManager.FillEnergy(i_LicenseID, i_AmountToAdd);
                MenuPrinter.PrintSuccess("Vehicle charged");
            }
            catch (Exception ex)
            {
                MenuPrinter.PrintError(ex.Message);
            }
        }

        private void displayVehicleDetails()
        {
            Console.WriteLine("Enter license ID:");
            string i_LicenseID = Console.ReadLine();

            try
            {
                string details = r_GarageManager.GetVehicleDetails(i_LicenseID);

                Console.WriteLine(details);
            }
            catch (Exception ex)
            {
                MenuPrinter.PrintError(ex.Message);
            }

            Console.ReadLine();
        }
    }
}
