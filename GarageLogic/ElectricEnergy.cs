namespace GarageLogic
{
    public class ElectricEnergy : SourceOfEnergy
    {
        private readonly float r_MaxBatteryHours;
        private float m_CurrentBatteryHours;

        public ElectricEnergy(float i_MaxBatteryHours, float i_CurrentBatteryHours)
        {
            if (i_MaxBatteryHours < 0)
            {
                throw new ValueRangeException(0.1f, float.MaxValue);
            }
            if (i_CurrentBatteryHours < 0 || i_CurrentBatteryHours > i_MaxBatteryHours)
            {
                throw new ValueRangeException(0f, i_MaxBatteryHours);
            }

            r_MaxBatteryHours = i_MaxBatteryHours;
            m_CurrentBatteryHours = i_CurrentBatteryHours;
        }

        public override float MaxEnergy
        {
            get 
            {
                return r_MaxBatteryHours;
            }
        }

        public override float EnergyPercentageLeftInEnergySource
        {
            get 
            {
                return (m_CurrentBatteryHours / r_MaxBatteryHours) * 100f; 
            }
        }

        public override void AddEnergy(float i_HoursToAdd, eFuelType? i_FuelType = null)
        {
            if (i_HoursToAdd < 0 || m_CurrentBatteryHours + i_HoursToAdd > r_MaxBatteryHours)
            {
                throw new ValueRangeException(0f, r_MaxBatteryHours - m_CurrentBatteryHours);
            }

            m_CurrentBatteryHours += i_HoursToAdd;
        }

        public override string GetEnergyDetails()
        {
            return string.Format("Battery: {0}h / {1}h, energy precentage left: {2}%", m_CurrentBatteryHours, r_MaxBatteryHours, EnergyPercentageLeftInEnergySource);
        }
    }
}
