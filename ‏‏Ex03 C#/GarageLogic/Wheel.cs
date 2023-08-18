using System;

namespace GarageLogic
{
    public class Wheel
    {

        private string m_ManufacturerName;
        private float m_CurrentAirPressure;
        private float m_MaxAirPressure;

        public Wheel(float i_MaxAirPressure)
        {
            m_MaxAirPressure = i_MaxAirPressure;
            m_CurrentAirPressure = 0;
        }

        public float MaxAirPressure
        {
            get
            {
                return m_MaxAirPressure;
            }
            set
            {
                m_MaxAirPressure = value;
            }
        }

        public float CurrentAirPressure
        {
            get
            {
                return m_CurrentAirPressure;
            }
            set
            {
                m_CurrentAirPressure = value;
            }
        }

        public string ManufacturerName
        {
            get
            {
                return m_ManufacturerName;
            }
            set
            {
                m_ManufacturerName = value;
            }
        }

        public void Pump(float i_AirToAdd)
        {
            bool possibleToFillAir = CurrentAirPressure + i_AirToAdd <= MaxAirPressure;

            if (!possibleToFillAir)
            {
                throw new ValueOutOfRangeException(i_AirToAdd, CurrentAirPressure, MaxAirPressure);
            }
            else
            {
                CurrentAirPressure += i_AirToAdd;
            }
        }

        public override string ToString()
        {
            string wheels = String.Format(@"
Manufatcurer name: {0}
Current air pressure: {1}
Max air pressure: {2}", this.ManufacturerName, this.CurrentAirPressure.ToString(), this.MaxAirPressure.ToString());

            return wheels;
        }
    }
}
