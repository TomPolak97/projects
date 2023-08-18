using System;
using static GarageLogic.Enums; 

namespace GarageLogic
{

    public class Fuel : Engine
    {
        private float m_CurrentFuel;
        private float m_MaxFuel;
        private eFuelType m_FuelType;

        public Fuel(float i_MaxFuel, float i_CurrentFuel, eFuelType i_fuelType)
        {
            m_MaxFuel = i_MaxFuel;
            m_CurrentFuel = i_CurrentFuel;
            m_FuelType = i_fuelType;
        }

        public float CurrentFuel
        {
            get
            {
                return m_CurrentFuel;
            }
            set
            {
                m_CurrentFuel = value;
                UpdateRemainingEnergyPercentage(m_CurrentFuel, m_MaxFuel);
            }
        }

        public float MaxFuel
        {
            get
            {
                return m_MaxFuel;
            }
            set
            {
                m_MaxFuel = value;
            }
        }

        public eFuelType FuelType
        {
            get
            {
                return m_FuelType;
            }
            set
            {
                m_FuelType = value;
            }
        }

        public override void FillEnergy(float i_FuelToAdd)
        {
            CurrentFuel += i_FuelToAdd;
        }

        public override string ToString()
        {
            string FuelDisplay = String.Format(@"
Fuel type: {0}
Current fuel: {1}
Max fuel: {2}", this.FuelType.ToString(), this.CurrentFuel.ToString(), this.MaxFuel.ToString());

            return FuelDisplay;
        }
    }
}