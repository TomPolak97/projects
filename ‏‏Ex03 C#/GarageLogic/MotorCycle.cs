using System;
using static GarageLogic.Enums;

namespace GarageLogic
{

    public class MotorCycle : Vehicle
    {
        private const float k_MotorCycleMaxAirPressure = 31f;
        private const int k_MotorCycleAmountOfWheels = 2;
        private const eFuelType k_MotorCycleFuelType = eFuelType.Octan98;
        private const float k_MotorCycleMaxFuelTank = 6.2f;
        private const float k_MotorCycleMaxBatteryTime = 2.5f;
        private eLicenseType m_LisenceType;
        private int m_EngineVolume;

        public MotorCycle(string i_LisenceID, string i_ModelName, Engine i_Engine)
            : base(i_LisenceID, i_ModelName, i_Engine)
        {
            base.WheelsList = AddWheels(k_MotorCycleAmountOfWheels, k_MotorCycleMaxAirPressure); 
        }

        public static float MotorCycleMaxAirPressure
        {
            get
            {
                return k_MotorCycleMaxAirPressure;
            }
        }

        public static int MotorCycleAmountOfWheels
        {
            get
            {
                return k_MotorCycleAmountOfWheels;
            }
        }

        public static eFuelType MotorCycleFuelType
        {
            get
            {
                return k_MotorCycleFuelType;
            }
        }

        public static float MotorCycleMaxFuelTank
        {
            get
            {
                return k_MotorCycleMaxFuelTank;
            }
        }

        public static float MotorCycleMaxBatteryTime
        {
            get
            {
                return k_MotorCycleMaxBatteryTime;
            }
        }

        public eLicenseType LicenseType
        {
            get
            {
                return m_LisenceType;
            }
            set
            {
                m_LisenceType = value;
            }
        }

        public int EngineVolume
        {
            get
            {
                return m_EngineVolume;
            }
            set
            {
                m_EngineVolume = value;
            }
        }

        public override string ToString()
        {
            string motorcycleDisplay = String.Format(@"
{0}
License type: {1}
Engine volume: {2}", base.ToString(), this.LicenseType.ToString(), this.EngineVolume.ToString());

            return motorcycleDisplay;
        }
    }
}