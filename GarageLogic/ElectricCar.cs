namespace GarageLogic
{
    public class ElectricCar : Car
    {
        private const float k_MaxBatteryTime = 4.8f;

        public ElectricCar(string i_LicenseID, string i_ModelName)
            : base(i_LicenseID, i_ModelName)
        {
            SourceOfEnergy = new ElectricEnergy(MaxBatteryTime, 0f);
        }

        public static float MaxBatteryTime
        {
            get 
            {
                return k_MaxBatteryTime; 
            }
        }

    }
}
