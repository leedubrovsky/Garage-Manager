namespace GarageLogic
{
    public class ElectricMotorcycle : Motorcycle
    {
        private const float k_MaxBatteryTime = 1.8f;

        public ElectricMotorcycle(string i_LicenseID, string i_ModelName)
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
