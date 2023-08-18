using System;

namespace GarageLogic
{

    public class Electric : Engine
    {
        private float m_CurrentBatteryTime;
        private float m_MaxBatteryTime;

        public Electric(float i_MaxBattery, float i_CurrentBatteryTime)
        {
            m_MaxBatteryTime = i_MaxBattery;
            m_CurrentBatteryTime = i_CurrentBatteryTime;
        }

        public override void FillEnergy(float i_EnergyToAdd)
        {
            CurrentBatteryTime += i_EnergyToAdd;
        }

        public float CurrentBatteryTime
        {
            get
            {
                return m_CurrentBatteryTime;
            }
            set
            {
                m_CurrentBatteryTime = value;
                UpdateRemainingEnergyPercentage(CurrentBatteryTime, MaxBatteryTime);
            }
        }

        public float MaxBatteryTime
        {
            get
            {
                return m_MaxBatteryTime;
            }
            set
            {
                m_MaxBatteryTime = value;
            }
        }

        public override string ToString()
        {
            string BatteryDisplay = String.Format(@"
Current battery time: {0}
Max battery time: {1}", this.CurrentBatteryTime.ToString(), this.MaxBatteryTime.ToString());

            return BatteryDisplay;
        }
    }
}