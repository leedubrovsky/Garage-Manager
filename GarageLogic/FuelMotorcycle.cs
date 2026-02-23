namespace GarageLogic
{
    public class FuelMotorcycle : Motorcycle
    {
        private const float k_MaxFuelCapacity = 5.8f;
        private const eFuelType k_FuelType = eFuelType.Octan98;

        public FuelMotorcycle(string i_LicenseID, string i_ModelName)
            : base(i_LicenseID, i_ModelName)
        {
            SourceOfEnergy = new FuelEnergy(MaxFuelCapacity, FuelType, 0f);
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
    }
}
