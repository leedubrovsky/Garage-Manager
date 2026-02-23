namespace GarageLogic
{
    public abstract class SourceOfEnergy
    {

        public abstract float EnergyPercentageLeftInEnergySource
        {
            get;
        }

        public abstract float MaxEnergy
        {
            get;
        }

        public abstract void AddEnergy(float i_EnergyAmountToAdd, eFuelType? i_FuelType = null);

        public abstract string GetEnergyDetails();
    }
}
