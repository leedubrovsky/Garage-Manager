using System;

namespace GarageLogic
{
    public class FuelEnergy: SourceOfEnergy
    {
        private readonly float r_MaxFuelLiters;
        private readonly eFuelType r_FuelType;
        private float m_CurrentFuelLiters;

        public FuelEnergy(float i_MaxFuelLiters, eFuelType i_FuelType, float i_CurrentFuelLiters)
        {
            if (i_CurrentFuelLiters < 0 || i_CurrentFuelLiters > i_MaxFuelLiters)
            {
                throw new ValueRangeException(0f, i_MaxFuelLiters);
            }

            r_MaxFuelLiters = i_MaxFuelLiters;
            r_FuelType = i_FuelType;
            m_CurrentFuelLiters = i_CurrentFuelLiters;
        }

        public override float MaxEnergy
        {
            get 
            {
                return r_MaxFuelLiters; 
            }
        }

        public override float EnergyPercentageLeftInEnergySource
        {
            get
            {
                return (m_CurrentFuelLiters / r_MaxFuelLiters) * 100f; 
            }
        }

        public eFuelType FuelType
        {
            get 
            {
                return r_FuelType;
            }
        }

        public override void AddEnergy(float i_LitersToAdd, eFuelType? i_FuelType = null)
        {
            if (i_LitersToAdd < 0 || m_CurrentFuelLiters + i_LitersToAdd > r_MaxFuelLiters)
            {
                throw new ValueRangeException(0f, r_MaxFuelLiters - m_CurrentFuelLiters);
            }
            if (i_FuelType == null || i_FuelType.Value != r_FuelType)
            {
                throw new ArgumentException("Incorrect or missing fuel type.");
            }

            m_CurrentFuelLiters += i_LitersToAdd;
        }


        public override string GetEnergyDetails()
        {
            return string.Format("Fuel type: {0}, Fuel left: {1}L out of {2}L and percentage Left are: {3}%", r_FuelType, m_CurrentFuelLiters, r_MaxFuelLiters, EnergyPercentageLeftInEnergySource);
        }
    }
}
